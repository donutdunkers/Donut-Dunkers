using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {


    public List<SelectionDonut> selectionDonuts;
    private static LevelSelectManager _instance;

    public static LevelSelectManager Instance { get { return _instance; } }

    private int currLevelId;
    private int currWorldId;

    LevelSettings levelSettings;
    WorldSettings worldSettings;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    public void SelectLevel(int levelId) {
        currLevelId = levelId;
    }

    public void SelectWorld(int worldId) {
        currWorldId = worldId;
        SelectLevel(0);
    }
    
}