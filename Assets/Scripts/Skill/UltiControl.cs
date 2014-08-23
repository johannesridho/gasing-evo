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
		}
		if (ultiActive) {
			timeCount += Time.deltaTime;
			if (timeCount >= 2f) {
				isCanUlti = false;
				timeCount = 0f;
			}
		}
	}
}
