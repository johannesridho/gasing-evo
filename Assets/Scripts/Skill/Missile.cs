﻿using UnityEngine;
using System.Collections;

public class Missile : Skill {

	private Gasing gasing;
	private GameObject targetEnemy;
	public GameObject prefabMissile;
	
	void Awake(){
        skillName = "Missile";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();

		targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	void Start () {
		
	}
	
	void Update () {
		if (!targetEnemy) {
			targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
		}
	}

    public override void doSkill()
    {
        if (gasing.getSP() > skillPointNeeded)
        {
            if (targetEnemy)
            {
                Instantiate(prefabMissile, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.LookRotation(targetEnemy.transform.position - transform.position - new Vector3(0, -90, 0)));
                //				Instantiate(prefabMissile, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.Euler(0, 0, 0));
                gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
            }
        }
    }
}
