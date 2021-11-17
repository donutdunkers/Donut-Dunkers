using System;
using UnityEngine;

public class ObjWall : ObjectInteraction {
	
	public GameObject tempObj;
	
    public override void PlayerInteraction() {
		BallController.Instance.IsMoving = false;
		BallController.Instance.CanAct = true;
		BallController.Instance.transform.position = this.transform.position - BallController.Instance.transform.forward;
		ScriptableSingleton<SoundEvent>.Instance.sndDonuthitWall.Play();
	}
	
	public override bool CanMoveTowards() {
		return false;
	}
}
