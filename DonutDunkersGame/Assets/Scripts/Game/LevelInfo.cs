using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
    public WorldSettings currWorld;
    public LevelSettings currLevel;
    public List<WorldSettings> allWorlds;
    public bool allowContinue = true;

    private static LevelInfo _Instance;

    public static LevelInfo Instance {
        get
        {
            if (_Instance == null) {
                _Instance = FindObjectOfType<LevelInfo>();
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _Instance = this;
        }
    }

    public void InitializeWorlds()
    {
        foreach(WorldSettings world in allWorlds)
        {
            world.InitializeLevels();
        }
    }

    public string FindLevel(int worldIndex, int levelIndex)
    {
        WorldSettings world = allWorlds[worldIndex];
        if(levelIndex < world.levels.Count)
        {
            foreach(LevelSettings level in world.levels)
            {
                if (level.levelIndex == levelIndex)
                    return level.sceneName;
            }
        }
        return "Menu_Main";
    }

    public bool FindNextLevel(int worldIndex, int levelIndex, out LevelSettings nextLevel)
    {
        WorldSettings world = allWorlds[worldIndex];
        if(levelIndex + 1 < world.levels.Count)
        {
            foreach (LevelSettings level in world.levels)
            {
                if (level.levelIndex == levelIndex + 1)
                {
                    nextLevel = level;
                    return true;
                }
            }
        }
        else if (worldIndex + 1 < allWorlds.Count)
        {
            nextLevel = allWorlds[worldIndex + 1].levels[0];
            return true;
        }
        nextLevel = null;
        return false;
    }
}
