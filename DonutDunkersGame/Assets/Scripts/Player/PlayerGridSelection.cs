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
	
	public LayerMask TerrainMask {
		get {
			return this.terrainMask;
		}
	}
	
	[SerializeField]
	private LayerMask terrainMask;    
	
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
				RaycastHit hit;
				if (Physics.Raycast(BallController.Instance.transform.position, -this.currentGrid.transform.forward, out hit, 1f, this.TerrainMask)) {
					ObjectInteraction interaction = hit.collider.GetComponent<ObjectInteraction>();
					if (interaction != null) {
						if (!interaction.CanMoveTowards()) {
							return;
						}
					}
				}
				BallController.Instance.SetForwardDirection(-this.currentGrid.transform.forward);
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
