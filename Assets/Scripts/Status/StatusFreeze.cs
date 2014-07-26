using UnityEngine;
using System.Collections;

public class StatusFreeze : Status {
	
	void Start () {
		aksi();
	}
	
	void aksi () {
		gasing.speedMaxChange(5f);
	}
}