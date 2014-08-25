using UnityEngine;
using System.Collections;

public class SelectHowManyScript : MonoBehaviour {
	public bool isTeamMode = false;
	public string number;
	public int numberInt;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TextMesh> ().text = number;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<TextMesh> ().text = number;
	}

	void OnMouseUp(){
		numberInt++;
		configureNames (numberInt);	
		configurePref ();
	}

	public void configureNames(int index){
		if (isTeamMode) {
			switch(index){
				case 1:
					number = "one";
					numberInt = 1;
					break;
				case 2:
					number = "two";
					numberInt = 2;
					break;
				case 3:
					number = "three";
					numberInt = 3;
					break;
				case 4:
					number = "one";
					numberInt = 1;
					break;
			}
			GameObject.Find ("Team Mode Menus").GetComponent<TeamModeMenuScript> ().gasingPerTeam = numberInt;
		}
		else {
			switch (index){
				case 1:
					number = "two";
					numberInt = 2;
					break;
				case 2:
					number = "two";
					numberInt = 2;
					break;
				case 3:
					number = "three";
					numberInt = 3;
					break;
				case 4:
					number = "four";
					numberInt = 4;
					break;
				case 5:
					number = "five";
					numberInt = 5;
					break;
				case 6:
					number = "six";
					numberInt = 6;
					break;
				case 7:
					number = "seven";
					numberInt = 7;
					break;
				case 8:
					number = "eight";
					numberInt = 8;
					break;
				case 9:
					number = "nine";
					numberInt = 9;
					break;
				case 10:
					number = "ten";
					numberInt = 10;
					break;
				case 11:
					number = "two";
					numberInt = 2;
					break;
			}
			GameObject.Find ("Royal Mode Menus").GetComponent<RoyalModeMenuScript> ().howManyGasing = numberInt;
		}
	}

	public void configurePref(){
		if (isTeamMode) {//team mode
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
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g + 40,renderer.material.color.b);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
