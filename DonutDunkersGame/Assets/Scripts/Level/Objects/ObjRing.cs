using System;
using UnityEngine;

public class ObjRing : ObjectInteraction, ICanReset {
	
	[SerializeField]
	private GameObject parentObj;
	
	public override void PlayerInteraction() {
		this.parentObj.SetActive(false);
	}
	
	public bool IsObtained() {
		return !this.parentObj.activeSelf;
	}
	
	public void Initialize() {
		this.parentObj.SetActive(true);
	}
}
