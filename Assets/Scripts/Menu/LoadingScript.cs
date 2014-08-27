using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {
	private float timer;
	private bool isFinishedLoading;

	// Use this for initialization
	void Start () {
		timer = 0;
		isFinishedLoading = false;

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

		switch(Utilities.storyModeLevel){
			case 1:
				gameObject.GetComponent<TextMesh>().text = "Level 1";
				break;
			case 2:
				gameObject.GetComponent<TextMesh>().text = "Level 2";
				break;
			case 3:
				gameObject.GetComponent<TextMesh>().text = "Level 3";
				break;
			case 4:
				gameObject.GetComponent<TextMesh>().text = "Level 4";
				break;
			case 5:
				gameObject.GetComponent<TextMesh>().text = "Level 5";
				break;
			case 6:
				gameObject.GetComponent<TextMesh>().text = "Level 6";
				break;
			case 7:
				gameObject.GetComponent<TextMesh>().text = "Level 7";
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isFinishedLoading) {
			timer += Time.deltaTime;	

			if(timer > 2){
				isFinishedLoading = true;
			} 	
		} else {
			Application.LoadLevel(Utilities.chosenArena);
		}
	}
}

