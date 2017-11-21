using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    static int updateInterval = 10;
    static int intervalAmount = 4;
    static float intervalLength = 2.5f;
    static int initialEffectiveness = 8;
    static float maxAplha = 0.25f;
    float maxLengthOfTime;
    float nextInterval;
    float time;
    int currentEffectiveness;
    float poisonAlpha;
    Material screen;

    void Start()
    {
        nextInterval = intervalLength;
        currentEffectiveness = initialEffectiveness;
        maxLengthOfTime = intervalAmount * intervalLength;
        poisonAlpha = maxAplha;
        screen = transform.GetChild(4).GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            time += Time.deltaTime * updateInterval;
            poisonAlpha = (1 - (time / maxLengthOfTime)) * maxAplha;
            screen.color = new Color(0.5f, 0, 1, poisonAlpha);
            if (time > nextInterval)
            {
                currentEffectiveness -= Mathf.RoundToInt(initialEffectiveness * (1.0f / intervalAmount));
                Camera.main.GetComponent<Gameplay>().ReduceHealth(currentEffectiveness, transform.GetChild(4).gameObject);
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
        poisonAlpha = maxAplha;
        currentEffectiveness = initialEffectiveness;
    }
}
