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
	
	[SerializeField]
	private int turns = 10;

	private int ringsCollected = 0;

	private int initialTurns;
	
	public int Turns {
		set {
			if (LevelUI.Instance != null) {
				LevelUI.Instance.SetRemainingTurns(value);
			}
			this.turns = value;
			Debug.Log("Updating turns");
		} get {
			return this.turns;
		}
	}

	public int TurnsTaken {
		get {
			return this.initialTurns - this.turns;
		}
    }

	public int RingsCollected
    {
		set {
			ringsCollected = value;
        } get {
			return this.ringsCollected;
        }
    }

	
	public GameObject tilePrefab, wallPrefab;
	
	public BallController ball;
	
	[SerializeField]
	private Transform levelContainer;
	
	[SerializeField]
	private Transform levelGridContainer;
	
	public Transform levelRotationContainer;
	
	private Vector3 startPos;
	
	public Vector3 StartPos {
		set {
			this.startPos = value;
		} get {
			return this.startPos;
		}
	}
	
	private ObjRing[] rings;

	public int RingsInLevel
	{
		get {
			return rings.Length;
		}
	}

	public IList<ICanReset> canReset;
	
	[NonSerialized]
	public Vector3 LevelStartPosition;
	
	[SerializeField]
	private RoomType roomType;
	
	private void Awake() {
		if (SoundManager.Instance == null) {
			GameManager gameManager = Resources.Load<GameManager>("Game Manager");
			GameObject.Instantiate(gameManager.soundManager, Vector3.zero, Quaternion.identity);
		}
		this.initialTurns = this.turns;
		this.LevelStartPosition = BallController.Instance.transform.localPosition;
	}
	
	private void Start() {
		Sound music = ScriptableSingleton<MusicEvent>.Instance.Theme01;
		SoundManager.Instance.CrossFade(music.audioClip, music.volume, 2.5f);
		
		this.GenerateGridTiles();
		this.GenerateGridWalls();
		
		this.Turns = this.initialTurns;
		
		this.rings = (ObjRing[])FindObjectsOfType<ObjRing>();
		this.canReset = InterfaceHelper.FindObjects<ICanReset>();
	}
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			this.ResetLevel();
		}
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
	}

	private void GenerateGridWalls() {
		
		Material floorMat;
		Material wallMat;
		Material ceilingMat;
		
		switch (this.roomType) {
			case RoomType.Kitchen:
				floorMat = ScriptableSingleton<LevelRoomData>.Instance.kitchenData.floorMat;
				wallMat = ScriptableSingleton<LevelRoomData>.Instance.kitchenData.wallMat;
				ceilingMat = ScriptableSingleton<LevelRoomData>.Instance.kitchenData.ceilingMat;
				break;
			default:
				floorMat = ScriptableSingleton<LevelRoomData>.Instance.noData.floorMat;
				wallMat = ScriptableSingleton<LevelRoomData>.Instance.noData.wallMat;
				ceilingMat = ScriptableSingleton<LevelRoomData>.Instance.noData.ceilingMat;
				break;
		}
		
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
				Vector3 pos = new Vector3(initX + x, (this.size / 2), initZ + z);
				GameObject tile = Instantiate(this.wallPrefab, pos, Quaternion.identity, this.levelGridContainer);
				Renderer renderer = tile.GetComponentInChildren<Renderer>();
				renderer.material = ceilingMat;
				tile.transform.forward = -Vector3.up;
			}
		}
					
		
		// Generate Bottom
		for (int x = 0; x < this.size; x++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3(initX + x, -(this.size / 2), initZ + z);
				GameObject tile = Instantiate(this.wallPrefab, pos, Quaternion.identity, this.levelGridContainer);
				Renderer renderer = tile.GetComponentInChildren<Renderer>();
				renderer.material = floorMat;
				tile.transform.forward = Vector3.up;
			}
		}
		
		// Generate Right
		for (int y = 0; y < this.size; y++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3((this.size / 2), initY + y, initZ + z);
				GameObject tile = Instantiate(this.wallPrefab, pos, Quaternion.identity, this.levelGridContainer);
				Renderer renderer = tile.GetComponentInChildren<Renderer>();
				renderer.material = wallMat;
				tile.transform.forward = -Vector3.right;
			}
		}
	
		// Generate Left
		for (int y = 0; y < this.size; y++) {
			for (int z = 0; z < this.size; z++) {
				Vector3 pos = new Vector3(-(this.size / 2), initY + y, initZ + z);
				GameObject tile = Instantiate(this.wallPrefab, pos, Quaternion.identity, this.levelGridContainer);
				Renderer renderer = tile.GetComponentInChildren<Renderer>();
				renderer.material = wallMat;
				tile.transform.forward = Vector3.right;
			}
		}
	
		// Generate Front
		for (int y = 0; y < this.size; y++) {
			for (int x = 0; x < this.size; x++) {
				Vector3 pos = new Vector3(initX + x, initY + y, (this.size / 2));
				GameObject tile = Instantiate(this.wallPrefab, pos, Quaternion.identity, this.levelGridContainer);
				Renderer renderer = tile.GetComponentInChildren<Renderer>();
				renderer.material = wallMat;
				tile.transform.forward = -Vector3.forward;
			}
		}
	
		// Generate Back
		for (int y = 0; y < this.size; y++) {
			for (int x = 0; x < this.size; x++) {
				Vector3 pos = new Vector3(initX + x, initY + y, -(this.size / 2));
				GameObject tile = Instantiate(this.wallPrefab, pos, Quaternion.identity, this.levelGridContainer);
				Renderer renderer = tile.GetComponentInChildren<Renderer>();
				renderer.material = wallMat;
				tile.transform.forward = Vector3.forward;
			}
		}
	}

	public bool CheckLevelCompletion() {
		for (int i = 0; i < this.rings.Length; i++) {
			if (!this.rings[i].IsObtained()) {
				return false;
			}
		}
		return true;
	}
	
	public void ResetLevel() {
		this.DoResetRoutine();
		for (int i = 0; i < this.canReset.Count; i++) {
			this.canReset[i].Initialize();
		}
		this.RingsCollected = 0;
		this.Turns = this.initialTurns;
	}
	
	private bool isResetting = false;
	
	public bool IsResetting {
		get {
			return this.isResetting;
		} set {
			this.isResetting = value;
		}
	}
	
	private void DoResetRoutine() {
		if (this.resetRoutine != null) {
			this.StopCoroutine(this.resetRoutine);
			this.resetRoutine = null;
		}
		this.resetRoutine = this.StartCoroutine(this.ResetRoutine());
	}
	
	public Coroutine resetRoutine;
	
	public IEnumerator ResetRoutine() {
		this.isResetting = true;
		yield return new WaitForSeconds(0.25f);
		this.isResetting = false;
	}
}
