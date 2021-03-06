﻿using UnityEngine;
using System.Collections;

public class FollowPemain : MonoBehaviour
{

    public GameObject pemain;
    public Transform pemainTransform;
    private Vector3 offset;
	private Vector3 posisiAwal;
    // Use this for initialization
    void Start()
    {
		transform.position = new Vector3 (0f,10.5f,-11f);
		transform.rotation = Quaternion.Euler (45,0,0);
        if (!pemain)
        {
            if (GamePrefs.isMultiplayer)
            {
                if (MultiplayerManager.instance.isDedicatedServer)
                {
                    if (Network.isServer)
                    {
                        //transform.rotation = Quaternion.Euler(60, 0, 0);
                        //transform.position = new Vector3(0, 30, -20);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        //GameObject.Find("Blank Camera").SetActive(true);
                    }
                }
                else
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("MP_Player");

                    Debug.Log("found = " + players.Length);
                    Debug.Log("my player " + int.Parse(Network.player.ToString()));
                    //foreach (GameObject go in players)
                    //{
                    //    Debug.Log("nomor = " + go.GetComponent<Server_Gasing>().networkPlayer);

                    //    if(go.GetComponent<Server_Gasing>().networkPlayer == Network.player)

                    //    //if (go.networkView.isMine)
                    //    {
                    //        pemain = go;
                    //        break;
                    //    }

                    //}

                    pemain = players[int.Parse(Network.player.ToString())];

                    //pemainTransform = pemain.GetComponent<MultiplayerInputHandler>().followPoint;

                    foreach (Transform tf in pemain.transform)
                    {
                        pemainTransform = tf;
                    }
                }
            }
            else
            {
                if (!pemain)
                {
                    pemain = GameObject.Find("Pemain");
                    if (pemain == null)
                    {
                        Debug.Log("pemain null");
                    }
                    pemainTransform = pemain.transform;
                }
            }
        }

		posisiAwal = transform.position;
        offset = transform.position;
    }

    void LateUpdate()
    {
        if (GamePrefs.isMultiplayer)
        {
            if (!MultiplayerManager.instance.isDedicatedServer)
            {
                if (pemain)
                {
                    transform.position = offset * 3 / 2 + pemainTransform.position;
                }
            }else{
				transform.position = new Vector3(0,30,-45);
			}
        }
        else
        {
            if (pemain)
            {
                transform.position = offset * 3 / 2 + pemainTransform.position;
            }else{
				transform.position = new Vector3(0,25,-45);
//				transform.position = posisiAwal;
			}
        }
    }
}