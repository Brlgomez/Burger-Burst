using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachine : MonoBehaviour
{
    static int updateInterval = 5;

    int maxOnTime = 5;
    float currentTimeOn;
    bool turnOn = true;
    float sodaMaxScale = 1f;
    float sodaCurrentScale;

    void Start()
    {
        transform.parent.GetComponent<ParticleSystem>().Play();
        Camera.main.GetComponent<VibrationManager>().LightTapticFeedback();
        Camera.main.GetComponent<SoundAndMusicManager>().PlayLoopFromSourceAndRaiseVolume(transform.parent.gameObject, 5, 1);
    }

    void Update()
    {
        currentTimeOn += Time.deltaTime;
        if (Time.frameCount % updateInterval == 0)
        {
            if (turnOn && sodaCurrentScale < sodaMaxScale)
            {
                sodaCurrentScale += Time.deltaTime * updateInterval * 50;
                transform.localScale = Vector3.one * sodaCurrentScale;
                if (transform.localScale.x > sodaMaxScale)
                {
                    transform.localScale = Vector3.one * sodaMaxScale;
                }
            }
            if (!turnOn || currentTimeOn > maxOnTime)
            {
                turnOn = false;
                sodaCurrentScale -= Time.deltaTime * updateInterval * 50;
                transform.localScale = Vector3.one * sodaCurrentScale;
                if (sodaCurrentScale < 0)
                {
                    TurnOff();
                }
            }
        }
    }

    public void ButtonPressed()
    {
        Camera.main.GetComponent<VibrationManager>().LightTapticFeedback();
        transform.parent.GetComponent<ParticleSystem>().Stop();
        Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(transform.parent.gameObject, -5);
        turnOn = false;
    }

    public void TurnOff()
    {
        transform.localScale = Vector3.zero;
        transform.parent.GetComponent<ParticleSystem>().Stop();
        Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(transform.parent.gameObject, -5);
        Destroy(gameObject.GetComponent<SodaMachine>());
    }
}
