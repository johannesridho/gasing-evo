using UnityEngine;
using System.Collections;

public class StatusBurn : Status {
	
	public GameObject efekLedakan;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter >= 0.75f) {
			gasing.EPKurang(15f);		//kurangi EP tiap detik
			counter = 0f;
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
		}	
	}
}
