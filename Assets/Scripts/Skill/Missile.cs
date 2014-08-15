using UnityEngine;
using System.Collections;

public class Missile : Skill {

	private Gasing gasing;
	public GameObject targetEnemy;
	private Object prefabMissile;
	
	void Awake(){
        skillName = "Missile";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		prefabMissile = Resources.Load("Prefab/Prefab Obstacle/Missile");
		targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	void Start () {
		
	}
	
	void Update () {
		if (!targetEnemy) {
            targetEnemy = findNearestEnemy();		//cari terus musuh terdekat
		}
	}

    public override void doSkill()
	{
		targetEnemy = findNearestEnemy();		//cari terus musuh terdekat
        if (gasing.getSP() > skillPointNeeded)
        {
            if (targetEnemy)
            {
                if (GamePrefs.isMultiplayer)
                {
                    Network.Instantiate(prefabMissile, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.LookRotation(targetEnemy.transform.position - transform.position - new Vector3(0, -90, 0)),11);
                }
                else
                {
                    GameObject misil = (GameObject) Instantiate(prefabMissile, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.LookRotation(targetEnemy.transform.position - transform.position - new Vector3(0, -90, 0)));
					misil.GetComponent<MissileScript>().nyalakan(targetEnemy.tag);
                    //				Instantiate(prefabMissile, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.Euler(0, 0, 0));
                }
                gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
            }
        }
    }
}
