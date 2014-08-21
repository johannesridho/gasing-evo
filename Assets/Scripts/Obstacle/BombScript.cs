using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{

    private GameObject targetEnemy;
    private GameObject self;
    public GameObject efekLedakan;
    private float clock;
    private bool on;

    void Awake()
    {
        clock = 0f;
        //taking the target:
        //		targetEnemy = findNearestEnemy();
        targetEnemy = null;
        on = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (on)
        {
            if (!targetEnemy)
            {
                targetEnemy = GameObject.FindGameObjectWithTag("Enemy");
            }
            clock += Time.deltaTime;
            if (clock >= 5)
            {
                if (GamePrefs.isMultiplayer)
                {
                    Network.Destroy(this.gameObject);
                    networkView.RPC("client_bombtriggerLedakan", RPCMode.All);
                }
                else
                {
                    Destroy(this.gameObject);
                    Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (on && targetEnemy)
        {
            transform.position += (targetEnemy.transform.position - transform.position).normalized * 30 * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject != self) && (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally"))
        {
            if (GamePrefs.isMultiplayer)
            {
                networkView.RPC("client_bombtriggerLedakan", RPCMode.All);
            }
            else
            {
                Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    public void nyalakan(GameObject _self, GameObject _targetTag)
    {
        targetEnemy = _targetTag;
        self = _self;
        on = true;
    }

    [RPC]
    public void client_bombtriggerLedakan()
    {
        Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
    }

}//end class
