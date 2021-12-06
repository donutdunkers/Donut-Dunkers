using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour, ICanReset {
	
    private static LevelCamera _Instance;
    public static LevelCamera Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<LevelCamera>();
            }
            return _Instance;
        }
    }
	
	
    public Vector3 mPrevPos = Vector3.zero;
	public Vector3 mPosDelta = Vector3.zero;
	
	[NonSerialized]
	public Camera camera;
	
	[SerializeField]
	private Transform pivot;
	
	[SerializeField]
	private float rotationSpeed = 0f;
	
	public bool isRotating = false;
	
	private Quaternion baseRotation;
	
	private const float MAX_ROTATION_SPEED = 200f;
	
	private const float ROTATION_SPEED = 800f;
	
	private float curRotSpeed = 0f;
	
	private bool isPerspective;
	
	private float PERSPECTIVE_VIEW_TOGGLE_SPEED = 15f;
	
	private void Start() {
		this.camera = this.GetComponentInChildren<Camera>();
		
		float average = (LevelData.Instance.size + LevelData.Instance.size + LevelData.Instance.size) / 3f;
		this.camera.transform.position = -this.camera.transform.forward * (100f * LevelData.Instance.size);
		this.camera.fieldOfView = 1f;
	//	this.pivot = LevelData.Instance.levelRotationContainer;
		this.baseRotation = this.pivot.rotation;
		this.isPerspective = false;
		
		float distance = Vector3.Distance(this.camera.transform.position, Vector3.zero);
		this.initHeightAtDist = this.FrustumHeightAtDistance(distance);
		TogglePerspective();
		
		this.Initialize();
	}
	
	public void Initialize() {
		this.isRotating = false;
		this.transform.localEulerAngles = new Vector3(10f, 45f, 0f);
		this.pivot.rotation = this.baseRotation;
	}
	
	public void DoRotation(Quaternion rot) {
		if (this.isRotating) {
			return;
		}
		if (this.rotationRoutine != null) {
			this.StopCoroutine(this.rotationRoutine);
			this.rotationRoutine = null;
		}
		this.rotationRoutine = this.StartCoroutine(RotationRoutine(rot));
	}
	
	private void Update() {
		if (TutorialWindow.Instance != null) {
			if (TutorialWindow.Instance.IsTutorialActive()) {
				return;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			this.TogglePerspective();
		}
		
		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || GameMenuUI.Instance.IsPauseMenuActive || GameMenuUI.Instance.IsEndGameMenuActive) {
			this.curRotSpeed = 0f;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
			this.curRotSpeed += Time.deltaTime * ROTATION_SPEED;
			this.curRotSpeed = Mathf.Clamp(this.curRotSpeed,  0f, MAX_ROTATION_SPEED);
		}
		
		if (Input.GetKey(KeyCode.A)) {
			Vector3 rot = this.pivot.eulerAngles;
			rot.y += curRotSpeed * Time.deltaTime;
			this.pivot.eulerAngles = rot;
		//	this.DoRotation(rot * Quaternion.AngleAxis(-ROTATION_HORIZONTAL_ANGLE, Vector3.up));
		}
		if (Input.GetKey(KeyCode.D)) {
			Vector3 rot = this.pivot.eulerAngles;
			rot.y -= curRotSpeed * Time.deltaTime;
			this.pivot.eulerAngles = rot;
		//	this.DoRotation(rot * Quaternion.AngleAxis(ROTATION_HORIZONTAL_ANGLE, Vector3.up));
			
		}
		
		if (Input.GetKey(KeyCode.W)) {
			Vector3 rot = this.transform.eulerAngles;
			rot.x += curRotSpeed * Time.deltaTime;
			if (rot.x <= 250f && rot.x >= 75f) {
				return;
			}
			this.transform.eulerAngles = rot;
			
		}
		if (Input.GetKey(KeyCode.S)) {
			Vector3 rot = this.transform.eulerAngles;
			rot.x -= curRotSpeed * Time.deltaTime;
			if (rot.x >= 80f && rot.x <= 285f) {
				return;
			}
			this.transform.eulerAngles = rot;
			
		}
		
		// OLD CAMERA MOVEMENT CODE
		/*
		if (Input.GetMouseButton(0)) {
			this.mPosDelta = Input.mousePosition - this.mPrevPos;
			/*
			if (this.mPosDelta.magnitude > this.rotationSpeed) {
				this.rotationSpeed = this.mPosDelta.magnitude;
				this.rotationSpeed = Mathf.Clamp(this.rotationSpeed, 0f, 50f);
			}
		}
	//	if (this.rotationSpeed > 0.2f) {
			if (Vector3.Dot(this.transform.up, Vector3.up) >= 0f) {
				this.pivot.Rotate(Vector3.up, -Vector3.Dot(this.mPosDelta, Camera.main.transform.right), Space.World);
			} else {
				this.pivot.Rotate(Vector3.up, Vector3.Dot(this.mPosDelta, Camera.main.transform.right), Space.World);
			}
			this.pivot.Rotate(Camera.main.transform.right, Vector3.Dot(this.mPosDelta, Camera.main.transform.up), Space.World);
		//	this.rotationSpeed -= Time.deltaTime;
	//	}
		this.mPrevPos = Input.mousePosition;
		*/
	}
	
	private Coroutine rotationRoutine;
	
	public IEnumerator RotationRoutine(Quaternion targetRot) {
		this.isRotating = true;
		while (this.pivot.rotation != targetRot) {
			this.pivot.rotation = Quaternion.Lerp(this.pivot.rotation, targetRot, Time.deltaTime * ROTATION_SPEED);
			yield return null;
		}
		Vector3 rot = this.pivot.eulerAngles;
		Debug.Log(rot.y);
		rot.y = Mathf.Round(rot.y);
		this.pivot.eulerAngles = rot;
		this.isRotating = false;
	}

	private void TogglePerspective() {
		if (this.perspectiveRoutine != null) {
			this.StopCoroutine(this.perspectiveRoutine);
		}
		this.perspectiveRoutine = this.StartCoroutine(this.PerspectiveRoutine());
	}

	private Coroutine perspectiveRoutine;

	private float initHeightAtDist;

    // Calculate the frustum height at a given distance from the camera.
	private float FrustumHeightAtDistance(float distance) {
        return 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance.
    private float FOVForHeightAndDistance(float height, float distance) {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }
	
	public IEnumerator PerspectiveRoutine() {
		float zPos = (this.isPerspective) ? -100f * LevelData.Instance.size : (LevelData.Instance.size * -1f) - (LevelData.Instance.size * 1.5f);
		Vector3 targetPos = new Vector3(0f, 0f, zPos);
		float speed = (this.isPerspective) ? PERSPECTIVE_VIEW_TOGGLE_SPEED / (PERSPECTIVE_VIEW_TOGGLE_SPEED * 4f) : PERSPECTIVE_VIEW_TOGGLE_SPEED;
		this.isPerspective = !this.isPerspective;
		while (this.camera.transform.localPosition != targetPos) {
			this.camera.transform.localPosition = Vector3.Lerp(this.camera.transform.localPosition, targetPos, Time.deltaTime * speed);
			float currDistance = Vector3.Distance(this.camera.transform.position, Vector3.zero);
			this.camera.fieldOfView = FOVForHeightAndDistance(this.initHeightAtDist, currDistance);
			yield return null;
		}
		this.perspectiveRoutine = null;
	}
}
