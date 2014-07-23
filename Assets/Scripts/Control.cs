using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	private Gasing gasing;
	public PhysicsTabrak gasing_pt;
	public Texture2D buttonSkill1;
	
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

	void OnGUI () {
		GUIStyle style = new GUIStyle(GUI.skin.box);
		style.normal.background = buttonSkill1;

//		if (GUI.Button (new Rect (Screen.width * 4/5, Screen.height * 7/10, 60, 30), "Skill 1",style)) {
			//cek apakah skill 1 ditekan
			// This code is executed when the Button is clicked
//			Debug.Log("wah");

			//sementara pake skill jump
//			float jump_coef = 15000f;
//			Vector3 movement = new Vector3 (0f, 1f, 0f);		
//			rigidbody.AddForce (movement * jump_coef * Time.deltaTime);	
//		}
		if (GUI.Button (new Rect (Screen.width * 4/5, Screen.height * 4/5, Screen.width / 7, Screen.height / 8), "Skill 2",style)) {
			//cek apakah skill 2 ditekan
			// This code is executed when the Button is clicked
			//			Debug.Log("wah");
			
			//sementara pake skill jump
			float jump_coef = 15000f;
			Vector3 movement = new Vector3 (0f, 1f, 0f);		
			rigidbody.AddForce (movement * jump_coef * Time.deltaTime);	
		}
		if (GUI.Button (new Rect (Screen.width * 1/10, Screen.height * 4/5, Screen.width / 7, Screen.height / 8), "Ulti",style)) {
			//cek apakah ultimate skill ditekan
			// This code is executed when the Button is clicked
			//			Debug.Log("wah");
			
			//sementara pake skill jump
			float jump_coef = 15000f;
			Vector3 movement = new Vector3 (0f, 1f, 0f);		
			rigidbody.AddForce (movement * jump_coef * Time.deltaTime);	
		}
	}

}
