using System;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class SoundArray
{
    public AudioClip[] audioCliplist;

    [Range(0f, 1f)]
    public float volume = 1f;

    public void Play()
    {
        SoundManager.Instance.soundSource.PlayOneShot(this.audioCliplist[Random.Range(0, audioCliplist.Length)], 1f * this.volume);
    }
}
