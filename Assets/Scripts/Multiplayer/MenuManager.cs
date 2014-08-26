using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    private float guiRatioX;
    private float guiRatioY;
    private float sWidth;
    private float sHeight;
    private Vector3 GUIsF;
    private int sizegui;

    public string currentMenu;
    public string serverName = "My Server";
    public int maxPlayer = 2;
    private string directConnectIP = "127.0.0.1";

    private Vector2 scrollLobby = Vector2.zero;
    private Vector2 scrollMap = Vector2.zero;
    private Vector2 scrollGasing = Vector2.zero;
    //public GUIStyle customButton;
    public GUISkin customSkin;

    void Start()
    {
        GamePrefs.isMultiplayer = true;
        currentMenu = "Main";
        maxPlayer = 4;

        //get the screen's width
        sWidth = Screen.width;
        sHeight = Screen.height;
        //calculate the rescale ratio
        guiRatioX = sWidth / 1280;
        guiRatioY = sHeight / 720;
        //create a rescale Vector3 with the above ratio
        GUIsF = new Vector3(guiRatioX, guiRatioY, 1);
    }

    /*
     * Navigate to other menu
     */
    public void navigateTo(string nextMenu)
    {
        currentMenu = nextMenu;
    }

    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
        GUI.skin = customSkin;
        if (!MultiplayerManager.instance.isGameStarted)
        {
            if (currentMenu == "Main")
            {
                menu_Main();
            }
            if (currentMenu == "Host")
            {
                menu_HostGame();
            }
            if (currentMenu == "Lobby")
            {
                menu_Lobby();
            }
            if (currentMenu == "Choose Map")
            {
                menu_chooseMap();
            }
            if (currentMenu == "Choose Gasing")
            {
                menu_chooseGasing();
            }
        }
    }

    private void menu_Main()
    {
        MasterServer.RequestHostList("gasing evo");

        if (GUI.Button(new Rect(325, 170, 630, 60), "Host a LAN Game"))
        {
            navigateTo("Host");
            MultiplayerManager.instance.isDedicatedServer = false;
        }

        if (GUI.Button(new Rect(325, 250, 630, 60), "Start a Big Screen Mode"))
        {
            navigateTo("Host");
            MultiplayerManager.instance.isDedicatedServer = true;
        }

        GUI.Label(new Rect(110, 390, 200, 60), "Player Name");
        MultiplayerManager.instance.playerName = GUI.TextField(new Rect(325, 390, 630, 60), MultiplayerManager.instance.playerName);
        if (GUI.Button(new Rect(970, 390, 260, 60), "Save Name"))
        {
            PlayerPrefs.SetString("PlayerName", MultiplayerManager.instance.playerName);
        }

        GUI.Label(new Rect(110, 490, 200, 60), "IP Address");
        directConnectIP = GUI.TextField(new Rect(325, 490, 630, 60), directConnectIP);
        if (GUI.Button(new Rect(970, 490, 260, 60), "Direct Connect"))
        {
            Network.Connect(directConnectIP, MultiplayerManager.instance.serverPort);
        }
    }

    private void menu_HostGame()
    {
        GUI.Label(new Rect(80, 175, 200, 60), "Server Name");
        serverName = GUI.TextField(new Rect(295, 175, 905, 60), serverName);

        GUI.Label(new Rect(80, 250, 200, 60), "Max Player");
        string str_maxPlayer = GUI.TextField(new Rect(295, 250, 905, 60), maxPlayer.ToString());
        maxPlayer = int.Parse(str_maxPlayer);

        GUI.Label(new Rect(80, 325, 200, 60), "Map");
        GUI.Label(new Rect(295, 325, 200, 60), MultiplayerManager.instance.currentMap.mapName);
        if (GUI.Button(new Rect(950, 325, 250, 60), "Choose Map"))
        {
            navigateTo("Choose Map");
        }

        if (GUI.Button(new Rect(485, 410, 250, 60), "Start Server"))
        {
            MultiplayerManager.instance.startServer(serverName, maxPlayer);
        }
        if (GUI.Button(new Rect(510, 485, 200, 60), "Back"))
        {
            navigateTo("Main");
        }
    }

    private void menu_Lobby()
    {
        GUI.Label(new Rect(80, 20, 200, 60), "Server Name");
        GUI.Label(new Rect(295, 20, 910, 60), MultiplayerManager.instance.serverName);

        GUI.Label(new Rect(80, 95, 200, 60), "Server IP Address");
        GUI.Label(new Rect(295, 95, 910, 60), MultiplayerManager.instance.getServerIP());

        GUI.Label(new Rect(80, 170, 200, 60), "Map");
        GUI.Label(new Rect(295, 170, 910, 60), MultiplayerManager.instance.currentMap.mapName);

        //select gasing
        if ((!MultiplayerManager.instance.isDedicatedServer) || (MultiplayerManager.instance.isDedicatedServer && Network.isClient))
        {
            GUI.Label(new Rect(80, 245, 200, 60), "Gasing");
            GUI.Label(new Rect(295, 245, 645, 60), MultiplayerManager.instance.selectableGasingString[MultiplayerManager.getMPPlayer(Network.player).selectedGasing]);
            if (GUI.Button(new Rect(945, 245, 260, 60), "Choose Gasing"))
            {
                navigateTo("Choose Gasing");
            }
        }

        GUI.Label(new Rect(80, 320, 200, 60), "Total Player");
        GUI.Label(new Rect(295, 320, 910, 60), MultiplayerManager.instance.playerList.Count + "/" + MultiplayerManager.instance.maxPlayer);

        scrollLobby = GUI.BeginScrollView(new Rect(80, 395, 1125, 220), scrollLobby, new Rect(0, 0, 1125, MultiplayerManager.instance.playerList.Count * 60));
        for (int i = 0; i < MultiplayerManager.instance.playerList.Count; i++)
        {
            GUI.Box(new Rect(0, i * 60, 1110, 60), MultiplayerManager.instance.playerList[i].playerName);
        }
        GUI.EndScrollView();

        if (Network.isServer)
        {
            if (GUI.Button(new Rect(300, 630, 200, 60), "Start"))
            {
                //start the game
                MultiplayerManager.instance.networkView.RPC("client_loadMultiplayerMap", RPCMode.AllBuffered, MultiplayerManager.instance.currentMap.mapLoadName, MultiplayerManager.instance.oldprefix + 1);
                MultiplayerManager.instance.oldprefix += 1;
                MultiplayerManager.instance.isGameStarted = true;
            }
        }

        if (GUI.Button(new Rect(800, 630, 200, 60), "Disconnect"))
        {
            Network.Disconnect();
        }
    }

    private void menu_chooseMap()
    {
        GUI.Label(new Rect(51, 30, 1170, 60), "Select Map");

        GUI.Box(new Rect(46, 100, 1180, 510), "");
        scrollMap = GUI.BeginScrollView(new Rect(51, 105, 1170, 500), scrollMap, new Rect(0, 0, MultiplayerManager.instance.mapList.Count * 405, 470), true, false);

        for (int i = 0; i < MultiplayerManager.instance.mapList.Count; i++)
        {
            if (GUI.Button(new Rect((400 * i) + 10, 0, 400, 400), MultiplayerManager.instance.mapList[i].mapTexture))
            {
                MultiplayerManager.instance.currentMap = MultiplayerManager.instance.mapList[i];
                navigateTo("Host");
            }
            if (GUI.Button(new Rect((400 * i) + 10, 405, 400, 60), MultiplayerManager.instance.mapList[i].mapName))
            {
                MultiplayerManager.instance.currentMap = MultiplayerManager.instance.mapList[i];
                navigateTo("Host");
            }
        }

        GUI.EndScrollView();

        //if (GUI.Button(new Rect(51, 626, 250, 60), "OK"))
        //{

        //}

        if (GUI.Button(new Rect(971, 626, 250, 60), "Back"))
        {
            navigateTo("Host");
        }
    }

    private void menu_chooseGasing()
    {
        GUI.Label(new Rect(51, 30, 1170, 60), "Select Gasing");

        int selected = PlayerPrefs.GetInt("Selected Gasing");
        int oldSelected = selected;

        GUI.Box(new Rect(51, 105, 550, 506), "");

        scrollGasing = GUI.BeginScrollView(new Rect(51, 105, 550, 506), scrollGasing, new Rect(0, 0, 500, MultiplayerManager.instance.selectableGasingString.Length * 65));

        for (int i = 0; i < MultiplayerManager.instance.selectableGasingString.Length; i++)
        {
            if (i == selected)
            {
                GUIStyle centered = customSkin.GetStyle("Label");
                centered.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(0, (i * 60) + 5, 550, 60), MultiplayerManager.instance.selectableGasingString[i], centered);
            }
            else
            {
                if (GUI.Button(new Rect(0, (i * 60) + 5, 550, 60), MultiplayerManager.instance.selectableGasingString[i]))
                {
                    selected = i;
                    PlayerPrefs.SetInt("Selected Gasing", selected);
                }
            }
            
        }

        GUI.EndScrollView();

        GUI.Box(new Rect(629, 105, 550, 506), "");
        GUI.Label(new Rect(635, 115, 550, 60), MultiplayerManager.instance.selectableGasingString[selected]);


        if (GUI.Button(new Rect(51, 626, 250, 60), "OK"))
        {
            MultiplayerManager.instance.networkView.RPC("client_changeGasing", RPCMode.All, Network.player, selected);
            PlayerPrefs.SetInt("Selected Gasing", selected);
            navigateTo("Lobby");
        }

        if (GUI.Button(new Rect(971, 626, 250, 60), "Cancel"))
        {
            selected = oldSelected;
            PlayerPrefs.SetInt("Selected Gasing", selected);
            navigateTo("Lobby");
        }


        //GUILayout.BeginArea(new Rect(5, 5, Screen.width - 10, Screen.height - 10));

        //#region gasing_selector
        //GUILayout.BeginVertical();
        //int selected = PlayerPrefs.GetInt("Selected Gasing");
        //int oldSelected = selected;
        //selected = GUILayout.SelectionGrid(selected, MultiplayerManager.instance.selectableGasingString, MultiplayerManager.instance.selectableGasingString.Length);
        //PlayerPrefs.SetInt("Selected Gasing", selected);
        //GUILayout.EndVertical();
        //#endregion

        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("OK"))
        //{
        //    MultiplayerManager.instance.networkView.RPC("client_changeGasing", RPCMode.All, Network.player, selected);
        //    navigateTo("Lobby");
        //}
        //if (GUILayout.Button("Cancel"))
        //{
        //    PlayerPrefs.SetInt("Selected Gasing", oldSelected);
        //    navigateTo("Lobby");
        //}
        //GUILayout.EndHorizontal();
        //GUILayout.EndArea();
    }

    private void menu_inGame()
    {
        GUI.Label(new Rect(5, 5, 200, 50), "player :" + MultiplayerManager.instance.playerList.Count);
    }

    void OnServerInitialized()
    {
        navigateTo("Lobby");
    }

    void OnConnectedToServer()
    {
        navigateTo("Lobby");
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        navigateTo("Main");
    }
}
