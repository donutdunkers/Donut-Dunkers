using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Levels/Level", fileName = "Level")]
public class LevelSettings : ScriptableObject {
    public string sceneName;
    public int levelID;
}