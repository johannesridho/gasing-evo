using UnityEngine;
using System.Collections;

public class SelectHowManyScript : MonoBehaviour {
	public bool isOneExist = false;
	public string number;
	public int numberInt;

	// Use this for initialization
	void Start () {
		if (isOneExist) {
			number = "one";
			numberInt = 1;
			GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().gasingPerTeam = numberInt;
		}
		else {
			number = "two";
			numberInt = 2;
			GameObject.Find ("Royal Mode Menus").GetComponent<RoyalModeMenuScript> ().howManyGasing = numberInt;
		}
		gameObject.GetComponent<TextMesh> ().text = number;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<TextMesh> ().text = number;
	}

	void OnMouseUp(){
		configureNames ();	
		configurePref ();
	}

	void configureNames(){
		if (isOneExist) {
			if(number == "one"){
				number = "two";
				numberInt = 2;
				GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().gasingPerTeam = numberInt;
			}	
			else if (number == "two")
			{
				number = "three";
				numberInt = 3;
				GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().gasingPerTeam = numberInt;
			} 
			else if (number == "three")
			{
				number = "one";
				numberInt = 1;
				GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().gasingPerTeam = numberInt;
			} 
		}
		else {
			switch (number){
				case "two":
					number = "three";
					numberInt = 3;
					break;
				case "three":
					number = "four";
					numberInt = 4;
					break;
				case "four":
					number = "five";
					numberInt = 5;
					break;
				case "five":
					number = "six";
					numberInt = 6;
					break;
			}
			GameObject.Find ("Royal Mode Menus").GetComponent<RoyalModeMenuScript> ().howManyGasing = numberInt;
		}
	}

	public void configurePref(){
		if (isOneExist) {//team mode
			//set how many allies	
			Utilities.howManyGasingTeam = numberInt;
			Debug.Log("how many gasing team : " + Utilities.howManyGasingTeam.ToString());
		}
		else {//royal mode
			Utilities.howManyGasingRoyal = numberInt;
			Debug.Log("how many gasing royal : " + Utilities.howManyGasingRoyal.ToString());
		}
	}

	void OnMouseOver(){
		renderer.material.color = new Color (renderer.material.color.r + 40,renderer.material.color.g,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
