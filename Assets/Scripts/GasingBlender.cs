using UnityEngine;
using System.Collections;

public class GasingBlender : MonoBehaviour {

	public float energiPointMax;
	public float skillPointMax;
	public float mass;
	public GasingBlender gasing;

	public static float COEF_DMG = 0.1f;
	public static float COEF_MOMENTUM = 2f;
	private static float COEF_SPIN = 500f;

	//ntar di-private
	//private float energiPoint;
	//private float skillPoint;
	public float energiPoint;
	public float skillPoint;

	// state
	public bool isOnGround;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<GasingBlender>();
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
		EPKurang(0.1f);		//kurangi EP tiap detik



		if(energiPoint <=0 ){

			//gasing berhenti
			//Debug.Log("wah");
			//Destroy (transform.root.gameObject); 
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
			float damage = COEF_DMG * Mathf.Sqrt(Mathf.Pow(momentum.x,2) + Mathf.Pow(momentum.y,2) + Mathf.Pow(momentum.z,2));

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
