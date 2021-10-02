using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
    private PlayerCore core;
	
	public bool wasFlung = false;
	
	private void Awake() {
		this.core = this.GetComponent<PlayerCore>();
	}
	
	private void Update() {
	}
	
	private void FixedUpdate() {
	}
	
	private void LateUpdate() {
	}
	
}
