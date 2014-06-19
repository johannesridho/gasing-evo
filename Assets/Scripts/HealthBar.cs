using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {



	public GameObject pemain;
	private Vector3 offset;

//	public GUIStyle : myGUIStyle;

//	public static HealthBar instance;
//	public Rect lifeBarRect;
//	public Rect lifeBarLabelRect;
//	public Rect lifeBarBackgroundRect;
//	public Texture2D lifeBarBackground;
//	public Texture2D lifeBar;
//
//	private float LifeBarWidth = 300f;
//

	void Awake(){
		if (!pemain) {
			pemain = GameObject.Find ("Pemain");
		}
	}

	// Use this for initialization
	void Start () {
//		instance = this;
//		offset = pemain.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
//		transform.position = offset + pemain.transform.position;
	}

	void OnGUI(){		
//		instance.lifeBarRect.width = LifeBarWidth * (200);
//		instance.lifeBarRect.height = 20;
//		
//		instance.lifeBarBackgroundRect.width = LifeBarWidth;
//		instance.lifeBarBackgroundRect.height= 20;
//		
//		GUI.DrawTexture(lifeBarRect, lifeBar);
//		GUI.DrawTexture(lifeBarBackgroundRect, lifeBarBackground);
//		
//		GUI.Label(lifeBarLabelRect, "LIFE");

		GUI.Box (new Rect(pemain.transform.position.x,pemain.transform.position.z,100,100),"Life");
	}

}//end class
