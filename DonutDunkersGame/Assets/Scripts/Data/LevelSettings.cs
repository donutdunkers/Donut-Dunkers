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

    [Tooltip("Min num of moves to earn first second and third star")]
    public int[] starMoveThresholds = new int[3];

    public bool isLevelComplete = false;

    public int GetNumStars(int numMoves) {
        int numStars = 0;
        for (int i = 0; i < starMoveThresholds.Length; i++) {
            if (numMoves <= starMoveThresholds[i]) {
                numStars++;
            }
        }
        return numStars;
    }

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