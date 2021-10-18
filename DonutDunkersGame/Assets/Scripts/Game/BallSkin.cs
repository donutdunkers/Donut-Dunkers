using System;
using UnityEngine;

public class BallSkin : MonoBehaviour {
	
	[SerializeField]
	private float followSpeed = 5f;
	
	private void Start() {
		
	}
	
	private void Update() {
		this.transform.position = Vector3.Lerp(this.transform.position, BallController.Instance.transform.position, Time.deltaTime * this.followSpeed);
	}
}
