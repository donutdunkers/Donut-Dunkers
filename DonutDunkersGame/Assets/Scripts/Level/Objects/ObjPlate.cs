using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlate : ObjectInteraction, ICanReset {
	
	private const int BASE_HEALTH = 2;
	
	[SerializeField]
	private int health;
	
	[SerializeField]
	private GameObject meshObj;
	
	[SerializeField]
	private Transform parentTrans;
	
	private Collider collider;
	
	[SerializeField]
	private ObjPlate.PlateType plateType;
	
	private enum PlateType {
		Nondiagonal,
		Diagonal
	}
	
	private void Awake() {
		this.collider = this.GetComponentInChildren<Collider>();
	}
	
	private void Start() {
		this.Initialize();
	}
	
	public void Initialize() {
		this.collider.enabled = true;
		this.health = BASE_HEALTH;
		this.meshObj.SetActive(true);
	}
	
	public override void PlayerInteraction() {
		Debug.Log("Hitting plate");
		Transform ballTransform = BallController.Instance.transform;
		switch (this.plateType) {
			case ObjPlate.PlateType.Nondiagonal:
				if (ballTransform.forward == this.collider.transform.up || ballTransform.forward == -this.collider.transform.up) {
					this.DoRoutine();
				}
				BallController.Instance.IsMoving = false;
				BallController.Instance.transform.position = this.parentTrans.position - BallController.Instance.transform.forward;
				break;
			case ObjPlate.PlateType.Diagonal:
				if (ballTransform.forward == this.parentTrans.up || ballTransform.forward == -this.parentTrans.up || ballTransform.forward == this.parentTrans.forward || ballTransform.forward == -this.parentTrans.forward) {
					ballTransform.position = this.transform.position;
					BallController.Instance.SetForwardDirection(((ballTransform.forward == this.parentTrans.up) ? -this.parentTrans.forward : (ballTransform.forward == -this.parentTrans.up) ? this.parentTrans.forward : (ballTransform.forward == this.parentTrans.forward) ? -this.parentTrans.up : this.parentTrans.up));
					this.DoRoutine();
				} else {
					BallController.Instance.IsMoving = false;
					BallController.Instance.transform.position = this.parentTrans.position - BallController.Instance.transform.forward;
				}
				break;
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
		if (this.plateType == ObjPlate.PlateType.Diagonal) {
			BallController.Instance.IsMoving = true;	
		}	
		this.health--;
		this.collider.enabled = this.health > 0;
		this.meshObj.SetActive(this.health > 0);		
	}
	
	public override bool CanMoveTowards() {
		bool flag = false;
		Transform ballTransform = BallController.Instance.transform;		
		switch (this.plateType) {
			case ObjPlate.PlateType.Nondiagonal:
				flag = (ballTransform.forward == this.collider.transform.up || ballTransform.forward == -this.collider.transform.up);
				break;
			case ObjPlate.PlateType.Diagonal:
				flag = (ballTransform.forward == this.parentTrans.up || ballTransform.forward == -this.parentTrans.up || ballTransform.forward == this.parentTrans.forward || ballTransform.forward == -this.parentTrans.forward);
				break;
		}
		Debug.Log(flag);
		return flag;		
	}
	
}
