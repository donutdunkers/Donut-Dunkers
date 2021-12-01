using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Levels/World", fileName = "World")]
public class WorldSettings : ScriptableObject {

    public string worldName;
    public int worldIndex;
    public int starsCollected;
    public List<LevelSettings> levels;

    public LevelSettings GetNextLevel() {
        int currLevelIndex = LevelInfo.Instance.currLevel.levelIndex;
        if (currLevelIndex + 1 < levels.Count - 1) {
            currLevelIndex++;
            return levels[currLevelIndex];
        }
        return null;
    }

    public void InitializeLevels()
    {
        starsCollected = 0;
        foreach(LevelSettings level in levels)
        {
            level.GetLevelSaveData();
            starsCollected += level.maxStars;
        }
    }
}
