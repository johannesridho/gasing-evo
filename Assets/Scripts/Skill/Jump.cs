using UnityEngine;
using System.Collections;

public class Jump : Skill {

	private Gasing gasing;

	void Awake(){
        skillName = "Jump";
		skillPointNeeded = 10;
		if(!gasing)
			gasing = GetComponent<Gasing>();	
	}

	void Start () {
        
	}

	void Update () {
		
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
            if (gasing.isOnGround)
            {
                float jump_coef = 15000f;
                Vector3 movement = new Vector3(0f, Physics.gravity.y / (-3), 0f);
                rigidbody.AddForce(movement * jump_coef * Time.deltaTime);
                gasing.isOnGround = false;
                gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing
            }
        }
    }
}
