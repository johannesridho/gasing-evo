using UnityEngine;
using System.Collections;

public class Cyclone : Skill {

	private Gasing gasing;
	private Object prefab;
	private GameObject targetEnemy;
	private Gasing gasingEnemy;

	public string targetEnemyName;
	
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
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
			if (go.GetComponent<HealthBar>())
			go.GetComponent<HealthBar>().isAvailable = false;
		}
		targetEnemy = findNearestEnemy();

		GetComponent<SkillController>().isAvailable = false;

		Application.LoadLevelAdditive("ArjunaUltimate");

		Debug.Log("targetEnemyName = "+targetEnemy.transform.name);
		if (targetEnemy) {
			Utilities.ultiTarget = targetEnemy.transform.name.Substring(0,targetEnemy.transform.name.IndexOf("_"));

		}

		Debug.Log(Utilities.ultiTarget);

		
		

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
