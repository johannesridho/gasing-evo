using UnityEngine;
using System.Collections;

public class GasingNumberSetter : MonoBehaviour {
	public GameObject textPrefab;
	private GameObject textInstance;

	// Use this for initialization
	void Start () {
		
	}

	void Awake() {
		setGasingNumbers ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setGasingNumbers(){
		GameObject parent = GameObject.Find ("how many");
		textInstance = Instantiate (textPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.name = "numbers";
		textInstance.transform.position = new Vector3 (parent.transform.position.x + 4.5f, parent.transform.position.y - 1.7f, parent.transform.position.z);
		textInstance.AddComponent<SelectHowManyScript> ();
	}
}
