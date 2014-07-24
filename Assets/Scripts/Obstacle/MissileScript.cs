using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

	private GameObject targetEnemy;
	public GameObject efekLedakan;
	private float clock;
	
	void Awake() {
		clock = 0f;
		//taking the target:
		targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
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
//			transform.position = Vector3.Lerp (transform.position, targetEnemy.transform.position,0.05f);
			transform.position += (targetEnemy.transform.position - transform.position ).normalized * 30 * Time.deltaTime;
			transform.LookAt(targetEnemy.transform.position);
			transform.Rotate(new Vector3(0,-90,0));
//			transform.rotation = (targetEnemy.transform.rotation);
		}
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));			
		}
	}
}
