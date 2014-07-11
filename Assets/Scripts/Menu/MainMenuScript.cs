using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	public bool isSinglePlayer = false;
	public bool isStoryMode = false;
	public bool isArcadeMode = false;
	public bool isMultiPlayer = false;
	public bool isTeamMode = false;
	public bool isRoyalRumble = false;
	public bool isOption = false;
	public bool isBackButton = false;
	
	void Start(){
		//atur yang mana yang hilang yang mana yang muncul
		setInitialState ();
	}
	
	void OnMouseUp(){
		//atur yang mana yang hilang yang mana yang muncul
		if (isSinglePlayer) {
			showSinglePlayerMenu();
		} else if (isMultiPlayer) {
			showMultiplayerMenu();
		} else if (isOption) {
			showOptionMenu();
		} else if (isArcadeMode) {
			Application.LoadLevel ("Arcade Menu");
		} else if (isRoyalRumble) {
			Application.LoadLevel ("Multiplayer Menu");
		}else if (isBackButton){
			setInitialState();
		}
	}
	
	void setInitialState(){
		Renderer[] renderers = GameObject.Find ("Single Player").GetComponentsInChildren<Renderer> ();
		Renderer[] MultiPlayerRenderers = GameObject.Find ("Multiplayer").GetComponentsInChildren<Renderer> ();
		Renderer[] OptionsRenderers = GameObject.Find ("Options").GetComponentsInChildren<Renderer> ();
		//disable parents' collider
		Collider[] colliders  = new Collider[3] {GameObject.Find ("Single Player").GetComponent<Collider>(),
			GameObject.Find ("Multiplayer").GetComponent<Collider>(),
			GameObject.Find ("Options").GetComponent<Collider>()};
		Collider[] submenucolliders = new Collider[7] {GameObject.Find ("Story Mode").GetComponent<Collider> (),
			GameObject.Find ("Arcade Mode").GetComponent<Collider> (),
			GameObject.Find ("Team Mode").GetComponent<Collider> (),
			GameObject.Find ("Royal Rumble").GetComponent<Collider> (),
			GameObject.Find ("Back Button S").GetComponent<Collider> (),
			GameObject.Find ("Back Button M").GetComponent<Collider> (),
			GameObject.Find ("Back Button O").GetComponent<Collider> ()};
		foreach(Collider c in submenucolliders){
			c.collider.enabled = false;
		}
		foreach(Collider c in colliders){
			c.collider.enabled = true;
		}
		//enable children's renderer
		foreach (Renderer r in renderers) {
			if (r.name == "Single Player") {
				r.enabled = true;
			} else {
				r.enabled = false;
			}
		}
		foreach (Renderer r in MultiPlayerRenderers) {
			if (r.name == "Multiplayer") {
				r.enabled = true;
			} else {
				r.enabled = false;
			}
		}
		foreach (Renderer r in OptionsRenderers) {
			if (r.name == "Options") {
				r.enabled = true;
			} else {
				r.enabled = false;
			}
		}
	}
	
	void showSinglePlayerMenu(){
		//show all its children, hide other's children, hide parents' renderer and collider
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer> ();
		Collider[] colliders  = new Collider[3] {GameObject.Find ("Single Player").GetComponent<Collider>(),
			GameObject.Find ("Multiplayer").GetComponent<Collider>(),
			GameObject.Find ("Options").GetComponent<Collider>()};
		Renderer[] MultiPlayerRenderers = GameObject.Find ("Multiplayer").GetComponentsInChildren<Renderer> ();
		Renderer[] OptionsRenderers = GameObject.Find ("Options").GetComponentsInChildren<Renderer> ();
		Collider[] submenucolliders = new Collider[3] {GameObject.Find ("Story Mode").GetComponent<Collider> (),
			GameObject.Find ("Arcade Mode").GetComponent<Collider> (),
			GameObject.Find ("Back Button S").GetComponent<Collider> ()};
		foreach(Collider c in submenucolliders){
			c.collider.enabled = true;
		}
		//disable parents' collider
		foreach(Collider c in colliders){
			c.collider.enabled = false;
		}
		//enable children's renderer
		foreach (Renderer r in renderers) {
			if (r.name == "Single Player") {
				r.enabled = false;
			} else {
				r.enabled = true;
			}
		}
		foreach (Renderer r in MultiPlayerRenderers) {
			if (r.name == "Multiplayer") {
				r.enabled = false;
			} else {
				r.enabled = false;
			}
		}
		foreach (Renderer r in OptionsRenderers) {
			if (r.name == "Options") {
				r.enabled = false;
			} else {
				r.enabled = false;
			}
		}
	}
	
	void showMultiplayerMenu(){
		//show all its children, hide other's children
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer> ();
		Collider[] colliders  = new Collider[3] {GameObject.Find ("Single Player").GetComponent<Collider>(),
			GameObject.Find ("Multiplayer").GetComponent<Collider>(),
			GameObject.Find ("Options").GetComponent<Collider>()};
		Renderer[] SinglePlayerRenderers = GameObject.Find ("Single Player").GetComponentsInChildren<Renderer> ();
		Renderer[] OptionsRenderers = GameObject.Find ("Options").GetComponentsInChildren<Renderer> ();
		Collider[] submenucolliders = new Collider[3] {
			GameObject.Find ("Team Mode").GetComponent<Collider> (),
			GameObject.Find ("Royal Rumble").GetComponent<Collider> (),
			GameObject.Find ("Back Button M").GetComponent<Collider> ()};
		foreach(Collider c in submenucolliders){
			c.collider.enabled = true;
		}
		//disable parents' collider
		foreach(Collider c in colliders){
			c.collider.enabled = false;
		}
		foreach (Renderer r in renderers) {
			if (r.name == "Multiplayer") {
				r.enabled = false;
			} else {
				r.enabled = true;
			}
		}
		foreach (Renderer r in SinglePlayerRenderers) {
			if (r.name == "Single Player") {
				r.enabled = false;
			} else {
				r.enabled = false;
			}
		}
		foreach (Renderer r in OptionsRenderers) {
			if (r.name == "Options") {
				r.enabled = false;
			} else {
				r.enabled = false;
			}
		}
	}
	
	void showOptionMenu(){
		//show all its children, hide other's children
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer> ();
		Collider[] colliders  = new Collider[3] {GameObject.Find ("Single Player").GetComponent<Collider>(),
			GameObject.Find ("Multiplayer").GetComponent<Collider>(),
			GameObject.Find ("Options").GetComponent<Collider>()};
		Renderer[] SinglePlayerRenderers = GameObject.Find ("Single Player").GetComponentsInChildren<Renderer> ();
		Renderer[] MultiPlayerRenderers = GameObject.Find ("Multiplayer").GetComponentsInChildren<Renderer> ();
		//						Collider[] submenucolliders = new Collider[5] {GameObject.Find ("Story Mode").GetComponent<Collider> (),
		//							GameObject.Find ("Arcade Mode").GetComponent<Collider> (),
		//							GameObject.Find ("Team Mode").GetComponent<Collider> (),
		//							GameObject.Find ("Royal Rumble").GetComponent<Collider> (),
		//							GameObject.Find ("Back Button O").GetComponent<Collider> ()};
		//						foreach(Collider c in submenucolliders){
		//							c.collider.enabled = true;
		//						}			
		
		//disable parents' collider
		foreach(Collider c in colliders){
			//options not yet assigned
			//c.collider.enabled = false;
		}
		foreach (Renderer r in renderers) {
			if (r.name == "Options") {
				//do nothing
			} else {
				//r.enabled = true;
			}
		}	
		foreach (Renderer r in SinglePlayerRenderers) {
			if (r.name == "Single Player") {
				//do nothing
			} else {
				r.enabled = false;
			}
		}
		foreach (Renderer r in MultiPlayerRenderers) {
			if (r.name == "Multiplayer") {
				//do nothing
			} else {
				r.enabled = false;
			}
		}
	}
	
	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}

}
