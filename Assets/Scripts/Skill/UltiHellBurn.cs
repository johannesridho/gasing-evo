﻿using UnityEngine;
using System.Collections;

public class UltiHellBurn : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "Hell Burn";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Start () {
		
	}
	
	void Update () {
//		if (!targetEnemy) {
//
//		}
	}
	
	public override void doSkill()
	{
		targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (gasing.getSP() > skillPointNeeded)
		{
			if (targetEnemies.Length > 0)
			{
				if (GamePrefs.isMultiplayer)
				{

				}
				else
				{
					foreach (GameObject targetEnemy in targetEnemies) {
						StatusController targetEnemySC = targetEnemy.GetComponent<StatusController>();
						Debug.Log(targetEnemy.GetComponent<StatusController>());
						if (targetEnemySC) {
							targetEnemySC.applyStatus("StatusBurn", 10);
						}
					}
//					while (targetEnemySC!=null) {
//						targetEnemySC = targetEnemy.GetComponent<StatusController>();
//					}
//					gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
				}
			}
		}
	}
}
