using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Levels/Level", fileName = "Level")]
public class LevelSettings : ScriptableObject {
    public string sceneName;
    public int levelIndex;
    public int levelPar;

    public int minTurns;
    public int maxStars;

    public bool isLevelComplete = false;

    public void SaveLevelData()
    {
        SaveData.Instance.SaveLevelData(sceneName, levelIndex, LevelInfo.Instance.currWorld.worldIndex);
    }
    public void GetLevelSaveData()
    {
        LevelSaveData saveData = SaveData.Instance.GetLevelSaveData(sceneName);
        minTurns = saveData.GetTurns();
        maxStars = saveData.GetStars();
        isLevelComplete = saveData.isLevelComplete();
    }
}