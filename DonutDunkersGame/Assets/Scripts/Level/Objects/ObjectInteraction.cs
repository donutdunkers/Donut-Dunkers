using System;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour {
	
    public virtual void PlayerInteraction() {
	}
	
	public virtual bool CanMoveTowards() {
		return true;
	}

}
