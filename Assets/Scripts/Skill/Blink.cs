using UnityEngine;
using System.Collections;

public class Blink : Skill {

	private Gasing gasing;
	
	void Awake(){
        skillName = "Blink";
		skillPointNeeded = 5;
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}
	
	void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
	}

    public override void doSkill()
    {
        if (gasing.getSP() > skillPointNeeded) {
            float x;
            float z;
            if (Application.platform == RuntimePlatform.Android) {
                x = transform.position.x + 50 * Input.acceleration.x;
                z = transform.position.z + 50 * Input.acceleration.y;
            } else {
                x = transform.position.x + 15 * Input.GetAxis("Horizontal");
                z = transform.position.z + 15 * Input.GetAxis("Vertical");
            }
            if (x < 20 && z < 20 && x > -20 && z > -20) {			//hardcode buat stonefield aja
                transform.position = new Vector3(x, 1f, z);
            }
			gasing.SPKurang(skillPointNeeded);
			base.doSkill();
        }
    }
}
