using System;
using UnityEngine;

public class ObjectParticleDatabase : MonoBehaviour
{
    private static ObjectParticleDatabase _Instance;
    public static ObjectParticleDatabase Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<ObjectParticleDatabase>();
            }
            return _Instance;
        }
    }
	
	public ParticleEmitter cupSplash;
	
	public ParticleEmitter donutGet;
	
}
