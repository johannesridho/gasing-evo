using UnityEngine;
using System.Collections;

public class StatusStun : Status {
	
	void Start () {
		aksi();
	}
	
	void aksi () {
		gasing.speedMaxChange(0.001f);
	}
}
