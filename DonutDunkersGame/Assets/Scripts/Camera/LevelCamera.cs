using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour {
	
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
	
	private void Start() {
		this.camera = this.GetComponentInChildren<Camera>();
		
		float average = (LevelData.Instance.size + LevelData.Instance.size + LevelData.Instance.size) / 3f;
		this.camera.transform.position += -this.camera.transform.forward * LevelData.Instance.size;
	//	this.pivot.transform.rotation = Quaternion.Euler(new Vector3(-10f, -45f, 10f));
	}
	
	private void Update() {
		if (Input.GetMouseButton(0)) {
			this.mPosDelta = Input.mousePosition - this.mPrevPos;
			/*
			if (this.mPosDelta.magnitude > this.rotationSpeed) {
				this.rotationSpeed = this.mPosDelta.magnitude;
				this.rotationSpeed = Mathf.Clamp(this.rotationSpeed, 0f, 50f);
			}
			*/
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
	}
}
