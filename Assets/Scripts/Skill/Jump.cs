using UnityEngine;
using System.Collections;

public class Jump : Skill {
	
	public float jump_coef;
	public Gasing gasing;
    //public Texture2D buttonSkill1;
    //private float skillPointNeeded;		//skill point yg diperlukan

	void Awake(){
        skillName = "Jump";
		skillPointNeeded = 10;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		jump_coef = 15000f;
//		buttonSkill1 = (Texture2D)Resources.Load("HUD_health_04.psd");
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

	void OnGUI () {
        //GUIStyle style = new GUIStyle (GUI.skin.box);
        //style.normal.background = buttonSkill1;

        //if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), "Jump", style) && gasing.getSP()>skillPointNeeded) {
        //    doSkill();				
        //}
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
