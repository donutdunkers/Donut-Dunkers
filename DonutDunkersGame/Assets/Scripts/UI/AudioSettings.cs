using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    //FMOD.Studio.EventInstance SFXVolumeTestEvent;

    //FMOD.Studio.Bus Music;
    //FMOD.Studio.Bus SFX;
    //FMOD.Studio.Bus Master;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float MasterVolume = 1f;

    void Awake()
    {
        //FMODUnity.RuntimeManager.WaitForAllLoads();
        //Music = FMODUnity.RuntimeManager.GetBus("vca:/Master/Music");
        //SFX = FMODUnity.RuntimeManager.GetBus("vca:/Master/SFX");
        //Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        //SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/SFXVolumeTest");
    }

    void Update()
    {
        //Music.setVolume(MusicVolume);
        //SFX.setVolume(SFXVolume);
        //Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    /*public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }*/
}
