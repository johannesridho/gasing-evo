using UnityEngine;
using System.Collections;

public class ExplosiveDrumScript : MonoBehaviour {
	public GameObject efekLedakan;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
		}
	}
}
