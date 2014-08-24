using UnityEngine;
using System.Collections;

public class CutsceneScript : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	// Use this for initialization
	void Start () {
		camera3.enabled = false;
		camera2.enabled = false;
		camera1.enabled = true;
		Invoke("AnimateEntrance", 3);
		Invoke("AnimateAttack", 5);
	}

	void AnimateEntrance() {
		camera1.enabled = false;
		camera2.enabled = true;
		GameObject.Find ("MovingArjuna").animation.Play ();
		Invoke("ZoomCamera", 5);
		Debug.Log ("yeah");
	}

	void AnimateAttack() {
		camera3.enabled = true;
		camera2.enabled = false;
		GameObject.Find ("AttackArjuna").animation.Play ();
		GameObject.Find ("Dipati").animation.Play ();
		Debug.Log ("naisuuuuuuuuuuuuuuuuuuu");
	}

	void ZoomCamera(){
		camera3.transform.position = new Vector3(-31.0f, 1.1f, -67.127f);
		Invoke("ExitScene",3.8f);
	}

	void ExitScene() {
		Destroy(camera1);
		Destroy(camera2);
		Destroy(camera3);
		Destroy(GameObject.Find("Arjuna"));
		Destroy(GameObject.Find("Directional Light"));
		Destroy(GameObject.Find("Particle System"));
		Destroy(GameObject.Find("MovingArjuna"));
		Destroy(GameObject.Find("Dipati"));
		Destroy(GameObject.Find("AttackArjuna"));
	}

	void Update (){
	}
}
