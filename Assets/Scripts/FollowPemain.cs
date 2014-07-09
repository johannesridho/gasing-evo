using UnityEngine;
using System.Collections;

public class FollowPemain : MonoBehaviour {

	public GameObject pemain;
	private Vector3 offset;

	void Awake(){
		if (!pemain) {
			pemain = GameObject.Find ("Pemain");
		}
	}

	// Use this for initialization
	void Start () {
		if (!pemain) {
			pemain = GameObject.Find ("Pemain");
		}
		offset = transform.position;
	}

	void LateUpdate () {
		transform.position = offset + pemain.transform.position;
	}
}
