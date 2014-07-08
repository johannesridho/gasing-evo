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

    public Vector3 currentPosition;
    public Quaternion currentRotation;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        thisPlayer.playerManager = this;
        //thisPlayer = MultiplayerManager.getMPPlayer(networkView.owner);
        gasingTransform.gameObject.SetActive(false);
        Debug.Log("started");
    }

    void FixedUpdate()
    {
        if (networkView.isMine)
        {
            currentPosition = gasingTransform.position;
            currentRotation = gasingTransform.rotation;
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
        }
        else
        {
            stream.Serialize(ref currentPosition);
            stream.Serialize(ref currentRotation);
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
