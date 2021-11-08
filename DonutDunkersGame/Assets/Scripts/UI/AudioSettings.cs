using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    Slider MasterSlider;

    [SerializeField]
    Slider MusicSlider;

    [SerializeField]
    Slider SFXSlider;

    static float MusicVolume = 1f;
    static float SFXVolume = 1f;
    static float MasterVolume = 1f;

    private void Awake()
    {
        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
        SoundManager.Instance.musicSourceA.volume = newMasterVolume;
        SoundManager.Instance.musicSourceB.volume = newMasterVolume;
        SoundManager.Instance.soundSource.volume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        SoundManager.Instance.musicSourceA.volume = MasterVolume * newMusicVolume;
        SoundManager.Instance.musicSourceB.volume = MasterVolume * newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
        SoundManager.Instance.soundSource.volume = MasterVolume * newSFXVolume;
    }
}
