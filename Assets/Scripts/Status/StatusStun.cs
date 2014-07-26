using UnityEngine;
using System.Collections;

public class StatusStun : Status {
	
	public override void aksi () {
		gasing.speedMaxChange(0.0001f);
	}
}
