using UnityEngine;
using System.Collections;

public class Bomb : Skill {
	
	private Gasing gasing;
    //public Texture2D buttonSkill1;
    //private float skillPointNeeded;		//skill point yg diperlukan
	public GameObject prefabBomb;
	
	void Awake(){
        skillName = "Bomb";
		skillPointNeeded = 20;
		if(!gasing)
			gasing = GetComponent<Gasing>();
	}
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	void OnGUI () {
        //GUIStyle style = new GUIStyle (GUI.skin.box);
        //style.normal.background = buttonSkill1;
		
        //if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), "Fireball", style) && gasing.getSP()>skillPointNeeded) {
        //    doSkill();
        //}
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
