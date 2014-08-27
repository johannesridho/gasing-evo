using UnityEngine;
using System.Collections;

public class Jump : Skill {

	private Gasing gasing;
//	private float cooldown;
	private AudioClip audioJump;

	void Awake(){
//		cooldown = 2f;
        skillName = "Jump";
		skillPointNeeded = 5;
		if(!gasing)
			gasing = GetComponent<Gasing>();	
	}

	void Start () {
		base.Start();
		audioJump = (AudioClip) Resources.Load("Audio/gasing_mental1");
	}

	void Update () {
		base.Update();
	}

	void FixedUpdate () {
        if (Input.GetKeyDown("space"))
        {
            doSkill();
        }
	}


    public override void doSkill() {
        if (gasing.getSP() > skillPointNeeded) {            
            if (gasing.isOnGround) {
                float jump_coef = 15000f;
				Vector3 movement = new Vector3(0f, 15f, 0f);
				if(audioJump) audio.PlayOneShot(audioJump);
                rigidbody.AddForce(movement * jump_coef * Time.deltaTime);
                gasing.isOnGround = false;
                gasing.SPKurang(skillPointNeeded);
				base.doSkill();
            }
        }
    }
}
