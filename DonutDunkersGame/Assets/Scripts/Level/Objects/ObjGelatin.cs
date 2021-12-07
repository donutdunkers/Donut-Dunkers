using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGelatin : ObjectInteraction {
	
    public override void PlayerInteraction() {
		BallController.Instance.transform.position = this.transform.position;
		BallController.Instance.IsMoving = false;
		BallController.Instance.CanAct = true;
		ScriptableSingleton<SoundEvent>.Instance.sndDonuthitGelatin.Play();
	}
	
	public override bool CanMoveTowards() {
		return true;
	}
}
