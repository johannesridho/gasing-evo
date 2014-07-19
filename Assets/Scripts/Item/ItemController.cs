using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {
	
	public GameObject[] listItem;
	public GameObject currentItem;
	public float clockItem;
	
	private void spawnRandomItem(Vector3 _pos) {
		int itemIdx = Random.Range(0, listItem.Length);
		currentItem = (GameObject) Instantiate(listItem[itemIdx], _pos, Quaternion.Euler(0, 0, 0));
	}
	
	private Vector3 getRandomPosition() {
		float x = Random.Range (-10, 10);
		float y = 10;
		float z = Random.Range (-10, 10);
		return new Vector3(x, y, z);
	}
	
	private void itemUpdate() {
		clockItem += Time.deltaTime;
		if (clockItem >= 5f) {
			if(currentItem){
				currentItem.GetComponent<Item>().selfDestroy();
			}
			spawnRandomItem(getRandomPosition());
			clockItem = 0f;
		}
	}
	
	void Awake(){
		spawnRandomItem(getRandomPosition());
		clockItem = 0f;
	}
	
	void FixedUpdate() {
		itemUpdate();
	}
}
