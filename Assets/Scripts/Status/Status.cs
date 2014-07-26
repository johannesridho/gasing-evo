using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {
	
	protected Gasing gasing;	
	protected float counter;
	protected float timeRepeat;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}

	void Start () {
		timeRepeat = 0.5f;
		aksi();
	}
	
	void Update () {
		updateCounter ();	
	}
	
	void updateCounter () {
		counter += Time.deltaTime;
		if (counter >= timeRepeat) {
			efek();
			counter = 0f;
		}	
	}

	/* silahkan dioverride */

	public virtual void aksi () {
		// dipanggil pertama kali
	}
	
	public virtual void efek () {
		// dipanggil berulang-ulang, tiap timeRepeat detik
	}
}
