using System;
using System.Collections;
using UnityEngine;

public class ObjAngle : ObjectInteraction, ICanReset {
	
	public void Initialize() {
		this.ResetRoutine();
	}
	
    public override void PlayerInteraction() {
		Transform ballTransform = BallController.Instance.transform;
		if (ballTransform.forward == this.gameObject.transform.right || ballTransform.forward == -this.gameObject.transform.right || ballTransform.forward == this.gameObject.transform.up || ballTransform.forward == this.gameObject.transform.forward) {
			BallController.Instance.IsMoving = false;
			BallController.Instance.transform.position = this.transform.position - BallController.Instance.transform.forward;
		} else {
			this.DoRoutine();
			ballTransform.position = this.transform.position;
			if (ballTransform.forward == -this.gameObject.transform.forward) {
				BallController.Instance.SetForwardDirection(this.gameObject.transform.up);
			} else if (ballTransform.forward == -this.gameObject.transform.up) {
				BallController.Instance.SetForwardDirection(this.gameObject.transform.forward);
			}
		}
	}
	
	private void DoRoutine() {
		this.ResetRoutine();
		this.hitRoutine = this.StartCoroutine(this.HitRoutine());
	}
	
	private void ResetRoutine() {
		if (this.hitRoutine != null) {
			this.StopCoroutine(this.hitRoutine);
			this.hitRoutine = null;
		}
	}
	
	private Coroutine hitRoutine;
	
	private IEnumerator HitRoutine() {
		BallController.Instance.IsMoving = false;
		yield return new WaitForSeconds(0.15f);
		BallController.Instance.IsMoving = true;		
	}
}
