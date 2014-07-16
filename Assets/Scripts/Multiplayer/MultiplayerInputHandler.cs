using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerInputHandler : MonoBehaviour
{
    public Client_Gasing client_gasing;

    public static MultiplayerInputHandler instance;

    public Transform followPoint;

    // Use this for initialization
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        followPoint = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        instance = this;
        if (MultiplayerManager.instance.isGameStarted)
        {
            if (Network.isServer)
            {
                // server side

                // do calculation for server's gasing (as a player)

                float hor = 0f;
                float ver = 0f;
                //if (gasing.isOnGround && !gasing_pt.isInvicibleAfterClash) {
                if (client_gasing.isOnGround)
                {
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        hor = Input.acceleration.x * Gasing.COEF_SPEED * client_gasing.speed;
                        ver = Input.acceleration.y * Gasing.COEF_SPEED * client_gasing.speed;
                    }
                    else
                    {
                        hor = Input.GetAxis("Horizontal") * Gasing.COEF_SPEED * client_gasing.speed;
                        ver = Input.GetAxis("Vertical") * Gasing.COEF_SPEED * client_gasing.speed;
                    }
                }
                Vector3 movement = new Vector3(hor, 0f, ver);

                server_addForce(Network.player, movement * 2000 * Time.deltaTime);

                //send update to all player
                for (int i = 0; i < MultiplayerManager.instance.serverSideGasings.Count; i++)
                {
                    if (MultiplayerManager.instance.playerList[i].playerNetwork == Network.player)
                    {
                        client_gasing.speed = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().speed;
                        client_gasing.isOnGround = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().isOnGround;
                    }
                    else
                    {
                        networkView.RPC("client_sendGasingUpdate", MultiplayerManager.instance.playerList[i].playerNetwork, MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().speed, MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().isOnGround);
                    }
                }
            }
            else
            {
                // client side

                // do calculation
                float hor = 0f;
                float ver = 0f;
                //if (gasing.isOnGround && !gasing_pt.isInvicibleAfterClash) {
                if (client_gasing.isOnGround)
                {
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        hor = Input.acceleration.x * Gasing.COEF_SPEED * client_gasing.speed;
                        ver = Input.acceleration.y * Gasing.COEF_SPEED * client_gasing.speed;
                    }
                    else
                    {
                        hor = Input.GetAxis("Horizontal") * Gasing.COEF_SPEED * client_gasing.speed;
                        ver = Input.GetAxis("Vertical") * Gasing.COEF_SPEED * client_gasing.speed;
                    }
                }
                Vector3 movement = new Vector3(hor, 0f, ver);
                networkView.RPC("server_addForce", RPCMode.Server, Network.player, movement * 2000 * Time.deltaTime);
            }
        }
    }

    [RPC]
    public void server_addForce(NetworkPlayer player, Vector3 force)
    {
        //MultiplayerManager.instance.getGasingOwnedByPlayer(player).rigidbody.AddForce(force);
        MultiplayerManager.instance.getGasingOwnedByPlayer(player).GetComponent<Server_Gasing>().gasingTransform.rigidbody.AddForce(force);
    }

    [RPC]
    public void client_sendGasingUpdate(float speed, bool isOnGround)
    {
        client_gasing.speed = speed;
        client_gasing.isOnGround = isOnGround;
    }
}
