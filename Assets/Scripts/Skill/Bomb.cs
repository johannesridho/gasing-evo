using UnityEngine;
using System.Collections;

public class Bomb : Skill {
	
	private Gasing gasing;
	private Object prefabBomb;
	
	void Awake(){
        skillName = "Bomb";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
		prefabBomb = Resources.Load("Prefab/Prefab Obstacle/Bomb");
	}
	
	void Start () {
		
	}
	
	void Update () {
		
	}

    public override void doSkill()
    {
        if (gasing.getSP() > skillPointNeeded)
        {
            Instantiate(prefabBomb, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.Euler(0, 0, 0));

            gasing.SPKurang(skillPointNeeded);		//kurangi skillpoint gasing		
        }
    }
}//end class
