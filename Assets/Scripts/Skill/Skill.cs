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
//			GameObject[] allEnemy = (GetComponent<AIMusuh>()==null) ? GameObject.FindGameObjectsWithTag("Enemy") : GameObject.FindGameObjectsWithTag("Player");

			GameObject[] allEnemy = null;
			if(Utilities.chosenMode == 1){		//team mode
				if(this.gameObject.tag.Equals("Player") || this.gameObject.tag.Equals("Ally")){
					allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
				}else{
					GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
					GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
					allEnemy = new GameObject[allies.Length+players.Length];
					allies.CopyTo(allEnemy,0);
					players.CopyTo(allEnemy,allies.Length);
				}
			}else{				//royal mode
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
				allEnemy = new GameObject[enemies.Length+players.Length];
				enemies.CopyTo(allEnemy,0);
				players.CopyTo(allEnemy,enemies.Length);
			}

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            foreach (GameObject enemy in allEnemy)
            {
//				if(((this.gameObject.tag.Equals("Player") || this.gameObject.tag.Equals("Ally")) && enemy.tag.Equals(enemy)) || (this.gameObject.tag.Equals("Enemy") && (enemy.tag.Equals("Player") || enemy.tag.Equals("Ally")) )){
					if (enemy != this.gameObject){		//biar ngga nembak diri sendiri
				        Vector3 diff = enemy.transform.position - position;
				        float curDistance = diff.sqrMagnitude;
				        if (curDistance < distance)
				        {
				            nearest = enemy;
				            distance = curDistance;
				        }
					}
//				}
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
