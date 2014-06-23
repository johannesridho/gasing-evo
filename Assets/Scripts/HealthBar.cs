using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Gasing gasing;

	public Vector3 screenPosition;
	public float healthBarLength;

	public Texture2D teksturHealth;
	public Texture2D teksturHealth2;

	void Awake(){
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
		GUIStyle style = new GUIStyle(GUI.skin.box);
//		Texture2D texture = new Texture2D(1, 1);
//		texture.SetPixel (1, 1, Color.green);
//		texture.Apply ();
		style.normal.background = teksturHealth;

		GUIStyle style2 = new GUIStyle(GUI.skin.box);
//		Texture2D texture2 = new Texture2D(1, 1);
//		texture2.SetPixel (1, 1, Color.red);
//		texture2.Apply ();
		style2.normal.background = teksturHealth2;

		GUI.Box(new Rect(screenPosition.x - 25, screenPosition.y - 35, 50, 7), "",style2);
		GUI.Box(new Rect(screenPosition.x - 25, screenPosition.y - 35, healthBarLength, 7), "",style);
	}

}//end class
