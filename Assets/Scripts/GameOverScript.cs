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

		if (GUI.Button (new Rect (Screen.width * 1 / 2 - 100, Screen.height * 7 / 10, 200, 30), "Back To Menu", style)) {
			Application.LoadLevel("Main Menu");
		}
	}
}
