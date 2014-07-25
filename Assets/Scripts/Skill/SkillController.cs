using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour {
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Skill skillUlti;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (skill1 != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skill1.buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 4 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skill1.skillName, style))
            {
                skill1.doSkill();
            }
        }

        if (skill2 != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skill2.buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 4 / 5, Screen.height * 5 / 10, Screen.width / 7, Screen.height / 8), skill2.skillName, style))
            {
                skill2.doSkill();
            }
        }

        if (skill3 != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skill3.buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 4 / 5, Screen.height * 3 / 10, Screen.width / 7, Screen.height / 8), skill3.skillName, style))
            {
                skill3.doSkill();
            }
        }

        if (skillUlti != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skillUlti.buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skillUlti.skillName, style))
            {
                skillUlti.doSkill();
            }
        }
    }
}
