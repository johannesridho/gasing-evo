using UnityEngine;
using System.Collections;

public class StatusBurn : Status {

	void Start () {
		timeRepeat = 0.75f;
	}

	void Update () {
		counter += Time.deltaTime;
		if (counter >= timeRepeat) {
			aksi ();
			counter = 0f;
		}	
	}

	void aksi () {
		gasing.EPKurang(25f);
		Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
	}
}
