using UnityEngine;
using System.Collections;

public class Meteor : Skill {

	private Gasing gasing;
	private Object prefab;
	private GameObject targetEnemy;
	private Gasing gasingEnemy;
	
	void Awake(){
		skillName = "Meteor";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		prefab = Resources.Load("Prefab/Prefab Obstacle/Meteor Storm");
	}
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public override void doSkill()
	{
		if (gasing.getSP() > skillPointNeeded)
		{
			targetEnemy = findNearestEnemy();		//cari musuh terdekat
			if (targetEnemy) {
				gasingEnemy = targetEnemy.GetComponent<Gasing> ();
                if (GamePrefs.isMultiplayer)
                {
                    Network.Instantiate(prefab, new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y + 15, targetEnemy.transform.position.z), Quaternion.Euler(0, 0, 0),12);
                }
                else
                {
                    Instantiate(prefab, new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y + 15, targetEnemy.transform.position.z), Quaternion.Euler(0, 0, 0));
                }
				gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
			}
		}
	}
}
