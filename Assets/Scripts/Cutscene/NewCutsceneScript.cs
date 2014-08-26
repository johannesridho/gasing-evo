using UnityEngine;
using System.Collections;

public class NewCutsceneScript : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	public GameObject[] enemies;
	public GameObject player;
	public string playerGasing;
	public string chosenArena;

	public GameObject caster;
	public GameObject instantiation;
	public bool centering = false;

    protected void Awake()
    {
    	enemies = GameObject.FindGameObjectsWithTag("Enemy");
    	player = GameObject.Find("Pemain");
    	playerGasing = Utilities.playerGasing;
    	chosenArena = Utilities.chosenArena;


    	/*if (Utilities.playerGasing == "Colonix") {

    	} else if (Utilities.playerGasing == "Craseed") {

    	} else if (Utilities.playerGasing == "Legasic") {

    	} else if (Utilities.playerGasing == "Prototype") {

    	} else if (Utilities.playerGasing == "Skymir") {

    	} else {
    		Debug.Log("Gasing Unknown");
    	}
    	*/

    	
    }

	// Use this for initialization
	void Start () {
		AnimateCasting();
		Invoke("AnimateUlti", 5);
	}

	void AnimateCasting(){
		GameObject.Find("Particle System").transform.position = player.transform.position;
		GameObject.Find("Particle System2").transform.position = player.transform.position;
		//camera1.transform.position = new Vector3(player.transform.position.x, player.transform.position.y +4, player.transform.position.z -7);
		centering = true;
		//camera1.transform.rotation = Quaternion.Euler(10, 0, 0);
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		//caster.transform.position = new Vector3(-1.061489f, 12.74201f, -2.033135f);		
	}

	void AnimateUlti() {
		centering = false;
		camera1.enabled = false;
		camera2.transform.position = new Vector3(-0.4496078f, 23.37582f, -39.01122f);
		camera2.transform.rotation = Quaternion.Euler(43.52179f, 0, 0);
		camera2.enabled = true;
		if (playerGasing == "Colonix") {
			instantiation = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Obstacle/Eternal Light"), player.transform.position, Quaternion.Euler(-90, 0, 0));
			instantiation.tag = "CutsceneSprite";
			foreach (GameObject enemy in enemies) {
				instantiation = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Obstacle/Flame Enchant"), enemy.transform.position, Quaternion.Euler(-90, 0, 0));
				instantiation.tag = "CutsceneSprite";
				instantiation = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Obstacle/Big Bang"), enemy.transform.position, Quaternion.Euler(0, 0, 0));
				instantiation.tag = "CutsceneSprite";
			}
    	} else if (playerGasing == "Craseed") {
    		 
    	} else if (playerGasing == "Legasic") {
    		  
    	} else if (playerGasing == "Prototype") {
    		  
    	} else if (playerGasing == "Skymir") {
    		  
    	} else {
    		Debug.Log("Gasing Unknown");
    	}

    	Invoke("ExitScene",7);
	}

	void ExitScene() {

		Destroy(GameObject.Find("Caster"));
		GameObject[] cutsceneSprites = GameObject.FindGameObjectsWithTag("CutsceneSprite");

		foreach (GameObject cutsceneSprite in cutsceneSprites) {
			Destroy(cutsceneSprite);
		}

		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
			if (go.GetComponent<HealthBar>())
				go.GetComponent<HealthBar>().isAvailable = true;
		}

		GameObject.Find("Pemain").GetComponent<SkillController>().isAvailable = true;
	}

	void Update (){
		if (centering) {
		    camera1.transform.LookAt(player.transform);
		    camera1.transform.Translate(Vector3.right * Time.deltaTime * 20);
		} 
	}
}
