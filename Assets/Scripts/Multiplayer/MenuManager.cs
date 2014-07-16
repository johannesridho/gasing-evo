using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public string currentMenu;
    public string serverName;
    public int maxPlayer = 2;
    private string directConnectIP = "127.0.0.1";

    private Vector2 scrollLobby = Vector2.zero;

    void Start()
    {
        currentMenu = "Main";
        maxPlayer = 2;
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
        }
        else
        {
            menu_inGame();
        }
    }

    private void menu_Main()
    {
        MasterServer.RequestHostList("gasing evo");

        int layoutWidth = (int)(Screen.width * 0.3);
        int layoutHeight = (int)(Screen.height);

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.MaxWidth(layoutWidth));
        if (GUILayout.Button("Host"))
        {
            navigateTo("Host");
        }

        //Input nama
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Name");
        MultiplayerManager.instance.playerName = GUILayout.TextField(MultiplayerManager.instance.playerName);
        if (GUILayout.Button("Save Name"))
        {
            PlayerPrefs.SetString("PlayerName", MultiplayerManager.instance.playerName);
        }
        GUILayout.EndHorizontal();

        //Direct connect
        GUILayout.BeginHorizontal();
        GUILayout.Label("IP");
        directConnectIP = GUILayout.TextField(directConnectIP);
        if (GUILayout.Button("Direct Connect"))
        {
            Network.Connect(directConnectIP, MultiplayerManager.instance.serverPort);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        //menampilkan list hosts
        GUILayout.BeginVertical();
        GUILayout.Label("Server List");
        foreach (HostData hostdata in MasterServer.PollHostList())
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(hostdata.gameName);
            if (GUILayout.Button("Join"))
            {
                //join server
                Network.Connect(hostdata);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void menu_HostGame()
    {
        GUILayout.BeginArea(new Rect(5, 5, Screen.width - 5, Screen.height - 5));

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Server Name");
        serverName = GUILayout.TextField(serverName);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Player");
        string str_maxPlayer = GUILayout.TextField(maxPlayer.ToString());
        maxPlayer = int.Parse(str_maxPlayer);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Map");
        GUILayout.Label(MultiplayerManager.instance.currentMap.mapName);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        if (GUILayout.Button("Choose Map"))
        {
            navigateTo("Choose Map");
        }
        if (GUILayout.Button("Start Server"))
        {
            MultiplayerManager.instance.startServer(serverName, maxPlayer);
        }
        if (GUILayout.Button("Back"))
        {
            navigateTo("Main");
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void menu_Lobby()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Server Name");
        GUILayout.Label(MultiplayerManager.instance.serverName);
        GUILayout.EndHorizontal();

        //menampilkan server ip
        GUILayout.BeginHorizontal();
        GUILayout.Label("Server IP Address");
        GUILayout.Label(MultiplayerManager.instance.getServerIP());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Total Player");
        GUILayout.Label(MultiplayerManager.instance.playerList.Count + "/" + MultiplayerManager.instance.maxPlayer);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Map");
        GUILayout.Label(MultiplayerManager.instance.currentMap.mapName);
        GUILayout.EndHorizontal();

        if (Network.isServer)
        {
            if (GUILayout.Button("Start"))
            {
                //start the game
                MultiplayerManager.instance.networkView.RPC("client_loadMultiplayerMap", RPCMode.AllBuffered, MultiplayerManager.instance.currentMap.mapLoadName, MultiplayerManager.instance.oldprefix + 1);
                MultiplayerManager.instance.oldprefix += 1;
                MultiplayerManager.instance.isGameStarted = true;
            }
        }
        //if (Network.isClient)
        //{
        if (GUILayout.Button("Disconnect"))
        {
            Network.Disconnect();
        }
        //}
        GUILayout.Label("Players:");
        scrollLobby = GUILayout.BeginScrollView(scrollLobby, GUILayout.MaxWidth(Screen.width));
        foreach (MPPlayer pl in MultiplayerManager.instance.playerList)
        {
            GUILayout.Box(pl.playerName);
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void menu_chooseMap()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        foreach (MapSetting map in MultiplayerManager.instance.mapList)
        {
            if (GUILayout.Button(map.mapName))
            {
                MultiplayerManager.instance.currentMap = map;
                navigateTo("Host");
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
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
