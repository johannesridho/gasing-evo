using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerInputHandler : MonoBehaviour
{
    public Client_Gasing client_gasing;

    public static MultiplayerInputHandler instance;

    public Transform followPoint;

    public Texture blackScreenTexture;

   

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
            float hor = 0f;
            float ver = 0f;
            if (Network.isServer)
            {
                // server side
                // do calculation for server's gasing (as a player)
                if (!MultiplayerManager.instance.isDedicatedServer)
                {
                    if (client_gasing.isOnGround && !client_gasing.isInvicibleAfterClash)
                    {
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
                    }
                }
                //send update to all player
                for (int i = 0; i < MultiplayerManager.instance.serverSideGasings.Count; i++)
                {
                    if (MultiplayerManager.instance.playerList[i].playerNetwork == Network.player)
                    {
                        if (MultiplayerManager.instance.serverSideGasings[i] != null)
                        {
                            if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>() != null)
                            {
                                client_gasing.speed = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().speed;
                                client_gasing.isOnGround = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().isOnGround;
                                client_gasing.isInvicibleAfterClash = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<PhysicsTabrak>().isInvicibleAfterClash;
                                client_gasing.energiPoint = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().energiPoint;
                            }
                        }
                    }
                    else
                    {
                        if (MultiplayerManager.instance.serverSideGasings[i] != null)
                        {
                            float speed = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().speed;
                            bool isOnGround = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().isOnGround;
                            bool isInvicibleAfterClash = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<PhysicsTabrak>().isInvicibleAfterClash;
                            float energiPoint = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().energiPoint;
                            networkView.RPC("client_sendGasingUpdate", MultiplayerManager.instance.playerList[i].playerNetwork, speed, isOnGround, isInvicibleAfterClash, energiPoint);
                        }
                    }
                }
            }
            else
            {
                // client side


                // do calculation
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

                    Vector3 movement = new Vector3(hor, 0f, ver);
                    networkView.RPC("server_addForce", RPCMode.Server, Network.player, movement * 2000 * Time.deltaTime);
                }
            }




        }
    }

    void OnGUI()
    {
        if (MultiplayerManager.instance.isDedicatedServer && !Network.isServer && MultiplayerManager.instance.isGameStarted)
        {

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackScreenTexture);
            GUILayout.BeginArea(new Rect(5, 5, Screen.width - 10, Screen.height - 10));
            GUILayout.BeginVertical();
            GUI.contentColor = Color.white;
            GUILayout.Label("Energi = " + client_gasing.energiPoint);
            GUILayout.EndVertical();
            GUILayout.EndArea();

            

        }
    }

    [RPC]
    public void server_addForce(NetworkPlayer player, Vector3 force)
    {
        //MultiplayerManager.instance.getGasingOwnedByPlayer(player).rigidbody.AddForce(force);
        GameObject gasing = MultiplayerManager.instance.getGasingOwnedByPlayer(player);
        if (gasing != null)
        {
            gasing.GetComponent<Server_Gasing>().gasingTransform.rigidbody.AddForce(force);
        }
    }

    [RPC]
    public void client_sendGasingUpdate(float speed, bool isOnGround, bool isInvicibleAfterClash, float energyPoint)
    {
        client_gasing.speed = speed;
        client_gasing.isOnGround = isOnGround;
        client_gasing.isInvicibleAfterClash = isInvicibleAfterClash;
        client_gasing.energiPoint = energyPoint;
    }

    

    
}
