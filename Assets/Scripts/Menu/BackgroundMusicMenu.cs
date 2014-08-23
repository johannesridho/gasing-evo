using UnityEngine;
using System.Collections;

public class BackgroundMusicMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject music = GameObject.Find ("Background Music");		
		DontDestroyOnLoad (music);		//biar di scene menu background music tetap hidup
//		if (!Application.loadedLevelName.Contains("Menu")) {
//			Destroy(music);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
