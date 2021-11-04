using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _Instance;
    public static SoundManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<SoundManager>();
            }
            return _Instance;
        }
    }

    //plays music
    public AudioSource musicSource;

    //plays sound effects
    public AudioSource soundSource;




}
