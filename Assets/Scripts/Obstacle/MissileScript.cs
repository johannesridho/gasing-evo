using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

	private GameObject targetEnemy;
	public GameObject efekLedakan;
	private float clock;
	private string targetTag;
	private bool on;
	
	void Awake() {
		clock = 0f;
		//taking the target:
		//targetEnemy = findNearestEnemy();
		targetTag = "Enemy";
		on = false;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){
		if (on) {
			if (!targetEnemy) {
				targetEnemy = GameObject.FindGameObjectWithTag(targetTag);
			}
			clock += Time.deltaTime;
			if (clock >= 5) {
				Destroy(this.gameObject);
				Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));
			}
		}
	}
	
	void FixedUpdate () {
		if (on) {
			if (targetEnemy) {
				transform.position += (targetEnemy.transform.position - transform.position ).normalized * 30 * Time.deltaTime;
				transform.LookAt(targetEnemy.transform.position);
				transform.Rotate(new Vector3(0,-90,0));
			}
		}
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			Instantiate (efekLedakan, transform.position, Quaternion.Euler (0, 0, 0));			
		}
	}
	
	public void nyalakan(string _targetTag){
		targetTag = _targetTag;
		on = true;
	}

	private GameObject findNearestEnemy(){
        GameObject nearest = new GameObject();

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
                    if ((curDistance < distance) && (curDistance > 4))
                    {
                        nearest = enemy;
                        distance = curDistance;
                    }
                }
            }
            Debug.Log("Distance = "+ distance);
        }
        else
        {
			GameObject[] allEnemy = GameObject.FindGameObjectsWithTag(targetTag);
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
}
