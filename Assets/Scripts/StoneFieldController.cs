using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour {

	public GameObject pemain;
	public GameObject musuh;
	public int jumlahMusuh;

	void Awake(){
//		jumlahMusuh = 3;
		if (!pemain) {
			pemain = GameObject.Find ("Pemain");
		}
		if (!musuh) {
			musuh = GameObject.Find ("Musuh");
			for (int i = 1; i < jumlahMusuh; i++) {
				Instantiate(musuh, new Vector3(i * 5, 1, 10), Quaternion.Euler(270,0,0));
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
