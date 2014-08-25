using UnityEngine;
using System.Collections;

public class AftermatchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		if (Utilities.chosenMode == 0 || Utilities.chosenMode == 1) {
			if (gameObject.name == "quit") {
					Application.LoadLevel ("Main Menu");		
			} else {
					Application.LoadLevel (Utilities.chosenArena);	
			}
		} else {
			if (gameObject.name == "quit") {
				Application.LoadLevel ("Main Menu");		
			} else {
				//set story level here
				if(Utilities.victory == true){
					switch(Utilities.storyModeLevel){
					case 1:
						Utilities.storyModeLevel = 2;
						Utilities.enemy1 = "craseed";
						break;
					case 2:
						Utilities.storyModeLevel = 3;
						Utilities.enemy1 = "craseed";
						break;
					}
					Application.LoadLevel("Loading Screen");
				}
				else{
					Application.LoadLevel("Loading Screen");
				}
			}
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
