using UnityEngine;
using System.Collections;

public class UltiGravity : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "GRAVITY";
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
	
	public override void doSkill() {
        if (GamePrefs.isMultiplayer) {
            targetEnemies = mp_findAllTarget().ToArray();
        } else {
            targetEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

		if (gasing.getSP() > skillPointNeeded) {
			if (targetEnemies.Length > 0) {
				if (GamePrefs.isMultiplayer) {
                    foreach (GameObject targetEnemy in targetEnemies)
                    {
                        StatusController targetEnemySC = targetEnemy.GetComponent<StatusController>();
                        Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
                        Network.Instantiate((GameObject)Resources.Load("Effect/Detonator-Simple"), targetEnemyGasing.transform.position, Quaternion.Euler(0, 0, 0),20);
                        if (targetEnemySC)
                        {
                            targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint * 0.25f);
                            targetEnemySC.applyStatus("StatusStun", 5);
                        }
                    }	
				} else {
					foreach (GameObject targetEnemy in targetEnemies) {
						StatusController targetEnemySC = targetEnemy.GetComponent<StatusController>();
						Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
						Instantiate ((GameObject) Resources.Load("Effect/Detonator-Simple"), targetEnemyGasing.transform.position, Quaternion.Euler (0, 0, 0));
						if (targetEnemySC) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.25f);
							targetEnemySC.applyStatus("StatusStun", 5);
						}
					}	
				}
				base.doSkill();
			}
		}
	}
}
