using UnityEngine;
using System.Collections;

public class ItemHeal : Item {

	private static float _HEAL = 100;
	
	void Awake(){
		base.init();
//		renderer.material.shader = Shader.Find("Specular");
//		renderer.material.SetColor("_Color", Color.green);
	}

	void OnCollisionEnter(Collision col){
        base.handleCollision();
        base.destroyOnCollide(col);
		heal(col);
	}

	void heal(Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			col.collider.SendMessage("EPTambah", _HEAL, SendMessageOptions.DontRequireReceiver);
		}
	}
}