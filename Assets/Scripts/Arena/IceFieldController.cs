using UnityEngine;
using System.Collections;

public class IceFieldController : StoneFieldController {

	void Awake(){
		base.Awake ();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject player in players){
			Gasing gasing = player.GetComponent<Gasing>();
			gasing.setSpeed(gasing.getSpeed()+10);		//tambahin speed tiap gasing
		}
		foreach(GameObject enemy in enemies){
			Gasing gasing = enemy.GetComponent<Gasing>();
			gasing.setSpeed(gasing.getSpeed()+3);
		}
	}

}
