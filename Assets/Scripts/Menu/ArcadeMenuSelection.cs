using UnityEngine;
using System.Collections;

public class ArcadeMenuSelection : MonoBehaviour {
	public bool isTeamMode;
	public bool isRoyalMode;
	public bool isNext;
	public bool isBack;
	public bool isInitialStateSet;

	// Use this for initialization
	void Start () {
		if (!isInitialStateSet) {
			chooseRoyalMode ();
			chooseCamera ("Main Camera");
			isInitialStateSet = true;
			setOtherInitialState();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void setOtherInitialState(){
		GameObject[] hasArcades = GameObject.FindGameObjectsWithTag("HasArcadeScript");
		foreach (GameObject h in hasArcades){
			if (h.name == gameObject.name){
				//do nothing
			}else{
				h.GetComponent<ArcadeMenuSelection>().isInitialStateSet = true;
			}
		}
	}

	void chooseCamera(string cameraname){
		Camera[] cameras = GameObject.FindObjectsOfType<Camera> ();

		foreach (Camera c in cameras) {
			if (c.name == cameraname){
				c.enabled = true;
			}	else{
				c.enabled = false;
			}	
		}
	}

	void OnMouseUp(){
		if (isTeamMode) {
						chooseRoyalMode ();		
				} else if (isRoyalMode) {
						chooseTeamMode ();
				} else if (isNext) {
						chooseCamera (GameObject.Find("Arcade Menus").GetComponent<ArcadeCameraController>().designatedCamera);	
						if(GameObject.Find("Arcade Menus").GetComponent<ArcadeCameraController>().designatedCamera == "Team Mode Camera")
						{
							GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().setTeamModeContent ();
						}else{
							GameObject.Find ("Royal Mode Menus").GetComponent<RoyalModeMenuScript> ().setRoyalModeContent ();
						}
				} else if (isBack) {
						chooseCamera ("Main Camera");	
						GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().isSet = false;
						GameObject.Find ("Royal Mode Menus").GetComponent<RoyalModeMenuScript> ().isSet = false;
				}
	}

	void chooseTeamMode(){
		Renderer[] renderers = GameObject.FindObjectsOfType<Renderer> ();
		Collider[] colliders = GameObject.FindObjectsOfType<Collider> ();
		foreach (Renderer r in renderers) {
			if (r.name == "royal"){
				r.renderer.enabled = false;
			}		
			else if (r.name == "team"){
				r.renderer.enabled = true;
			}
		}
		foreach (Collider c in colliders) {
			if (c.name == "royal"){
				c.collider.enabled = false;
			}	
			else if (c.name == "team"){
				c.collider.enabled = true;
			}	
		}
		GameObject.Find("Arcade Menus").GetComponent<ArcadeCameraController>().designatedCamera = "Team Mode Camera";
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().isOneExist = true;
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().number = "one";
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().numberInt = 1;
		GameObject.Find ("numbers").GetComponent<SelectHowManyScript> ().configurePref ();
		Utilities.chosenMode = 1;
	}

	void chooseRoyalMode(){
		Renderer[] renderers = GameObject.FindObjectsOfType<Renderer> ();
		Collider[] colliders = GameObject.FindObjectsOfType<Collider> ();
		foreach (Renderer r in renderers) {
			if (r.name == "royal"){
				r.renderer.enabled = true;
			}		
			else if (r.name == "team"){
				r.renderer.enabled = false;
			}
		}
		foreach (Collider c in colliders) {
			if (c.name == "royal"){
				c.collider.enabled = true;
			}	
			else if (c.name == "team"){
				c.collider.enabled = false;
			}	
		}
		GameObject.Find("Arcade Menus").GetComponent<ArcadeCameraController>().designatedCamera = "Royal Mode Camera";
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().isOneExist = false;
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().number = "two";
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().numberInt = 2;
		GameObject.Find ("numbers").GetComponent<SelectHowManyScript> ().configurePref ();
		GameObject.Find ("Royal Mode Menus").GetComponent<RoyalModeMenuScript> ().howManyGasing = 2;
		Utilities.chosenMode = 0;
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
