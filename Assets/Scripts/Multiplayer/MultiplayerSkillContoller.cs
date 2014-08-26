using UnityEngine;
using System.Collections;

public class MultiplayerSkillContoller : MonoBehaviour
{
    public string[] availableSkills = new string[4] { "", "", "", "" };

    private bool isActive = false;

    public Texture2D[] skillButtons;

    public MultiplayerInputHandler mpInputHandler;

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
        if (MultiplayerManager.instance.isGameStarted)
        {
            if (Network.isServer)
            {
                initializeClientsSkills();
            }
        }
    }

    void OnGUI()
    {

        if (isActive)
        {
            GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
            if (MultiplayerManager.instance.isGameStarted)
            {
                if (MultiplayerManager.instance.isDedicatedServer && !Network.isServer && MultiplayerManager.instance.isGameStarted)
                {
                    //health
                    GUI.DrawTexture(new Rect(0, 0, 1280, 720), mpInputHandler.blackScreenTexture);
                    GUIStyle style = new GUIStyle(GUI.skin.box);
                    //		Texture2D texture = new Texture2D(1, 1);
                    //		texture.SetPixel (1, 1, Color.green);
                    //		texture.Apply ();
                    style.normal.background = mpInputHandler.teksturHealth;

                    GUIStyle style2 = new GUIStyle(GUI.skin.box);
                    //		Texture2D texture2 = new Texture2D(1, 1);
                    //		texture2.SetPixel (1, 1, Color.red);
                    //		texture2.Apply ();
                    style2.normal.background = mpInputHandler.teksturHealth2;

                    GUIStyle styleSkill = new GUIStyle(GUI.skin.box);
                    styleSkill.normal.background = mpInputHandler.teksturSkill;

                    GUIStyle styleSkill2 = new GUIStyle(GUI.skin.box);
                    styleSkill2.normal.background = mpInputHandler.teksturSkill2;

                    GUI.Box(new Rect(40, 114, 1200, 60), "", style2);
                    GUI.Box(new Rect(40, 114, mpInputHandler.healthBarLength, 60), "", style);

                    GUI.Box(new Rect(40, 186, 1200, 60), "", styleSkill2);
                    GUI.Box(new Rect(40, 186, mpInputHandler.skillBarLength, 60), "", styleSkill);
                }

                if ((!MultiplayerManager.instance.isDedicatedServer) || (Network.isClient && MultiplayerManager.instance.isDedicatedServer))
                {
                    if (availableSkills[0] != null)
                    {
                        GUIStyle style1 = new GUIStyle(GUI.skin.box);
                        style1.normal.background = getButton(availableSkills[0]);
                        //            if (GUI.Button(new Rect(Screen.width * 4 / 5 - Screen.width / 8 / 2, Screen.height * 7 / 10, Screen.width / 8, Screen.width / 8), skills[0].skillName, style))
                        if (GUI.Button(new Rect(855, 470, 200, 200), "", style1))
                        {
                            if (Network.isServer)
                            {
                                server_doSkill(Network.player, 0);
                            }
                            else
                            {
                                networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 0);
                            }
                        }
                    }

                    if (availableSkills[1] != null)
                    {
                        GUIStyle style1 = new GUIStyle(GUI.skin.box);
                        style1.normal.background = getButton(availableSkills[1]);
                        //            if (GUI.Button(new Rect(Screen.width * 9 / 10 - Screen.width / 8 / 2, Screen.height * 5 / 10, Screen.width / 8, Screen.width / 8), skills[1].skillName, style))
                        if (GUI.Button(new Rect(1027, 303, 200, 200), "", style1))
                        {
                            if (Network.isServer)
                            {
                                server_doSkill(Network.player, 1);
                            }
                            else
                            {
                                networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 1);
                            }
                        }
                    }

                    if (availableSkills[2] != null)
                    {
                        GUIStyle style1 = new GUIStyle(GUI.skin.box);
                        style1.normal.background = getButton(availableSkills[2]);
                        //            if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skills[2].skillName, style))
                        if (GUI.Button(new Rect(60, 370, 300, 300), "", style1))
                        {
                            if (Network.isServer)
                            {
                                server_doSkill(Network.player, 2);
                            }
                            else
                            {
                                networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 2);
                            }
                        }
                    }
                }
            }
        }

    }

    private void initializeClientsSkills()
    {
        for (int i = 0; i < MultiplayerManager.instance.serverSideGasings.Count; i++)
        {
            if (MultiplayerManager.instance.serverSideGasings[i] != null)
            {
                if (MultiplayerManager.instance.playerList[i].playerNetwork == Network.player)
                {
                    //for server
                    if (MultiplayerManager.instance.serverSideGasings[i] != null)
                    {
                        if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>() != null)
                        {
                            for (int j = 0; j < MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length; j++)
                            {
                                if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j] != null)
                                {
                                    availableSkills[j] = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j].skillName;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (MultiplayerManager.instance.isAllPlayerReady)
                    {
                        //for other clients
                        string[] stringOfSkills = new string[MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length];
                        for (int j = 0; j < MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length; j++)
                        {
                            if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j] != null)
                            {
                                stringOfSkills[j] = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j].skillName;

                            }
                        }
                        networkView.RPC("client_sendAvailableSkills", MultiplayerManager.instance.playerList[i].playerNetwork, string.Join("-", stringOfSkills));
                    }
                }
            }
        }
    }

    [RPC]
    public void client_sendAvailableSkills(string stringOfSkills)
    {
        //stringOfSkill ( '-' separated)
        availableSkills = stringOfSkills.Split('-');
    }

    [RPC]
    public void server_doSkill(NetworkPlayer player, int skillIndex)
    {
        MultiplayerManager.instance.getGasingOwnedByPlayer(player).GetComponentInChildren<SkillController>().skills[skillIndex].doSkill();
    }

    public void setActive(bool active)
    {
        isActive = active;
    }

    private Texture2D getButton(string skillName)
    {
        Texture2D hasil = null;
        if (skillName == "Jump")
        {
            hasil = skillButtons[0];
        }
        else
        {
            hasil = skillButtons[1];
        }
        return hasil;
    }

    void OnLevelWasLoaded(int level)
    {
        isActive = true;
    }
}
