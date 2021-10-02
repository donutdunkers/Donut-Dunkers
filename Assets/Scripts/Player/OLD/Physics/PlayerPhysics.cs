using System;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private PlayerCore core;
	
	[SerializeField]
	private PlayerPhysicsData physicsData;
	
	private PlayerPhysicsParams curPhysicsParams;
	
	[SerializeField]
	private float maxSpeed = 10f;
	
	[SerializeField]
	private float viewportEdgeOffset = 0.05f;
	
	private Vector3 bottomLeft;
	
	private Vector3 topRight;
	
	private Rect cameraRect;
	
	public LayerMask terrainMask;
	
	private Vector3 prevVelocity;
	
	private void Awake() {
		this.core = this.GetComponent<PlayerCore>();
	}
	
	private void Start() {
		this.bottomLeft = this.core.camera.ScreenToWorldPoint(Vector3.zero);
		this.topRight = this.core.camera.ScreenToWorldPoint(new Vector3(this.core.camera.pixelWidth, this.core.camera.pixelHeight));
		
		this.cameraRect = new Rect(this.bottomLeft.x, this.bottomLeft.y, this.topRight.x - this.bottomLeft.x, this.topRight.y - this.bottomLeft.y);
		
		this.Initialize();
	}
	
	private void Initialize() {
		if (this.physicsData != null) {
			this.SetPhysicsParams(this.physicsData.normalPhysics);
		}
	}
	
	private void Update() {
		if (this.core.gameplay.wasFlung) {
			
			this.prevVelocity = this.core.rigidbody.velocity;
			
			Vector3 pos = this.core.camera.WorldToViewportPoint(this.core.rigidbody.position);
			
			if (pos.x < this.viewportEdgeOffset && this.core.rigidbody.velocity.x < 0f) {
				this.core.rigidbody.velocity = Vector3.Reflect(this.core.rigidbody.velocity, Vector3.right) * this.curPhysicsParams.deflectMultiplier;
			}
			if (pos.x > 1f - this.viewportEdgeOffset && this.core.rigidbody.velocity.x > 0f) {
				this.core.rigidbody.velocity = Vector3.Reflect(this.core.rigidbody.velocity, -Vector3.right) * this.curPhysicsParams.deflectMultiplier;
				
			}
			if (pos.y < this.viewportEdgeOffset && this.core.rigidbody.velocity.y < 0f) {
				this.core.rigidbody.velocity = Vector3.Reflect(this.core.rigidbody.velocity, -Vector3.up) * this.curPhysicsParams.deflectMultiplier;
				
			}
			if (pos.y > 1f - this.viewportEdgeOffset && this.core.rigidbody.velocity.y > 0f) {
				this.core.rigidbody.velocity = Vector3.Reflect(this.core.rigidbody.velocity, Vector3.up) * this.curPhysicsParams.deflectMultiplier;
			}
		}
		
	}
	
	private void FixedUpdate() {
		if (this.core.gameplay.wasFlung) {
			this.core.rigidbody.velocity *= this.curPhysicsParams.deceleration * Time.fixedDeltaTime;
			this.core.rigidbody.velocity = Vector3.ClampMagnitude(this.core.rigidbody.velocity, this.maxSpeed);
			if (this.core.rigidbody.velocity.magnitude <= 0.5f) {
				this.core.rigidbody.velocity = Vector3.zero;
				this.core.gameplay.wasFlung = false;
			}
		}
	}
	
	private void OnCollisionEnter(Collision collision) {
		ContactPoint contactPoint = collision.contacts[0];
		if (contactPoint.otherCollider.gameObject.layer.InLayerMask(this.terrainMask)) {
			float velocity = this.prevVelocity.magnitude;
			Vector3 direction = Vector3.Reflect(this.prevVelocity.normalized, contactPoint.normal);
			this.core.rigidbody.velocity = direction * velocity * this.curPhysicsParams.deflectMultiplier;
		}
	}
	
	public PlayerPhysicsParams GetCurrentPhysicsParams() {
		return this.curPhysicsParams;
	}
	
	public void SetPhysicsParams(PlayerPhysicsParams param) {
		this.curPhysicsParams = param;
	}
}
