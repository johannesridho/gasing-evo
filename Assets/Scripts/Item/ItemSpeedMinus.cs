using UnityEngine;
using System.Collections;

public class ItemSpeedMinus : Item {
	
	private static float _SPEEDMINUS = 10;
	
	void Awake(){
		base.init();
		//		renderer.material.shader = Shader.Find("Specular");
		//		renderer.material.SetColor("_Color", Color.red);
	}
	
	void OnCollisionEnter(Collision col){
		base.handleCollision();
		base.destroyOnCollide(col);
		speedMinus(col);
	}
	
	void speedMinus(Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			col.collider.SendMessage("speedMaxChange", _SPEEDMINUS, SendMessageOptions.DontRequireReceiver);
		}
	}
}