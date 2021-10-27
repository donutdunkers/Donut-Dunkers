using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridSelection : MonoBehaviour {
	
    private static PlayerGridSelection _Instance;
    public static PlayerGridSelection Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<PlayerGridSelection>();
            }
            return _Instance;
        }
    }
	
	public LevelGridSelection currentGrid;
	
	public LayerMask gridMask;
	
	public LayerMask terrainMask;
    
	
	private void Update() {
		if (LevelData.Instance.Turns <= 0) {
			return;
		}
		
		this.HandleGridSelection();
		
		if (this.currentGrid != null) {
			if (PlayerInput.Instance.GetMouseLeftClick()) {
				if (BallController.Instance.IsMoving) {
					return;
				}
				BallController.Instance.gameObject.SetActive(true);
				BallController.Instance.SetForwardDirection(-this.currentGrid.transform.forward);
				Vector3 ballFwd = BallController.Instance.transform.forward;
				RaycastHit hit;
				if (Physics.Raycast(BallController.Instance.transform.position, -this.currentGrid.transform.forward, out hit, 1f, this.terrainMask)) {
					ObjRing ring = hit.collider.GetComponent<ObjRing>();
					ObjGelatin gelatin = hit.collider.GetComponent<ObjGelatin>();
					ObjAngle angle = hit.collider.GetComponent<ObjAngle>();
					ObjWarpCup cup = hit.collider.GetComponent<ObjWarpCup>();
					if (ring != null) {
						Vector3 ringUp = ring.transform.up;
						if (ballFwd != ringUp && ballFwd != -ringUp) {
							return;
						}
					}
					if (angle != null) {
						Transform angleTransform = angle.transform;
						if (ballFwd == angleTransform.right || ballFwd == -angleTransform.right || ballFwd == angleTransform.up || ballFwd == angleTransform.forward) {
							return;
						}
					}
					if (ring == null && gelatin == null && angle == null && cup == null) {
						return;
					}												
				}
				BallController.Instance.IsMoving = true;
				LevelData.Instance.Turns--;
			}
		}
	}
	
	private void HandleGridSelection() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000f, this.gridMask, QueryTriggerInteraction.Ignore)) {
			LevelGridSelection grid = hit.collider.GetComponent<LevelGridSelection>();
			if (grid != null) {
				this.currentGrid = grid;
			}
		} else {
			this.currentGrid = null;
		}
	}
}
