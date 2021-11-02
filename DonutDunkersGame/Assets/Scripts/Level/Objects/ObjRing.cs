using System;
using UnityEngine;

public class ObjRing : ObjectInteraction, ICanReset {
	
	public override void PlayerInteraction() {
		if (BallController.Instance.transform.forward == this.transform.up || BallController.Instance.transform.forward == -this.transform.up) {
			LevelData.Instance.RingsCollected += 1;
			this.gameObject.SetActive(false);
		} else {
			BallController.Instance.IsMoving = false;
			BallController.Instance.transform.position = this.transform.position - BallController.Instance.transform.forward;
			LevelData.Instance.Turns--;
		}
	}
	
	public bool IsObtained() {
		return !this.gameObject.activeSelf;
	}
	
	public void Initialize() {
		this.gameObject.SetActive(true);
	}
	
	public virtual bool CanMoveTowards() {
		float angle = Vector3.Dot(this.transform.forward, (BallController.Instance.transform.position - this.transform.position).normalized);
		return angle <= -0.9f || angle >= 0.9f;
	}
}
