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
			}else if(Application.loadedLevelName == "Arcade Menu" || Application.loadedLevelName == "Story Menu"){
				Application.LoadLevel("Main Menu");
			}else{	//arena

			}
		}
	}
}
