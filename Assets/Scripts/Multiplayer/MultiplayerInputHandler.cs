using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerInputHandler : MonoBehaviour
{
    public Client_Gasing client_gasing;

    public static MultiplayerInputHandler instance;

    public Transform followPoint;

    public Texture blackScreenTexture;

    #region untuk GUI
    private float guiRatioX;
    private float guiRatioY;
    private float sWidth;
    private float sHeight;
    private Vector3 GUIsF;
    private int sizegui;
    #endregion

    #region health bar
    public float healthBarLength;
    public float skillBarLength;

    public Texture2D teksturHealth;
    public Texture2D teksturHealth2;

    public Texture2D teksturSkill;
    public Texture2D teksturSkill2;
    #endregion

    // Use this for initialization
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        followPoint = transform;

        //screen size
        //get the screen's width
        sWidth = Screen.width;
        sHeight = Screen.height;
        //calculate the rescale ratio
        guiRatioX = sWidth / 1280;
        guiRatioY = sHeight / 720;
        //create a rescale Vector3 with the above ratio
        GUIsF = new Vector3(guiRatioX, guiRatioY, 1);

        //health bar
        teksturHealth = (Texture2D)Resources.Load("Health/HUD_health_04");
        teksturHealth2 = (Texture2D)Resources.Load("Health/HUD_health_04");
        teksturSkill = (Texture2D)Resources.Load("Health/HUD_health_04");
        teksturSkill2 = (Texture2D)Resources.Load("Health/HUD_health_04");

        healthBarLength = 1200;
        skillBarLength = 1200;
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
                    if (!client_gasing.isInvicibleAfterClash)
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
                        if (MultiplayerManager.instance.isAllPlayerReady)
                        {
                            if (MultiplayerManager.instance.serverSideGasings[i] != null)
                            {
                                if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>() != null)
                                {
                                    float speed = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().speed;
                                    bool isOnGround = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().isOnGround;
                                    bool isInvicibleAfterClash = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<PhysicsTabrak>().isInvicibleAfterClash;
                                    float energiPoint = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().energiPoint;
                                    float skillPoint = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().skillPoint;
                                    float energiPointMax = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().energiPointMax;
                                    float skillPointMax = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<Gasing>().skillPointMax;
                                    networkView.RPC("client_sendGasingUpdate", MultiplayerManager.instance.playerList[i].playerNetwork, speed, isOnGround, isInvicibleAfterClash, energiPoint, skillPoint, energiPointMax, skillPointMax);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // client side


                // do calculation
                if (!client_gasing.isInvicibleAfterClash)
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

    void LateUpdate()
    {
        //health bar
        healthBarLength = client_gasing.energiPoint / client_gasing.energiPointMax * 1200;		//update terus panjang bar
        skillBarLength = client_gasing.skillPoint / client_gasing.skillPointMax * 1200;
    }

    void OnGUI()
    {
        if (MultiplayerManager.instance.isDedicatedServer && !Network.isServer && MultiplayerManager.instance.isGameStarted)
        {
            GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
            



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
    public void client_sendGasingUpdate(float speed, bool isOnGround, bool isInvicibleAfterClash, float energyPoint, float skillPoint, float energyPointMax, float skillPointMax)
    {
        client_gasing.speed = speed;
        client_gasing.isOnGround = isOnGround;
        client_gasing.isInvicibleAfterClash = isInvicibleAfterClash;
        client_gasing.energiPoint = energyPoint;
        client_gasing.skillPoint = skillPoint;
        client_gasing.energiPointMax = energyPointMax;
        client_gasing.skillPointMax = skillPointMax;
    }

    



}
