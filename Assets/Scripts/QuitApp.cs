using UnityEngine;
using System.Collections;

public class QuitApp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(Application.loadedLevelName == "Main Menu"){
				Application.Quit(); 
			}else if(Application.loadedLevelName == "Arcade Menu" || Application.loadedLevelName == "Story Menu" || Application.loadedLevelName == "Voice Calibration"){
				Application.LoadLevel("Main Menu");
			}else if(Application.loadedLevelName == "Multiplayer Menu"){

			}else{	//arena
				if (Time.timeScale == 0) Time.timeScale = 1;
					else Time.timeScale = 0;		//pause game
			}
		}
	}

	void OnGUI(){
		if(Time.timeScale == 0){		//if paused
			GUIStyle style = new GUIStyle(GUI.skin.box);
	//		style.normal.background = skills.buttonSkill1;
//		    if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), "tes", style))
			if (GUI.Button(new Rect(Screen.width * 1/2 - 150, 20, 300, 100), "Restart", style))
			{
				Application.LoadLevel(Utilities.chosenArena);
				Time.timeScale = 1;
			}
			if (GUI.Button(new Rect(Screen.width * 1/2 - 150, 120, 300, 100), "Main Menu", style))
			{
				Application.LoadLevel("Main Menu");
				Time.timeScale = 1;
			}
			if (GUI.Button(new Rect(Screen.width * 1/2 - 150, 220, 300, 100), "Quit Game", style))
			{
				Application.Quit();
				Time.timeScale = 1;
			}

		}
	}
	
	
}//end class
