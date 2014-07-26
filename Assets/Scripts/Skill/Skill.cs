using UnityEngine;
using System.Collections;

[System.Serializable]
public class Skill : MonoBehaviour {

    public Texture2D buttonSkill1;
    public string skillName;
    protected float skillPointNeeded;
	protected float damageInflicted;		//sementara baru thunder yg pake ini, ntar bakal dipake buat semua

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void doSkill()
    {
    }
}
