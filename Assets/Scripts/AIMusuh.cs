using UnityEngine;
using System.Collections;

public class AIMusuh : MonoBehaviour {
	private Gasing gasing;
	private PhysicsTabrak gasing_pt;
	private float speedAI;

	void Awake(){
		if(!gasing){
			gasing = GetComponent<Gasing>();
		}
		if(!gasing_pt){
			gasing_pt = GetComponent<PhysicsTabrak>();
		}
		speedAI = 300;
	}

	void Start () {

	}

	void Update () {

	}

	void FixedUpdate () {
		float hor = Random.Range(-10.0F, 10.0F) * Gasing.COEF_SPEED * gasing.speed;
		float ver = Random.Range(-10.0F, 10.0F) * Gasing.COEF_SPEED * gasing.speed;

		Vector3 movement = new Vector3 (hor,0.0f,ver);
		if (!gasing_pt.isInvicibleAfterClash)
			rigidbody.AddForce (movement * speedAI * Time.deltaTime);		
	}
}//end class
