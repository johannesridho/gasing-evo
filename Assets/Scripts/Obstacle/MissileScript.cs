using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

	private GameObject targetEnemy;
	public GameObject efekLedakan;
	private float clock;
	private GameObject targetTag;
	private bool on;
	
	void Awake() {
		clock = 0f;
		//taking the target:
		//targetEnemy = findNearestEnemy();
		targetTag = null;
//		if (!targetEnemy) {
//			targetEnemy = targetTag;
//		}
		on = false;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){
		if (on) {
			if (!targetEnemy) {
				targetEnemy = targetTag;
			}
			clock += Time.deltaTime;
			if (clock >= 5) {
				Destroy(this.gameObject);
				Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
			}
		}
	}
	
	void FixedUpdate () {
		if (on) {
			if (targetEnemy) {
				transform.position += (targetEnemy.transform.position - transform.position ).normalized * 30 * Time.deltaTime;
				transform.LookAt(targetEnemy.transform.position);
				transform.Rotate(new Vector3(0,-90,0));
			}
		}
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));			
		}
	}
	
	public void nyalakan(GameObject _targetTag){
		targetTag = _targetTag;
		on = true;
	}


}//end class
