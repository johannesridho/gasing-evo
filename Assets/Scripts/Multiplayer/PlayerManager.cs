using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public MPPlayer thisPlayer
    {
        get
        {
            return MultiplayerManager.getMPPlayer(networkView.owner);
        }
    }

    //gasing controller
    //public PlayerController controller;
    public Transform gasingTransform;
    public Gasing gasing;

    //properti gasing
    public float energiPoint;
    public float skillPoint;
    public float mass;
    public float power;
    public float speed;
    public float speedMax;

    public Vector3 currentPosition;
    public Quaternion currentRotation;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        thisPlayer.playerManager = this;
        //thisPlayer = MultiplayerManager.getMPPlayer(networkView.owner);
        gasingTransform.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        //gasing = gasingTransform.GetComponent<Gasing>();
        //Debug.Log("Energy " + gasing.energiPoint);
        //Debug.Log(gasingTransform.GetComponent<Gasing>().energiPoint);
        if (networkView.isMine)
        {
            currentPosition = gasingTransform.position;
            currentRotation = gasingTransform.rotation;

            //energiPoint = gasingTransform.GetComponent<Gasing>().energiPoint;
            //skillPoint = gasingTransform.GetComponent<Gasing>().skillPoint;
        }
        else
        {
            gasingTransform.position = currentPosition;
            gasingTransform.rotation = currentRotation;

            //gasingTransform.GetComponent<Gasing>().energiPoint = energiPoint;
            //gasingTransform.GetComponent<Gasing>().skillPoint = skillPoint;
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

    [RPC]
    public void server_handleDamage(int damage)
    {
        // Do something


    }

    public void handleDamage(int damage)
    {
        if (Network.isServer)
        {
            server_handleDamage(damage);
        }
        else
        {
            networkView.RPC("server_handleDamage", RPCMode.Server, damage);
        }
    }

    /*
     * Called when a player dies
     */
    [RPC]
    public void client_playerDied()
    {
        gasingTransform.gameObject.SetActive(false);
    }

    /*
     * Called when a player comes back to live
     */
    [RPC]
    public void client_playerAlive()
    {
        Debug.Log("client_playerAlive");
        gasingTransform.gameObject.SetActive(true);
    }
}
