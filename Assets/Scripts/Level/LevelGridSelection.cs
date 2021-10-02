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
	
	private void Update() {
		if (PlayerGridSelection.Instance.currentGrid == this) {
			this.transform.localScale = Vector3.one * 0.5f;
		} else {
			this.transform.localScale = Vector3.one * 0.25f;
		}
	}
}
