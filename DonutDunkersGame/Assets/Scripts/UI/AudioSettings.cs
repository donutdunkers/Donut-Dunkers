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

    static float MusicVolume = 0.5f;
    static float SFXVolume = 0.5f;
    static float MasterVolume = 0.5f;

    private static bool savedLevelsLoaded = false;

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

    public void InitializeAudio()
    {
        if (!savedLevelsLoaded)
        {
            MasterVolume = SaveData.Instance.LoadMasterVolume();
            MusicVolume = SaveData.Instance.LoadMusicVolume();
            SFXVolume = SaveData.Instance.LoadSFXVolume();

            SoundManager.Instance.CurrentSource.volume = MasterVolume * MusicVolume;
            SoundManager.Instance.soundSource.volume = MasterVolume * SFXVolume;

            savedLevelsLoaded = true;
        }
        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
        SoundManager.Instance.CurrentSource.volume = MasterVolume * MusicVolume;
        SoundManager.Instance.soundSource.volume = MasterVolume * SFXVolume;

        SaveData.Instance.SaveMasterVolume(newMasterVolume);
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        SoundManager.Instance.CurrentSource.volume = MasterVolume * MusicVolume;

        SaveData.Instance.SaveMusicVolume(MusicVolume);
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
        SoundManager.Instance.soundSource.volume = MasterVolume * SFXVolume;

        SaveData.Instance.SaveSFXVolume(SFXVolume);
    }
}
