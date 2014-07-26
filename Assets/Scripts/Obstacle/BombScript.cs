using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	private GameObject targetEnemy;
	private Vector3 enemyPosition;
	public GameObject efekLedakan;
	private float clock;

	void Awake() {
		clock = 0f;
		//taking the target:
		targetEnemy = findNearestEnemy();
		enemyPosition = targetEnemy.transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		if (!targetEnemy) {
			targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
		}
		clock += Time.deltaTime;
		if (clock >= 5) {
			Destroy(this.gameObject);
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
		}
	}

	void FixedUpdate () {
		if (targetEnemy) {
//			transform.position = Vector3.Lerp (transform.position, enemyPosition,0.05f);
			transform.position += (enemyPosition - transform.position ).normalized * 30 * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
			
		}
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
