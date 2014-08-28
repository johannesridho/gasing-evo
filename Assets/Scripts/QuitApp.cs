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


	
	
}//end class
