using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="SoundSystem/Sound Event", fileName = "Audio_")]
public class SoundEvent : ScriptableSingleton<SoundEvent>
{
    //variable for each sound effect we have, one time sounds
    public Sound sndDonutmoving; //faint sound that plays as ball is moving
    public SoundArray sndDonuthitWall;
    public Sound sndDonuthitJelly;
    public SoundArray sndDonuthitWater;
    public Sound sndDonuthitFullsink;
    public Sound sndDonuthitPlate;
    public Sound sndDonuthitWindow;
    public SoundArray sndDonutlaunch;

    //UI Sounds
    public Sound sndButtonClickBack;
    public Sound sndButtonClickForward;

    //Title Screen sound
    public Sound sndTitleScreen;









    
}
