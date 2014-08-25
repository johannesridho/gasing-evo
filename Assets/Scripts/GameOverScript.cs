using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public Texture2D button;
	public GameObject aftermatchPrefab;
	private GameObject textInstance;

	// Use this for initialization
	void Start () {
//		Debug.LogError (Utilities.chosenArena+"-----------------");
		switch (Utilities.chosenArena) {
		case "ice field":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepSpaceBlue/DSB");
			break;		
		default:
			break;
		}

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
		if (Utilities.victory == true) {
			textInstance.GetComponent<TextMesh> ().text = "next";
			textInstance.name = "next";	
		} else {
			textInstance.GetComponent<TextMesh> ().text = "retry";
			textInstance.name = "retry";	
		}

		textInstance = Instantiate (aftermatchPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
		textInstance.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
		textInstance.transform.position = new Vector3 (parent.transform.position.x + 2.2f, parent.transform.position.y - 1, parent.transform.position.z);
		textInstance.GetComponent<TextMesh>().text = "QUIT";
		textInstance.name = "quit";
	}
	
	// Update is called once per frame
	void Update () {
	}

}
