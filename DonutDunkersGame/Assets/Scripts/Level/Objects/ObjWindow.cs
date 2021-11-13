using System;
using System.Collections;
using UnityEngine;

public class ObjWindow : ObjectInteraction, ICanReset {
	
    [SerializeField]
	private Animator animator;
	
	public override void PlayerInteraction() {
		this.hitRoutine = this.StartCoroutine(this.HitRoutine());
	}
	
	private Coroutine hitRoutine;
	
	public IEnumerator HitRoutine() {
		// play animation
		yield return new WaitForSeconds(0.5f);
		BallController.Instance.Disable();
		BallController.Instance.Explode();
		yield return new WaitForSeconds(0.75f);
		GameMenuUI.Instance.ShowEndScreen(false);
		this.hitRoutine = null;
	}
	
	public void Initialize() {
		if (this.hitRoutine != null) {
			this.StopCoroutine(this.hitRoutine);
			this.hitRoutine = null;
		}
		// play default animation
	}
}
