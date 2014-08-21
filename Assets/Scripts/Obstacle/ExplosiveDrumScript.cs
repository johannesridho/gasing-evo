using UnityEngine;
using System.Collections;

public class ExplosiveDrumScript : MonoBehaviour
{
    public GameObject efekLedakan;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally")
        {
            if (GamePrefs.isMultiplayer)
            {
                networkView.RPC("client_explosiveDrumtriggerLedakan", RPCMode.All);
            }
            else
            {
                Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    [RPC]
    public void client_explosiveDrumtriggerLedakan()
    {
        Instantiate(efekLedakan, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
