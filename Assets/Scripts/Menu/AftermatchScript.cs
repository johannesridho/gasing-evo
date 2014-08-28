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
				Utilities.deleteAllGasingSelected();		//kosongin data gasing2
			} else {
					Application.LoadLevel (Utilities.chosenArena);	
			}
		} else {		//arcade mode
			if (gameObject.name == "quit") {
				Application.LoadLevel ("Main Menu");		
				Utilities.deleteAllGasingSelected();		//kosongin data gasing2
			} else {		//button next dipilih

				Debug.Log("---------------------------------------------"+Utilities.storyModeLevel);
				//set story level here
				if(Utilities.victory == true){
					switch(Utilities.storyModeLevel){
					case 1:
						Utilities.storyModeLevel = 2;
						Utilities.enemy1 = "Legasic";
						Utilities.enemy2 = "Skymir";
						Utilities.chosenArena = "Explode Arena";
						break;
					case 2:
						Utilities.storyModeLevel = 3;
						Utilities.enemy1 = "Legasic";
						Utilities.enemy2 = "Skymir";
						Utilities.enemy3 = "Legasic";
						Utilities.chosenArena = "Gladiator";
						break;
					case 3:
						Utilities.storyModeLevel = 4;
						Utilities.enemy1 = "Legasic";
						Utilities.enemy2 = "Skymir";
						Utilities.enemy3 = "Legasic";
						Utilities.chosenArena = "Steel Arena";
						break;
					case 4:
						Utilities.storyModeLevel = 5;
						Utilities.enemy1 = "Colonix";
						Utilities.enemy2 = "Skymir";
						Utilities.enemy3 = "Legasic";
						Utilities.enemy4 = "Prototype";
						Utilities.enemy5 = "Craseed";
						Utilities.chosenArena = "Gasing Evo Arena";
						break;
					case 5:
						Utilities.storyModeLevel = 6;
						Utilities.enemy1 = "Colonix";
						Utilities.enemy2 = "Skymir";
						Utilities.enemy3 = "Legasic";
						Utilities.enemy4 = "Prototype";
						Utilities.enemy5 = "Craseed";
						Utilities.enemy6 = "Colonix";
						Utilities.enemy7 = "Skymir";
						Utilities.chosenArena = "Nebula";
						break;
					case 6:
						Utilities.storyModeLevel = 7;
						Utilities.enemy1 = "Colonix";
						Utilities.enemy2 = "Skymir";
						Utilities.enemy3 = "Legasic";
						Utilities.enemy4 = "Prototype";
						Utilities.enemy5 = "Craseed";
						Utilities.enemy6 = "Colonix";
						Utilities.enemy7 = "Skymir";
						Utilities.enemy8 = "Legasic";
						Utilities.enemy9 = "Prototype";
						Utilities.chosenArena = "Space";
						break;
					case 7:
						Utilities.storyModeLevel = 8;					
						break;					
					}
					if(Utilities.storyModeLevel < 8) {		//klo udah 8 brarti udah tamat & lgsg masuk credit
						Application.LoadLevel("Loading Screen");		
					}
				}
				else{
					Application.LoadLevel("Loading Screen");
				}
			}
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r,renderer.material.color.g + 40,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
