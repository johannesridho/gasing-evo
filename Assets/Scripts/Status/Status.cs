using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {
	
	public Gasing gasing;	
	public float counter;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
