using UnityEngine;
using System.Collections;

public class UltiNuke : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "NUCLEAR";
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
                foreach (GameObject targetEnemy in targetEnemies) {
                    StatusController targetEnemySC = targetEnemy.GetComponent<StatusController>();
                    Gasing targetEnemyGasing = targetEnemy.GetComponent<Gasing>();
                    PhysicsTabrak targetEnemyPT = targetEnemy.GetComponent<PhysicsTabrak>();
                    if (targetEnemySC) {
						targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint * 0.333f);
                        if (GamePrefs.isMultiplayer)
                        {
                            Network.Instantiate((GameObject)Resources.Load("Effect/Detonator-Simple"), targetEnemyGasing.transform.position, Quaternion.Euler(0, 0, 0), 20);
                        }
                        else
                        {
                            Instantiate((GameObject)Resources.Load("Effect/Detonator-Simple"), targetEnemyGasing.transform.position, Quaternion.Euler(0, 0, 0));
                        }
                        targetEnemySC.applyStatus("StatusStun", 2.5f);
                        Vector3 heading = targetEnemyGasing.rigidbody.position - gasing.rigidbody.position;
                        Vector3 direction = heading / heading.magnitude;
                        Vector3 mdirXZ = new Vector3(direction.x, 0, direction.z);
                        targetEnemySC.rigidbody.AddForce(mdirXZ * 4000);
                        targetEnemyPT.isInvicibleAfterClash = true;
                        targetEnemyPT.timeCountAfterClash = 0f;
                        targetEnemyPT.geserForce = 600 * mdirXZ;
                    }
				}	
			}
			base.doSkill();
		}
	}
}