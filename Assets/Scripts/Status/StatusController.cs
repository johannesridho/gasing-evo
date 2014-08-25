using UnityEngine;
using System.Collections;

public class StatusController : MonoBehaviour {
	
	private Gasing gasing;
	private float timeCounter;
	private float timeLimit;
	private static float default_timeLimit = 5f;
	
	public string statusName;
	public bool isStatusApplied;

	void Start () {
		timeCounter = 0;
		timeLimit = default_timeLimit;
		if(!gasing){
			gasing = GetComponent<Gasing>();
		}	
		statusName = "";
		isStatusApplied = false;
	}

	void Update () {
		timeCounter += Time.deltaTime;
		if (timeCounter >= timeLimit) {
			removeStatus();
			timeCounter = 0f;
		}	
	}
	
	public void applyStatus (string _name, float _time) {
		timeLimit = _time;
		statusName = _name;
		gameObject.AddComponent(statusName);
		isStatusApplied = true;
	}
	
	public void removeStatus () {
		timeLimit = default_timeLimit;
		Destroy(gameObject.GetComponent(statusName));
		statusName = "";
		isStatusApplied = false;
	}
}
