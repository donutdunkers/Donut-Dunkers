using System;
using UnityEngine;

public class LevelTerrain : MonoBehaviour
{
    public enum TerrainType {
		Wall,
		Angle,
		ForceDirection
	}
	
	public LevelTerrain.TerrainType terrainType;
	
}
