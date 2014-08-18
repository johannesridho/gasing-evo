using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    public Skill[] skills = new Skill[3];

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        if (GamePrefs.isMultiplayer)
        {
            OnGUI_MultiPlayer();
        }
        else
        {
            OnGUI_SinglePlayer();
        }


    }

    private void OnGUI_SinglePlayer()
    {
        if (skills[0] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[0].buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 4 / 5 - Screen.width / 8 / 2, Screen.height * 7 / 10, Screen.width / 8, Screen.width / 8), skills[0].skillName, style))
            {
                skills[0].doSkill();
            }
        }

        if (skills[1] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[1].buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 9 / 10 - Screen.width / 8 / 2, Screen.height * 5 / 10, Screen.width / 8, Screen.width / 8), skills[1].skillName, style))
            {
                skills[1].doSkill();
            }
        }

        //ULTI
        if (skills[2] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[2].buttonSkill1;
            if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skills[2].skillName, style))
            {
                skills[2].doSkill();
            }
        }
    }

    private void OnGUI_MultiPlayer()
    {

    }


}
