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
	
}
