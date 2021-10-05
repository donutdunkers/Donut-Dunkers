using System;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [NonSerialized]
	public Rigidbody rigidbody;
	
	[NonSerialized]
	public PlayerPhysics physics;
	
	[NonSerialized]
	public PlayerGameplay gameplay;
	
	[NonSerialized]
	public PlayerFling fling;
	
	public Camera camera;
	
	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
		
		this.physics = this.GetComponent<PlayerPhysics>();
		this.gameplay = this.GetComponent<PlayerGameplay>();
		this.fling = this.GetComponent<PlayerFling>();
		
	}
}
