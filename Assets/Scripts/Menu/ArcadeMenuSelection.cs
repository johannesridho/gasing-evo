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
						chooseRoyalMode();
				}
	}

	void chooseTeamMode(){
		GameObject[] objects = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject o in objects) {
			if (o.name == "royal"){
				o.renderer.enabled = false;
				o.collider.enabled = false;
			}		
			else if (o.name == "team"){
				o.renderer.enabled = true;
				o.collider.enabled = true;
			}
		}
		GameObject.Find("Arcade Menus").GetComponent<ArcadeCameraController>().designatedCamera = "Team Mode Camera";
		GameObject.Find ("how many").GetComponent<TextMesh> ().text = "gasing per team";
		setNumbers (1);
		Utilities.chosenMode = 1;
	}

	void chooseRoyalMode(){
		GameObject[] objects = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject o in objects) {
			if (o.name == "royal"){
				o.renderer.enabled = true;
				o.collider.enabled = true;
			}		
			else if (o.name == "team"){
				o.renderer.enabled = false;
				o.collider.enabled = false;
			}
		}
		GameObject.Find("Arcade Menus").GetComponent<ArcadeCameraController>().designatedCamera = "Royal Mode Camera";
		GameObject.Find ("how many").GetComponent<TextMesh> ().text = "number of gasing";
		setNumbers (2);
		Utilities.chosenMode = 0;
	}

	void setNumbers(int num){
		if (num == 1) {
			GameObject.Find("numbers").GetComponent<SelectHowManyScript>().isTeamMode = true;
			GameObject.Find("numbers").GetComponent<SelectHowManyScript>().number = "one";
		} else {
			GameObject.Find("numbers").GetComponent<SelectHowManyScript>().isTeamMode = false;
			GameObject.Find("numbers").GetComponent<SelectHowManyScript>().number = "two";
		}
		GameObject.Find("numbers").GetComponent<SelectHowManyScript>().numberInt = num;	
		GameObject.Find("numbers").GetComponent<SelectHowManyScript> ().configureNames (num);
		GameObject.Find("numbers").GetComponent<SelectHowManyScript> ().configurePref ();
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r,renderer.material.color.g + 40,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
