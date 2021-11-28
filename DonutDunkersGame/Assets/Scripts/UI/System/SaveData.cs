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

    private void Start() {
        string levelKey = LevelData.Instance.LevelKey;
        int didBeatLevel = PlayerPrefs.GetInt(levelKey);
        int turns = PlayerPrefs.GetInt(levelKey + "_turns");
        if (didBeatLevel == 1) {
            Debug.Log(levelKey + " has been beaten in " + turns + "turns");
        } else {
            Debug.Log(levelKey + " has not been beaten");
        }
    }

    private void Update() {
        if (LevelData.Instance.RingsCollected == LevelData.Instance.RingsInLevel)
        {
            int turns = PlayerPrefs.GetInt(LevelData.Instance.LevelKey + "_turns");
            bool didBeatLevel = PlayerPrefs.GetInt(LevelData.Instance.LevelKey) == 1;
            if (didBeatLevel) {
                turns = turns < LevelData.Instance.TurnsTaken ? turns : LevelData.Instance.TurnsTaken;
            } else {
                turns = LevelData.Instance.TurnsTaken;
            }

            Debug.Log("Beat level: " + LevelData.Instance.LevelKey);
            PlayerPrefs.SetInt(LevelData.Instance.LevelKey, 1);
            PlayerPrefs.SetInt(LevelData.Instance.LevelKey + "_turns", turns);
            PlayerPrefs.Save();
        }
    }
}
