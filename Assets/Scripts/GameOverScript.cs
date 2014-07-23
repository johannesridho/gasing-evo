using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public Texture2D button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle (GUI.skin.box);
		style.normal.background = button;
		style.fontSize = 15;

		if (GUI.Button (new Rect (Screen.width * 1 / 2 - Screen.width / 6 / 2, Screen.height * 7 / 10, Screen.width / 6, Screen.height / 8), "Back To Menu", style)) {
			Application.LoadLevel("Main Menu");
		}
	}
}
