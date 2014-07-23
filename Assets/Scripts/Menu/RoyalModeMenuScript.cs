using UnityEngine;
using System.Collections;

public class RoyalModeMenuScript : MonoBehaviour {
	public GameObject textPrefab;
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
				textInstance = Instantiate (textPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
				textInstance.transform.position = new Vector3 (parent.transform.position.x, parent.transform.position.y - ((i + 1) * 2), parent.transform.position.z);
				textInstance.AddComponent<SelectGasingScript> ();	
				if (i > 0 ) {
					textInstance.GetComponent<SelectGasingScript> ().control = "AI";
					textInstance.GetComponent<SelectGasingScript> ().name = "arjuna";
					textInstance.name = "e" + i.ToString();
					textInstance.GetComponent<SelectGasingScript>().configurePref();
				} else {
					textInstance.GetComponent<SelectGasingScript> ().control = "P";
					textInstance.GetComponent<SelectGasingScript> ().name = "arjuna";
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
			GameObject parent = GameObject.Find ("Royal Arena");
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
	
	public void setRoyalModeContent(){
		Debug.Log ("how many gasing:" + howManyGasing);
		deleteAllObjectByName (textPrefab.name + "(Clone)");
		setContent (howManyGasing);
		setArenaContent ();
		isSet = true;
	}
}
