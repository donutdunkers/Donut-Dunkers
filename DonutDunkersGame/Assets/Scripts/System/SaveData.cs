using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private static SaveData _Instance;
    public static SaveData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<SaveData>();
            }
            return _Instance;
        }
    }
 
    // Returns true if level has been complete false otherwise
    public bool IsLevelComplete(string levelkey) {
        if (PlayerPrefs.HasKey(levelkey)) {
            return PlayerPrefs.GetInt(levelkey) == 0;
        }
        return false;
    }

    // Returns lowest number of turns level has been completed in; -1 if level has not been completed
    public int BestTurnsToComplete(string levelkey) {
        if (PlayerPrefs.HasKey(levelkey + "_turns")) {
            return PlayerPrefs.GetInt(levelkey + "_turns");
        }
        return -1;
    }

    private void Start() {
        LevelSettings currLevel = LevelInfo.Instance.currLevel;

        string levelKey = currLevel.sceneName;
        int didBeatLevel = PlayerPrefs.GetInt(levelKey);
        int turns = PlayerPrefs.GetInt(levelKey + "_turns");
        if (didBeatLevel == 1) {
            Debug.Log(levelKey + " has been beaten in " + turns + "turns");
        } else {
            Debug.Log(levelKey + " has not been beaten");
        }
    }

    private void Update() {
        LevelSettings currLevel = LevelInfo.Instance.currLevel;
        string levelKey = currLevel.sceneName;

        if (LevelData.Instance.RingsCollected == LevelData.Instance.RingsInLevel)
        {
            int turns = PlayerPrefs.GetInt(levelKey + "_turns");
            bool didBeatLevel = PlayerPrefs.GetInt(levelKey) == 1;
            if (didBeatLevel) {
                turns = turns < LevelData.Instance.TurnsTaken ? turns : LevelData.Instance.TurnsTaken;
            } else {
                turns = LevelData.Instance.TurnsTaken;
            }

            Debug.Log("Beat level: " + levelKey);
            PlayerPrefs.SetInt(levelKey, 1);
            PlayerPrefs.SetInt(levelKey + "_turns", turns);
            PlayerPrefs.Save();
        }
    }
}
