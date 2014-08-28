using UnityEngine;
using System.Collections;

public class UltiEmptySoul : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "EMPTY SOUL";
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
                        Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
                        Network.Instantiate((GameObject)Resources.Load("Effect/Detonator-Simple"), targetEnemyGasing.transform.position, Quaternion.Euler(0, 0, 0),20);
                        if (targetEnemyGasing)
                        {
                            targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint * 0.5f);
                        }
                    }
				} else {
					foreach (GameObject targetEnemy in targetEnemies) {
						Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
						Instantiate ((GameObject) Resources.Load("Effect/Detonator-Simple"), targetEnemyGasing.transform.position, Quaternion.Euler (0, 0, 0));
						if (targetEnemyGasing) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.5f);
						}
					}
				}
				base.doSkill();
			}
		}
	}
}
