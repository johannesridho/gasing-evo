using UnityEngine;
using System.Collections;

public class NameGUI : MonoBehaviour {
	//GUI vars
	public bool isActive = false;

	private Vector3 textPos;
	private string label;
	private GUIStyle style = new GUIStyle();
	public Font guiFont;
	private int marginx, marginy;

	Camera Cam;
	// Use this for initialization
	void Start () {
		marginx = (int)Screen.width / 100;
		marginy = (int)(Screen.height / 4.5f);

		if (Utilities.chosenMode == 0) {
			Cam = GameObject.Find ("Royal Mode Camera").camera;
		} else {
			Cam = GameObject.Find ("Team Mode Camera").camera;
		}
		//GUI init
		style.font = guiFont;
		style.normal.textColor = new Color(1,(float)172/255,(float)18/255,1);
		textPos = Cam.WorldToScreenPoint(transform.position);
		if (gameObject.name == "arena name") {
			label = Utilities.chosenArena;
		} else {
			label = gameObject.name;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		if (isActive) {
			if(Utilities.chosenMode == 0){
				if (gameObject.name == "p1" || gameObject.name == "e1" || 
					gameObject.name == "e2" || gameObject.name == "e3" ||
					gameObject.name == "e4") {
					GUI.Label (new Rect (textPos.x - marginx, textPos.y - marginy, 200, 20), label, style);
				} else if (gameObject.name == "e5" || gameObject.name == "e6" || 
					gameObject.name == "e7" || gameObject.name == "e8" ||
					gameObject.name == "e9") {
					GUI.Label (new Rect (textPos.x - marginx, textPos.y + (marginy / 3.5f), 200, 20), label, style);
				} else {
					GUI.Label (new Rect (textPos.x - marginx * 5.5f, textPos.y + (marginy / 6), 200, 20), label, style);
					label = Utilities.chosenArena;
				}
			}else{
				if (gameObject.name == "p1" || gameObject.name == "e1") {
					GUI.Label (new Rect (textPos.x - marginx, textPos.y - (marginy*0.9f), 200, 20), label, style);
				} else if (gameObject.name == "a1" || gameObject.name == "e2") {
					GUI.Label (new Rect (textPos.x - marginx, textPos.y + (marginy*0.1f), 200, 20), label, style);
				} else if (gameObject.name == "a2" || gameObject.name == "e3") {
					GUI.Label (new Rect (textPos.x - marginx, textPos.y +(marginy*1.1f), 200, 20), label, style);
				} 
				else {
					GUI.Label (new Rect (textPos.x - marginx * 5.5f, textPos.y + (marginy / 6), 200, 20), label, style);
					label = Utilities.chosenArena;
				}
			}
		}else{

		}
	}
}
