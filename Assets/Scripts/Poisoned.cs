using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    static int updateInterval = 15;
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
    ParticleSystem poisonBubbles;
    ParticleSystem.MainModule main;

    void Start()
    {
        nextInterval = intervalLength;
        currentEffectiveness = initialEffectiveness;
        maxLengthOfTime = intervalAmount * intervalLength;
        poisonAlpha = maxAplha;
        screen = transform.GetChild(4).GetComponent<Renderer>().material;
        poisonBubbles = transform.GetChild(5).GetComponent<ParticleSystem>();
        poisonBubbles.Play();
        main = poisonBubbles.main;
        main.maxParticles = intervalAmount;
        GetComponent<SoundAndMusicManager>().PlayBubblingSound(gameObject);
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
                main.maxParticles--;
            }
            if (time > maxLengthOfTime)
            {
                DestroyPoison();
            }
        }
    }

    public void ResetTime()
    {
        time = 0;
        nextInterval = intervalLength;
        poisonAlpha = maxAplha;
        currentEffectiveness = initialEffectiveness;
        main.maxParticles = intervalAmount;
        GetComponent<SoundAndMusicManager>().PlayBubblingSound(gameObject);
    }

    public void DestroyPoison()
    {
        screen.color = Color.clear;
        poisonBubbles.Stop();
        Destroy(GetComponent<Poisoned>());
    }
}
