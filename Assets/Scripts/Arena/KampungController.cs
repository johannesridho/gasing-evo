﻿using UnityEngine;
using System.Collections;

public class KampungController : StoneFieldController {
	
	private float clock;
		
	void Update(){
		base.Update ();

        if (!GamePrefs.isMultiplayer || (GamePrefs.isMultiplayer && Network.isServer))
        {
            clock += Time.deltaTime;

            if (clock >= 2)
            {
//                float x = Random.Range(-27.0F, 25.0F);
//                if (GamePrefs.isMultiplayer)
//                {
//					Network.Instantiate(Resources.Load("Prefab/Prefab Obstacle/Explosive Drum"), new Vector3(x, 50, 57), Quaternion.Euler(90, -90, 0), 10);
//                }
//                else
//                {
//					Instantiate(Resources.Load("Prefab/Prefab Obstacle/Explosive Drum"), new Vector3(x, 50, 57), Quaternion.Euler(90, -90, 0));
//                }

				float x2 = Random.Range(-25.0F, 25.0F);
				float z2 = Random.Range(-25.0F, 25.0F);
				if (GamePrefs.isMultiplayer)
				{
					Network.Instantiate(Resources.Load("Prefab/Prefab Obstacle/Stone Obstacle"), new Vector3(x2, 20, z2), Quaternion.Euler(0, 0, 0), 10);
				}
				else
				{
					Instantiate(Resources.Load("Prefab/Prefab Obstacle/Stone Obstacle"), new Vector3(x2, 20, z2), Quaternion.Euler(x2+z2, x2+z2, x2+z2));
				}

                clock = 0;
            }
        }
	}
}