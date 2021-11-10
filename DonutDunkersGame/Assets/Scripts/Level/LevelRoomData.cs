using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Room Data", fileName = "Level Room Database")]
public class LevelRoomData : ScriptableSingleton<LevelRoomData> {
	
	public RoomData noData, kitchenData;
	
}

[System.Serializable]
public class RoomData {
	
	// add music to this too
	
	public Material floorMat, wallLowMat, wallMidMat, wallHighMat, ceilingMat;
	
}
