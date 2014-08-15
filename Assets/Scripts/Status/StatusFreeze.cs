using UnityEngine;
using System.Collections;

public class StatusFreeze : Status {
		
	public override void aksi () {
		gasing.speedMaxChange(5f);
	}
}