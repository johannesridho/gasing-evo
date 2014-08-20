using UnityEngine;
using System.Collections;

public class Jump : Skill {

	private Gasing gasing;
	private float cooldown;

	void Awake(){
		cooldown = 2f;
        skillName = "Jump";
		skillPointNeeded = 10;
		if(!gasing)
			gasing = GetComponent<Gasing>();	
	}

	void Start () {
        
	}

	void Update () {
		cooldown += Time.deltaTime;
	}

	void FixedUpdate () {
        //float jump = 0f;		
        //if (gasing.isOnGround) {
        //    jump = (Input.GetKeyDown("space")) ? 1f : jump;
        //    if (Input.GetKeyDown("space"))
        //        gasing.isOnGround = false;
        //}		
        //Vector3 movement = new Vector3(0f, jump, 0f);
        //rigidbody.AddForce(movement * jump_coef * Time.deltaTime);	
        if (Input.GetKeyDown("space"))
        {
            doSkill();
        }
	}


    public override void doSkill()
    {
        if (gasing.getSP() > skillPointNeeded)
        {
            Debug.Log("do Skill");
//            if (gasing.isOnGround)
			if (cooldown >= 1.5)
            {
                float jump_coef = 15000f;
//                Vector3 movement = new Vector3(0f, Physics.gravity.y / (-1), 0f);
				Vector3 movement = new Vector3(0f, 15f, 0f);
                rigidbody.AddForce(movement * jump_coef * Time.deltaTime);
                gasing.isOnGround = false;
                gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing
				cooldown = 0;
            }
        }
    }
}
