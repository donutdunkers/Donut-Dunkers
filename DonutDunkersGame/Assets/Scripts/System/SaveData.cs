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
        if (didBeatLevel == 1) {
            Debug.Log(levelKey + " has been beaten");
            PlayerPrefs.SetInt(levelKey, 0);
        } else {
            Debug.Log(levelKey + "has not been beaten");
        }
    }

    private void Update() {
        if (LevelData.Instance.RingsCollected == LevelData.Instance.RingsInLevel)
        {
            Debug.Log("Beat level: " + LevelData.Instance.LevelKey);
            PlayerPrefs.SetInt(LevelData.Instance.LevelKey, 1);
            PlayerPrefs.Save();
        }
    }
}
