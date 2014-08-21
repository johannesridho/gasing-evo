using UnityEngine;
using System.Collections;

public class CycloneScript : MonoBehaviour {
	
	private float clock;
	private float damageInflicted;
	
	void Awake() {
		clock = 0f;
		damageInflicted = 0.5f;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){
		clock += Time.deltaTime;
		if (clock >= 25) {
            if (GamePrefs.isMultiplayer)
            {
                Network.Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
		}

		Collider[] colls = Physics.OverlapSphere (new Vector3(transform.position.x, 1, transform.position.z), 3);
		foreach (Collider col in colls){
			if (col.tag == "Enemy" || col.gameObject.tag == "Player" || col.gameObject.tag == "Ally") {
				col.gameObject.GetComponent<Gasing>().EPKurang(damageInflicted);
			}		
		}
	}
	
	void FixedUpdate () {
		float hor = Random.Range(-10.0F, 10.0F) * Gasing.COEF_SPEED;
		float ver = Random.Range(-10.0F, 10.0F) * Gasing.COEF_SPEED;
		
		Vector3 movement = new Vector3 (hor,0.0f,ver);
		rigidbody.AddForce (movement * 20 * Time.deltaTime);	
	}

	void OnParticleCollision(GameObject objek) {	

	}

	private GameObject findNearestEnemy(){
		GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearest = new GameObject();
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject enemy in allEnemy) {
			Vector3 diff = enemy.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				nearest = enemy;
				distance = curDistance;
			}
		}
		return nearest;
	}
}
