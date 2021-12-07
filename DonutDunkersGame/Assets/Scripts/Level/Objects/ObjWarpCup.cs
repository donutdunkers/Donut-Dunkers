using System;
using System.Collections;
using UnityEngine;

public class ObjWarpCup : ObjectInteraction, ICanReset {
	
    [SerializeField]
	private ObjWarpCup otherCup;
	
	[NonSerialized]
	public Collider collider;
	
	private const float WAIT_TIME = 0.5f;
	
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
		BallSkin.Instance.Trail.enabled = false;
		BallSkin.Instance.BallTransform.gameObject.SetActive(false);
		Vector3 efPos = this.transform.position + (this.transform.forward * 0.5f);
		Quaternion rotation = this.transform.rotation * Quaternion.Euler(new Vector3(90f, 0f, 0f));
		ObjectParticleDatabase.Instance.cupSplash.Emit(efPos, rotation);
		ScriptableSingleton<SoundEvent>.Instance.sndDonuthitWater.Play();
		yield return new WaitForSeconds(WAIT_TIME);
		BallController.Instance.transform.position = otherCup.transform.position;
		BallSkin.Instance.transform.position = BallController.Instance.transform.position;
		efPos = otherCup.transform.position + (otherCup.transform.forward * 0.5f);
		rotation = otherCup.transform.rotation * Quaternion.Euler(new Vector3(90f, 0f, 0f));
		ObjectParticleDatabase.Instance.cupSplash.Emit(efPos, rotation);
		BallController.Instance.SetForwardDirection(otherCup.transform.forward);
		BallController.Instance.IsMoving = true;
		BallSkin.Instance.BallTransform.gameObject.SetActive(true);
		BallSkin.Instance.Trail.Clear();
		BallSkin.Instance.Trail.enabled = true;
		yield return new WaitForSeconds(0.1f);
		otherCup.collider.enabled = true;
		this.warpRoutine = null;
	}
	
	public override bool CanMoveTowards() {
		return Vector3.Dot(this.transform.forward, (BallController.Instance.transform.position - this.transform.position).normalized) >= 0.9f;
	}		
}
