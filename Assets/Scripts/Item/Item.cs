using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	private static float spin_velo = 100f;

	void Awake(){
		init();
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//called every fixed framerate frame
	void FixedUpdate () {		
		spin();
	}
	
	void OnCollisionEnter(Collision col){
		destroyOnCollide(col);
	}
	
	protected void init() {

	}
	
	protected void spin() {
		Vector3 rotation = new Vector3 (0f, spin_velo*Time.deltaTime, 0f);
		transform.Rotate(rotation);
	}

	protected void destroyOnCollide(Collision col) {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            Destroy(transform.root.gameObject);
            //Network.Destroy(transform.root.gameObject);
        }
	}

	public void selfDestroy () {
		Destroy(transform.root.gameObject);
	}

    protected void handleCollision()
    {
        //MultiplayerManager.instance.handleItemCollision(transform.root.gameObject);
        //Debug.Log("destroying " + transform.root.gameObject.name);
    }
}