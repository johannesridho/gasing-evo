using UnityEngine;
using System.Collections;

public abstract class Status : MonoBehaviour {
	
	protected Gasing gasing;	
	protected float counter;
	protected float timeRepeat;
	protected GameObject efekLedakan;

	protected void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}

	protected void Start () {
		timeRepeat = 0.5f;
	}

	protected void Update () {
		counter += Time.deltaTime;
		if (counter >= timeRepeat) {
			aksi ();
			counter = 0f;
		}	
	}
	
	protected void aksi () {
		// dipanggil tiap timeRepeat
	}
}
