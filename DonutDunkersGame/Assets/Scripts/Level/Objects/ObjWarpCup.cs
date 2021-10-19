using System;
using System.Collections;
using UnityEngine;

public class ObjWarpCup : ObjectInteraction, ICanReset {
	
    [SerializeField]
	private ObjWarpCup otherCup;
	
	[NonSerialized]
	public Collider collider;
	
	private void Awake() {
		this.collider = this.GetComponent<Collider>();
	}
	
	public void Initialize() {
		this.CheckRoutine();
	}
	
	public override void PlayerInteraction() {
		if (otherCup.warpRoutine != null) {
			return;
		}
		this.CheckRoutine();
		this.warpRoutine = this.StartCoroutine(this.WarpRoutine());
	}
	
	private void CheckRoutine() {
		if (this.warpRoutine != null) {
			this.StopCoroutine(this.warpRoutine);
		}
		this.warpRoutine = null;
	}
	
	public Coroutine warpRoutine;
	
	private IEnumerator WarpRoutine() {
		otherCup.collider.enabled = false;
		BallController.Instance.IsMoving = false;
		BallController.Instance.transform.position = this.transform.position;
		yield return new WaitForSeconds(0.1f);
		BallController.Instance.transform.position = otherCup.transform.position;
		BallSkin.Instance.transform.position = BallController.Instance.transform.position;
		BallController.Instance.SetForwardDirection(otherCup.transform.forward);
		BallController.Instance.IsMoving = true;
		yield return new WaitForSeconds(0.5f);
		otherCup.collider.enabled = true;
		this.warpRoutine = null;
	}
}
