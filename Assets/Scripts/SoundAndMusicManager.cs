using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicManager : MonoBehaviour
{
    AudioSource source;
    public AudioClip scrollSound, pickingSlotSound, pickItemSound, removePowerUpSound;
    public AudioClip boughtItemWithCoins, horn, vibrate, stereoSwitch;

    bool canPlayMusic = true;
    bool canPlaySound = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlayMusic = GetComponent<PlayerPrefsManager>().GetMusic();
        canPlaySound = GetComponent<PlayerPrefsManager>().GetSound();
    }

    public void ChangeSoundSetting(bool b)
    {
        canPlaySound = b;
    }

    public void PlayMusic()
    {
        if (canPlayMusic)
        {

        }
    }

    /* DEVICE SOUNDS */

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

    public void PlayRemovePowerUpSound()
    {
        if (canPlaySound)
        {
            source.clip = removePowerUpSound;
            source.Play();
        }
    }

    public void PlayBoughtItemWithCoinsSound()
    {
        if (canPlaySound)
        {
            source.clip = boughtItemWithCoins;
            source.Play();
        }
    }

	public void PlayHornSound()
	{
		if (canPlaySound)
		{
            source.clip = horn;
			source.Play();
		}
	}

	public void PlayVibrateSound()
	{
		if (canPlaySound)
		{
            source.clip = vibrate;
			source.Play();
		}
	}

	public void PlayStereoSwitchSound()
	{
		if (canPlaySound)
		{
            source.clip = stereoSwitch;
			source.Play();
		}
	}
}
