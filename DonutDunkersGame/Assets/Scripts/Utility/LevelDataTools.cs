#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelDataGenerator))]
public class LevelDataTools : Editor 
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate Walls")) {
			LevelData.Instance.GenerateWalls();
		}
		if (GUILayout.Button("Update Level Theme")) {
			LevelData.Instance.GenerateLevelTheme();
		}
    }
}
#endif