using System;
using UnityEngine;

public class ObjAngle : ObjectInteraction {
	
    public override void PlayerInteraction() {
		Transform ballTransform = BallController.Instance.transform;
		if (ballTransform.forward == this.gameObject.transform.right || ballTransform.forward == -this.gameObject.transform.right || ballTransform.forward == this.gameObject.transform.up || ballTransform.forward == this.gameObject.transform.forward) {
			BallController.Instance.IsMoving = false;
			BallController.Instance.transform.position = this.transform.position - BallController.Instance.transform.forward;
		} else {
			ballTransform.position = this.transform.position;
			if (ballTransform.forward == -this.gameObject.transform.forward) {
				BallController.Instance.SetForwardDirection(this.gameObject.transform.up);
			} else if (ballTransform.forward == -this.gameObject.transform.up) {
				BallController.Instance.SetForwardDirection(this.gameObject.transform.forward);
			}
		}
	}
}
