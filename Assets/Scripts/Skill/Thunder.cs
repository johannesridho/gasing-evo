using UnityEngine;
using System.Collections;

public class Thunder : Skill {

	private Gasing gasing;
	private Object prefab;
	private GameObject targetEnemy;
	private Gasing gasingEnemy;
	
	void Awake(){
		skillName = "Thunder";
		skillPointNeeded = 20;
		damageInflicted = 25;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		prefab = Resources.Load("Prefab/Prefab Obstacle/Thunder");
	}
	
	void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
	}
	
	public override void doSkill()
	{
		if (gasing.getSP () > skillPointNeeded) {
			targetEnemy = findNearestEnemy ();		//cari terus musuh terdekat
			if (targetEnemy) {
				gasingEnemy = targetEnemy.GetComponent<Gasing> ();			

				if (GamePrefs.isMultiplayer) {
					Network.Instantiate (prefab, targetEnemy.transform.position, Quaternion.Euler (270, 0, 0), 11);
					Network.Instantiate (prefab, targetEnemy.transform.position, Quaternion.Euler (270, 0, 0), 11);
					Network.Instantiate (prefab, targetEnemy.transform.position, Quaternion.Euler (270, 0, 0), 11);
				} else {
					Instantiate (prefab, targetEnemy.transform.position, Quaternion.Euler (270, 0, 0));
					Instantiate (prefab, targetEnemy.transform.position, Quaternion.Euler (270, 0, 0));
					Instantiate (prefab, targetEnemy.transform.position, Quaternion.Euler (270, 0, 0));
				}

				gasingEnemy.EPKurang (damageInflicted);
				gasing.SPKurang (skillPointNeeded);		//kurangi skillpoint gasing		
				
				base.doSkill();
			}
		}
	}
}
