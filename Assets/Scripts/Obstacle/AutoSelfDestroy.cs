using UnityEngine;
using System.Collections;

public class AutoSelfDestroy : MonoBehaviour {

	private float clock;

	void Awake() {
		clock = 0f;	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		clock += Time.deltaTime;
		if (clock >= 2) {
			Destroy(this.gameObject);
		}
	}
}
