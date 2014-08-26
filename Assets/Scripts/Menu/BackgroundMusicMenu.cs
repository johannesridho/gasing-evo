using UnityEngine;
using System.Collections;

public class BackgroundMusicMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		GameObject music = GameObject.Find ("Background Music");		
		DontDestroyOnLoad (this.gameObject);		//biar di scene menu background music tetap hidup
//		if (!Application.loadedLevelName.Contains("Menu")) {
//			Destroy(music);
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GamePrefs.isBGM == false){
			AudioListener.pause = true;
			AudioListener.volume = 0;
		}else{
			AudioListener.pause = false;
			AudioListener.volume = 1;
		}

	}
}
