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
		if (name == "arena a") {
			name = "arena b";		
		} else if (name == "arena b") {
			name = "arena a";		
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
