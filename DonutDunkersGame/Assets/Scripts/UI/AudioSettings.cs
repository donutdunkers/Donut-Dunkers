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

    private bool savedLevelsLoaded = false;

    public static float MusicVol
    {
        get
        {
            return MasterVolume * MusicVolume;
        }
    }

    public static float SFXVol
    {
        get
        {
            return MasterVolume * SFXVolume;
        }
    }

    private void Awake()
    {
        if(!savedLevelsLoaded)
        {
            MusicVolume = SaveData.Instance.LoadMasterVolume();
            SFXVolume = SaveData.Instance.LoadMusicVolume();
            MasterVolume = SaveData.Instance.LoadSFXVolume();
            savedLevelsLoaded = true;
        }
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

        SaveData.Instance.SaveMasterVolume(newMasterVolume);
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        SoundManager.Instance.musicSourceA.volume = MasterVolume * newMusicVolume;
        SoundManager.Instance.musicSourceB.volume = MasterVolume * newMusicVolume;

        SaveData.Instance.SaveMusicVolume(newMusicVolume);
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
        SoundManager.Instance.soundSource.volume = MasterVolume * newSFXVolume;

        SaveData.Instance.SaveSFXVolume(newSFXVolume);
    }
}
