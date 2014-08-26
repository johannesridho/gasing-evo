using UnityEngine;
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

	}
	
	public override void doSkill()
	{
        if (GamePrefs.isMultiplayer)
        {
            targetEnemies = mp_findAllTarget().ToArray() ;
        }
        else
        {
            targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
		if (gasing.getSP() > skillPointNeeded)
		{
			if (targetEnemies.Length > 0)
			{
                foreach (GameObject targetEnemy in targetEnemies)
                {
                    StatusController targetEnemySC = targetEnemy.GetComponent<StatusController>();
                    Debug.Log(targetEnemy.GetComponent<StatusController>());
                    if (targetEnemySC)
                    {
                        targetEnemySC.applyStatus("StatusBurn", 10);
                    }
                }
			}
		}
	}
}
