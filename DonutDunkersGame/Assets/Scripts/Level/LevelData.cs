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
	public int size = 4;
	
	public GameObject tilePrefab;
	
	public BallController ball;
	
	[SerializeField]
	private Transform levelContainer;
	
	[SerializeField]
	private Transform levelGridContainer;
	
	private Vector3 startPos;
	
	public Vector3 StartPos {
		set {
			this.startPos = value;
		} get {
			return this.startPos;
		}
	}
	
	private void Start() {
		this.GenerateGridTiles();
	}
	
	private void OnDrawGizmos() {
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		
		int div = (int)Mathf.Floor(this.size / 2f);
		
		bool isEven = (this.size % 2) == 0;
		
		float init = ((isEven) ? 0.5f : 0f) - div;
		
		for (int x = 0; x < this.size; x++) {
			for (int y = 0; y < this.size; y++) {
				for (int z = 0; z < this.size; z++) {
					Vector3 pos = new Vector3(init + x, init + y, init + z);
					Gizmos.DrawWireCube(pos, Vector3.one);
				}
			}
		}
	}
	
	private void GenerateGridTiles() {
		
		// Generates the outer tiles used to choose which direction to move the Ball towards
		
		Vector3 pos;
		GameObject tile;
		LevelGridSelection gridTile;
		
		Vector3 scale = Vector3.one * (this.size + 0.5f);
		
		// Generate Top
		pos = new Vector3(0f, (this.size / 2f) + 1f, 0f);
		tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
		tile.transform.forward = Vector3.up;
		tile.transform.localScale = scale;
		gridTile = tile.GetComponent<LevelGridSelection>();
		if (gridTile != null) {
			gridTile.gridSide = LevelGridSelection.GridSide.Top;
			gridTile = null;
		}
		
		// Generate Bottom
		pos = new Vector3(0f, -(this.size / 2f) - 1f, 0f);
		tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
		tile.transform.forward = -Vector3.up;
		tile.transform.localScale = scale;
		gridTile = tile.GetComponent<LevelGridSelection>();
		if (gridTile != null) {
			gridTile.gridSide = LevelGridSelection.GridSide.Bottom;
			gridTile = null;
		}
		
		// Generate Right
		pos = new Vector3((this.size / 2f) + 1f, 0f, 0f);
		tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
		tile.transform.forward = Vector3.right;
		tile.transform.localScale = scale;
		gridTile = tile.GetComponent<LevelGridSelection>();
		if (gridTile != null) {
			gridTile.gridSide = LevelGridSelection.GridSide.Right;
			gridTile = null;
		}
		
		// Generate Left
		pos = new Vector3(-(this.size / 2f) - 1f, 0f, 0f);
		tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
		tile.transform.forward = -Vector3.right;
		tile.transform.localScale = scale;
		gridTile = tile.GetComponent<LevelGridSelection>();
		if (gridTile != null) {
			gridTile.gridSide = LevelGridSelection.GridSide.Left;
			gridTile = null;
		}
		
		// Generate Front
		pos = new Vector3(0f, 0f, (this.size / 2f) + 1f);
		tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
		tile.transform.forward = Vector3.forward;
		tile.transform.localScale = scale;
		gridTile = tile.GetComponent<LevelGridSelection>();
		if (gridTile != null) {
			gridTile.gridSide = LevelGridSelection.GridSide.Front;
			gridTile = null;
		}
		
		// Generate Back
		pos = new Vector3(0f, 0f, -(this.size / 2f) - 1f);
		tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
		tile.transform.forward = -Vector3.forward;
		tile.transform.localScale = scale;
		gridTile = tile.GetComponent<LevelGridSelection>();
		if (gridTile != null) {
			gridTile.gridSide = LevelGridSelection.GridSide.Back;
			gridTile = null;
		}
		
		
		/*
		THE BELOW CODE IS OUTDATED AND NO LONGER USED AS WE'VE CHANGED THE OVERALL GAMEPLAY FOR THIS GAME
		
		int divX = (int)Mathf.Floor(this.size / 2f);
		int divY = (int)Mathf.Floor(this.size / 2f);
		int divZ = (int)Mathf.Floor(this.size / 2f);
		
		bool xIsEven = (this.size % 2) == 0;
		bool yIsEven = (this.size % 2) == 0;
		bool zIsEven = (this.size % 2) == 0;
		
		float initX = ((xIsEven) ? 0.5f : 0f) - divX;
		float initY = ((yIsEven) ? 0.5f : 0f) - divY;
		float initZ = ((zIsEven) ? 0.5f : 0f) - divZ;
		
		// Generate Top
		for (int x = 0; x < this.size; x++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3(initX + x, (this.size / 2) + 1, initZ + z);
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
		for (int x = 0; x < this.size; x++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3(initX + x, -(this.size / 2) - 1, initZ + z);
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
		for (int y = 0; y < this.size; y++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3((this.size / 2) + 1, initY + y, initZ + z);
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
		for (int y = 0; y < this.size; y++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3(-(this.size / 2) - 1, initY + y, initZ + z);
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
		for (int y = 0; y < this.size; y++) {
			for (int x = 0; x < this.size; x++) {
				Vector3 pos = new Vector3(initX + x, initY + y, (this.size / 2) + 1);
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
		for (int y = 0; y < this.size; y++) {
			for (int x = 0; x < this.size; x++) {
				Vector3 pos = new Vector3(initX + x, initY + y, -(this.size / 2) - 1);
				GameObject tile = Instantiate(this.tilePrefab, pos, Quaternion.identity, this.levelGridContainer);
				tile.transform.forward = -Vector3.forward;
				tile.transform.localScale = Vector3.one * 0.25f;
				LevelGridSelection gridTile = tile.GetComponent<LevelGridSelection>();
				if (gridTile != null) {
					gridTile.gridSide = LevelGridSelection.GridSide.Back;
				}
			}
		}
	*/
	}
}
