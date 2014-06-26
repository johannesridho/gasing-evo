using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	private static float spin_velo = 100f;

	void Awake(){
		init();
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//called every fixed framerate frame
	void FixedUpdate () {		
		spin();
	}
	
	void OnCollisionEnter(Collision col){
		destroyOnCollide(col);
	}
	
	protected void init() {

	}
	
	protected void spin() {
		Vector3 rotation = new Vector3 (0f, spin_velo*Time.deltaTime, 0f);
		transform.Rotate(rotation);
	}

	protected void destroyOnCollide(Collision col) {
		if ((col.gameObject.name == "Musuh")||(col.gameObject.name == "Pemain")||(col.gameObject.name == "Musuh(Clone)")) {
			Destroy(transform.root.gameObject); 
		}
	}


}