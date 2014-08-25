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
		if (gameObject.name == "quit") {
			Application.LoadLevel ("Main Menu");		
		} else {
			Application.LoadLevel(Utilities.chosenArena);	
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
