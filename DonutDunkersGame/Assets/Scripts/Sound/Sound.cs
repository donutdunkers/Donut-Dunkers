using System;
using UnityEngine;

[Serializable]
public class Sound
{

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume  = 1f; 

    public void playSound()
    {
        SoundManager.Instance.soundSource.PlayOneShot(this.audioClip, 1f * this.volume);
    }
    
}
