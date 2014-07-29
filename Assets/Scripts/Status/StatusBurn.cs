using UnityEngine;
using System.Collections;

public class StatusBurn : Status {

	private GameObject efekLedakan;

	void Start () {
		timeRepeat = 0.75f;
		efekLedakan = (GameObject) Resources.Load("Effect/Detonator-Simple");
	}

	public override void efek () {
		gasing.EPKurang(25f);
		Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
	}
}
