using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public Texture2D button;
	public GameObject aftermatchPrefab;
	private GameObject textInstance;

	// Use this for initialization
	void Start () {
		if (Utilities.victory == true) {
			GameObject.Find ("GameOverText").GetComponent<TextMesh>().text = "YOU WIN";	
		} else {
			GameObject.Find ("GameOverText").GetComponent<TextMesh>().text = "YOU LOSE";		
		}


		if (Utilities.chosenMode == 1 || Utilities.chosenMode == 0) {
			setBattleModeContent ();
		} else {
			setStoryModeContent();
		}
	}

	void setBattleModeContent(){
		GameObject parent = GameObject.Find ("GameOverText");	
		textInstance = Instantiate (aftermatchPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
		textInstance.transform.position = new Vector3 (parent.transform.position.x - 3, parent.transform.position.y - 1, parent.transform.position.z);
		textInstance.GetComponent<TextMesh>().text = "REMATCH";
		textInstance.name = "rematch";

		textInstance = Instantiate (aftermatchPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
		textInstance.transform.position = new Vector3 (parent.transform.position.x + 2.2f, parent.transform.position.y - 1, parent.transform.position.z);
		textInstance.GetComponent<TextMesh>().text = "QUIT";
		textInstance.name = "quit";
	}

	void setStoryModeContent(){
		GameObject parent = GameObject.Find ("GameOverText");	
		textInstance = Instantiate (aftermatchPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
		textInstance.transform.position = new Vector3 (parent.transform.position.x - 3, parent.transform.position.y - 1, parent.transform.position.z);
		textInstance.GetComponent<TextMesh>().text = "next";
		textInstance.name = "next";
		
		textInstance = Instantiate (aftermatchPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
		textInstance.transform.position = new Vector3 (parent.transform.position.x + 2.2f, parent.transform.position.y - 1, parent.transform.position.z);
		textInstance.GetComponent<TextMesh>().text = "QUIT";
		textInstance.name = "quit";
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle (GUI.skin.box);
		style.normal.background = button;
		style.fontSize = 15;

		if (GUI.Button (new Rect (Screen.width * 1 / 2 - Screen.width / 6 / 2, Screen.height * 7 / 10, Screen.width / 6, Screen.height / 8), "Back To Menu", style)) {
			Application.LoadLevel("Main Menu");
		}
	}
}
