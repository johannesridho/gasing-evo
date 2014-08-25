using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	void Awake(){
		GameObject music = GameObject.Find ("Background Music");
		if(!music){
			GameObject menuMusic = (GameObject) Instantiate(Resources.Load("Prefab/BackgroundMusicMenu"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
			menuMusic.name = "Background Music";
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
