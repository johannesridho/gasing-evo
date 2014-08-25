using UnityEngine;
using System.Collections;

public class SpaceController : StoneFieldController {
	
	private float clock;
		
	void Update(){
		if (!paused) {
			base.Update ();

	        if (!GamePrefs.isMultiplayer || (GamePrefs.isMultiplayer && Network.isServer))
	        {
	            clock += Time.deltaTime;

	            if (clock >= 2)
	            {                
					float x2 = Random.Range(-25.0F, 25.0F);
					float z2 = Random.Range(-25.0F, 25.0F);
					if (GamePrefs.isMultiplayer)
					{
						Network.Instantiate(Resources.Load("Prefab/Prefab Obstacle/Bomb Obstacle"), new Vector3(x2, 20, z2), Quaternion.Euler(0, 0, 0), 10);
					}
					else
					{
						Instantiate(Resources.Load("Prefab/Prefab Obstacle/Bomb Obstacle"), new Vector3(x2, 20, z2), Quaternion.Euler(0, 0, 0));
					}

	                clock = 0;
	            }
	        }
	    }
	}
}
