using UnityEngine;
using System.Collections;

public class BombObstacle : MonoBehaviour {
	
	public GameObject efekLedakan;
	private float clock;

	void Awake() {
		clock = 0f;
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		clock += Time.deltaTime;
		if (clock >= 5) {
            if (GamePrefs.isMultiplayer)
            {
                Network.Destroy(this.gameObject);
                networkView.RPC("client_bombObstriggerLedakan", RPCMode.All);
            }
            else
            {
                Destroy(this.gameObject);
                Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
            }
		}
	}

	void FixedUpdate () {

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally") {
            if (GamePrefs.isMultiplayer)
            {
                networkView.RPC("client_bombObstriggerLedakan", RPCMode.All);
            }
            else
            {
                Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
            }
		}
	}

    //efek ledakan diinstantiate lewat RPC
    [RPC]
    public void client_bombObstriggerLedakan()
    {
        Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
