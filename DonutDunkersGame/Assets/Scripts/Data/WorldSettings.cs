using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Levels/World", fileName = "World")]
public class WorldSettings : ScriptableObject {
    public int worldID;
    public List<LevelSettings> levels;


}
