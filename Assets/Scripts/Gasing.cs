using UnityEngine;
using System.Collections;

public class Gasing : MonoBehaviour {

	public float energiPointMax;
	public float skillPointMax;
	public float mass;
	public Gasing gasing;

	public static float COEF_POWER = 0.1f;
	public static float COEF_MOMENTUM = 1.2f;
	public static float COEF_SPIN = 500f;
	public static float COEF_SPEED_LIMIT = 25;
	public static float COEF_SPEED = 2;

	//ntar di-private
	//private float energiPoint;
	//private float skillPoint;
	public float energiPoint;
	public float skillPoint;

	// state
	public bool isOnGround;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();
		//		energiPointMax = 50;
		//		skillPointMax = 50;
	}

	// Use this for initialization
	void Start () {	
		energiPoint = energiPointMax;
		skillPoint = skillPointMax;
		isOnGround = true;
		mass = 100;
	}

	// Update is called once per frame
	void Update () {
		EPKurang(0.05f);		//kurangi EP tiap detik
		if(energiPoint <=0 ){
			//gasing berhenti
			//Debug.Log("wah");
			Destroy (transform.root.gameObject); 
			Application.LoadLevel("GameOver");
		} else if (energiPoint <= 2) {
			removeConstraint ();
			Vector3 rotation = new Vector3 (0f, COEF_SPIN*Time.deltaTime, 0f);
			transform.Rotate(rotation);
		}
	}

	//called every fixed framerate frame
	void FixedUpdate () {	
		if (energiPoint > 0) {
			spin ();
			this.rigidbody.rotation.Set (0f, this.rigidbody.rotation.y, 0f, 0f);
		}
		Vector3 vel = gasing.rigidbody.velocity;
		float vec = Mathf.Sqrt(Mathf.Pow(vel.x,2) + Mathf.Pow(vel.y,2) + Mathf.Pow(vel.z,2));
		gasing.rigidbody.velocity = (vec > COEF_SPEED_LIMIT) ? vel * COEF_SPEED_LIMIT/vec : vel;
	}

	void EPKurang(float dmg){
		if (energiPoint - dmg > 0) {
			energiPoint -= dmg;
		} else {
			energiPoint = 0;
		}
	}

	void EPTambah(float n){
		if (energiPoint + n < energiPointMax) {
			energiPoint += n;
		} else {
			energiPoint = energiPointMax;
		}
	}

	void velChange(Vector3 n){
		gasing.rigidbody.velocity = n;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			Vector3 vel = gasing.rigidbody.velocity;
			Vector3 momentum = COEF_MOMENTUM * vel;
			float damage = COEF_POWER * Mathf.Sqrt(Mathf.Pow(momentum.x,2) + Mathf.Pow(momentum.y,2) + Mathf.Pow(momentum.z,2));

			col.collider.SendMessage ("velChange", momentum, SendMessageOptions.DontRequireReceiver);
			col.collider.SendMessage ("EPKurang", damage, SendMessageOptions.DontRequireReceiver);

		} else if (col.gameObject.name == "Tanah") {
			this.isOnGround = true;
		}
	}

	public void setEPMax(float ep){
		energiPointMax = ep;
	}

	public void setSPMax(float sp){
		skillPointMax = sp;
	}

	public float getEPMax(){
		return energiPointMax;
	}

	public float getSPMax(){
		return skillPointMax;
	}

	public void setEP(float ep){
		energiPoint = ep;
	}

	public void setSP(float sp){
		skillPoint = sp;
	}

	public float getEP(){
		return energiPoint;
	}

	public float getSP(){
		return skillPoint;
	}

	protected void spin() {
		Vector3 rotation = new Vector3 (0f, 0f, COEF_SPIN*Time.deltaTime);
		transform.Rotate(rotation);
	}

	public void removeConstraint() {
		rigidbody.constraints = RigidbodyConstraints.None;
	}

}//end class
