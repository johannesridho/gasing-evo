using UnityEngine;
using System.Collections;

public class MultiplayerSkillContoller : MonoBehaviour {
    public string[] availableSkills = new string[4]{"","","",""};

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
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
        if (MultiplayerManager.instance.isGameStarted)
        {
            if (availableSkills[0] != null)
            {
                GUIStyle style = new GUIStyle(GUI.skin.box);
                //style.normal.background = skills[0].buttonSkill1;
                if (GUI.Button(new Rect(Screen.width * 4 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), availableSkills[0], style))
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
                GUIStyle style = new GUIStyle(GUI.skin.box);
                //style.normal.background = skills[1].buttonSkill1;
                if (GUI.Button(new Rect(Screen.width * 4 / 5, Screen.height * 5 / 10, Screen.width / 7, Screen.height / 8), availableSkills[1], style))
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
                GUIStyle style = new GUIStyle(GUI.skin.box);
                //style.normal.background = skills[2].buttonSkill1;
                if (GUI.Button(new Rect(Screen.width * 4 / 5, Screen.height * 3 / 10, Screen.width / 7, Screen.height / 8), availableSkills[2], style))
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

            //ULTI
            if (availableSkills[3] != null)
            {
                GUIStyle style = new GUIStyle(GUI.skin.box);
                //style.normal.background = skills[3].buttonSkill1;
                if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), availableSkills[3], style))
                {
                    if (Network.isServer)
                    {
                        server_doSkill(Network.player, 3);
                    }
                    else
                    {
                        networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 3);
                    }
                }
            }
        }
    }

    private void initializeClientsSkills()
    {
        for (int i = 0; i < MultiplayerManager.instance.serverSideGasings.Count; i++)
        {
            if (MultiplayerManager.instance.playerList[i].playerNetwork == Network.player)
            {
                //for server
                for (int j = 0; j < MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length; j++)
                {
                    if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j] != null)
                    {
                        availableSkills[j] = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j].skillName;
                    }
                }
            }
            else
            {
                Debug.Log("masuk");
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
}
