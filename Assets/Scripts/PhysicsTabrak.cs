using UnityEngine;
using System.Collections;

public class PhysicsTabrak : MonoBehaviour {
	
	public bool isInvicibleAfterClash;
	private float timeCountAfterClash;
	private Gasing gasing;
	private static float COEF_MOMENTUM = 1.2f;

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
	
	void geserAfterClash(Collision C) {
		float coeff_geser = 0.3f;
		Vector3 heading = C.collider.rigidbody.position - gasing.rigidbody.position;
		Vector3 direction = heading / heading.magnitude;
		Vector3 posSelf = C.collider.rigidbody.position + new Vector3(direction.x * coeff_geser, 0, direction.z * coeff_geser);
		Vector3 posEnemy = gasing.rigidbody.position - new Vector3(direction.x * coeff_geser, 0, direction.z * coeff_geser);
		gasing.rigidbody.MovePosition(posSelf);
		C.collider.SendMessage ("movePosition", posEnemy, SendMessageOptions.DontRequireReceiver);
	}	
	
	void changeSpeedAfterClash(Collision C) {
		Vector3 momentum = COEF_MOMENTUM * gasing.rigidbody.velocity;
		C.collider.SendMessage ("speedChange", momentum, SendMessageOptions.DontRequireReceiver);
		gasing.collider.SendMessage ("speedChange", -momentum, SendMessageOptions.DontRequireReceiver);
	}

	void damageAfterClash(Collision C) {
		Vector3 vel = gasing.rigidbody.velocity;
		float damage = Gasing.COEF_POWER * gasing.power * Mathf.Sqrt(Mathf.Pow(vel.x,2) + Mathf.Pow(vel.y,2) + Mathf.Pow(vel.z,2));
		C.collider.SendMessage ("EPKurang", damage, SendMessageOptions.DontRequireReceiver);
	}
	
	void bounceAgainstObstacles(Collision C) {
		float coeff_obs = 30f;
		ContactPoint cp = C.contacts[0];
		Vector3 dir = gasing.rigidbody.position - cp.point;
		Vector3 direction = dir / dir.magnitude;
		Vector3 momentum = COEF_MOMENTUM * direction * coeff_obs;
		gasing.collider.SendMessage("speedChange", momentum, SendMessageOptions.DontRequireReceiver);
	}	
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			if (!isInvicibleAfterClash) {
				isInvicibleAfterClash = true;
				timeCountAfterClash = 0f;
			}
			geserAfterClash(col);
			changeSpeedAfterClash(col);
			damageAfterClash(col);
		}
        if (col.gameObject.tag == "Item")
        {
            if (!isInvicibleAfterClash)
            {
                isInvicibleAfterClash = true;
                timeCountAfterClash = 0f;
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