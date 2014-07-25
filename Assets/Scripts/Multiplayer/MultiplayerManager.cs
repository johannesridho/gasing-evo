﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
//using System.Net.Sockets;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;

    public string playerName;

    public int selectedGasing =1;

    #region Loadable_prefabs
    public GameObject playerManagerPrefab;
    public GameObject[] servergasingPrefab;
    public string[] selectableGasingString = new string[] { "Arjuna", "Srikandi", "Prototype" };
    public GameObject multiplayerInputHandlerPrefab;
    #endregion

    #region server_conf
    public int serverPort = 45000;
    public string serverName;
    public int maxPlayer;
    public bool isAllRigidbodyOnServer = false;
    public bool isDedicatedServer = false;
    #endregion

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

    public GameObject instantiatedPlayer;
    

    //Server-only properies
    public List<GameObject> serverSideGasings = new List<GameObject>();

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

        Network.InitializeServer(this.maxPlayer, serverPort, false);
        //Network.InitializeSecurity();
        MasterServer.RegisterHost("gasing evo", serverName);
    }

    void OnServerInitialized()
    {
        if (isAllRigidbodyOnServer)
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
            if (isAllRigidbodyOnServer)
            {
                myPlayer = temp;
                // instantiate client's input sender
                instantiatedPlayer = Network.Instantiate(multiplayerInputHandlerPrefab, Vector3.zero, Quaternion.identity, 5) as GameObject;
            }
            else
            {
                myPlayer = temp;
                // instantiate player
                instantiatedPlayer = Network.Instantiate(playerManagerPrefab, new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0), 5) as GameObject;
            }
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
        if (isAllRigidbodyOnServer)
        {
            if (Network.isServer)
            {
                foreach (GameObject entry in serverSideGasings)
                {
                    int spawnindex = Random.Range(0, spawnPoints.Length - 1);
                    entry.transform.position = spawnPoints[spawnindex].transform.position;
                    entry.transform.rotation = Quaternion.Euler(270, 0, 0);
                    entry.GetComponent<Server_Gasing>().networkView.RPC("client_playerAlive", RPCMode.All);
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
        else
        {
            int spawnindex = Random.Range(0, spawnPoints.Length - 1);
            networkView.RPC("client_spawnPlayer", RPCMode.All, player, spawnPoints[spawnindex].transform.position, Quaternion.Euler(270, 0, 0));
        }
    }

    /*
     * Tell the spawn position to the client
     */
    [RPC]
    void client_spawnPlayer(NetworkPlayer player, Vector3 position, Quaternion rotation)
    {
        if (isAllRigidbodyOnServer)
        {
            
        }
        else
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
            if (gobj.GetComponent<Server_Gasing>().networkPlayer == player)
            {
                return gobj;
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
    public int selectedGasing;
}

[System.Serializable]
public class MapSetting
{
    public string mapName;
    public string mapLoadName;
    public Texture mapTexture;
}