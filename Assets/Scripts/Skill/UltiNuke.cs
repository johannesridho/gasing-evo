using UnityEngine;
using System.Collections;

public class UltiNuke : Skill {
	
	private Gasing gasing;
	public GameObject[] targetEnemies;
	private Object prefabMissile;
	
	void Awake(){
		skillName = "Nuke";
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
						if (targetEnemySC) {
							targetEnemyGasing.EPKurang(targetEnemyGasing.energiPoint*0.333f);
							targetEnemySC.applyStatus("StatusStun", 3);
							Vector3 heading = targetEnemyGasing.rigidbody.position - gasing.rigidbody.position;
							Vector3 direction = heading / heading.magnitude;
							Vector3 mdirXZ = new Vector3(direction.x , 0, direction.z);
							targetEnemySC.rigidbody.AddForce(mdirXZ * 4000);
						}
					}	
				}
			}
		}
	}
}