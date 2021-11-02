using System;
using UnityEngine;

public class ObjWall : ObjectInteraction {
	
    public override void PlayerInteraction() {
		BallController.Instance.IsMoving = false;
		BallController.Instance.transform.position = this.transform.position - BallController.Instance.transform.forward;
	}
	
	public override bool CanMoveTowards() {
		return false;
	}
}
