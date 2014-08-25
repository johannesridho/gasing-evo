using UnityEngine;
using System.Collections;

public class IceFieldController : StoneFieldController {

	private float clock;

	void Awake(){
		base.Awake ();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject[] allies = null;
		if (Utilities.chosenMode == 1) {		//team mode
			allies = GameObject.FindGameObjectsWithTag ("Ally");
		}
        if (!GamePrefs.isMultiplayer || (GamePrefs.isMultiplayer && Network.isServer))
        {
            foreach (GameObject player in players)
            {
                Gasing gasing = player.GetComponent<Gasing>();
                if (gasing)
                    gasing.setSpeed(gasing.getSpeed() + 3);		//tambahin speed tiap gasing
            }
        }
        if (!GamePrefs.isMultiplayer)
        {
            foreach (GameObject enemy in enemies)
            {
                Gasing gasing = enemy.GetComponent<Gasing>();
                gasing.setSpeed(gasing.getSpeed() + 3);
            }
			if (Utilities.chosenMode == 1) {		//team mode
				foreach (GameObject ally in allies)
				{
					Gasing gasing = ally.GetComponent<Gasing>();
					gasing.setSpeed(gasing.getSpeed() + 3);
				}
			}
        }
		clock = 0f;
	}

	void Update(){

        if (!paused) {
    		base.Update ();
    		clock += Time.deltaTime;
            if (!GamePrefs.isMultiplayer || (GamePrefs.isMultiplayer && Network.isServer))
            {
                if (clock >= 5)
                {
                    float x = Random.Range(-25.0F, 25.0F);
                    float z = Random.Range(-25.0F, 25.0F);
                    if (GamePrefs.isMultiplayer)
                    {
    					Network.Instantiate(Resources.Load("Prefab/Prefab Obstacle/Ice Block"), new Vector3(x, 20, z), Quaternion.Euler(0, 0, 0), 10);
                    }
                    else
                    {
    					Instantiate(Resources.Load("Prefab/Prefab Obstacle/Ice Block"), new Vector3(x, 20, z), Quaternion.Euler(0, 0, 0));
                    }
                    clock = 0;
                }
            }
        }
	}
}
