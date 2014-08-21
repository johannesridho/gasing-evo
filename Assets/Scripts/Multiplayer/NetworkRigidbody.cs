using UnityEngine;
using System.Collections;

public class NetworkRigidBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GamePrefs.isMultiplayer)
        {
            if (networkView.owner == Network.player)
            {
                rigidbody.isKinematic = false;
            }
            else
            {
                rigidbody.isKinematic = true;
            }
        }
	}
}
