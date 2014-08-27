using UnityEngine;
using System.Collections;

public class UltiBlizzard : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "BLIZZARD";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
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

		if (gasing.getSP() > skillPointNeeded) {
			if (targetEnemies.Length > 0) {
				if (GamePrefs.isMultiplayer) {
					
				} else {
					foreach (GameObject targetEnemy in targetEnemies) {
						StatusController targetEnemySC = targetEnemy.GetComponent<StatusController>();
						Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
						Instantiate ((GameObject) Resources.Load("Prefab/Prefab Obstacle/Holy Blast"), targetEnemyGasing.transform.position, Quaternion.Euler (0, 0, 0));
						if (targetEnemySC) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.333f);
							targetEnemySC.applyStatus("StatusFreeze", 10);
						}
					}
				}
				base.doSkill();
			}
		}
	}
}