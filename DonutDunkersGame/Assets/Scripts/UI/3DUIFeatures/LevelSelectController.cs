using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour {


    //must be set in inspector
    public WorldBoxController worldBoxController;
    public LevelStatsController statsController;

    
    private bool isSelectable = false;
    public List<SelectionDonut> selectionDonuts;
    
    // private static LevelSelectManager _instance;

    // public static LevelSelectManager Instance { get { return _instance; } }

    private int currLevelId;
    private LevelSettings currLevel;
    private WorldSettings world;


    private void Start() {
        for (int i = 0; i < selectionDonuts.Count; i++) {
            selectionDonuts[i].Initialize(i);
        }
        world = worldBoxController.worldSettings;
    }
    
    public bool SelectLevel(int levelId) {
        //dont select if box isn't open
        if (!isSelectable) return false;
        
        //if level id out of range don't do anything
        if (levelId > world.levels.Count - 1) return false;

        //deselect other donut
        selectionDonuts[currLevelId].ClearSelection();
        //udpate levelId and levelSettings
        currLevelId = levelId; 
        currLevel = world.levels[currLevelId];


        LevelInfo.Instance.currWorld = world;
        LevelInfo.Instance.currLevel = currLevel;
        statsController.UpdateStats();

        return true;
    }

    public void SelectWorld() {
        //allow selection if box is open
        isSelectable = true;  
        //select first donut by default
        selectionDonuts[0].OnSelected();
        currLevel = world.levels[0];

        LevelInfo.Instance.currWorld = world;
        LevelInfo.Instance.currLevel = currLevel;
    }

    
    public void DeselectWorld() {
        LevelInfo.Instance.currWorld = null;
        LevelInfo.Instance.currLevel = null;
        isSelectable = false;
        ClearLevelSelection();
    }

    //reset to first level
    public void ClearLevelSelection() {
        selectionDonuts[currLevelId].ClearSelection();
        currLevelId = 0;
    }
    
}