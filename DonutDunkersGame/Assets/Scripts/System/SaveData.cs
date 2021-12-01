using System;
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

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Awake() {

        if(!PlayerPrefs.HasKey("lastComplete_level"))
        {
            PlayerPrefs.SetInt("lastComplete_level", -1);
            PlayerPrefs.SetInt("lastComplete_world", -1);
        }
    }

    public void SaveMasterVolume(float masterVol)
    {
        PlayerPrefs.SetFloat("vol_master", masterVol);
    }
    public void SaveMusicVolume(float musicVol)
    {
        PlayerPrefs.SetFloat("vol_music", musicVol);
    }
    public void SaveSFXVolume(float SFXVol)
    {
        PlayerPrefs.SetFloat("vol_sfx", SFXVol);
    }

    public float LoadMasterVolume()
    {
        return PlayerPrefs.GetFloat("vol_master");
    }
    public float LoadMusicVolume()
    {
        return PlayerPrefs.GetFloat("vol_music");
    }
    public float LoadSFXVolume()
    {
        return PlayerPrefs.GetFloat("vol_sfx");
    }

    public void SaveLevelData(string levelKey, int levelIndex, int worldIndex)
    {
        int lastCompletedLevel = PlayerPrefs.GetInt("lastComplete_level");
        int lastCompletedWorld = PlayerPrefs.GetInt("lastComplete_world");
        if(levelIndex > lastCompletedLevel || worldIndex > lastCompletedWorld)
        {
            PlayerPrefs.SetInt("lastComplete_level", levelIndex);
            PlayerPrefs.SetInt("lastComplete_world", worldIndex);
        }

        int turns = LevelData.Instance.TurnsTaken;
        int stars = LevelData.Instance.Stars;
        if(PlayerPrefs.HasKey(levelKey))
        {
            int oldTurns = PlayerPrefs.GetInt(levelKey + "_turns");
            turns = oldTurns < turns ? oldTurns : turns;

            int oldStars = PlayerPrefs.GetInt(levelKey + "_stars");
            stars = oldStars > stars ? oldStars : stars;
        }

        PlayerPrefs.SetInt(levelKey, 1);

        PlayerPrefs.SetInt(levelKey + "_turns", turns);
        PlayerPrefs.SetInt(levelKey + "_stars", stars);

        PlayerPrefs.Save();
    }

    public LevelSaveData GetLevelSaveData(string levelKey)
    {
        if(!PlayerPrefs.HasKey(levelKey))
        {
            return new LevelSaveData(0, 0, false);
        }

        int turns = PlayerPrefs.GetInt(levelKey + "_turns");
        int stars = PlayerPrefs.GetInt(levelKey + "_stars");

        return new LevelSaveData(turns, stars, true);
    }

    public int GetLastCompletedLevelIndex()
    {
        return PlayerPrefs.GetInt("lastComplete_level");
    }

    public int GetLastCompletedWorldIndex()
    {
        return PlayerPrefs.GetInt("lastComplete_world");
    }

    public bool isLevelSaved(string levelKey)
    {
        return PlayerPrefs.HasKey(levelKey);
    }
}
public class LevelSaveData
{
    int turns;
    int stars;
    bool levelCompleted;

    public LevelSaveData(int turns, int stars, bool levelCompleted)
    {
        this.turns = turns;
        this.stars = stars;
        this.levelCompleted = levelCompleted;
    }

    public int GetTurns()
    {
        return this.turns;
    }

    public int GetStars()
    {
        return this.stars;
    }

    public bool isLevelComplete()
    {
        return this.levelCompleted;
    }
}
