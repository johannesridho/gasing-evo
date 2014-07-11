using UnityEngine;
using System.Collections;

public class PhysicsTabrak : MonoBehaviour {
	
	public bool isInvicibleAfterClash;
	public float timeCountAfterClash;
	public Gasing gasing;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}
	
	void Start () {	
		isInvicibleAfterClash = false;
		timeCountAfterClash = 0f;
	}
	void Update () {
	}

	void FixedUpdate () {	
		if (isInvicibleAfterClash) {
			timeCountAfterClash += Time.deltaTime;
			if (timeCountAfterClash >= 0.20f) {
				isInvicibleAfterClash = false;
				timeCountAfterClash = 0f;
			}
		}
	}
	
	void geserAfterClash(Collider C) {
		float coeff_geser = 0.3f;
		Vector3 heading = C.rigidbody.position - gasing.rigidbody.position;
		Vector3 direction = heading / heading.magnitude;
		Vector3 posSelf = C.rigidbody.position + new Vector3(direction.x * coeff_geser, 0, direction.z * coeff_geser);
		Vector3 posEnemy = gasing.rigidbody.position - new Vector3(direction.x * coeff_geser, 0, direction.z * coeff_geser);
		gasing.rigidbody.MovePosition(posSelf);
		C.SendMessage ("movePosition", posEnemy, SendMessageOptions.DontRequireReceiver);
	}	

	void bounceAgainstObstacles(Collision C) {
		float coeff_obs = 30f;
		float coeff_geser = 3f;
		ContactPoint cp = C.contacts[0];
		Vector3 dir = gasing.rigidbody.position - cp.point;
		Vector3 direction = dir / dir.magnitude;
		print (direction);
		Vector3 momentum = Gasing.COEF_MOMENTUM * direction * coeff_obs;
		gasing.collider.SendMessage("speedChange", momentum, SendMessageOptions.DontRequireReceiver);
	}	
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			if (!isInvicibleAfterClash) {
				isInvicibleAfterClash = true;
				timeCountAfterClash = 0f;
				geserAfterClash(col.collider);
			}
		}
		if (col.gameObject.tag == "Obstacle") {
			if (!isInvicibleAfterClash) {
				isInvicibleAfterClash = true;
				timeCountAfterClash = 0f;
			}
			bounceAgainstObstacles(col);
		}
	}

}//end class