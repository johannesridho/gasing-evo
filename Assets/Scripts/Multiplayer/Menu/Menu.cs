using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public static Menu SP;

	private JoinMenu joinMenuScript;
	private GameLobby gameLobbyScript;
	private MultiplayerMenu multiplayerMenuScript;

	private string playerNameInput = "";
	private bool requirePlayerName = false; 

	//menu state
	public bool hostGameMode  = false;
	public bool joinGameMode = false;

	//properti untuk host a game
	public string lobbyName = "";
	public int maxPlayers = 2;
	//properti untuk join a game


	void Awake()
	{
		SP = this;
		playerNameInput = PlayerPrefs.GetString("playerName", "");
		requirePlayerName=true;

		joinMenuScript = GetComponent<JoinMenu> ();
		gameLobbyScript = GetComponent<GameLobby> ();
		multiplayerMenuScript = GetComponent<MultiplayerMenu> ();

		openMenu ("multiplayer");
	}

	void OnGUI ()
	{
		if (requirePlayerName) {
			GUILayout.Window(0, new Rect(Screen.width/2-150,Screen.height/2-100,300,100), nameMenu, "Please enter a name:");
		}
	}

	public void openMenu(string newMenu){
//		if(requirePlayerName){
//
//		}
		if(!requirePlayerName){
			if(newMenu=="multiplayer-quickplay"){					
				joinMenuScript.enableMenu(true);//quickplay=true	
			}else if(newMenu=="multiplayer-host"){ 
				gameLobbyScript.EnableLobby();		
			}else if(newMenu=="multiplayer-join"){ 
				joinMenuScript.enableMenu(false);//quickplay:false
				
			}else if(newMenu=="multiplayer"){ 
				multiplayerMenuScript.enableMenu();
				
			}else{			
				Debug.LogError("Wrong menu:"+newMenu);	
				
			}
		}
	}

	private void nameMenu(int id){
		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Please insert your name");
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		playerNameInput = GUILayout.TextField (playerNameInput);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (playerNameInput.Length >= 1) {
			//button save name
			if(GUILayout.Button("Save")){
				requirePlayerName = false;
				PlayerPrefs.SetString("playerName", playerNameInput);
				openMenu("multiplayer");
			}
			else{
				GUILayout.Label("Enter a name to continue...");
			}
		}
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
	}








	private void hostGameModeMenu(){
		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Lobby name");
		lobbyName = GUILayout.TextField(lobbyName);
		if(GUI.changed){
			
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Max Players");
		maxPlayers =  int.Parse (GUILayout.TextField(maxPlayers.ToString()));
		if(GUI.changed){
			if(maxPlayers < 2){
				maxPlayers = 2;
			}
		}
		GUILayout.EndHorizontal();


		GUILayout.EndVertical ();
	}
}
