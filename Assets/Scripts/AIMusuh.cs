using UnityEngine;
using System.Collections;

public class AIMusuh : MonoBehaviour {
	private Gasing gasing;
	private PhysicsTabrak gasing_pt;
	private SkillController skill_con;
	private float speedAI;
	public float timeGerak;
	public float timeSkill;
	public float time;

	void Awake(){
		if(!gasing){
			gasing = GetComponent<Gasing>();
		}
		if(!gasing_pt){
			gasing_pt = GetComponent<PhysicsTabrak>();
		}
		if(!skill_con){
			skill_con = GetComponent<SkillController>();
		}
		speedAI = 6000;
		time = 0f;
		timeGerak = 0f;
		timeSkill = 0f;
	}

	void Start () {

	}

	void Update () {

	}

	void FixedUpdate () {
		updateGerakan();
		updateSkill();
	}
	
	void updateGerakan () {
		timeGerak += Time.deltaTime;
		if (timeGerak >= 0.05) {
			gerak();
			timeGerak = 0f;
		}
	}
	
	void updateSkill () {
		timeSkill += Time.deltaTime;
		if (timeSkill >= 0.5) {
			skill();
			timeSkill = 0f;
		}
	}

	void skill() {
		if (Random.Range(0,100) < 7) {
			skill_con.skills[1].doSkill();
		}
		if (Random.Range(0,100) < 10) {
			skill_con.skills[0].doSkill();
		}
	}

	void gerak() {
		if (!gasing_pt.isInvicibleAfterClash) {
			Vector3 movement;
			if (Random.Range(0,100) < 75) {
				GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
				Vector3 heading = player.rigidbody.position - rigidbody.position;
				Vector3 direction = heading / heading.magnitude;
				movement = direction * Gasing.COEF_SPEED * gasing.speed;
			} else {
				float hor = Random.Range(-5.0F, 5.0F) * Gasing.COEF_SPEED * gasing.speed;
				float ver = Random.Range(-5.0F, 5.0F) * Gasing.COEF_SPEED * gasing.speed;
				movement = new Vector3 (hor,0.0f,ver);
			}
			rigidbody.AddForce ( movement * speedAI * Time.deltaTime);	
		}
	}

}//end class
