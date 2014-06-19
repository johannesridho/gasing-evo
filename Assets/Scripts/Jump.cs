using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	
	public float jump_coef;
	public Gasing gasing;
	
	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
		jump_coef = 20000f;
	}

	void Start () {
		
	}

	void Update () {
		
	}

	void FixedUpdate () {
		float jump = 0f;		
		if (gasing.isOnGround) {
			jump = (Input.GetKeyDown("space")) ? 1f : jump;
			if (Input.GetKeyDown("space"))
				gasing.isOnGround = false;
		}		
		Vector3 movement = new Vector3 (0f, jump, 0f);		
		rigidbody.AddForce (movement * jump_coef * Time.deltaTime);		
	}
}
