using UnityEngine;
using System.Collections;

public class ItemSpeedPlus : Item {
	
	private static float _SPEEDPLUS = 60;
	
	void Awake(){
		base.init();
		//		renderer.material.shader = Shader.Find("Specular");
		//		renderer.material.SetColor("_Color", Color.red);
	}
	
	void OnCollisionEnter(Collision col){
		base.destroyOnCollide(col);
		speedPlus(col);
	}
	
	void speedPlus(Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			col.collider.SendMessage("speedMaxChange", _SPEEDPLUS, SendMessageOptions.DontRequireReceiver);
		}
	}
}