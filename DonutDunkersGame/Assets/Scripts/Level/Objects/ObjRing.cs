using System;
using UnityEngine;

public class ObjRing : ObjectInteraction, ICanReset {
	
	
	
	public override void PlayerInteraction() {
		bool flag = Vector3.Angle(BallController.Instance.transform.forward, this.transform.up) < 1f || Vector3.Angle(BallController.Instance.transform.forward, -this.transform.up) < 1f;
			Debug.Log(BallController.Instance.transform.forward.ToString("F8"));
			Debug.Log(this.transform.up.ToString("F8"));
			Debug.Log((-this.transform.up).ToString("F8"));
			Debug.Log(flag);
		if (flag) {
			LevelData.Instance.RingsCollected += 1;
			this.gameObject.SetActive(false);
		} else {
			BallController.Instance.IsMoving = false;
			BallController.Instance.CanAct = true;
			BallController.Instance.transform.position = this.transform.position - BallController.Instance.transform.forward;
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
