using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Levels/World", fileName = "World")]
public class WorldSettings : ScriptableObject {

    public string worldName;
    public int worldIndex;
    public List<LevelSettings> levels;

    public string GetNextLevel() {
        int currLevelIndex = LevelInfo.Instance.currLevel.levelIndex;
        if (currLevelIndex + 1 < levels.Count - 1) {
            currLevelIndex++;
            return levels[currLevelIndex].sceneName;
        }
        return "Menu_Main";
    }
}
