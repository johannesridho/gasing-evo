using UnityEngine;
using System.Collections;

public class SelectGasingScript : MonoBehaviour {
	public string control;
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
		switch (name){
		case "Craseed":
			name = "Legasic";
			thumbnail.sprite = thumbnails[1];
			break;
		case "Legasic":
			name = "Prototype";
			thumbnail.sprite = thumbnails[2];
			break;
		case "Prototype":
			name = "Skymir";
			thumbnail.sprite = thumbnails[3];
			break;
		case "Skymir":
			name = "Colonix";
			thumbnail.sprite = thumbnails[4];
			break;
		case "Colonix":
			name = "Craseed";
			thumbnail.sprite = thumbnails[0];
			break;
		default:
			name = "Legasic";
			thumbnail.sprite = thumbnails[1];
			break;
		}
		configurePref ();
	}

	public void configurePref(){
		switch (gameObject.name){
		case "a1":
			Utilities.ally1 = name;
			break;
		case "a2":
			Utilities.ally2 = name;
			break;
		case "e1":
			Utilities.enemy1 = name;
			break;
		case "e2":
			Utilities.enemy2 = name;
			break;
		case "p1":
			Utilities.playerGasing = name;
			break;
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
