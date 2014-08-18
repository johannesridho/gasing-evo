using UnityEngine;
using System.Collections;

/*
 * Mirip kelas PlayerManager
 */
public class Server_Gasing : MonoBehaviour
{
    public Transform gasingTransform;
    public Gasing gasing;

    public Vector3 currentPosition;
    public Quaternion currentRotation;

    public NetworkPlayer networkPlayer;

    private bool isDeathMessageSent = false;

    private bool isAlive = true;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gasingTransform.gameObject.SetActive(false);
        ////gasingTransform.rigidbody.detectCollisions = false;
        ////gasingTransform.rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        if (networkView.isMine)
        {
            currentPosition = gasingTransform.position;
            currentRotation = gasingTransform.rotation;
            if (gasing.energiPoint <= 0)
            {
                // DEAD
                if (!isDeathMessageSent)
                {
                    MultiplayerManager.instance.networkView.RPC("client_playerDead", RPCMode.All, networkPlayer);
                    isDeathMessageSent = true;
                }
                isAlive = false;
            }
        }
        else
        {
            gasingTransform.position = currentPosition;
            gasingTransform.rotation = currentRotation;
        }
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref currentPosition);
            stream.Serialize(ref currentRotation);

            stream.Serialize(ref gasingTransform.GetComponent<Gasing>().energiPoint);
            stream.Serialize(ref gasingTransform.GetComponent<Gasing>().skillPoint);
        }
        else
        {
            stream.Serialize(ref currentPosition);
            stream.Serialize(ref currentRotation);

            stream.Serialize(ref gasingTransform.GetComponent<Gasing>().energiPoint);
            stream.Serialize(ref gasingTransform.GetComponent<Gasing>().skillPoint);
        }
    }

    /*
    * Called when a player dies
     * unused
    */
    [RPC]
    public void client_playerDied()
    {
        //gasingTransform.gameObject.SetActive(false);
        Debug.Log("player DEAD!");
    }

    /*
     * Called when a player comes back to live
     */
    [RPC]
    public void client_playerAlive()
    {
        Debug.Log("client_playerAlive");
        gasingTransform.gameObject.SetActive(true);
        //gasingTransform.rigidbody.detectCollisions = true;
        //gasingTransform.rigidbody.useGravity = true;
    }
}
