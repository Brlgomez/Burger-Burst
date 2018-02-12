using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookMeat : MonoBehaviour
{
    static int updateInterval = 4;

    float maxTimeOnGrill;
    float timeOnGrill;
    Color initialColor;
    Color cookedColor;
    Color burntColor;
    ParticleSystem particleSyst;
    ParticleSystem.MainModule mainModule;
    Renderer myRenderer;
    Material meatMaterial;
    bool burned;

    void Start()
    {
        initialColor = gameObject.GetComponent<Renderer>().material.color;
        cookedColor = new Color(0.480f, 0.613f, 0.266f);
        burntColor = new Color(cookedColor.r * 0.25f, cookedColor.g * 0.25f, cookedColor.b * 0.25f);
        particleSyst = transform.GetChild(0).GetComponent<ParticleSystem>();
        mainModule = particleSyst.main;
        myRenderer = GetComponent<Renderer>();
        meatMaterial = GetComponent<Renderer>().material;
        maxTimeOnGrill = GetComponent<Meat>().GetMaxTimeOnGrill();
        timeOnGrill = GetComponent<Meat>().GetTimeOnGrill();
    }

    void Update()
    {
        timeOnGrill += Time.deltaTime;
        if (timeOnGrill < maxTimeOnGrill)
        {
            if (Time.frameCount % updateInterval == 0)
            {
                ChangeMeatColor();
                if (myRenderer.isVisible && timeOnGrill > (maxTimeOnGrill * 0.25f) && !particleSyst.isPlaying)
                {
                    particleSyst.Play();
                }
                if (!myRenderer.isVisible && particleSyst.isPlaying)
                {
                    particleSyst.Pause();
                }
                ChangeParticleColors();
            }
        }
        else
        {
            particleSyst.Play();
            particleSyst.Stop();
            meatMaterial.color = burntColor;
            GetComponent<Meat>().SetTimeOnGrill(timeOnGrill);
            Destroy(GetComponent<CookMeat>());
        }
        if (timeOnGrill > maxTimeOnGrill && !burned)
        {
            burned = true;
            Camera.main.GetComponent<OnlineManagement>().BurnedBurger();
            Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -2);
        }
    }

    void ChangeMeatColor()
    {
        if (myRenderer.isVisible)
        {
            float percentage = (timeOnGrill / (maxTimeOnGrill / 2));
            Color newColor;
            if (timeOnGrill < maxTimeOnGrill / 2)
            {
                newColor = Color.Lerp(initialColor, cookedColor, percentage);
            }
            else
            {
                newColor = Color.Lerp(cookedColor, burntColor, percentage - 1);
            }
            meatMaterial.color = newColor;
        }
    }

    void ChangeParticleColors()
    {
        if (myRenderer.isVisible && particleSyst.isPlaying)
        {
            float percentage = ((timeOnGrill - (maxTimeOnGrill * 0.25f)) / (maxTimeOnGrill - (maxTimeOnGrill * 0.25f)));
            Color newColor = Color.Lerp(Color.white, Color.black, percentage);
            mainModule.startColor = newColor;
        }
    }

    public float GetTimeOnGrill()
    {
        return timeOnGrill;
    }
}
