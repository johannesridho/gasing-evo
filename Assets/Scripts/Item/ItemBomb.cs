using UnityEngine;
using System.Collections;

public class ItemBomb : Item {
	
	private static float _DAMAGE = 50;
	
	void Awake(){
		base.init();
//		renderer.material.shader = Shader.Find("Specular");
//		renderer.material.SetColor("_Color", Color.red);
	}

	void OnCollisionEnter(Collision col){
        base.handleCollision();
        base.destroyOnCollide(col);
		damage(col);
	}
	
	void damage(Collision col) {
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
			col.collider.SendMessage("EPKurang", _DAMAGE, SendMessageOptions.DontRequireReceiver);
		}
	}
}