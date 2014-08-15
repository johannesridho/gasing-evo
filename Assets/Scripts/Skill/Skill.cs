using UnityEngine;
using System.Collections;

[System.Serializable]
public class Skill : MonoBehaviour {

    public Texture2D buttonSkill1;
    public string skillName;
    protected float skillPointNeeded;
	protected float damageInflicted;		//dipake buat skill yg bukan rigidbody (ngga perlu detect collision)
											//skill yg pake rigidbody damage diatur di script object yg dicreate oleh skill

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void doSkill()
    {
    }

	protected GameObject findNearestEnemy(){
		GameObject nearest = null;

        if (GamePrefs.isMultiplayer)
        {
            GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Player");
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject enemy in allEnemy)
            {
                if (enemy != this.gameObject)
                {
                    Vector3 diff = enemy.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        nearest = enemy;
                        distance = curDistance;
                    }
                }
            }
        }
        else
        {
            GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject enemy in allEnemy)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    nearest = enemy;
                    distance = curDistance;
                }
            }
        }
		return nearest;
	}

	public float DamageInflicted {
		get {
			return damageInflicted;
		}
	}
}
