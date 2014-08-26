using UnityEngine;
using System.Collections;

public class UltiGravity : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "Gravity";
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
						Debug.Log(targetEnemy.GetComponent<StatusController>());
						if (targetEnemySC) {
							targetEnemySC.applyStatus("StatusStun", 5);
						}
					}	
				}
			}
		}
	}
}
