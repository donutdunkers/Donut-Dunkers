using System;
using UnityEngine;

public class BallController : MonoBehaviour {
	
    private static BallController _Instance;
    public static BallController Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<BallController>();
            }
            return _Instance;
        }
    }
	
	[NonSerialized]
	public Rigidbody rigidbody;
	
	private enum Direction {
		Up,
		Down,
		Left,
		Right,
		Forward,
		Back
	}
	
	public Vector3 forwardDirection;
	
	[SerializeField]
	private bool isMoving = false;
	
	public bool IsMoving {
		set {
			this.isMoving = value;
		} get {
			return this.isMoving;
		}
	}
	
	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}
	
	private void Start() {
		this.Initialize();
	//	this.gameObject.SetActive(false);
	}
	
	public void Initialize() {
		// this.transform.position = LevelData.Instance.LevelStartPosition;
		this.isMoving = false;
	}
	
	private void FixedUpdate() {
		if (!this.isMoving) {
			return;
		}
		this.transform.position = this.transform.position + this.transform.forward * 10f * Time.fixedDeltaTime;
	}
	
	public void SetForwardDirection(Vector3 forward) {
		this.transform.forward = forward;
	}
	
	public void OnCollisionEnter(Collision other) {
		LevelTerrain terrain = other.gameObject.GetComponent<LevelTerrain>();
		if (terrain != null) {
			switch (terrain.terrainType) {
				case LevelTerrain.TerrainType.Wall:
					this.isMoving = false;
					Vector3 otherPos = terrain.transform.position;
					this.transform.position = otherPos - this.transform.forward;
					break;
				case LevelTerrain.TerrainType.Angle:
					if (this.transform.forward == other.gameObject.transform.right || this.transform.forward == -other.gameObject.transform.right || this.transform.forward == other.gameObject.transform.up || this.transform.forward == other.gameObject.transform.forward) {
						this.isMoving = false;
						Vector3 otherPos2 = terrain.transform.position;
						this.transform.position = otherPos2 - this.transform.forward;
					} else {
						this.transform.position = other.transform.position;
						if (this.transform.forward == -other.gameObject.transform.forward) {
							this.SetForwardDirection(other.gameObject.transform.up);
						} else if (this.transform.forward == -other.gameObject.transform.up) {
							this.SetForwardDirection(other.gameObject.transform.forward);
						}
					}
					break;
			}
		} else {
			this.SetForwardDirection(-this.transform.forward);
		}
	}
}
