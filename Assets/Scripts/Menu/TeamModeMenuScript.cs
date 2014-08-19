using UnityEngine;
using System.Collections;

public class TeamModeMenuScript : MonoBehaviour {
	public GameObject textPrefab;
	private GameObject textInstance;
	public int gasingPerTeam;
	public bool isSet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setTeamContent(string teamName, int amount){
		//check if team mode content is set. if not set, set content
		if (!isSet) {
						GameObject parent = GameObject.Find (teamName);
						for (int i = 0; i < amount; i++) {
								textInstance = Instantiate (textPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
								textInstance.transform.position = new Vector3 (parent.transform.position.x, parent.transform.position.y - ((i + 1) * 2), parent.transform.position.z);
								textInstance.AddComponent<SelectGasingScript> ();
								if (i > 0 || i >= 0 && teamName != "Team A") {
										textInstance.GetComponent<SelectGasingScript> ().control = "AI";
										textInstance.GetComponent<SelectGasingScript> ().name = "Craseed";
										if(teamName == "Team A"){
											textInstance.name = "a" + i.ToString();
										}else{
											textInstance.name = "e" + (i+1).ToString();
										}
										textInstance.GetComponent<SelectGasingScript>().configurePref();
								} else {
										textInstance.GetComponent<SelectGasingScript> ().control = "P";
										textInstance.GetComponent<SelectGasingScript> ().name = "Craseed";
										textInstance.name = "p1";
										textInstance.GetComponent<SelectGasingScript>().configurePref();
								}
						}
				} else {
				  //do nothing		
		}
	}

	void setArenaContent(){
		//check if team mode content is set. if not set, set content
		if (!isSet) {
						GameObject parent = GameObject.Find ("Team Arena");
						textInstance = Instantiate (textPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
						textInstance.transform.position = new Vector3 (parent.transform.position.x, parent.transform.position.y - (2), parent.transform.position.z);
						textInstance.AddComponent<SelectArenaScript> ();
						textInstance.GetComponent<SelectArenaScript> ().name = "ice field";
						textInstance.name = "arena name";
						textInstance.GetComponent<SelectArenaScript>().configurePref();
				} else {
				  //do nothing		
		}
	}

	void deleteAllObjectByName(string name){
		GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject o in objects) {
			if(o.name == name){
				DestroyObject(o);	
			}	
		}
	}

	public void setTeamModeContent(){
		Debug.Log ("gasing per team:" + gasingPerTeam);
		GameObject[] texts = GameObject.FindGameObjectsWithTag ("GasingSelection");
		foreach (GameObject text in texts) {
			DestroyObject(text);		
		}
		setTeamContent ("Team A", gasingPerTeam);
		setTeamContent ("Team B", gasingPerTeam);
		setArenaContent ();
		GameObject.Find ("Main Camera").GetComponent<GasingNumberSetter>().setGasingNumbers();
		isSet = true;
	}
}
