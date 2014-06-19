using UnityEngine;
using System.Collections;

public class AIMusuh : MonoBehaviour {
	public Gasing gasing;
	public float speed;

	void Awake(){
		if(!gasing){
			gasing = GetComponent<Gasing>();
			gasing.setEPMax(70);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		float hor = Random.Range(-10.0F, 10.0F);
		float ver = Random.Range(-10.0F, 10.0F);

		Vector3 movement = new Vector3 (hor,0.0f,ver);
		
		rigidbody.AddForce (movement * speed * Time.deltaTime);		
	}
}//end class
