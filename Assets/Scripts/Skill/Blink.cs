using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	public Gasing gasing;
	public Texture2D buttonSkill1;
	private float skillPointNeeded;		//skill point yg diperlukan
	
	void Awake(){
		skillPointNeeded = 10;
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}
	
	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnGUI () {
		GUIStyle style = new GUIStyle (GUI.skin.box);
		style.normal.background = buttonSkill1;
		
		if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height * 7 / 10, 60, 30), "Blink", style) && gasing.getSP()>skillPointNeeded) {
			float x;
			float z;
			if (Application.platform == RuntimePlatform.Android){
				x = transform.position.x + 50 * Input.acceleration.x;
				z = transform.position.z + 50 * Input.acceleration.y;
			}else{
				x = transform.position.x + 15 * Input.GetAxis("Horizontal");
				z = transform.position.z + 15 * Input.GetAxis("Vertical");
			}
			if(x<20 && z<20 && x>-20 && z>-20){			//hardcode buat stonefield aja
				transform.position = new Vector3(x, 1f, z);
			}
//			transform.position+=transform.;
			gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing

		}
	}
}
