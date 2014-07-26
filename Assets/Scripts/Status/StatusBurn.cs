using UnityEngine;
using System.Collections;

public class StatusBurn : Status {
	
	public GameObject efekLedakan;

	void Start () {
		timeRepeat = 0.75f;
	}

	public override void efek () {
		gasing.EPKurang(25f);
		Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
	}
}
