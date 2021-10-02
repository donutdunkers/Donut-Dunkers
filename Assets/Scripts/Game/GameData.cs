using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _Instance;
    public static GameData Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameData>();
            }
            return _Instance;
        }
    }
	
	public float ballMovementSpeed = 20f;
    
}
