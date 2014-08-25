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
			case "Ice Field":
				name = "Gladiator";
				thumbnail.sprite = thumbnails[1];
				break;
			case "Gladiator":
				name = "Steel Arena";
				thumbnail.sprite = thumbnails[2];
				break;
			case "Steel Arena":
				name = "Gasing Evo Arena";
				thumbnail.sprite = thumbnails[3];
				break;
			case "Gasing Evo Arena":
				name = "Nebula";
				thumbnail.sprite = thumbnails[4];
				break;
			case "Nebula":
				name = "Explode Arena";
				thumbnail.sprite = thumbnails[5];
				break;
			case "Explode Arena":
				name = "Space";
				thumbnail.sprite = thumbnails[6];
				break;	
			case "Space":
				name = "Ice Field";
				thumbnail.sprite = thumbnails[0];
				break;	
			default:
				name = "Gladiator";
				thumbnail.sprite = thumbnails[1];
				break;
		}
		configurePref ();
	}

	public void configurePref(){
		Utilities.chosenArena = name;
		Debug.Log ("chosen arena prefs changed to: " + Utilities.chosenArena);
	}
}
