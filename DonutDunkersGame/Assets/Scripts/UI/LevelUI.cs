using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour {
	
    private static LevelUI _Instance;
    public static LevelUI Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<LevelUI>();
            }
            return _Instance;
        }
    }
	
	public TextMeshProUGUI remainingTurns;
	
	public void SetRemainingTurns(int turns) {
		this.remainingTurns.SetText("TURNS REMAINING: " + turns.ToString());
	}
}
