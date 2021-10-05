using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {
	
    private static LevelData _Instance;
    public static LevelData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<LevelData>();
            }
            return _Instance;
        }
    }
	
	[SerializeField, Range(4, 20)]
	public int xSize = 4, ySize = 4, zSize = 4;
	
	public GameObject tilePrefab;
	
	public BallController ball;
	
	[SerializeField]
	private Transform levelContainer;
	
	[SerializeField]
	private Transform levelGridContainer;
	
	private void Start() {
		this.GenerateGridTiles();
	}
	
	private void OnDrawGizmos() {
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		
		int divX = (int)Mathf.Floor(this.xSize / 2f);
		int divY = (int)Mathf.Floor(this.ySize / 2f);
		int divZ = (int)Mathf.Floor(this.zSize / 2f);
		
		bool xIsEven = (this.xSize % 2) == 0;
		bool yIsEven = (this.ySize % 2) == 0;
		bool zIsEven = (this.zSize % 2) == 0;
		
		float initX = ((xIsEven) ? 0.5f : 0f) - divX;
		float initY = ((yIsEven) ? 0.5f : 0f) - divY;
		float initZ = ((zIsEven) ? 0.5f : 0f) - divZ;
		
		for (int x = 0; x < this.xSize; x++) {
			for (int y = 0; y < this.ySize; y++) {
				for (int z = 0; z < this.zSize; z++) {
					Vector3 pos = new Vector3(initX + x, initY + y, initZ + z);
					Gizmos.DrawWireCube(pos, Vector3.one);
				}
			}
		}
	}
	
	private void GenerateGridTiles() {
		
		int divX = (int)Mathf.Floor(this.xSize / 2f);
		int divY = (int)Mathf.Floor(this.ySize / 2f);
		int divZ = (int)Mathf.Floor(this.zSize / 2f);
		
		bool xIsEven = (this.xSize % 2) == 0;
		bool yIsEven = (this.ySize % 2) == 0;
		bool zIsEven = (this.zSize % 2) == 0;
		
		float initX = ((xIsEven) ? 0.5f : 0f) - divX;
		float initY = ((yIsEven) ? 0.5f : 0f) - divY;
		float initZ = ((zIsEven) ? 0.5f : 0f) - divZ;
		
		// Generate Top
			for (int x = 0; x < this.xSize; x++) {
				for (int z = 0; z < this.zSize; z++) {
					Vector3 pos = new Vector3(initX + x, (this.ySize / 2) + 1, initZ + z);
					GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
					tile.transform.forward = Vector3.up;
					tile.transform.localScale = Vector3.one * 0.25f;
					LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
					if (gridTile != null) {
						gridTile.gridSide = LevelGridSelection.GridSide.Top;
					}
				}
			}
					
		
		// Generate Bottom
			for (int x = 0; x < this.xSize; x++) {
				for (int z = 0; z < this.zSize; z++) {
					Vector3 pos = new Vector3(initX + x, -(this.ySize / 2) - 1, initZ + z);
					GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
					tile.transform.forward = -Vector3.up;
					tile.transform.localScale = Vector3.one * 0.25f;
					LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
					if (gridTile != null) {
						gridTile.gridSide = LevelGridSelection.GridSide.Bottom;
					}
				}
			}
		
		// Generate Right
			for (int y = 0; y < this.ySize; y++) {
				for (int z = 0; z < this.zSize; z++) {
					Vector3 pos = new Vector3((this.xSize / 2) + 1, initY + y, initZ + z);
					GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
					tile.transform.forward = Vector3.right;
					tile.transform.localScale = Vector3.one * 0.25f;
					LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
					if (gridTile != null) {
						gridTile.gridSide = LevelGridSelection.GridSide.Right;
					}
				}
			}
		
		// Generate Left
			for (int y = 0; y < this.ySize; y++) {
				for (int z = 0; z < this.zSize; z++) {
					Vector3 pos = new Vector3(-(this.xSize / 2) - 1, initY + y, initZ + z);
					GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
					tile.transform.forward = -Vector3.right;
					tile.transform.localScale = Vector3.one * 0.25f;
					LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
					if (gridTile != null) {
						gridTile.gridSide = LevelGridSelection.GridSide.Left;
					}
				}
			}
		
		// Generate Front
			for (int y = 0; y < this.ySize; y++) {
				for (int x = 0; x < this.xSize; x++) {
					Vector3 pos = new Vector3(initX + x, initY + y, (this.zSize / 2) + 1);
					GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
					tile.transform.forward = Vector3.forward;
					tile.transform.localScale = Vector3.one * 0.25f;
					LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
					if (gridTile != null) {
						gridTile.gridSide = LevelGridSelection.GridSide.Front;
					}
				}
			}
		
		// Generate Back
			for (int y = 0; y < this.ySize; y++) {
				for (int x = 0; x < this.xSize; x++) {
					Vector3 pos = new Vector3(initX + x, initY + y, -(this.zSize / 2) - 1);
					GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
					tile.transform.forward = -Vector3.forward;
					tile.transform.localScale = Vector3.one * 0.25f;
					LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
					if (gridTile != null) {
						gridTile.gridSide = LevelGridSelection.GridSide.Back;
					}
				}
			}
	}
}
