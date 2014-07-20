using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour {

	void OnMouseUp(){
		if(Utilities.chosenArena == "ice field"){
			Application.LoadLevel("IceField");
		}else if(Utilities.chosenArena == "stone field"){
			Application.LoadLevel("Single Player");
		}else if(Utilities.chosenArena == "gladiator"){
			Application.LoadLevel("Gladiator");
		}else if(Utilities.chosenArena == "explode arena"){
			Application.LoadLevel("Drum Slide");
		}
	}
	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}

}
