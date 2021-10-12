using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridSelection : MonoBehaviour
{
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
    
	
	private void Update() {
		this.HandleGridSelection();
		
		if (this.currentGrid != null) {
			if (Input.GetMouseButtonDown(0)) {
				if (BallController.Instance.IsMoving) {
					return;
				}
				BallController.Instance.gameObject.SetActive(true);
			//	BallController.Instance.transform.position = this.currentGrid.transform.position + this.currentGrid.transform.forward * 5f;
				BallController.Instance.SetForwardDirection(this.currentGrid.transform.forward);
				BallController.Instance.IsMoving = true;
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
