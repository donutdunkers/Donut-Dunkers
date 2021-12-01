using System;
using UnityEngine;

public class BallSkin : MonoBehaviour, ICanReset {
	
    private static BallSkin _Instance;
    public static BallSkin Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<BallSkin>();
            }
            return _Instance;
        }
    }
	
	[SerializeField]
	private float followSpeed = 5f;
	
	[SerializeField]
	private Transform ballTransform;
	
	public Transform BallTransform {
		get {
			return this.ballTransform;
		}
	}
	
	[SerializeField]
	public GameObject[] directionArrows = new GameObject[6];
	
	private TrailRenderer trail;
	
	public TrailRenderer Trail {
		get {
			return this.trail;
		}
	}
	
	private void Awake() {
		this.trail = this.GetComponentInChildren<TrailRenderer>();
	}
	
	private void Start() {
		this.Initialize();
	}
	
	public void Initialize() {
		this.transform.position = BallController.Instance.transform.position;
		BallSkin.Instance.Trail.enabled = false;
		this.trail.Clear();
		BallSkin.Instance.Trail.enabled = true;
	//	this.ToggleArrows(true);
	}
	
	private void Update() {
		this.transform.position = Vector3.Lerp(this.transform.position, BallController.Instance.transform.position, Time.deltaTime * this.followSpeed);
		this.ballTransform.forward = BallController.Instance.transform.forward;
	}
	
	public void ToggleArrows(bool toggle) {
		if (LevelData.Instance.Turns <= 0) {
			toggle = false;
		}
		switch (toggle) {
			case true:
				for (int i = 0; i < this.directionArrows.Length; i++) {
					RaycastHit hit;
					bool flag = true;
					if (Physics.Raycast(BallController.Instance.transform.position, this.directionArrows[i].transform.up, out hit, 1.5f, PlayerGridSelection.Instance.TerrainMask, QueryTriggerInteraction.Collide)) {
						ObjectInteraction obj = hit.collider.GetComponent<ObjectInteraction>();
						if (obj != null) {
							Debug.Log(obj.gameObject.name);
							flag = obj.CanMoveTowards();
						}
					}
					this.directionArrows[i].SetActive(flag);	
				}
				break;
			case false:
				for (int i = 0; i < this.directionArrows.Length; i++) {
					this.directionArrows[i].SetActive(false);
				}
				break;
		}		
	}
}
