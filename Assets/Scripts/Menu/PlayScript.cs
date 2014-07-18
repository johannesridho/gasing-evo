﻿using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour {

	void OnMouseUp(){
		if(Utilities.chosenArena == "arena a"){
			Application.LoadLevel("IceField");
		}
	}
	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}

}
