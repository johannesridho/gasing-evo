using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Skill : MonoBehaviour
{

    public Texture2D buttonSkill1;
    public string skillName;
    protected float skillPointNeeded;
    protected float damageInflicted;		//dipake buat skill yg bukan rigidbody (ngga perlu detect collision)
    //skill yg pake rigidbody damage diatur di script object yg dicreate oleh skill
	protected float timeLimitSkillNameAppear = 0f;
	public Camera cam;

    // Use this for initialization
	protected void Start()
    {
		cam = GameObject.Find("Main Camera").camera;
    }

    // Update is called once per frame
    protected void Update()
    {
		timeLimitSkillNameAppear -= Time.deltaTime;
		if (timeLimitSkillNameAppear <= 0) {
			timeLimitSkillNameAppear = 0;
		}
    }

	void OnGUI() {
        if (!GamePrefs.isMultiplayer)
        {
            if (timeLimitSkillNameAppear > 0)
            {
                Vector3 screenPos = cam.WorldToScreenPoint(gameObject.rigidbody.position);
                if (GUI.Button(new Rect(screenPos.x - 45, Screen.height - screenPos.y - 75, 90, 30), skillName, new GUIStyle(GUI.skin.box))) { }
            }
        }
	}

    public virtual void doSkill()
    {
		timeLimitSkillNameAppear = 1f;
    }

    protected GameObject findNearestEnemy()
    {
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
            if (Utilities.chosenMode == 1)
            {		//team mode
                if (this.gameObject.tag.Equals("Player") || this.gameObject.tag.Equals("Ally"))
                {
                    allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                }
                else
                {
                    GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    allEnemy = new GameObject[allies.Length + players.Length];
                    allies.CopyTo(allEnemy, 0);
                    players.CopyTo(allEnemy, allies.Length);
                }
            }
            else
            {				//royal mode
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                allEnemy = new GameObject[enemies.Length + players.Length];
                enemies.CopyTo(allEnemy, 0);
                players.CopyTo(allEnemy, enemies.Length);
            }

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            foreach (GameObject enemy in allEnemy)
            {
                //				if(((this.gameObject.tag.Equals("Player") || this.gameObject.tag.Equals("Ally")) && enemy.tag.Equals(enemy)) || (this.gameObject.tag.Equals("Enemy") && (enemy.tag.Equals("Player") || enemy.tag.Equals("Ally")) )){
                if (enemy != this.gameObject)
                {		//biar ngga nembak diri sendiri
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

    public List<GameObject> mp_findAllTarget()
    {
        List<GameObject> hasil = new List<GameObject>();

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gobj in temp)
        {
            if (gobj != gameObject)
            {
                hasil.Add(gobj);
                Debug.Log("Get target "+gobj);
            }
        }

        return hasil;
    }

    public float DamageInflicted
    {
        get
        {
            return damageInflicted;
        }
    }
}
