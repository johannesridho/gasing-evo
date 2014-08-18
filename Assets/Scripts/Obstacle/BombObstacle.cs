using UnityEngine;
using System.Collections;

public class BombObstacle : MonoBehaviour {
	
	public GameObject efekLedakan;
	private float clock;

	void Awake() {
		clock = 0f;
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		clock += Time.deltaTime;
		if (clock >= 5) {
			Destroy(this.gameObject);
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
		}
	}

	void FixedUpdate () {

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));			
		}
	}
}
