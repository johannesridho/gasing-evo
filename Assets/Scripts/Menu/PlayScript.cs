using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour {

	void OnMouseUp(){
		Application.LoadLevel(Utilities.chosenArena);
//		switch (Utilities.chosenArena){
//		case "ice field":
//			Application.LoadLevel("IceField");
//			break;
//		case "steel arena":
//			Application.LoadLevel("Single Player");
//			break;
//		case "gladiator":
//			Application.LoadLevel("Gladiator");
//			break;
//		case "explode arena":
//			Application.LoadLevel("Drum Slide");
//			break;
//		case "nebula":
//			Application.LoadLevel("Nebula");
//			break;
//		case "gasing evo":
//			Application.LoadLevel("Drum Slide");
//			break;
//		case "explode arena":
//			Application.LoadLevel("Drum Slide");
//			break;
//		default:
//			Application.LoadLevel("Drum Slide");
//			break;
//		}
	}
	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r,renderer.material.color.g + 40,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}

}
