using UnityEngine;
using System.Collections;

public class MeteorScript : MonoBehaviour {

	private float clock;
	private float damageInflicted;
	
	void Awake() {
		clock = 0f;
		damageInflicted = 5;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){
		clock += Time.deltaTime;
		if (clock >= 15) {
			Destroy(this.gameObject);
		}
	}

//	private ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[16];
	void OnParticleCollision(GameObject objek) {
//		Debug.Log (transform.position);
		Collider[] colls = Physics.OverlapSphere (new Vector3(transform.position.x, 1, transform.position.z), 7);
		foreach (Collider col in colls){
			if (col.tag == "Enemy") {
				col.gameObject.GetComponent<Gasing>().EPKurang(10);
			}		
		}
	}
}
