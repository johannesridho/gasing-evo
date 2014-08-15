using UnityEngine;
using System.Collections;

public class SelectArenaScript : MonoBehaviour {
	public string name;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TextMesh> ().text = name;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<TextMesh> ().text = name;
	}

	void OnMouseUp(){
		if (name == "ice field") {
			name = "gladiator";		
		} else if (name == "gladiator") {
			name = "steel arena";		
		} else if (name == "steel arena") {
			name = "explode arena";		
		} else if (name == "explode arena") {
			name = "ice field";		
		}
		configurePref ();
	}

	public void configurePref(){
		Utilities.chosenArena = name;
		Debug.Log ("chosen arena prefs changed to: " + Utilities.chosenArena);
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
