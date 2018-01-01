using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicManager : MonoBehaviour 
{
    bool canPlayMusic = true;
    bool canPlaySound = true;

	void Start () 
    {
        canPlayMusic = GetComponent<PlayerPrefsManager>().GetMusic();
        canPlaySound = GetComponent<PlayerPrefsManager>().GetSound();
	}

    public void PlayMusic()
    {
        if (canPlayMusic)
        {
            
        }
    }

    public void PlayDeviceButtonSound()
    {
        if (canPlaySound)
        {
            
        }
    }
}
