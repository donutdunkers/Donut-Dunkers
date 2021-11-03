using System;
using UnityEngine;

public class LevelGridSelection : MonoBehaviour
{
    public enum GridSide {
		Top,
		Bottom,
		Front,
		Back,
		Left,
		Right
	}
	
//	[NonSerialized]
	public LevelGridSelection.GridSide gridSide;
	
	[SerializeField]
	private Renderer renderer;
	
	private MaterialPropertyBlock propBlock;
	
	private void Awake() {
		this.propBlock = new MaterialPropertyBlock();
		this.renderer = this.GetComponentInChildren<Renderer>();
		this.renderer.GetPropertyBlock(this.propBlock);
	}
	
	private void LateUpdate() {
		if (PlayerGridSelection.Instance.currentGrid == this) {
			this.propBlock.SetColor("_BaseColor", Color.Lerp(this.propBlock.GetColor("_BaseColor"), PlayerGridSelection.Instance.highlightColor, Time.deltaTime * 25f));
		} else {
			this.propBlock.SetColor("_BaseColor", Color.Lerp(this.propBlock.GetColor("_BaseColor"), PlayerGridSelection.Instance.idleColor, Time.deltaTime * 25f));
		}
		this.renderer.SetPropertyBlock(this.propBlock);
	}
	
}
