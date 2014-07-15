using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;

    public GameObject playerManagerPrefab;

    public string playerName;

    public int serverPort = 45000;

    public string serverName;
    public int maxPlayer;

    public List<MPPlayer> playerList = new List<MPPlayer>();
    public List<MapSetting> mapList = new List<MapSetting>();

    public MapSetting currentMap = null;
    public int oldprefix;
    public bool isGameStarted = false;

    // game
    public MPPlayer myPlayer;
    public GameObject[] spawnPoints;
    public bool isMapLoaded = false;

    public GameObject[] items;

    // Use this for initialization
    void Start()
    {
        instance = this;
        playerName = PlayerPrefs.GetString("PlayerName");
        currentMap = mapList[0];
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        instance = this;
    }

    /*
     * Start the server
     */
    public void startServer(string serverName, int maxPlayer)
    {
        this.serverName = serverName;
        this.maxPlayer = maxPlayer;
        Network.InitializeServer(this.maxPlayer, serverPort, false);
        //Network.InitializeSecurity();
        MasterServer.RegisterHost("gasing evo", serverName);
    }

    void OnServerInitialized()
    {
        //Add the server creator as a player in the server
        server_playerJoinRequest(playerName, Network.player);
    }

    void OnConnectedToServer()
    {
        networkView.RPC("server_playerJoinRequest", RPCMode.Server, playerName, Network.player);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        //send all player's info the newly connected player
        foreach (MPPlayer pl in playerList)
        {
            networkView.RPC("client_addPlayerToList", player, pl.playerName, pl.playerNetwork);
        }
        //send map info to the newly connected player
        networkView.RPC("client_getMultiplayerMapSetting", player, currentMap.mapName, "");

        //send the server's maxPlayer
        networkView.RPC("client_getMaxPlayer", player, maxPlayer);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        networkView.RPC("client_removePlayer", RPCMode.All, player);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        playerList.Clear();
    }

    /*
     * Client joins the server and the server will tell all players that a new player has been connected
     */
    [RPC]
    void server_playerJoinRequest(string playerName, NetworkPlayer view)
    {
        networkView.RPC("client_addPlayerToList", RPCMode.All, playerName, view);
    }

    /*
     * Tell the client to add playerName and view
     */
    [RPC]
    void client_addPlayerToList(string playerName, NetworkPlayer view)
    {
        MPPlayer temp = new MPPlayer();
        temp.playerName = playerName;
        temp.playerNetwork = view;
        playerList.Add(temp);

        if (Network.player == view)
        {
            myPlayer = temp;
            // instantiate player
            GameObject play = Network.Instantiate(playerManagerPrefab, new Vector3(0, 1, -15), Quaternion.Euler(0, 0, 0), 5) as GameObject;
        }
    }

    [RPC]
    void client_removePlayer(NetworkPlayer view)
    {
        MPPlayer temp = null;
        foreach (MPPlayer pl in playerList)
        {
            if (pl.playerNetwork == view)
            {
                temp = pl;
            }
        }
        if (temp != null)
        {
            playerList.Remove(temp);
        }
    }

    [RPC]
    void client_getMultiplayerMapSetting(string mapname, string mode)
    {
        currentMap = getMap(mapname);
    }

    public MapSetting getMap(string mapName)
    {
        MapSetting get = null;

        foreach (MapSetting map in mapList)
        {
            if (map.mapName == mapName)
            {
                get = map;
                break;
            }
        }
        return get;
    }

    /*
     * Tell the client to load the map
     */
    [RPC]
    void client_loadMultiplayerMap(string mapLoadName, int prefix)
    {
        //Network.SetLevelPrefix(prefix);
        Application.LoadLevel(mapLoadName);
        //GameObject go = GameObject.Find("StoneFieldController");
        //StoneFieldController script = (StoneFieldController)go.GetComponent(typeof(StoneFieldController));
        //script.isMultiplayer = true;
    }

    /*
     * Tell the client server's max player
     */
    [RPC]
    void client_getMaxPlayer(int maxPlayer)
    {
        this.maxPlayer = maxPlayer;
    }

    /*
     * Ask the server for the spawn points
     */
    [RPC]
    void server_spawnPlayer(NetworkPlayer player)
    {
        int spawnindex = Random.Range(0, spawnPoints.Length - 1);
        networkView.RPC("client_spawnPlayer", RPCMode.All, player, spawnPoints[spawnindex].transform.position, Quaternion.Euler(270, 0, 0));
    }

    /*
     * Tell the spawn position to the client
     */
    [RPC]
    void client_spawnPlayer(NetworkPlayer player, Vector3 position, Quaternion rotation)
    {
        if (player == myPlayer.playerNetwork)
        {
            // set player position
            myPlayer.playerManager.gasingTransform.position = position;
            myPlayer.playerManager.gasingTransform.rotation = rotation;
            myPlayer.playerManager.networkView.RPC("client_playerAlive", RPCMode.All);
        }
        else
        {

        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == currentMap.mapLoadName)
        {
            isMapLoaded = true;
            // Populate all spawnpoints
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            // Populate all items
            items = GameObject.FindGameObjectsWithTag("Item");

            foreach (GameObject go in items)
            {
                Debug.Log("item = "+go.name);
            }

            isGameStarted = true;
            networkView.RPC("client_serverLoaded", RPCMode.AllBuffered, isGameStarted);

        }
    }

    [RPC]
    public void client_serverLoaded(bool started)
    {
        isGameStarted = started;

        //spawn the player
        if (Network.isServer)
        {
            server_spawnPlayer(Network.player);
        }
        else
        {
            networkView.RPC("server_spawnPlayer", RPCMode.Server, Network.player);
        }
    }

    public void handleItemCollision(GameObject item)
    {
        networkView.RPC("client_itemCollected", RPCMode.All, item);
    }

    [RPC]
    public void client_itemCollected(GameObject item)
    {
        Debug.Log("Item collected");
    }

    [RPC]
    public void client_spawnItem()
    {

    }
    
    //misc

    public string getServerIP()
    {
        //IPHostEntry host;
        //string localIP = "";
        //host = Dns.GetHostEntry(Dns.GetHostName());
        //foreach (IPAddress ip in host.AddressList)
        //{
        //    if (ip.AddressFamily == AddressFamily.InterNetwork)
        //    {
        //        localIP += ip.ToString() + " ";
        //        //break;
        //    }
        //}

        return Network.player.ipAddress;
    }

    public static MPPlayer getMPPlayer(NetworkPlayer player)
    {
        foreach (MPPlayer play in MultiplayerManager.instance.playerList)
        {
            if (play.playerNetwork == player)
            {
                return play;
            }
        }

        return null;
    }

}

[System.Serializable]
public class MPPlayer
{
    public string playerName;
    public NetworkPlayer playerNetwork;
    public bool isAlive = true;
    public PlayerManager playerManager;
}

[System.Serializable]
public class MapSetting
{
    public string mapName;
    public string mapLoadName;
    public Texture mapTexture;
}