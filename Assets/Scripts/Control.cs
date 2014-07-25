using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	private Gasing gasing;
	private PhysicsTabrak gasing_pt;
	
	void Awake(){
		if(!gasing){
			gasing = GetComponent<Gasing>();
		}
        if (!gasing_pt)
        {
            gasing_pt = GetComponent<PhysicsTabrak>();
        }
	}

	void Start () {
	
	}

	void Update () {
	
	}

	void FixedUpdate () {
		float hor = 0f;
		float ver = 0f;
        if (gasing.isOnGround && !gasing_pt.isInvicibleAfterClash) {
			if (Application.platform == RuntimePlatform.Android){
				hor = Input.acceleration.x * Gasing.COEF_SPEED * gasing.speed;
				ver = Input.acceleration.y * Gasing.COEF_SPEED * gasing.speed;
			}else{
				hor = Input.GetAxis("Horizontal") * Gasing.COEF_SPEED * gasing.speed;
				ver = Input.GetAxis("Vertical") * Gasing.COEF_SPEED * gasing.speed;
			}
		}
		Vector3 movement = new Vector3 (hor, 0f, ver);
		rigidbody.AddForce (movement * 2000 * Time.deltaTime);
	}

}
