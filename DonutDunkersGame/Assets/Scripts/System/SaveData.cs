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

    public void BeatLevel(string levelKey) {
        PlayerPrefs.SetInt(levelKey, 1);
        PlayerPrefs.Save();
    }
}
