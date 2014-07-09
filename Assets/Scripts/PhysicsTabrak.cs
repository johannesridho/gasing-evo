using UnityEngine;
using System.Collections;

public class PhysicsTabrak : MonoBehaviour {
	
	public bool isInvicibleAfterClash;
	private float timeCountAfterClash;
	public Gasing gasing;
	
	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}
	
	void Start () {	
		isInvicibleAfterClash = false;
		timeCountAfterClash = 0f;
	}
	void Update () {
		
	}
	
	//called every fixed framerate frame
	void FixedUpdate () {	
		if (isInvicibleAfterClash) {
			timeCountAfterClash += Time.deltaTime;
			if (timeCountAfterClash >= 0.20f) {
				isInvicibleAfterClash = false;
				timeCountAfterClash = 0f;
			}
		}
	}
	
	void geserAfterClash(Collider C) {
		float coeff_geser = 3.3f;
		Vector3 heading = C.rigidbody.position - gasing.rigidbody.position;
		Vector3 direction = heading / heading.magnitude;
		Vector3 posSelf = C.rigidbody.position + new Vector3(direction.x * coeff_geser, 0, direction.z * coeff_geser);
		Vector3 posEnemy = gasing.rigidbody.position - new Vector3(direction.x * coeff_geser, 0, direction.z * coeff_geser);
		gasing.rigidbody.MovePosition(posSelf);
		C.SendMessage ("movePosition", posEnemy, SendMessageOptions.DontRequireReceiver);
	}	
}//end class