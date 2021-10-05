using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFling : MonoBehaviour
{
    private PlayerCore core;
	
	[SerializeField]
	private Vector2 minPower, maxPower;
	
	private Vector2 force;
	
	public Vector3 startPoint, endPoint;
	
	private void Awake() {
		this.core = this.GetComponent<PlayerCore>();
	}
	
	private void Update() {
		
		/*
		if (this.core.gameplay.wasFlung) {
			if (Input.GetMouseButtonUp(0)) {
				this.core.rigidbody.velocity = Vector3.zero;
				this.core.gameplay.wasFlung = false;
			}
			return;
		}*/
		
		if (Input.GetMouseButtonDown(0)) {
			this.startPoint = this.core.camera.ScreenToWorldPoint(Input.mousePosition);
			this.startPoint.z = 0f;
		}
		
		if (Input.GetMouseButton(0)) {
			Vector3 currentPoint = this.core.camera.ScreenToWorldPoint(Input.mousePosition);
			currentPoint.z = 0f;
		}
		
		if (Input.GetMouseButtonUp(0)) {
			this.endPoint = this.core.camera.ScreenToWorldPoint(Input.mousePosition);
			this.endPoint.z = 0f;
			
			float distance = Vector3.Distance(this.startPoint, this.endPoint);
			
			Debug.Log(distance);
			
			Vector3 dir = this.startPoint - this.endPoint;
			
			dir.z = 0f;
			
			dir = Vector3.ClampMagnitude(dir, 1f);
			
			if (distance < 2.5f) {
				return;
			}
			
			Debug.DrawLine(this.startPoint, this.endPoint - this.startPoint);
			
			this.core.rigidbody.velocity = dir * (distance * this.core.physics.GetCurrentPhysicsParams().flingMultiplier);
			this.core.gameplay.wasFlung = true;
		}
	}	
}
