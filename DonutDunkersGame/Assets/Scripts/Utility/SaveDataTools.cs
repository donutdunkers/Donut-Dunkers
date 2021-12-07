#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveData))]
public class SaveDataTools : Editor
{
	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Clear Save Data"))
		{
			SaveData.Instance.ClearData();
		}
	}
}
#endif