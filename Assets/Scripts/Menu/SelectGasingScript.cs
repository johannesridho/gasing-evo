﻿using UnityEngine;
using System.Collections;

public class SelectGasingScript : MonoBehaviour {
	public string control;
	public string name;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TextMesh> ().text = control+" "+name;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<TextMesh> ().text = control+" "+name;
	}

	void OnMouseUp(){
		if (name == "arjuna") {
						name = "srikandi";		
				} else if (name == "srikandi") {
			name = "arjuna";		
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
