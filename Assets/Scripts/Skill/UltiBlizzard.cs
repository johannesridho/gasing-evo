﻿using UnityEngine;
using System.Collections;

public class UltiBlizzard : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "Blizzard";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Start () {
		
	}
	
	void Update () {
		
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
						Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
						Debug.Log(targetEnemy.GetComponent<StatusController>());
						if (targetEnemySC) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.333f);
							targetEnemySC.applyStatus("StatusFreeze", 10);
						}
					}
				}
			}
		}
	}
}