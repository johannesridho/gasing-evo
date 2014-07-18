using UnityEngine;
using System.Collections;

public class GasingNumberSetter : MonoBehaviour {
	public GameObject textPrefab;
	private GameObject textInstance;

	// Use this for initialization
	void Start () {
		setGasingNumbers ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setGasingNumbers(){
		GameObject parent = GameObject.Find ("how many");
		textInstance = Instantiate (textPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.transform.position = new Vector3 (parent.transform.position.x + 15.8f, parent.transform.position.y, parent.transform.position.z);
		textInstance.name = "numbers";
		textInstance.AddComponent<SelectHowManyScript> ();
	}
}
