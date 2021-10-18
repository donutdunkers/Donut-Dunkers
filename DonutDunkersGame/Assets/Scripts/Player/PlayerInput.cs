using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	
    private static PlayerInput _Instance;
    public static PlayerInput Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<PlayerInput>();
            }
            return _Instance;
        }
    }
	
	private float leftTimer = 0f, rightTimer = 0f;
	
	private const float CLICK_TIMER = 0.25f;
	
	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			this.leftTimer = 0f;
		}
		if (Input.GetMouseButton(0)) {
			this.leftTimer += Time.deltaTime;
		}
	}
	
    public bool GetMouseLeftClick() {
		return Input.GetMouseButtonUp(0) && this.leftTimer < CLICK_TIMER;
	}
	
	public bool GetMouseLeftHeld() {
		return Input.GetMouseButton(0) && this.leftTimer >= CLICK_TIMER;		
	}
}
