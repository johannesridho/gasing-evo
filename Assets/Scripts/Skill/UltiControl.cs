using UnityEngine;
using System.Collections;

public class UltiControl : MonoBehaviour {
	
	public bool isCanUlti;
	private bool ultiActive;
	private Gasing gasing;
	private float timeCount;
	public float temp;

	void Start () {
		if(!gasing)
			gasing = GetComponent<Gasing>();
		timeCount = 0f;
		isCanUlti = false;
		ultiActive = false;
	}

	void Update () {
		temp = gasing.energiPoint / gasing.energiPointMax;
		if ((gasing.energiPoint <= gasing.energiPointMax * 0.25) && !ultiActive) {
			isCanUlti = true;
			ultiActive = true;
			GetComponent<SkillController>().startUltiCountdown();
		}
		if (ultiActive) {
			timeCount += Time.deltaTime;
			if (timeCount >= 2f) {
				isCanUlti = false;
				timeCount = 0f;
			}
		}
	}

	void OnGUI(){
		if(isCanUlti){
			GUIStyle style = new GUIStyle(GUI.skin.box);
			//		style.normal.background = skills.buttonSkill1;
			//		    if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), "tes", style))
			if (GUI.Button(new Rect(Screen.width * 1/2 - 150, 20, 300, 30), "You Can Use Ultimate Skill Now", style))
			{

			}
		}
	}
}
