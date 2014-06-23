using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public float speed;
	public Gasing gasing;
	public Texture2D buttonSkill1;
	
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
				hor = Input.acceleration.x *2;
				ver = Input.acceleration.y *2;
			}else{
				hor = Input.GetAxis ("Horizontal");
				ver = Input.GetAxis ("Vertical");
			}
		}


		Vector3 movement = new Vector3 (hor, 0f, ver);

		rigidbody.AddForce (movement * speed * Time.deltaTime);

	}

	void OnGUI () {
		GUIStyle style = new GUIStyle(GUI.skin.box);
		style.normal.background = buttonSkill1;

		if (GUI.Button (new Rect (Screen.width * 4/5, Screen.height * 4/5, 60, 30), "",style)) {
			//cek apakah skill 1 ditekan
			// This code is executed when the Button is clicked
//			Debug.Log("wah");

			//sementara pake skill jump
			float jump_coef = 15000f;
			Vector3 movement = new Vector3 (0f, 1f, 0f);		
			rigidbody.AddForce (movement * jump_coef * Time.deltaTime);	
		}
	}

}
