using UnityEngine;
using System.Collections;

public class Cyclone : Skill {

	private Gasing gasing;
	private Object prefab;
	private GameObject targetEnemy;
	private Gasing gasingEnemy;
	
	void Awake(){
		skillName = "Cyclone";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		prefab = Resources.Load("Prefab/Prefab Obstacle/Cyclone");
	}
	
	void Start () {

	}
	
	void Update () {

	}
	
	public override void doSkill()
	{
		Application.LoadLevelAdditive("ArjunaUltimate");
		Debug.Log("succeed");
		/*if (gasing.getSP() > skillPointNeeded)
		{
			targetEnemy = findNearestEnemy();		//cari musuh terdekat
			if (targetEnemy) {
				gasingEnemy = targetEnemy.GetComponent<Gasing> ();
                if (GamePrefs.isMultiplayer)
                {
                    Network.Instantiate(prefab, targetEnemy.transform.position, Quaternion.Euler(0, 0, 0),12);
                }
                else
                {
                    Instantiate(prefab, targetEnemy.transform.position, Quaternion.Euler(0, 0, 0));
                }
				gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
			}
		}*/
	}
}
