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

        if (GamePrefs.isMultiplayer)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                if (go != this.gameObject)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
            }

            targetEnemy = closest;
            
            if (targetEnemy)
                gasingEnemy = targetEnemy.GetComponent<Gasing>();
        }
        else
        {
            targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
            if (targetEnemy)
                gasingEnemy = targetEnemy.GetComponent<Gasing>();
        }
	}
	
	void Start () {
		
	}
	
	void Update () {

        if (GamePrefs.isMultiplayer)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                if (go != this.gameObject)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
            }

            targetEnemy = closest;

            if (targetEnemy)
                gasingEnemy = targetEnemy.GetComponent<Gasing>();
        }
        else
        {
            if (!targetEnemy)
            {
                targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
                if (!gasingEnemy)
                {
                    gasingEnemy = targetEnemy.GetComponent<Gasing>();
                }
            }
        }
	}
	
	public override void doSkill()
	{
		if (gasing.getSP() > skillPointNeeded && targetEnemy)
		{
			Instantiate(prefab, targetEnemy.transform.position, Quaternion.Euler(270, 0, 0));			
			Instantiate(prefab, targetEnemy.transform.position, Quaternion.Euler(270, 0, 0));
			Instantiate(prefab, targetEnemy.transform.position, Quaternion.Euler(270, 0, 0));
			gasingEnemy.EPKurang(damageInflicted);
			gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
		}
	}
}
