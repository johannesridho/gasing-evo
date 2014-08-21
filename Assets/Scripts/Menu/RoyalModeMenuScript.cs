using UnityEngine;
using System.Collections;

public class RoyalModeMenuScript : MonoBehaviour {
	public GameObject selectGasingPrefab;
	public GameObject selectArenaPrefab;
	private GameObject textInstance;
	public int howManyGasing;
	public bool isSet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setContent(int amount){
		if (!isSet) {
			GameObject parent = GameObject.Find ("All Gasing");
			for (int i = 0; i < amount; i++) {
				textInstance = Instantiate (selectGasingPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
				textInstance.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
				Vector3 newPos; 
				if(i < 5){
					newPos = i == 0 ? new Vector3(parent.transform.position.x , parent.transform.position.y - 4, parent.transform.position.z - 1) : new Vector3(parent.transform.position.x , parent.transform.position.y - 4, GameObject.Find("p1").transform.position.z - (i * 2));
				} else{
					newPos = i == 5 ? new Vector3(parent.transform.position.x , parent.transform.position.y - 6, parent.transform.position.z - 1) : new Vector3(parent.transform.position.x , parent.transform.position.y - 6, GameObject.Find("p1").transform.position.z - ((i - 5) * 2));
				}
				textInstance.transform.position = newPos;
				if (i > 0 ) {
					textInstance.GetComponent<SelectGasingScript> ().control = "AI";
					textInstance.GetComponent<SelectGasingScript> ().name = "Craseed";
					textInstance.name = "e" + i.ToString();
				} else {
					textInstance.GetComponent<SelectGasingScript> ().control = "P";
					textInstance.GetComponent<SelectGasingScript> ().name = "Craseed";
					textInstance.name = "p1";
				}
				textInstance.GetComponent<SelectGasingScript>().configurePref();
			}
		} else {
			//do nothing		
		}
	}

	void setArenaContent(){
		//check if team mode content is set. if not set, set content
		if (!isSet) {
			GameObject parent = GameObject.Find ("Royal Arena");
			textInstance = Instantiate (selectArenaPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
			textInstance.transform.position = new Vector3 (parent.transform.position.x, parent.transform.position.y - 4, parent.transform.position.z - 2);
			textInstance.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
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
	
	public void setRoyalModeContent(){
		Debug.Log ("how many gasing:" + howManyGasing);
		GameObject[] texts = GameObject.FindGameObjectsWithTag ("GasingSelection");
		foreach (GameObject text in texts) {
			if (text.name != "numbers")
				DestroyObject(text);	
		}
		setContent (howManyGasing);
		setArenaContent ();
		isSet = true;
	}
}
