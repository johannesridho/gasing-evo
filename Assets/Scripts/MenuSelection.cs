using UnityEngine;
using System.Collections;

public class MenuSelection : MonoBehaviour {
	public bool isSinglePlayer = false;
	public bool isStoryMode = false;
	public bool isArcadeMode = false;
	public bool isMultiPlayer = false;
	public bool isTeamMode = false;
	public bool isRoyalRumble = false;
	public bool isOption = false;

	void Start(){
		//atur yang mana yang hilang yang mana yang muncul
		if (isSinglePlayer) {
			//hide all its children
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				if (r.name == "Single Player"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
		} else if (isMultiPlayer) {
			//hide all its children
			gameObject.renderer.enabled = true;
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				if (r.name == "Multiplayer"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
		} else if (isOption) {
			//hide all its children
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				if (r.name == "Options"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}				
		} 
	}

void OnMouseUp(){
		//atur yang mana yang hilang yang mana yang muncul
		if (isSinglePlayer) {
			//show all its children, hide other's children
//			Debug.Log("wah");
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			Renderer[] MultiPlayerRenderers = GameObject.Find("Multiplayer").GetComponentsInChildren<Renderer>();
			Renderer[] OptionsRenderers = GameObject.Find("Options").GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				if (r.name == "Single Player"){
					//do nothing
				}
				else{
					r.enabled = true;
				}
			}
			foreach (Renderer r in MultiPlayerRenderers) {
				if (r.name == "Multiplayer"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
			foreach (Renderer r in OptionsRenderers) {
				if (r.name == "Options"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
		} else if (isMultiPlayer) {
			//show all its children, hide other's children
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			Renderer[] SinglePlayerRenderers = GameObject.Find("Single Player").GetComponentsInChildren<Renderer>();
			Renderer[] OptionsRenderers = GameObject.Find("Options").GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				if (r.name == "Multiplayer"){
					//do nothing
				}
				else{
					r.enabled = true;
				}
			}
			foreach (Renderer r in SinglePlayerRenderers) {
				if (r.name == "Single Player"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
			foreach (Renderer r in OptionsRenderers) {
				if (r.name == "Options"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
		} else if (isOption) {
			//show all its children, hide other's children
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			Renderer[] SinglePlayerRenderers = GameObject.Find("Single Player").GetComponentsInChildren<Renderer>();
			Renderer[] MultiPlayerRenderers = GameObject.Find("Multiplayer").GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				if (r.name == "Options"){
					//do nothing
				}
				else{
					r.enabled = true;
				}
			}	
			foreach (Renderer r in SinglePlayerRenderers) {
				if (r.name == "Single Player"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
			foreach (Renderer r in MultiPlayerRenderers) {
				if (r.name == "Multiplayer"){
					//do nothing
				}
				else{
					r.enabled = false;
				}
			}
		}
		else if (isArcadeMode) {
			Application.LoadLevel("Single Player");
//			Debug.Log("wah");
		}
	}

	void OnMouseOver(){
		renderer.material.color = Color.red;
	}

	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
