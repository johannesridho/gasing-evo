using UnityEngine;
using System.Collections;

public class PhysicsTabrak : MonoBehaviour {
	
	public bool isInvicibleAfterClash;
	private float timeCountAfterClash;
	private Gasing gasing;
	private static float COEF_MOMENTUM = 70f;
	private Vector3 geserForce;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}
	
	void Start () {	
		isInvicibleAfterClash = false;
		timeCountAfterClash = 0f;
		geserForce = new Vector3(0,0,0);
	}

	void Update () {
	}

	void FixedUpdate () {	
		if (isInvicibleAfterClash) {
			timeCountAfterClash += Time.deltaTime;
			gasing.dorong(geserForce);
			if (timeCountAfterClash >= 0.2f) {
				isInvicibleAfterClash = false;
				timeCountAfterClash = 0f;
			}
		}
	}
	
	void changeSpeedAfterClash(Collision C, Vector3 vel) {
		Vector3 heading = C.collider.rigidbody.position - gasing.rigidbody.position;
		Vector3 direction = heading / heading.magnitude;
		Vector3 mdirXZ = new Vector3(-direction.x , 0, -direction.z);
		geserForce = COEF_MOMENTUM * mdirXZ * vel.magnitude;
	}

	void damageAfterClash(Collision C) {
		Vector3 vel = gasing.rigidbody.velocity;
		float damage = Gasing.COEF_POWER * gasing.power * Mathf.Sqrt(Mathf.Pow(vel.x,2) + Mathf.Pow(vel.y,2) + Mathf.Pow(vel.z,2));
		C.collider.SendMessage ("EPKurang", damage, SendMessageOptions.DontRequireReceiver);
	}
	
	void bounceAgainstObstacles(Collision C) {
		float coeff_obs = 15f;
		ContactPoint cp = C.contacts[0];
		Vector3 dir = gasing.rigidbody.position - cp.point;
		Vector3 direction = dir / dir.magnitude;
		Vector3 momentum = COEF_MOMENTUM * direction * coeff_obs;
		geserForce = momentum;
	}	
	
	void OnCollisionEnter(Collision col){
		Vector3 vel = gasing.rigidbody.velocity;
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			if (!isInvicibleAfterClash) {
				isInvicibleAfterClash = true;
				timeCountAfterClash = 0f;
			}
			damageAfterClash(col);
			changeSpeedAfterClash(col, vel);
		}
        if (col.gameObject.tag == "Item")
        {
//            if (!isInvicibleAfterClash) {
//                isInvicibleAfterClash = true;
//                timeCountAfterClash = 0f;
//            }
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