﻿using UnityEngine;
using System.Collections;

public class UltiEmptySoul : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "Empty Soul";
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
						Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
						if (targetEnemyGasing) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.5f);
						}
					}
				}
			}
		}
	}
}
