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
        if (GamePrefs.isMultiplayer)
        {
            targetEnemies = mp_findAllTarget().ToArray();
        }
        else
        {
            targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

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
						if (targetEnemySC) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.333f);
							targetEnemySC.applyStatus("StatusStun", 5);
						}
					}	
				}
			}
		}
	}
}
