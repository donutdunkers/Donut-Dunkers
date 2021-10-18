using System;
using UnityEngine;

public class BallController : MonoBehaviour, ICanReset {
	
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
	
	private bool isAlive = true;
	
	public bool IsAlive {
		set {
			this.isAlive = value;
		} get {
			return this.isAlive;
		}
	}
	
	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}
	
	private void Start() {
		LevelData.Instance.StartPos = this.transform.localPosition;
		this.Initialize();
	//	this.gameObject.SetActive(false);
	}
	
	public void Initialize() {
		this.transform.localPosition = LevelData.Instance.LevelStartPosition;
		this.isMoving = false;
		this.isAlive = true;
	}
	
	private void Update() {
		this.HandleLevelExit();
	}
	
	private void HandleLevelExit() {
		float distance = LevelData.Instance.size;
		bool flagX = this.transform.position.x > distance || this.transform.position.x < -distance;
		bool flagY = this.transform.position.y > distance || this.transform.position.y < -distance;
		bool flagZ = this.transform.position.z > distance || this.transform.position.z < -distance;
		if (flagX || flagY || flagZ) {
			this.transform.localPosition = LevelData.Instance.StartPos;
			this.isMoving = false;
		}
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
		ObjectInteraction interaction = other.gameObject.GetComponent<ObjectInteraction>();
		if (interaction != null) {
			interaction.PlayerInteraction();
		}
	}
	
	public void OnTriggerEnter(Collider other) {
		ObjectInteraction interaction = other.gameObject.GetComponent<ObjectInteraction>();
		if (interaction != null) {
			interaction.PlayerInteraction();
		}
	}
}
