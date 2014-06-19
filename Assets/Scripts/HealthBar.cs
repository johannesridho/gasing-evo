using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public GameObject pemain;
	public Gasing gasing;

	public Vector3 screenPosition;
	public float healthBarLength;

	void Awake(){
		if (!pemain) {
			pemain = GameObject.Find ("Pemain");
		}
		if(!gasing){
			gasing = GetComponent<Gasing>();
		}
	}

	// Use this for initialization
	void Start () {
		healthBarLength = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;	//naikin dikit biar di atasnya gasing
		healthBarLength = gasing.getEP () / gasing.getEPMax() * 50;
	}

	void OnGUI(){		
//		GUIStyle styleBarKosong = new GUIStyle ();
//		GUISkin skin = new GUISkin ();

		GUIStyle style = new GUIStyle(GUI.skin.box);
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel (1, 1, Color.green);
		texture.Apply ();
		//Set new texture as background
		style.normal.background = texture;

		GUIStyle style2 = new GUIStyle(GUI.skin.box);
		Texture2D texture2 = new Texture2D(1, 1);
		texture2.SetPixel (1, 1, Color.red);
		texture2.Apply ();
		//Set new texture as background
		style2.normal.background = texture2;

//		GUI.backgroundColor = Color.yellow;
//		GUI.Box (new Rect(pemain.transform.position.x,pemain.transform.position.z,100,100),"Life");
		GUI.Box(new Rect(screenPosition.x - 25, screenPosition.y - 35, 50, 10), "",style2);
		GUI.Box(new Rect(screenPosition.x - 25, screenPosition.y - 35, healthBarLength, 10), "",style);
	}

}//end class
