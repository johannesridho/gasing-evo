using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		Application.LoadLevel ("Main Menu");
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r,renderer.material.color.g + 40,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
