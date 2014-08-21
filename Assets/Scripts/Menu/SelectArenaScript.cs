using UnityEngine;
using System.Collections;

public class SelectArenaScript : MonoBehaviour {
	public string name;
	public Sprite[] thumbnails;
	private SpriteRenderer thumbnail;
	// Use this for initialization
	void Start () {
		thumbnail = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseUp(){
		switch (name) {
			case "ice field":
				name = "gladiator";
				thumbnail.sprite = thumbnails[1];
				break;
			case "gladiator":
				name = "steel arena";
				thumbnail.sprite = thumbnails[2];
				break;
			case "steel arena":
				name = "gasing evo";
				thumbnail.sprite = thumbnails[3];
				break;
			case "gasing evo":
				name = "nebula";
				thumbnail.sprite = thumbnails[4];
				break;
			case "nebula":
				name = "explode arena";
				thumbnail.sprite = thumbnails[5];
				break;
			case "explode arena":
				name = "ice field";
				thumbnail.sprite = thumbnails[0];
				break;	
			default:
				name = "gladiator";
				thumbnail.sprite = thumbnails[1];
				break;
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
