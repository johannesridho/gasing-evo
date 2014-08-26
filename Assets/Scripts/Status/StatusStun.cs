using UnityEngine;
using System.Collections;

public class StatusStun : Status {

	private PhysicsTabrak gasingpt;


	public override void aksi () {
		gasing.rigidbody.velocity = new Vector3(0,0,0);
		gasing.isCanMove = false;
	}

	~StatusStun() {
		gasing.isCanMove = true;
	}
}
