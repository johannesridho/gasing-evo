using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLobby : MonoBehaviour {

	int serverPort = 45671;
	string gameName="gameserver";
	private bool launchingGame = false;
	private bool showMenu  = false;
	
	private List<PlayerInfo> playerList;
	class PlayerInfo {
		public string username;
		public NetworkPlayer player;
	}

	private int serverMaxPlayers  =4;
	private string serverTitle  = "Loading..";
	private bool serverPasswordProtected  = false;
	
	private string playerName = "";
	
	private Menu mainMenuScript;

	private string hostSetting_title = "No server title";
	private int hostSetting_players  = 4;
	private string hostSetting_password  = "";

	float lastRegTime  = -60;

	void Awake(){
		showMenu = false;
	}

	void Start(){
		mainMenuScript =  Menu.SP;
	}

	public void EnableLobby(){
		playerName = PlayerPrefs.GetString("playerName");

//		lastRegTime=Time.time-3600;
		
		launchingGame=false;
		showMenu=true;
		
//		LobbyChat chat = GetComponent(LobbyChat);		
//		chat.ShowChatWindow();
	}

	void OnGUI () {
		if(showMenu){
			//Back to main menu
			if(GUI.Button(new Rect(40,10,150,20), "Back to main menu")){
				leaveLobby();
			}
			
			if(launchingGame){		
				launchingGameGUI();
				
			} else if(!Network.isServer && !Network.isClient){
				//First set player count, server name and password option			
				hostSettings();
				
			} else {
				//Show the lobby		
				showLobby();
			}
		}
	}

	void leaveLobby(){
		//Disconnect fdrom host, or shotduwn host
		if (Network.isServer || Network.isClient){
			if(Network.isServer){
				MasterServer.UnregisterHost();
			}
			Network.Disconnect();
//			yield WaitForSeconds(0.3);
		}	
		
//		var chat : LobbyChat = GetComponent(LobbyChat);
//		chat.CloseChatWindow();
		
		mainMenuScript.openMenu("multiplayer");
		
		showMenu=false;
	}

	void hostSettings(){
		
		GUI.BeginGroup (new Rect (Screen.width/2-175, Screen.height/2-75-50, 350, 150));
		GUI.Box (new Rect (0,0,350,150), "Server options");
		
		GUI.Label (new Rect (10,20,150,20), "Server title");
		hostSetting_title = GUI.TextField (new Rect (175,20,160,20), hostSetting_title);
		
		GUI.Label (new Rect (10,40,150,20), "Max. players (2-32)");
		hostSetting_players = int.Parse(GUI.TextField (new Rect (175,40,160,20), hostSetting_players+""));
		
		GUI.Label (new Rect (10,60,150,50), "Password\n");
		hostSetting_password = (GUI.TextField (new Rect (175,60,160,20), hostSetting_password));
		
		
		if(GUI.Button (new Rect (100,115,150,20), "Go to lobby")){
			StartHost(hostSetting_password, int.Parse(hostSetting_players.ToString()), hostSetting_title);
		}
		GUI.EndGroup();
	}

	void StartHost(string password, int players, string serverName){
		if(players<1){
			players=1;
		}
		if(players>=32){
			players=32;
		}
		if(password!=""){
			serverPasswordProtected  = true;
			Network.incomingPassword = password;
		}else{
			serverPasswordProtected  = false;
			Network.incomingPassword = "";
		}
		
		serverTitle = serverName;
		
		Network.InitializeSecurity();
		Network.InitializeServer((players-1), serverPort);	
	}

	void showLobby(){
		string players = "";
		int currentPlayerCount  =0;

		foreach(PlayerInfo playerInfo in playerList){
			players+= playerInfo.username+"\n";
			currentPlayerCount++;
		}

//		for (var playerInstance : PlayerInfo in playerList) {
//			players=playerInstance.username+"\n"+players;
//			currentPlayerCount++;	
//		}
		
		GUI.BeginGroup (new Rect (Screen.width/2-200, Screen.height/2-200, 400, 180));
		GUI.Box (new Rect (0,0,400,200), "Game lobby");
		
		
		var pProtected="no";
		if(serverPasswordProtected){
			pProtected="yes";
		}
		GUI.Label (new Rect (10,20,150,20), "Password protected");
		GUI.Label (new Rect (150,20,100,100), pProtected);
		
		GUI.Label (new Rect (10,40,150,20), "Server title");
		GUI.Label (new Rect (150,40,100,100), serverTitle);
		
		GUI.Label (new Rect (10,60,150,20), "Players");
		GUI.Label (new Rect (150,60,100,100), currentPlayerCount+"/"+serverMaxPlayers);
		
		GUI.Label (new Rect (10,80,150,20), "Current players");
		GUI.Label (new Rect (150,80,100,100), players);
		
		
		if(Network.isServer){
			if(GUI.Button (new Rect (25,140,150,20), "Start the game")){
				HostLaunchGame();
			}
		}else{
			GUI.Label (new Rect (25,140,200,40), "Waiting for the server to start the game..");
		}
		
		GUI.EndGroup();
	}

	void OnConnectedToServer(){
		//Called on client
		//Send everyone this clients data
		playerList = new List<PlayerInfo> ();
		playerName = PlayerPrefs.GetString("playerName");
		networkView.RPC("addPlayer",RPCMode.AllBuffered, Network.player, playerName);	
	}

	void OnServerInitialized(){
		//Called on host
		//Add hosts own data to the playerlist	
		playerList = new List<PlayerInfo> ();
		networkView.RPC("addPlayer",RPCMode.AllBuffered, Network.player, playerName);
		
		bool pProtected  = false;
		if(Network.incomingPassword!=""){
			pProtected=true;
		}
		int maxPlayers  = Network.maxConnections+1;
		
		networkView.RPC("setServerSettings",RPCMode.AllBuffered, pProtected, maxPlayers, hostSetting_title);
		
	}

	void Update(){
		if(Network.isServer && lastRegTime<Time.time-60){
			lastRegTime=Time.time;
			MasterServer.RegisterHost(gameName,hostSetting_title, "No description");
		}
	}

	[RPC]
	void setServerSettings(bool password, int maxPlayers, string newSrverTitle){
		serverMaxPlayers = maxPlayers;
		serverTitle  = newSrverTitle;
		serverPasswordProtected  = password;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		//Called on host
		//Remove player information from playerlist
		networkView.RPC("playerLeft", RPCMode.All, player);
		
//		var chat : LobbyChat = GetComponent(LobbyChat);
//		chat.addGameChatMessage("A player left the lobby");
	}

	[RPC]
	void addPlayer(NetworkPlayer player, string username){
		Debug.Log("got addplayer"+username);
		
		PlayerInfo playerInstance = new PlayerInfo();
		playerInstance.player = player;
		playerInstance.username = username;		
		playerList.Add(playerInstance);
	}

	[RPC]
	void playerLeft(NetworkPlayer player){
		
		PlayerInfo deletePlayer = null;

		foreach(PlayerInfo playerInfo in playerList){
			if(player == playerInfo.player){
				deletePlayer = playerInfo;
			}
		}

		playerList.Remove(deletePlayer);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	void HostLaunchGame(){
		if(!Network.isServer){
			return;
		}
		
		// Don't allow any more players
		Network.maxConnections = -1;
		MasterServer.UnregisterHost();	
		
		networkView.RPC("launchGame",RPCMode.All);
	}

	[RPC]
	void launchGame(){
		Network.isMessageQueueRunning=false;
		launchingGame=true;
	}

	void launchingGameGUI(){
		//Show loading progress, ADD LOADINGSCREEN?
		GUI.Box(new Rect(Screen.width/4+180,Screen.height/2-30,280,50), "");
		if(Application.CanStreamedLevelBeLoaded ("Single Player")){
			GUI.Label(new Rect(Screen.width/4+200,Screen.height/2-25,285,150), "Loaded, starting the game!");

			//NANTI DIGANTI DENGAN MULTIPLAYER
			Application.LoadLevel("Single Player");
		}else{
			GUI.Label(new Rect(Screen.width/4+200,Screen.height/2-25,285,150), "Starting..Loading the game: "+Mathf.Floor(Application.GetStreamProgressForLevel("Single Player")*100)+" %");
		}	
		
	}
}
