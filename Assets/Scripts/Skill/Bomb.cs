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
		base.Start();
	}
	
	void Update () {
		base.Update();
	}

    public override void doSkill() {
        if (gasing.getSP() > skillPointNeeded) {
            if (GamePrefs.isMultiplayer) {
				GameObject bom = (GameObject) Network.Instantiate(prefabBomb, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.Euler(0, 0, 0),11);
				bom.GetComponent<BombScript>().nyalakan(this.gameObject, findNearestEnemy());
            }
            else {
				GameObject bom = (GameObject) Instantiate(prefabBomb, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.Euler(0, 0, 0));
				bom.GetComponent<BombScript>().nyalakan(this.gameObject, findNearestEnemy());
            }
			gasing.SPKurang(skillPointNeeded);	
			base.doSkill();	
        }
    }
}//end class
