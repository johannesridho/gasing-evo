using UnityEngine;
using System.Collections;

public class StoneObstacle : MonoBehaviour {

	private float damageInflicted;
	
	void Awake() {	
		damageInflicted = 15f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			col.gameObject.GetComponent<Gasing>().EPKurang(damageInflicted);
		}
	}
}
