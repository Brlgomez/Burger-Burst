using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryFries : MonoBehaviour
{
    static int updateInterval = 4;

    float timeInFryer;
    float maxTimeInFryer;
    Color initialColor;
    Color friedColor;
    Color burntColor;
    ParticleSystem particleSyst;
    GameObject basket;
    Renderer myRenderer;
    Material fryMaterial;

    void Start()
    {
        initialColor = gameObject.GetComponent<Renderer>().material.color;
        friedColor = new Color(1, 0.781f, 0.5f);
        burntColor = new Color(0.25f, 0.125f, 0);
        particleSyst = transform.GetChild(0).GetComponent<ParticleSystem>();
        maxTimeInFryer = GetComponent<Fry>().GetMaxTimeInFryer();
        fryMaterial = GetComponent<Renderer>().material;
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        timeInFryer += Time.deltaTime;
        if (timeInFryer < maxTimeInFryer)
        {
            if (Time.frameCount % updateInterval == 0)
            {
                ChangeFryColor();
                if (!myRenderer.isVisible && !particleSyst.isPaused)
                {
                    particleSyst.Pause();
                }
                else
                {
                    particleSyst.Play();
                }
            }
        }
        else
        {
            particleSyst.Play();
            particleSyst.Stop();
            fryMaterial.color = burntColor;
            GetComponent<Fry>().SetTimeInFryer(timeInFryer);
            Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -1);
            Destroy(GetComponent<FryFries>());
        }
    }

    void ChangeFryColor()
    {
        if (myRenderer.isVisible)
        {
            float percentage = (timeInFryer / (maxTimeInFryer / 2));
            Color newColor;
            if (timeInFryer < maxTimeInFryer / 2)
            {
                newColor = Color.Lerp(initialColor, friedColor, percentage);
            }
            else
            {
                newColor = Color.Lerp(friedColor, burntColor, percentage - 1);
            }
            fryMaterial.color = newColor;
        }
    }

    public float GetTimeInFryer()
    {
        return timeInFryer;
    }
}
