using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public float speed;
	public Gasing gasing;
	
	void Awake(){
		if(!gasing){
			gasing = GetComponent<Gasing>();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//called every fixed framerate frame
	void FixedUpdate () {
		float hor = 0f;
		float ver = 0f;

		if (gasing.isOnGround) {
			if (Application.platform == RuntimePlatform.Android){
				hor = Input.acceleration.x;
				ver = Input.acceleration.y;
			}else{
				hor = Input.GetAxis ("Horizontal");
				ver = Input.GetAxis ("Vertical");
			}
		}


		Vector3 movement = new Vector3 (hor, 0f, ver);

		rigidbody.AddForce (movement * speed * Time.deltaTime);

	}
}
