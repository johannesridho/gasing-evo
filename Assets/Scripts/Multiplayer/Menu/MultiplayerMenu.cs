using UnityEngine;
using System.Collections;

public class MultiplayerMenu : MonoBehaviour {

	private bool showMenu = false;
	private Rect myWindowRect;
	private Menu mainMenuScript;

	void Awake(){
		myWindowRect  = new Rect (Screen.width/2-150,Screen.height/2-100,300,200);	
	}

	// Use this for initialization
	void Start () {
		mainMenuScript = Menu.SP;
	}
	
	public void enableMenu(){
		showMenu = true;
	}

	void OnGUI(){
		if (showMenu) {
			GUILayout.Window (0, myWindowRect, windowGUI, "Multiplayer");
		}
	}

	void windowGUI(int id){


		GUILayout.BeginVertical ();
		//button Host a game
		if(GUILayout.Button("Host a game")){
			showMenu = false;
			mainMenuScript.openMenu("multiplayer-host");
		}

		//button Host a game
		if(GUILayout.Button("Select a game to join")){
			showMenu = false;
			mainMenuScript.openMenu("multiplayer-join");
		}

		//button Host a game
		if(GUILayout.Button("Join via quickplay")){
			showMenu = false;
			mainMenuScript.openMenu("multiplayer-quickplay");
		}
		GUILayout.EndVertical ();
	}
}
