using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    public Skill[] skills = new Skill[3];

    #region untuk GUI
    private float guiRatioX;
    private float guiRatioY;
    private float sWidth;
    private float sHeight;
    private Vector3 GUIsF;
    private int sizegui;
    #endregion

    // Use this for initialization
    void Start()
    {
        //get the screen's width
        sWidth = Screen.width;
        sHeight = Screen.height;
        //calculate the rescale ratio
        guiRatioX = sWidth / 1280;
        guiRatioY = sHeight / 720;
        //create a rescale Vector3 with the above ratio
        GUIsF = new Vector3(guiRatioX, guiRatioY, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
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
//            if (GUI.Button(new Rect(Screen.width * 4 / 5 - Screen.width / 8 / 2, Screen.height * 7 / 10, Screen.width / 8, Screen.width / 8), skills[0].skillName, style))
            if (GUI.Button(new Rect(855, 470, 200, 200), "", style))
            {
                skills[0].doSkill();
            }
        }

        if (skills[1] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[1].buttonSkill1;
//            if (GUI.Button(new Rect(Screen.width * 9 / 10 - Screen.width / 8 / 2, Screen.height * 5 / 10, Screen.width / 8, Screen.width / 8), skills[1].skillName, style))
            if (GUI.Button(new Rect(1027, 303, 200, 200), "", style))
            {
                skills[1].doSkill();
            }
        }

        //ULTI
        if (skills[2] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[2].buttonSkill1;
//            if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skills[2].skillName, style))
            if (GUI.Button(new Rect(60, 370, 300, 300), "", style))
            {
                skills[2].doSkill();
            }
        }
    }

    private void OnGUI_MultiPlayer()
    {

    }


}
