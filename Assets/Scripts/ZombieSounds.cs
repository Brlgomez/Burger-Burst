using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public void PlayWalkingSound()
    {
        Camera.main.GetComponent<SoundAndMusicManager>().PlayWalkingOnGrassSound(gameObject);
    }
}
