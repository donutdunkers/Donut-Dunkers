using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
    public WorldSettings currWorld;
    public LevelSettings currLevel;
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
}
