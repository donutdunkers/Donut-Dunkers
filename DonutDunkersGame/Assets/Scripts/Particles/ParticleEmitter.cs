using System;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour, ICanReset {
	
	private ParticleSystem[] childParticles;
	
	private Transform[] childTransforms;
	
	private new Transform transform;
	
	private Vector3 position;
	
	private Quaternion rotation;
	
	public void Initialize() {
		this.StopAndClearEmit();
	}
	
	private void Awake() {
		this.childParticles = this.GetComponentsInChildren<ParticleSystem>();
		this.childTransforms = this.GetComponentsInChildren<Transform>();
	}
	
	private void Start() {
		this.transform = base.gameObject.transform;
		this.childTransforms = new Transform[this.childParticles.Length];
		for (int i = 0; i < this.childParticles.Length; i++) {
			this.childTransforms[i] = this.childParticles[i].transform;
			this.childTransforms[i].parent = this.transform.parent;
		}
	}
	
	public void Emit() {
		this.position = this.transform.position;
		this.rotation = this.transform.rotation;
		for (int i = 0; i < this.childTransforms.Length; i++) {
			this.childTransforms[i].SetPositionAndRotation(this.position, this.rotation);
			var em = this.childParticles[i].emission;
			ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[em.burstCount];
			em.GetBursts(bursts);
			int rand = UnityEngine.Random.Range(0, bursts.Length);
			if (bursts.Length > 0f) {
				this.childParticles[i].Emit(UnityEngine.Random.Range(bursts[rand].minCount, bursts[rand].maxCount + 1));
			}
			this.childParticles[i].Play();
		}
	}
	
	public void Emit(Vector3 position) {
		this.position = position;
		this.rotation = this.transform.rotation;
		for (int i = 0; i < this.childTransforms.Length; i++) {
			this.childTransforms[i].SetPositionAndRotation(position, this.rotation);
			var em = this.childParticles[i].emission;
			ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[em.burstCount];
			em.GetBursts(bursts);
			int rand = UnityEngine.Random.Range(0, bursts.Length);
			if (bursts.Length > 0f) {
				this.childParticles[i].Emit(UnityEngine.Random.Range(bursts[rand].minCount, bursts[rand].maxCount + 1));
			}
			this.childParticles[i].Play();
		}
	}
	
	public void Emit(Quaternion rotation) {
		this.position = this.transform.position;
		this.rotation = rotation;
		for (int i = 0; i < this.childTransforms.Length; i++) {
			this.childTransforms[i].SetPositionAndRotation(this.position, rotation);
			var em = this.childParticles[i].emission;
			ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[em.burstCount];
			em.GetBursts(bursts);
			int rand = UnityEngine.Random.Range(0, bursts.Length);
			if (bursts.Length > 0f) {
				this.childParticles[i].Emit(UnityEngine.Random.Range(bursts[rand].minCount, bursts[rand].maxCount + 1));
			}
			this.childParticles[i].Play();
		}
	}
	
	public void Emit(Vector3 position, Quaternion rotation) {
		this.position = position;
		this.rotation = rotation;
		for (int i = 0; i < this.childTransforms.Length; i++) {
			this.childTransforms[i].SetPositionAndRotation(position, rotation);
			var em = this.childParticles[i].emission;
			ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[em.burstCount];
			em.GetBursts(bursts);
			int rand = UnityEngine.Random.Range(0, bursts.Length);
			if (bursts.Length > 0f) {
				this.childParticles[i].Emit(UnityEngine.Random.Range(bursts[rand].minCount, bursts[rand].maxCount + 1));
			}
			this.childParticles[i].Play();
		}
	}
	
	public void StopEmit() {
		for (int i = 0; i < this.childParticles.Length; i++) {
			this.childParticles[i].Stop();
		}
	}
	
	public void StopAndClearEmit() {
		for (int i = 0; i < this.childParticles.Length; i++) {
			this.childParticles[i].Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);;
		}
	}
	
	public void SetParticleColors(Color color) {
		for (int i = 0; i < this.childParticles.Length; i++) {
			this.childParticles[i].startColor = color;
		}
	}
	
	public bool IsEmitting() {
		for (int i = 0; i < this.childParticles.Length; i++) {
			if (this.childParticles[i].isPlaying) {
				return true;
			}
		}
		return false;
	}
}