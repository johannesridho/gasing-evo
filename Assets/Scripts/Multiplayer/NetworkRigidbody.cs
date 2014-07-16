using UnityEngine;
using System.Collections;

public class NetworkRigidbody : MonoBehaviour {
	
	public double m_InterpolationBackTime = 0.1;
	public double m_ExtrapolationLimit = 0.5;
	public Rigidbody target;
	
	internal struct  State
	{
		internal double timestamp;
		internal Vector3 pos;
		internal Vector3 velocity;
		internal Quaternion rot;
		internal Vector3 angularVelocity;
	}
	
	// We store twenty states with "playback" information
	State[] m_BufferedState = new State[20];
	// Keep track of what slots are used
	int m_TimestampCount;
	
	public void Start()
	{
		if( target == null )
		{
			target = rigidbody;	
		}
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		// Send data to server
		if (stream.isWriting)
		{
			Vector3 pos = target.position;
			Quaternion rot = target.rotation;
			Vector3 velocity = target.velocity;
			Vector3 angularVelocity = target.angularVelocity;

			stream.Serialize(ref pos);
			stream.Serialize(ref velocity);
			stream.Serialize(ref rot);
			stream.Serialize(ref angularVelocity);
		}
		// Read data from remote client
		else
		{
			Vector3 pos = Vector3.zero;
			Vector3 velocity = Vector3.zero;
			Quaternion rot = Quaternion.identity;
			Vector3 angularVelocity = Vector3.zero;
			stream.Serialize(ref pos);
			stream.Serialize(ref velocity);
			stream.Serialize(ref rot);
			stream.Serialize(ref angularVelocity);
			
			// Build state
			State state;
			state.timestamp = info.timestamp;
			state.pos = pos;
			state.velocity = velocity;
			state.rot = rot;
			state.angularVelocity = angularVelocity;
			
			
			NewState( state );
		}
	}
	
	//This Function inserts the new State in the proper slot. 
	//This is the Insertion-Sort
	private void NewState( State newState )
	{
		//If no States are present, put in first slot.
		if( m_TimestampCount == 0 )
		{
			m_BufferedState[0] = newState;
		}
		else
		{
		
			//First find proper place in buffer. If no place is found, state can be dropped (newState is too old)
			for( int i = 0; i < m_TimestampCount; i ++ )
			{
				//If the state in slot i is older than our new state, we found our slot.	
				if( m_BufferedState[i].timestamp < newState.timestamp )
				{
					// Shift the buffer sideways, to make room in slot i. possibly deleting state 20
					for (int k=m_BufferedState.Length-1;k>i;k--)
					{
						m_BufferedState[k] = m_BufferedState[k-1];
					}
					
					//insert state
					m_BufferedState[i] = newState;
					
					//We are done, exit loop
					break;
				}
				
			}
		
		}
		
		//Update TimestampCount
		m_TimestampCount = Mathf.Min(m_TimestampCount + 1, m_BufferedState.Length);
		
	}
	
	// We have a window of interpolationBackTime where we basically play 
	// By having interpolationBackTime the average ping, you will usually use interpolation.
	// And only if no more data arrives we will use extra polation
	void Update () {
		// This is the target playback time of the rigid body
		double interpolationTime = Network.time - m_InterpolationBackTime;
		Debug.Log("" +  " - " + Network.time + " - " + interpolationTime);
		
		// Use interpolation if the target playback time is present in the buffer
		if (m_BufferedState[0].timestamp > interpolationTime)
		{
			// Go through buffer and find correct state to play back
			for (int i=0;i<m_TimestampCount;i++)
			{
				if (m_BufferedState[i].timestamp <= interpolationTime || i == m_TimestampCount-1)
				{
					// The state one slot newer (<100ms) than the best playback state
					State rhs = m_BufferedState[Mathf.Max(i-1, 0)];
					// The best playback state (closest to 100 ms old (default time))
					State lhs = m_BufferedState[i];
					
					// Use the time between the two slots to determine if interpolation is necessary
					double length = rhs.timestamp - lhs.timestamp;
					float t = 0.0F;
					// As the time difference gets closer to 100 ms t gets closer to 1 in 
					// which case rhs is only used
					// Example:
					// Time is 10.000, so sampleTime is 9.900 
					// lhs.time is 9.910 rhs.time is 9.980 length is 0.070
					// t is 9.900 - 9.910 / 0.070 = 0.14. So it uses 14% of rhs, 86% of lhs
					if (length > 0.0001)
						t = (float)((interpolationTime - lhs.timestamp) / length);
					
					// if t=0 => lhs is used directly
					target.transform.position = Vector3.Lerp(lhs.pos, rhs.pos, t);
					target.transform.rotation = Quaternion.Slerp(lhs.rot, rhs.rot, t);
					
					Debug.Log("Found packet for: " + gameObject.name);
					return;
				}
			}
			
			
			Debug.Log("Did not find packet " + m_TimestampCount + " for: " + gameObject.name);
			
			
		}
		// Use extrapolation
		else
		{
			State latest = m_BufferedState[0];
			
			float extrapolationLength = (float)(interpolationTime - latest.timestamp);
			// Don't extrapolation for more than 500 ms, you would need to do that carefully
			if (extrapolationLength < m_ExtrapolationLimit)
			{
				float axisLength = extrapolationLength * latest.angularVelocity.magnitude * Mathf.Rad2Deg;
				Quaternion angularRotation = Quaternion.AngleAxis(axisLength, latest.angularVelocity);
				
				target.position = latest.pos + latest.velocity * extrapolationLength;
				target.rotation = angularRotation * latest.rot;
				target.velocity = latest.velocity;
				target.angularVelocity = latest.angularVelocity;
			}
		}
	}
}
