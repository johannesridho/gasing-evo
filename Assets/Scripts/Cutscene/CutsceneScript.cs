using UnityEngine;
using System.Collections;

public class CutsceneScript : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	public GameObject caster;
    public GameObject target;

    protected void Awake()
    {
    	caster = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.playerGasing), new Vector3(-1.061489f, 12.74201f, -2.033135f), Quaternion.Euler(270, 0, 0));
    	caster.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    	caster.name = "Caster";
    	/*caster2 = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.playerGasing), new Vector3(49.98103f, 2.399985f, -50.14171f), Quaternion.Euler(270, 0, 0));
    	caster2.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    	caster2.name = "CasterMid";
    	caster3 = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.playerGasing), new Vector3(-44.28552f, -2.999988f, -30.77173f), Quaternion.Euler(270, 0, 0));
    	caster3.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    	caster3.name = "CasterEnd";*/

    	target = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.ultiTarget), new Vector3(-0.004385955f, 0.5325689f, 0.01271754f), Quaternion.Euler(270, 0, 0));
    	target.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    	target.name = "Target";
    	
    	/*caster2.transform.parent = GameObject.Find("MovingParticle").transform;
    	caster2.transform.position = new Vector3(0, 0, 0);
    	caster3.transform.parent = GameObject.Find("AttackParticle").transform;
    	caster3.transform.position = new Vector3(0, 0, 0);*/
    	target.transform.parent = GameObject.Find("TargetParent").transform;
    	target.transform.position = new Vector3(0, 0, 0);

    }

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
		caster.transform.position = new Vector3(49.98103f, 2.399985f, -50.14171f);
		caster.transform.parent = GameObject.Find("MovingParticle").transform;
		GameObject.Find ("MovingParticle").animation.Play ();
		Invoke("ZoomCamera", 5);
	}

	void AnimateAttack() {
		camera3.enabled = true;
		camera2.enabled = false;
		caster.transform.position = new Vector3(-44.28552f, -2.999988f, -30.77173f);
		caster.transform.parent = GameObject.Find("AttackParticle").transform;
		GameObject.Find ("AttackParticle").animation.Play ();
		GameObject.Find ("TargetParent").animation.Play ();
	}

	void ZoomCamera(){
		camera3.transform.position = new Vector3(-31.0f, 1.1f, -67.127f);
		Invoke("ExitScene",2.3f);
	}

	void ExitScene() {
		Destroy(GameObject.Find("Caster"));
		Destroy(GameObject.Find("Target"));
		Destroy(GameObject.Find("TargetParent"));
		Destroy(GameObject.Find("Particle System"));
		Destroy(GameObject.Find("Particle System2"));
		Destroy(GameObject.Find("MovingParticle"));
		Destroy(GameObject.Find("AttackParticle"));
		Destroy(GameObject.Find("ChargeCamera"));
		Destroy(GameObject.Find("StartCamera"));
		Destroy(GameObject.Find("AttackCamera"));

		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
		}
	}

	void Update (){
	}
}
