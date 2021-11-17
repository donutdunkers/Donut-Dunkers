using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Levels/World", fileName = "World")]
public class WorldSettings : ScriptableObject {

    public string worldName;
    public int worldIndex;
    public List<LevelSettings> levels;

    public string GetNextLevel(int currLevelIndex) {
        if (currLevelIndex < levels.Count - 1) {
            return levels[currLevelIndex].sceneName;
        }
        return "Menu_Main";
    }
}
