using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour {

	public GameObject pemain;
	public GameObject musuh;
	public Gasing gasingPemain;
	public int jumlahMusuh;

	void Awake(){
//		jumlahMusuh = 3;
		if (!pemain) {
			pemain = GameObject.Find ("Pemain");
			if(!gasingPemain){
				gasingPemain = pemain.GetComponent<Gasing>();
			}
		}
		if (!musuh) {
			musuh = GameObject.Find ("Musuh");
			for (int i = 1; i < jumlahMusuh; i++) {
				if(i<=4){
					Instantiate(musuh, new Vector3(i * (-5), 1, 10), Quaternion.Euler(270,0,0));
				}else if(i<=7){
					Instantiate(musuh, new Vector3((i-4) * 5, 1, 10), Quaternion.Euler(270,0,0));
				}else if(i<=11){
					Instantiate(musuh, new Vector3((i-7) * 5, 1, -10), Quaternion.Euler(270,0,0));
				}else if(i<=14){
					Instantiate(musuh, new Vector3((i-11) * (-5), 1, -10), Quaternion.Euler(270,0,0));
				}else if(i<=17){
					Instantiate(musuh, new Vector3((i-14) * 5, 1, -15), Quaternion.Euler(270,0,0));
				}else if(i<=20){
					Instantiate(musuh, new Vector3((i-17) * (-5), 1, -15), Quaternion.Euler(270,0,0));
				}

			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gasingPemain.getEP()<=0 || GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
			Application.LoadLevel("GameOver");
		}
	}
}
