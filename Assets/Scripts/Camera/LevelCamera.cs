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
	
	private void Start() {
		this.camera = this.GetComponentInChildren<Camera>();
		
		float average = (LevelData.Instance.xSize + LevelData.Instance.ySize + LevelData.Instance.zSize) / 3f;
		this.camera.transform.position = new Vector3(0f, 0f, -(average * 2f));
	}
	
	private void Update() {
		if (Input.GetMouseButton(0)) {
			this.mPosDelta = Input.mousePosition - this.mPrevPos;
			
			if (Vector3.Dot(this.transform.up, Vector3.up) >= 0f) {
				this.pivot.Rotate(Vector3.up, -Vector3.Dot(this.mPosDelta, Camera.main.transform.right), Space.World);
			} else {
				this.pivot.Rotate(Vector3.up, Vector3.Dot(this.mPosDelta, Camera.main.transform.right), Space.World);
			}
			this.pivot.Rotate(Camera.main.transform.right, Vector3.Dot(this.mPosDelta, Camera.main.transform.up), Space.World);
		}
		this.mPrevPos = Input.mousePosition;
	}
}
