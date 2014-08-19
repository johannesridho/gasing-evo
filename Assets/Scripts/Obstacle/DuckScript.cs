using UnityEngine;
using System.Collections;

public class DuckScript : MonoBehaviour {

	private float damageInflicted;
	
	void Awake() {	
		damageInflicted = 15f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(0,0,0);
		if (Random.Range(0,100) < 75) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player){
				Vector3 heading = player.rigidbody.position - rigidbody.position;
				Vector3 direction = heading / heading.magnitude;
				movement = direction * Gasing.COEF_SPEED * 2;
			}
		} else {
			float hor = Random.Range(-5.0F, 5.0F) * Gasing.COEF_SPEED * 2;
			float ver = Random.Range(-5.0F, 5.0F) * Gasing.COEF_SPEED * 2;
			movement = new Vector3 (hor,0.0f,ver);
		}
		rigidbody.AddForce ( movement * 100 * Time.deltaTime);	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			col.gameObject.GetComponent<Gasing>().EPKurang(damageInflicted);
		}
	}
}
