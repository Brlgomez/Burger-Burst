using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    static int updateInterval = 5;

    static int intervalAmount = 4;
    static float intervalLength = 1;
    static int initialEffectiveness = 8;
    float maxLengthOfTime;
    float nextInterval;
    float time;
    float currentEffectiveness;
    float poisonAlpha;

    void Start()
    {
        nextInterval = intervalLength;
        currentEffectiveness = initialEffectiveness;
        maxLengthOfTime = intervalAmount * intervalLength;
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            time += Time.deltaTime * updateInterval;
            poisonAlpha = (maxLengthOfTime - (time / maxLengthOfTime));
            //TODO: decrease purple effect
            if (time > nextInterval)
            {
                //TODO: hurt player
                Debug.Log(currentEffectiveness);
                currentEffectiveness -= (initialEffectiveness * (1.0f / intervalAmount));
                nextInterval += intervalLength;
            }
            if (time > maxLengthOfTime)
            {
                Destroy(GetComponent<Poisoned>());
            }
        }
    }

    public void ResetTime()
    {
        time = 0;
        nextInterval = intervalLength;
        poisonAlpha = 0;
        currentEffectiveness = initialEffectiveness;
    }
}
