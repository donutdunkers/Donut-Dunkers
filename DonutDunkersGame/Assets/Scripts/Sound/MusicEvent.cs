using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundSystem/Music Event", fileName = "Audio_")]
public class MusicEvent : ScriptableSingleton<MusicEvent>
{
    
    public Sound Theme01; //music that plays during gameplay
    public Sound MainMenu; // music that plays on main menu

}
