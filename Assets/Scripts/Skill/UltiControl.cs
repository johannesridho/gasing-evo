using UnityEngine;
using System.Collections;

public class UltiControl : MonoBehaviour {
	
	public bool isCanUlti;
	private bool ultiActive;
	private Gasing gasing;
	private float timeCount;
	public float temp;
	public float timeUltiActive = 3f;

	private bool ultiCondition () {
		return gasing.energiPoint <= gasing.energiPointMax * 0.75;
	}

	void Start () {
		if(!gasing)
			gasing = GetComponent<Gasing>();
		timeCount = 0f;
		isCanUlti = false;
		ultiActive = false;
	}

	void Update () {
		temp = gasing.energiPoint / gasing.energiPointMax;
		if (ultiCondition() && !ultiActive) {
			isCanUlti = true;
			ultiActive = true;
		}
		if (ultiActive) {
			timeCount += Time.deltaTime;
			if (timeCount >= timeUltiActive) {
				isCanUlti = false;
				timeCount = 0f;
			}
		}
	}

	void OnGUI(){
		if(isCanUlti){
			GUIStyle style = new GUIStyle(GUI.skin.box);
			if (GUI.Button(new Rect(Screen.width * 1/2 - 150, 20, 300, 30), "Ultimate Skill Now!!!", style)) {

			}
		}
	}
}
