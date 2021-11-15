using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTrashCan : ObjectInteraction, ICanReset {
    
	[SerializeField]
	private Animator animator;
	
	public override void PlayerInteraction() {
		this.hitRoutine = this.StartCoroutine(this.HitRoutine());
	}
	
	private Coroutine hitRoutine;
	
	public IEnumerator HitRoutine() {
		// play animation
		yield return new WaitForSeconds(0.75f);
		BallController.Instance.Disable();
		yield return new WaitForSeconds(0.75f);
		GameMenuUI.Instance.ShowEndScreen(false);
		this.hitRoutine = null;
	}
	
	public void Initialize() {
		if (this.hitRoutine != null) {
			this.StopCoroutine(this.hitRoutine);
			this.hitRoutine = null;
		}
		// Reset animation
	}
}
