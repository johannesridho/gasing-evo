using UnityEngine;
using System.Collections;

public class FollowPemain : MonoBehaviour {

	public GameObject pemain;
    public Transform pemainTransform;
	private Vector3 offset;
    public bool isMultiplayer = true;

	void Awake(){
		if (!pemain) {
            if (isMultiplayer)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("MP_Player");
                
                foreach (GameObject go in players)
                {
                    if (go.networkView.isMine)
                    {
                        pemain = go;
                        break;
                    }
                    
                }
                foreach (Transform tf in pemain.transform)
                {
                    pemainTransform = tf;
                }
            }
            else
            {
                pemain = GameObject.Find("Pemain");
                pemainTransform = pemain.transform;
            }
		}
	}

	// Use this for initialization
	void Start () {
        //if (!pemain) {
        //    if (isMultiplayer)
        //    {
        //        GameObject[] players = GameObject.FindGameObjectsWithTag("MP_Player");
        //        foreach (GameObject go in players)
        //        {
        //            if (go.networkView.isMine)
        //            {
        //                pemain = go;
        //                break;
        //            }
        //        }               
        //    }
        //    else
        //    {
        //        pemain = GameObject.Find("Pemain");
        //    }
        //}
		offset = transform.position;
	}

	void LateUpdate () {
if (pemain){transform.position = offset * 3 / 2 + pemainTransform.position;
}
	}
}