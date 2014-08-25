using UnityEngine;
using System.Collections;

public class CollisionSpark : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.particleSystem.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			gameObject.particleSystem.enableEmission = true;
			Invoke("DisableEmission", 0.2f);
		}
	}

	void DisableEmission () {
		gameObject.particleSystem.enableEmission = false;
	}

}
