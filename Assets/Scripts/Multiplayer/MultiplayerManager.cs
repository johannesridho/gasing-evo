using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
//using System.Net.Sockets;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;

    public string playerName;

    #region Loadable_prefabs
    public GameObject[] servergasingPrefab;
    public string[] selectableGasingString;
    public GameObject multiplayerInputHandlerPrefab;
    #endregion

    #region server_conf
    public int serverPort = 45000;
    public string serverName;
    public int maxPlayer;
    public bool isDedicatedServer = false;
    #endregion

    public List<MPPlayer> playerList = new List<MPPlayer>();
    public List<MapSetting> mapList = new List<MapSetting>();

    public MapSetting currentMap = null;
    public int oldprefix;
    public bool isGameStarted = false;

    // game
    public MPPlayer myPlayer;
    private List<GameObject> spawnPoints;
    public bool isMapLoaded = false;

    public bool isAllPlayerReady = false;

    public GameObject[] items;

    public GameObject instantiatedPlayer;

    private int playerReady;

    //Server-only properies
    public List<GameObject> serverSideGasings = new List<GameObject>();

    public InGameMessageViewer inGameMessageViewer;


    public NetworkPlayer siapaYangUlti;

    // Use this for initialization
    void Start()
    {
        instance = this;
        playerName = PlayerPrefs.GetString("PlayerName");
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        instance = this;
        //Debug.Log("Player Ready = "+ playerReady);
        //Debug.Log("=========");
        //foreach (KeyValuePair<NetworkPlayer, GameObject> entry in MultiplayerManager.instance.serverSideGasings)
        //{
        //    if (entry.Value != null)
        //    {
        //        Debug.Log(entry.Value.GetComponentInChildren<Gasing>());
        //    }
        //}
        //Debug.Log("+++++++");
    }

    /*
     * Start the server
     */
    public void startServer(string serverName, int maxPlayer)
    {
        this.serverName = serverName;
        this.maxPlayer = maxPlayer;
        playerReady = 0;
        isAllPlayerReady = false;
        Network.InitializeServer(this.maxPlayer, serverPort, false);
        //Network.InitializeSecurity();
        MasterServer.RegisterHost("gasing evo", serverName);
    }

    void OnServerInitialized()
    {

        if (!isDedicatedServer)
        {
            //Add the server creator as a player in the server
            server_playerJoinRequest(playerName, Network.player);
            // add new gasing to server side gasing
            //GameObject newGasing = Network.Instantiate(servergasingPrefab[selectedGasing], new Vector3(0, 1, -15), Quaternion.Euler(0, 0, 0), 5) as GameObject;
            //newGasing.GetComponent<Server_Gasing>().networkPlayer = Network.player;
            //serverSideGasings.Add(newGasing);
        }

    }

    void OnConnectedToServer()
    {
        networkView.RPC("server_playerJoinRequest", RPCMode.Server, playerName, Network.player);
        networkView.RPC("client_changeGasing", RPCMode.All, Network.player, PlayerPrefs.GetInt("Selected Gasing"));
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        //send all player's info the newly connected player
        foreach (MPPlayer pl in playerList)
        {
            networkView.RPC("client_addPlayerToList", player, pl.playerName, pl.playerNetwork);
        }
        //send map info to the newly connected player
        networkView.RPC("client_getMultiplayerMapSetting", player, currentMap.mapName, "", isDedicatedServer);

        //send the server's maxPlayer
        networkView.RPC("client_getMaxPlayer", player, maxPlayer);

        //if (isAllRigidbodyOnServer)
        //{
        //    // add new gasing to server side gasing
        //    GameObject newGasing = Network.Instantiate(servergasingPrefab[selectedGasing], new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0), 5) as GameObject;
        //    newGasing.GetComponent<Server_Gasing>().networkPlayer = player;
        //    serverSideGasings.Add(newGasing);
        //}

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
            // instantiate client's input sender
            instantiatedPlayer = Network.Instantiate(multiplayerInputHandlerPrefab, Vector3.zero, Quaternion.identity, 5) as GameObject;

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

        Debug.Log("player disconnected. Before: " + serverSideGasings.Count);
        serverSideGasings.Remove(getGasingOwnedByPlayer(view));
        Debug.Log("player disconnected. After: " + serverSideGasings.Count);
    }

    [RPC]
    void client_getMultiplayerMapSetting(string mapname, string mode, bool isDedicated)
    {
        currentMap = getMap(mapname);
        this.isDedicatedServer = isDedicated;
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
        if (Network.isServer)
        {
            initializePlayers();
        }
        //Network.SetLevelPrefix(prefix);
        LoadingScreen.show();
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

        if (Network.isServer)
        {
            // Populate all spawnpoints
            spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
            foreach (GameObject entry in serverSideGasings)
            {
                int spawnindex = Random.Range(0, spawnPoints.Count - 1);
                entry.transform.position = spawnPoints[spawnindex ].transform.position;
                entry.transform.rotation = Quaternion.Euler(270, 0, 0);
                entry.GetComponent<Server_Gasing>().networkView.RPC("client_playerAlive", RPCMode.All);
                spawnPoints.Remove(spawnPoints[spawnindex]);
            }
        }
        else
        {
            //GameObject[] asd = GameObject.FindGameObjectsWithTag("MP_Player");
            //Debug.Log("asdasdasdasd = " );
            //foreach (GameObject gobj in asd)
            //{
            //    gobj.SetActive(true);
            //}
        }

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

    }

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == currentMap.mapLoadName)
        {
            isMapLoaded = true;
            // Populate all spawnpoints
            spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));

            // Populate all items
            items = GameObject.FindGameObjectsWithTag("Item");

            foreach (GameObject go in items)
            {
                Debug.Log("item = " + go.name);
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
            // BUG: INI DIPANGGIL SETIAP CLIENT READY. JADI ADA SI SERVER SPAWN SEMUA PLAYER BERKALI-KALI
            
            playerReady += 1;
            Debug.Log("==============client_serverLoaded PLAYER READY = " + playerReady);
            if (isDedicatedServer)
            {
                Debug.Log("playerlist.count = " + playerList.Count);
                if (playerReady == playerList.Count)
                {
                    Debug.Log("maaaaaaaaaaaaasuuuuuuuuk");
                    isAllPlayerReady = true;
                    server_spawnPlayer(Network.player);
                }
            }
            else
            {
                if (playerReady == playerList.Count)
                {
                    isAllPlayerReady = true;
                    server_spawnPlayer(Network.player);
                }
            }
            
        }
        else
        {
            //networkView.RPC("server_spawnPlayer", RPCMode.Server, Network.player);
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

    public void initializePlayers()
    {
        foreach (MPPlayer mpp in playerList)
        {
            // add new gasing to server side gasing
            GameObject newGasing = Network.Instantiate(servergasingPrefab[mpp.selectedGasing], new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0), 5) as GameObject;
            newGasing.GetComponent<Server_Gasing>().networkPlayer = mpp.playerNetwork;
            serverSideGasings.Add(newGasing);
        }


    }

    [RPC]
    public void client_changeGasing(NetworkPlayer player, int newGasing)
    {
        Debug.Log("gasing changed");
        getMPPlayer(player).selectedGasing = newGasing;
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

    public GameObject getGasingOwnedByPlayer(NetworkPlayer player)
    {
        foreach (GameObject gobj in serverSideGasings)
        {
            if (gobj != null)
            {
                if (gobj.GetComponent<Server_Gasing>().networkPlayer == player)
                {
                    return gobj;
                }
            }
        }
        return null;
    }

    [RPC]
    public void client_playerDead(NetworkPlayer player)
    {
        Debug.Log("player DEAD!");
        if (player == Network.player)
        {
            instantiatedPlayer.GetComponent<MultiplayerSkillContoller>().setActive(false);
            inGameMessageViewer.showMyPlayerDeadScreen();
        }
        else
        {
            inGameMessageViewer.showPlayerDeadMessage(player);
        }
    }

    public void disconnectServer()
    {
        foreach (GameObject gobj in serverSideGasings)
        {
            Destroy(gobj);
        }
        serverSideGasings = new List<GameObject>();

        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i] = null;
        }
        playerList = new List<MPPlayer>();
    }

    public void decideWinner()
    {
        GameObject winner = null;
        foreach (GameObject gobj in serverSideGasings)
        {
            if (gobj.GetComponentInChildren<Gasing>().energiPoint > 0)
            {
                winner = gobj;
            }
        }

        networkView.RPC("client_gameOver", RPCMode.All, getMPPlayer(winner.GetComponent<Server_Gasing>().networkPlayer).playerNetwork);
    }

    [RPC]
    public void client_gameOver(NetworkPlayer winner)
    {
        if (Network.isServer)
        {
            foreach (GameObject gobj in serverSideGasings)
            {
                gobj.SetActive(false);
            }
        }
        isGameStarted = false;
        Debug.Log("~~~~~~~ WINNER = "+ getMPPlayer(winner).playerName +" ~~~~~~~");
        PlayerPrefs.SetString("MP Winner", getMPPlayer(winner).playerName);
        Application.LoadLevel("GameOver");
    }
}

[System.Serializable]
public class MPPlayer
{
    public string playerName;
    public NetworkPlayer playerNetwork;
    public bool isAlive = true;
    public PlayerManager playerManager;
    public int selectedGasing;
}

[System.Serializable]
public class MapSetting
{
    public string mapName;
    public string mapLoadName;
    public Texture mapTexture;
}