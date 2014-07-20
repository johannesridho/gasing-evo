using UnityEngine;
using System.Collections;

public class DrumSlideController : StoneFieldController {
	
	private float clock;
		
	void Update(){
		base.Update ();
		clock += Time.deltaTime;
		
		if (clock >= 2) {
			float x = Random.Range(-10.0F, 10.0F);
			Instantiate(listObstacle[0], new Vector3(x, 50, 57), Quaternion.Euler(90, -90, 0));
			clock = 0;
		}
	}
}
