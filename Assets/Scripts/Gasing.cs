using UnityEngine;
using System.Collections;

public class Gasing : MonoBehaviour {

	public float energiPointMax;
	public float skillPointMax;
	private Gasing gasing;

	public static float COEF_POWER = 0.3f;
	public static float COEF_SPIN = 500f;
	public static float COEF_SPEED = 2;
	public static float GLOBAL_speedMax = 100f;

	//ntar di-private
	//private float energiPoint;
	//private float skillPoint;
	public float energiPoint;
	public float skillPoint;
	public float mass;
	public float power;
	public float speed;
	public float speedMax;

	// state
	public bool isOnGround;
    public bool isPlayerAlive = true;

	public AudioClip audioPutaran;
	private AudioClip audioTabrakTembok;
	private AudioClip audioTabrakan1;
	private AudioClip audioTabrakan2;
	private AudioClip audioTabrakan3;
	private AudioClip audioTabrakan4;

	protected bool paused = false;

	void Awake(){
		if(!gasing)
			gasing = GetComponent<Gasing>();	
	}

	void Start () {	
		audioTabrakTembok = (AudioClip) Resources.Load("Audio/gasing_mental1");
		audioTabrakan1 = (AudioClip) Resources.Load("Audio/gasing_tabrakan");
		audioTabrakan2 = (AudioClip) Resources.Load("Audio/gasing_tabrakan2");
		audioTabrakan3 = (AudioClip) Resources.Load("Audio/gasing_tabrakan3");
		audioTabrakan4 = (AudioClip) Resources.Load("Audio/pedang_slash");

		energiPoint = energiPointMax;
		skillPoint = skillPointMax;


		//epmax, spmax, mass, power, speed diset di controller
//		mass = 100;
//		power = 1f;
		speed = 1f;
		speedMax = 20f;

		if(audioPutaran){
//			audio.loop = true;
//			audio.clip = audioPutaran;
//			audio.Play();
		}
	}

	void Update () {
		if (!paused) {
			EPKurang(0.05f);		//kurangi EP tiap detik
			SPTambah (0.01f);		//tambah SP tiap detik
			if(energiPoint <=0 || transform.position.y <= (-4)){
				//gasing berhenti
				//Debug.Log("wah");
	            if (GamePrefs.isMultiplayer)
	            {
	                //Debug.Log("gasing.dead");
	                isPlayerAlive = false;
	                energiPoint = 0;
	                gameObject.SetActive(false);
	            }
	            else
	            {
	                isPlayerAlive = false;
	                energiPoint = 0;
	                Destroy(transform.root.gameObject);
	            }
			} else if (energiPoint <= 2) {
				removeConstraint ();
				Vector3 rotation = new Vector3 (0f, COEF_SPIN*Time.deltaTime, 0f);
				transform.Rotate(rotation);
			}
		}
	}

	void FixedUpdate () {	
		if (!paused) {
			if (energiPoint > 0) {
				spin ();
				this.rigidbody.rotation.Set (0f, this.rigidbody.rotation.y, 0f, 0f);
			}
			Vector3 vel = gasing.rigidbody.velocity;
			float vec = Mathf.Sqrt(Mathf.Pow(vel.x,2) + Mathf.Pow(vel.y,2) + Mathf.Pow(vel.z,2));
			vel = (vec > speedMax) ? vel * speedMax/vec : vel;
			vec = Mathf.Sqrt(Mathf.Pow(vel.x,2) + Mathf.Pow(vel.y,2) + Mathf.Pow(vel.z,2));
			vel = (vec > GLOBAL_speedMax) ? vel * GLOBAL_speedMax/vec : vel;
			gasing.rigidbody.velocity = vel;

			rigidbody.AddForce(2 * Physics.gravity * rigidbody.mass); 	//gasing terpengaruh gravity 2 kali lebih besar dari objek lain
		}
	}
	
	public void EPKurang(float dmg){
		energiPoint = ((energiPoint - dmg) > 0) ? energiPoint - dmg : 0 ;
	}
	
	public void EPTambah(float n){
		energiPoint = ((energiPoint + n) < energiPointMax) ? energiPoint + n : energiPointMax;
	}
	
	public void SPKurang(float dmg){
		skillPoint = ((skillPoint - dmg) > 0) ? skillPoint - dmg : 0 ;
	}
	
	public void SPTambah(float n){
		skillPoint = ((skillPoint + n) < skillPointMax) ? skillPoint + n : skillPointMax;
	}
	
	public void speedChange(Vector3 n){
		gasing.rigidbody.velocity = n;
	}
	
	public void dorong(Vector3 n){
		gasing.rigidbody.AddForce(n);
	}
	
	public void speedMaxChange(float n){
		speedMax = n;
	}
	void movePosition(Vector3 v){
//        Debug.Log("move position");
        gasing.rigidbody.MovePosition(v);
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			if(audioTabrakan1 && audioTabrakan2 && audioTabrakan3 && audioTabrakan4){
				int n = Random.Range(1,5);

				switch(n){
					case 1:
						audio.PlayOneShot(audioTabrakan1);	
						break;
					case 2:
						audio.PlayOneShot(audioTabrakan2);	
						break;
					case 3:
						audio.PlayOneShot(audioTabrakan3);	
						break;
					case 4:
						audio.PlayOneShot(audioTabrakan4);	
						break;
					default:
						audio.PlayOneShot(audioTabrakan1);	
						break;
				}
			}
		} else if(col.gameObject.tag == "Obstacle"){
			this.EPKurang(5f);
			if(audioTabrakTembok){
				audio.PlayOneShot(audioTabrakTembok);
			}
		} else if(col.gameObject.tag == "Explosive Obstacle"){
			this.EPKurang(30f);
            if (GamePrefs.isMultiplayer)
            {
                Network.Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }
		}
		else if (col.gameObject.tag == "Tanah") {
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

	void setEP(float ep){
		energiPoint = ep;
	}

	void setSP(float sp){
		skillPoint = sp;
	}

	public float getEP(){
		return energiPoint;
	}

	public float getSP(){
		return skillPoint;
	}

	public float getMass(){
		return mass;
	}
	public float getSpeed(){
		return speed;
	}
	public float getPower(){
		return power;
	}
	public void setMass(float f){
		mass = f;
	}
	public void setPower(float f){
		power = f;
	}
	public void setSpeed(float f){
		speed = f;
	}

	protected void spin() {
		Vector3 rotation = new Vector3 (0f, 0f, COEF_SPIN*Time.deltaTime);
		transform.Rotate(rotation);
	}

	public void removeConstraint() {
		rigidbody.constraints = RigidbodyConstraints.None;
	}

	protected void OnPauseGame ()
	{
		paused = true;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		Debug.Log("paused");
	}
	 
	protected void OnResumeGame ()
	{
		paused = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

}//end class