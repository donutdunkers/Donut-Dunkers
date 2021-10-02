using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
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
	
	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}
	
	private void Start() {
		this.gameObject.SetActive(false);
	}
	
	private void FixedUpdate() {
		this.transform.position += this.transform.forward * 10f * Time.fixedDeltaTime;
	//	this.rigidbody.MovePosition(this.transform.localPosition + (this.transform.forward * 20f * Time.fixedDeltaTime));
	}
	
	public void SetForwardDirection(Vector3 forward) {
		this.transform.forward = forward;
	}
	
	public void OnCollisionEnter(Collision other) {
		LevelTerrain terrain = other.gameObject.GetComponent<LevelTerrain>();
		if (terrain != null) {
			switch (terrain.terrainType) {
				case LevelTerrain.TerrainType.Wall:
					this.SetForwardDirection(-this.transform.forward);
					break;
				case LevelTerrain.TerrainType.Angle:
					if (this.transform.forward == other.gameObject.transform.right || this.transform.forward == -other.gameObject.transform.right || this.transform.forward == other.gameObject.transform.up || this.transform.forward == other.gameObject.transform.forward) {
						this.SetForwardDirection(-this.transform.forward);
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
		}
	}
}
