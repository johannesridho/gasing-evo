using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {
	private float timer;
	private bool isFinishedLoading;

	// Use this for initialization
	void Start () {
		timer = 0;
		isFinishedLoading = false;

		switch(Utilities.storyModeLevel){
			case 1:
				gameObject.GetComponent<TextMesh>().text = "Level 1";
				break;
			case 2:
				gameObject.GetComponent<TextMesh>().text = "Level 2";
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isFinishedLoading) {
			timer += Time.deltaTime;	

			if(timer > 5){
				isFinishedLoading = true;
			} 	
		} else {
			switch(Utilities.storyModeLevel){
				case 1:
				Application.LoadLevel("Ice Field");
				break;
				case 2:
				Application.LoadLevel("Space");
				break;
				case 3:
				Application.LoadLevel("Nebula");
				break;
			}
		}
	}
}
