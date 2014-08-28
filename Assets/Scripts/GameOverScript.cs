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
		case "Gladiator":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/sky5x/sky5x_skyboxes/sky5x3");
			break;		
		case "Steel Arena":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepSpaceRed/DSR");
			break;		
		case "Gasing Evo Arena":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepSpaceGreen/DSG");
			break;		
		case "Nebula":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepsSpaceRedWithPlanet/DSRWP");
			break;		
		case "Explode Arena":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepSpaceGreen/DSG");
			break;		
		case "Space":
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepSpaceGreen/DSG");
			break;		
		default:
			RenderSettings.skybox = (Material) Resources.Load("Skybox/SkyBox Volume 2/DeepSpaceBlue/DSB");
			break;
		}

        if (GamePrefs.isMultiplayer)
        {
            GameObject.Find("GameOverText").GetComponent<TextMesh>().text = PlayerPrefs.GetString("MP Winner")+ " WINS THE GAME";
        }
        else
        {
            if (Utilities.victory == true)
            {
                GameObject.Find("GameOverText").GetComponent<TextMesh>().text = "YOU WIN";
            }
            else
            {
                GameObject.Find("GameOverText").GetComponent<TextMesh>().text = "YOU LOSE";
            }
        }


		if (Utilities.chosenMode == 1 || Utilities.chosenMode == 0) {
			setBattleModeContent ();
		} else {
			setStoryModeContent();
		}
	}

	void setBattleModeContent(){
		GameObject parent = GameObject.Find ("GameOverText");
        if (!GamePrefs.isMultiplayer)
        {
            textInstance = Instantiate(aftermatchPrefab, parent.transform.position, parent.transform.rotation) as GameObject;
            textInstance.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            textInstance.transform.position = new Vector3(parent.transform.position.x - 3, parent.transform.position.y - 1, parent.transform.position.z);
            textInstance.GetComponent<TextMesh>().text = "REMATCH";
            textInstance.name = "rematch";
        }

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
