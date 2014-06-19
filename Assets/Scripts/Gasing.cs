using UnityEngine;
using System.Collections;

public class Gasing : MonoBehaviour {

	public float energiPointMax;
	public float skillPointMax;

	//ntar di-private
	//private float energiPoint;
	//private float skillPoint;
	public float energiPoint;
	public float skillPoint;

	// state
	public bool isOnGround;

	void Awake(){
//		energiPointMax = 50;
//		skillPointMax = 50;
	}

	// Use this for initialization
	void Start () {	
		energiPoint = energiPointMax;
		skillPoint = skillPointMax;
		isOnGround = true;
	}
	
	// Update is called once per frame
	void Update () {
		EPKurang(Time.deltaTime);		//kurangi EP tiap detik

		if(energiPoint <=0 ){
			//gasing berhenti
			//Debug.Log("wah");
			Destroy (transform.root.gameObject); 
		}
	}
	
	void EPKurang(float dmg){
		energiPoint -= dmg;
	}
	
	void EPTambah(float n){
		energiPoint += n;
	}

	void OnCollisionEnter(Collision col){
		if ((col.gameObject.name == "Musuh")||(col.gameObject.name == "Pemain")) {
			col.collider.SendMessage ("EPKurang", 10.0, SendMessageOptions.DontRequireReceiver);	//trigger objek yg ditabrak buat eksekusi method EPKurang()
		} else if (col.gameObject.name == "Tanah") {
			this.isOnGround = true;
		}
	}

	public void setEPMax(float ep){
		energiPointMax = ep;
	}
	
	public void setSPMax(float sp){
		skillPointMax = sp;
	}
	
	public float getEPMax(){
		return energiPointMax;
	}
	
	public float getSPMax(){
		return skillPointMax;
	}

	public void setEP(float ep){
		energiPoint = ep;
	}

	public void setSP(float sp){
		skillPoint = sp;
	}

	public float getEP(){
		return energiPoint;
	}

	public float getSP(){
		return skillPoint;
	}
	
}//end class
