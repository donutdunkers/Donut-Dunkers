using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {

    public bool isSelectable = false;
    public List<SelectionDonut> selectionDonuts;
    // private static LevelSelectManager _instance;

    // public static LevelSelectManager Instance { get { return _instance; } }

    private int currLevelId;
    private WorldSettings currWorld;

    private void Start() {
        for (int i = 0; i < selectionDonuts.Count; i++) {
            selectionDonuts[i].Initialize(i);
        }
    }
    
    public bool SelectLevel(int levelId) {
        if (!isSelectable) return false;

        //deselect other donut
        selectionDonuts[currLevelId].ClearSelection();
        //mark selection as current
        currLevelId = levelId; 
        Debug.Log("Selected index " + currLevelId);
        Debug.Log("Selected level " + currWorld.levels[currLevelId].sceneName);
        return true;
    }

    public void SelectWorld(WorldSettings worldSettings) {
        isSelectable = true;
        currWorld = worldSettings;
    }

    public void DeselectWorld() {
        isSelectable = false;
        ClearLevelSelection();
    }

    public void ClearLevelSelection() {
        selectionDonuts[currLevelId].ClearSelection();
        currLevelId = -1;
    }
    
}