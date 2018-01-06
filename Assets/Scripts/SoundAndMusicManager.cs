using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicManager : MonoBehaviour
{
    AudioSource source;

    public AudioClip scrollSound;
    public AudioClip pickingSlotSound;
    public AudioClip pickItemSound;

    bool canPlayMusic = true;
    bool canPlaySound = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
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

    public void PlayScrollSound()
    {
        if (canPlaySound)
        {
            source.clip = scrollSound;
            source.Play();
        }
    }

    public void PlayPickingSlotSound()
    {
        if (canPlaySound)
        {
            source.clip = pickingSlotSound;
            source.Play();
        }
    }

    public void PlayPickItemSound()
    {
        if (canPlaySound)
        {
            source.clip = pickItemSound;
            source.Play();
        }
    }

    public void ChangeSoundSetting(bool b)
    {
        canPlaySound = b;
    }
}
