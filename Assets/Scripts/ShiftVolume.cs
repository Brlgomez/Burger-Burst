using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftVolume : MonoBehaviour
{
    int direction;
    AudioSource source;
    bool changeVolume;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (changeVolume)
        {
            source.volume += Time.deltaTime * direction;
            if (source.volume >= 1 && direction > 0)
            {
                source.volume = 1;
                Destroy(GetComponent<ShiftVolume>());
            }
            else if (source.volume <= 0 && direction < 0)
            {
                source.volume = 0;
                source.Stop();
                Destroy(GetComponent<ShiftVolume>());
            }
        }
    }

    public void SetDirection(int directionAndSpeed)
    {
        changeVolume = true;
        direction = directionAndSpeed;
    }
}
