using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicManager : MonoBehaviour
{
    AudioSource source;
    public AudioClip scrollSound, pickingSlotSound, pickItemSound, removePowerUpSound;
    public AudioClip boughtItemWithCoins, horn, vibrate, stereoSwitch, bootUp, highScore;
    public AudioClip steam, button, dropCup, dropLid, dropPatty, dropFries, dropBasket;

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

    public void PlayBootUpSound()
    {
        if (canPlaySound)
        {
            source.clip = bootUp;
            source.Play();
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

    public void PlayHighScoreSound()
    {
        if (canPlaySound)
        {
            source.clip = highScore;
            source.Play();
        }
    }

    /* Gameplay */

    public void PlayLoopFromSourceAndRaiseVolume(GameObject obj, int directionAndSpeed, float maxVolume)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().loop = true;
            obj.GetComponent<AudioSource>().Play();
            if (obj.GetComponent<ShiftVolume>() == null)
            {
                obj.AddComponent<ShiftVolume>().SetDirection(directionAndSpeed, maxVolume);
            }
            else
            {
                obj.GetComponent<ShiftVolume>().SetDirection(directionAndSpeed, maxVolume);
            }
        }
    }

    public void StopLoopFromSourceAndLowerVolume(GameObject obj, int directionAndSpeed)
    {
        if (canPlaySound)
        {
            if (obj.GetComponent<ShiftVolume>() == null)
            {
                obj.AddComponent<ShiftVolume>().SetDirection(directionAndSpeed, 0);
            }
            else
            {
                obj.GetComponent<ShiftVolume>().SetDirection(directionAndSpeed, 0);
            }
        }
    }

    public void PlayLoopFromSource(GameObject obj)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().loop = true;
            obj.GetComponent<AudioSource>().Play();
        }
    }

    public void StopLoopFromSource(GameObject obj)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().Stop();
        }
    }

    public void PlayFromSource(GameObject obj)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().Play();
        }
    }

    public void PauseFromSource(GameObject obj)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().Pause();
        }
    }

    public void UnPauseFromSource(GameObject obj)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().UnPause();
        }
    }

    public void PlayFromSourceWithSelectedVolume(GameObject obj, float vol)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().Play();
            obj.GetComponent<AudioSource>().volume = vol;
            obj.GetComponent<AudioSource>().pitch = Random.Range(0.90f, 1.1f);
        }
    }

    public void PlaySteamSound(GameObject obj)
    {
        if (canPlaySound)
        {
            AudioSource.PlayClipAtPoint(steam, obj.transform.position);
        }
    }

    public void PlayButtonSound(GameObject obj)
    {
        if (canPlaySound)
        {
            AudioSource.PlayClipAtPoint(button, obj.transform.position);
        }
    }

    public void ChangePitchShift(GameObject obj, float shift)
    {
        if (canPlaySound)
        {
            obj.GetComponent<AudioSource>().pitch = shift;
        }
    }

    public void GraduallyShiftPitch(GameObject obj, float newPitch)
    {
        if (canPlaySound)
        {
            if (obj.GetComponent<ShiftPitch>() == null)
            {
                obj.AddComponent<ShiftPitch>().SetPitch(newPitch);
            }
            else
            {
                obj.GetComponent<ShiftPitch>().SetPitch(newPitch);
            }
        }
    }

    public void PlayDropLidSound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropLid, obj.transform.position, volume, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayDropCupSound(GameObject obj, float volume, float pitch)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropCup, obj.transform.position, volume, (pitch + Random.Range(-0.1f, 0.1f)));
        }
    }

    public void PlayDropPattySound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropPatty, obj.transform.position, volume, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayDropFriesSound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropFries, obj.transform.position, volume, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayDropBasketSound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropBasket, obj.transform.position, volume, Random.Range(0.9f, 1.1f));
        }
    }

    AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume, float pitch)
    {
        GameObject temp = new GameObject("TempAudio");
        temp.transform.position = pos;
        AudioSource tempSource = temp.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.volume = volume;
        tempSource.pitch = pitch;
        tempSource.Play();
        Destroy(temp, clip.length);
        return tempSource;
    }
}
