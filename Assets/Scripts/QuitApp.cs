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
		GameObject gasingMusuh = GameObject.FindGameObjectWithTag ("Enemy");
		if(Time.timeScale == 0 && gasingMusuh){		//if paused dan ada musuh (ada musuh = lagi battle di arena single player)
			GUIStyle style = new GUIStyle(GUI.skin.box);
			float lebar = Screen.width / 2;
			float tinggi = Screen.height / 4;
	//		style.normal.background = skills.buttonSkill1;
//		    if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), "tes", style))
			if (GUI.Button(new Rect(Screen.width * 1/2 - lebar / 2, tinggi / 2, lebar, tinggi), "Restart", style))
			{
				Application.LoadLevel(Utilities.chosenArena);
				Time.timeScale = 1;
			}
			if (GUI.Button(new Rect(Screen.width * 1/2 - lebar / 2, tinggi * 3 / 2, lebar, tinggi), "Main Menu", style))
			{
				Application.LoadLevel("Main Menu");
				Time.timeScale = 1;
			}
			if (GUI.Button(new Rect(Screen.width * 1/2 - lebar / 2, tinggi * 5 / 2, lebar, tinggi), "Quit Game", style))
			{
				Application.Quit();
				Time.timeScale = 1;
			}

		}
	}
	
	
}//end class
