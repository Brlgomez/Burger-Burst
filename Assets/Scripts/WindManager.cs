using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    float windPower;
    float timer;
    float timeForNextWindChange;
    ParticleSystem windParticles;
    public GameObject windObject;

    void Start()
    {
        windParticles = GetComponent<ObjectManager>().WindParticles().GetComponent<ParticleSystem>();
        ResetValues();
    }

    public void WindUpdate(int updateInterval)
    {
        timer += Time.deltaTime * updateInterval;
        if (timer > timeForNextWindChange)
        {
            timeForNextWindChange = Random.Range(15, 60);
            if (Random.value > 0.75f)
            {
                StartWind();
            }
            else
            {
                StopWind();
            }
            timer = 0;
        }
    }

    void StartWind()
    {
        windPower = Random.Range(-5f, 5f);
        windParticles.Play();
        var main = windParticles.main;
        main.startSpeed = windPower * 5f;
        main.maxParticles = Mathf.RoundToInt(Mathf.Abs(windPower * 2));
        GetComponent<ObjectManager>().WindParticles().transform.position = new Vector3(
            -windPower * 5f,
            GetComponent<ObjectManager>().WindParticles().transform.position.y,
            GetComponent<ObjectManager>().WindParticles().transform.position.z
        );
        if (!GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().noWind.powerUpNumber))
        {
            if (windObject.GetComponent<Wind>())
            {
                windObject.GetComponent<Wind>().windStrength = windPower;
            }
            else
            {
                windObject.AddComponent<Wind>().windStrength = windPower;
            }
        }
    }

    void StopWind()
    {
        windParticles.Stop();
        if (windObject.GetComponent<Wind>())
        {
            Destroy(windObject.GetComponent<Wind>());
        }
    }

    public void ResetValues()
    {
        timeForNextWindChange = 120;
        windPower = 0;
        StopWind();
    }

    public float GetWindPower()
    {
        return windPower;
    }
}
