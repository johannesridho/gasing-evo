using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	private GameObject targetEnemy;
	private GameObject self;
	public GameObject efekLedakan;
	private float clock;
	private bool on;

	void Awake() {
		clock = 0f;
		//taking the target:
//		targetEnemy = findNearestEnemy();
		targetEnemy = null;
		on = false;
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		if (on) {
				if (!targetEnemy) {
						targetEnemy = GameObject.FindGameObjectWithTag ("Enemy");
				}
				clock += Time.deltaTime;
				if (clock >= 5) {
						Destroy (this.gameObject);
						Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
				}
		}
	}

	void FixedUpdate () {
		if (on && targetEnemy) {
			transform.position += (targetEnemy.transform.position - transform.position ).normalized * 30 * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision col){
		if ((col.gameObject != self) && (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally")) {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
		}
	}

	public void nyalakan(GameObject _self, GameObject _targetTag){
		targetEnemy = _targetTag;
		self = _self;
		on = true;
	}

}//end class
