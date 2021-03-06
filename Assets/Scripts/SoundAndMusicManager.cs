﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicManager : MonoBehaviour
{
    AudioSource source;
    AudioSource musicSource;
    public AudioClip scrollSound, pickingSlotSound, pickItemSound, removePowerUpSound;
    public AudioClip boughtItemWithCoins, horn, vibrate, stereoSwitch, bootUp, highScore;
    public AudioClip steam, button, dropCup, dropLid, dropPatty, dropFries, dropBasket;
    public AudioClip foodComplete, badFood, dropDrink, healthUp, healthDown, punch;
    public AudioClip deathPunch, puff, sparkle, bubbling, death, ice, freeze, dropIce, drip;
    public AudioClip woosh, buttonSound, iapSound, gameover, movementWoosh, beep;
    public AudioClip[] walkOnGrass, zombieIdleNoises, zombieGruntNoises, zombieGroans;
    public AudioClip[] music;

    bool canPlayMusic = true;
    bool canPlaySound = true;

    int trackNumber;

    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlayMusic = GetComponent<PlayerPrefsManager>().GetMusic();
        canPlaySound = GetComponent<PlayerPrefsManager>().GetSound();
        musicSource = GetComponent<ObjectManager>().Stereo().GetComponent<AudioSource>();
        trackNumber = Random.Range(0, music.Length);
        ShuffleMusic(music);
        PlayMusic();
    }

    public void ChangeSoundSetting(bool b)
    {
        canPlaySound = b;
    }

    public void PlayMusic()
    {
        if (canPlayMusic)
        {
            musicSource.clip = music[trackNumber];
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        PickNextSong();
        canPlayMusic = false;
        musicSource.Stop();
    }

    public void CanPlayMusic()
    {
        canPlayMusic = true;
        PlayMusic();
    }

    public void PickNextSong()
    {
        if (canPlayMusic)
        {
            trackNumber++;
            if (trackNumber >= music.Length)
            {
                trackNumber = 0;
            }
            PlayMusic();
        }
    }

    public void CheckIfMusicPlaying()
    {
        if (!musicSource.isPlaying && canPlayMusic && Time.timeScale > 0)
        {
            PickNextSong();
        }
    }

    void ShuffleMusic(AudioClip[] clips)
    {
        for (int t = 0; t < clips.Length; t++)
        {
            AudioClip tmp = clips[t];
            int r = Random.Range(t, clips.Length);
            clips[t] = clips[r];
            clips[r] = tmp;
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

    public void PlayBeepSound(float pitch)
    {
        if (canPlaySound)
        {
            PlayClipAt(beep, transform.position, 1, pitch);
        }
    }

    public void PlayDeviceButtonSound()
    {
        if (canPlaySound)
        {
            source.clip = buttonSound;
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

    public void PlayIAPSound()
    {
        if (canPlaySound)
        {
            source.clip = iapSound;
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

    public void PlayDropDrinkSound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropDrink, obj.transform.position, volume, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayDropBurgerSound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropPatty, obj.transform.position, volume, Random.Range(0.6f, 0.8f));
        }
    }

    public void PlayDropIceSound(GameObject obj, float volume)
    {
        if (canPlaySound)
        {
            PlayClipAt(dropIce, obj.transform.position, volume, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayFoodCompleteSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(foodComplete, obj.transform.position, 1, 1);
        }
    }

    public void PlayBadFoodSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(badFood, obj.transform.position, 1, 1);
        }
    }

    public void PlayHealthUpSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(healthUp, obj.transform.position, 1, 1);
        }
    }

    public void PlayHealthDownSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(healthDown, obj.transform.position, 0.25f, 1);
        }
    }

    public void PlayGameOverSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(gameover, obj.transform.position, 0.5f, 1);
        }
    }

    public void PlayWalkingOnGrassSound(GameObject obj)
    {
        /*
        if (canPlaySound)
        {
            PlayClipAt(walkOnGrass[Random.Range(0, walkOnGrass.Length)], obj.transform.position, 0.25f, Random.Range(0.9f, 1.1f));
        }
        */
    }

    public void PlayZombieIdleSound(GameObject obj, float pitch)
    {
        if (canPlaySound)
        {
            PlayClipAt(zombieIdleNoises[Random.Range(0, zombieIdleNoises.Length)], obj.transform.position, 1f, pitch).transform.parent = obj.transform;
        }
    }

    public void PlayZombieGruntSound(GameObject obj, float pitch)
    {
        if (canPlaySound)
        {
            PlayClipAt(zombieGruntNoises[Random.Range(0, zombieGruntNoises.Length)], obj.transform.position, 1.5f, pitch).transform.parent = obj.transform;
        }
    }

    public void PlayZombieGroanSound(GameObject obj, float pitch)
    {
        if (canPlaySound)
        {
            PlayClipAt(zombieGroans[Random.Range(0, zombieGroans.Length)], obj.transform.position, 1.25f, pitch).transform.parent = obj.transform;
        }
    }

    public void PlayPunchSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(punch, obj.transform.position, 1, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayWooshSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(woosh, obj.transform.position, 1, Random.Range(0.9f, 1.1f));
        }
    }

    public void PlayDeathPunchSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(deathPunch, obj.transform.position, 1, 1);
        }
    }

    public void PlayPuffSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(puff, obj.transform.position, 1, 1);
        }
    }

    public void PlayZombieSparckleSound(AudioSource zombieSource)
    {
        if (canPlaySound)
        {
            zombieSource.volume = 1;
            zombieSource.clip = sparkle;
            zombieSource.Play();
        }
    }

    public void PlayZombieBubblingSound(AudioSource zombieSource)
    {
        if (canPlaySound)
        {
            zombieSource.volume = 1;
            zombieSource.clip = bubbling;
            zombieSource.Play();
        }
    }

    public void PlayZombieInstantDeathSound(AudioSource zombieSource)
    {
        if (canPlaySound)
        {
            zombieSource.volume = 1;
            zombieSource.clip = death;
            zombieSource.Play();
        }
    }

    public void PlayZombieIceSound(AudioSource zombieSource)
    {
        if (canPlaySound)
        {
            zombieSource.volume = 1;
            zombieSource.clip = ice;
            zombieSource.Play();
        }
    }

    public void PlayZombieDripSound(AudioSource zombieSource)
    {
        if (canPlaySound)
        {
            zombieSource.volume = 1;
            zombieSource.clip = drip;
            zombieSource.Play();
        }
    }

    public void PlayFreezeSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(freeze, obj.transform.position, 1, 1).transform.parent = obj.transform;
        }
    }

    public void PlayBubblingSound(GameObject obj)
    {
        if (canPlaySound)
        {
            PlayClipAt(bubbling, obj.transform.position, 1, 1).transform.parent = obj.transform;
        }
    }

    public void PlayMovementWoosh(GameObject obj, float pitch)
    {
        if (canPlaySound)
        {
            PlayClipAt(movementWoosh, obj.transform.position, 1, pitch).transform.parent = obj.transform;
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
        tempSource.spatialBlend = 1;
        tempSource.maxDistance = 10;
        tempSource.Play();
        Destroy(temp, clip.length);
        return tempSource;
    }

    public void PauseAllSounds()
    {
        if (canPlaySound)
        {
            AudioSource[] allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource a in allAudio)
            {
                if (a.name != "One shot audio")
                {
                    a.Pause();
                }
            }
        }
    }

    public void ResumeAllSounds()
    {
        if (canPlaySound)
        {
            AudioSource[] allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource a in allAudio)
            {
                if (a.name != "Empty_Cup(Clone)")
                {
                    a.UnPause();
                }
            }
        }
    }
}
