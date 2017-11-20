using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    float windPower;
    float timer;
    float timeForNextWindChange = 30;
    ParticleSystem windParticles;

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
            PickNextWindChangeTime();
            timer = 0;
        }
    }

    void PickNextWindChangeTime()
    {
        timeForNextWindChange = Random.Range(15, 60);
        windPower = Random.Range(-2.5f, 2.5f);
        if (Mathf.Abs(windPower) > 0.25f)
        {
            windParticles.Play();
            var main = windParticles.main;
            main.startSpeed = windPower * 4;
        }
        else
        {
            windParticles.Stop();
        }
    }

    void BlowWindToThrownObjects()
    {
        GameObject[] allFood = GameObject.FindGameObjectsWithTag("Ingredient");
        foreach (GameObject food in allFood)
        {
            ApplyWindToObject(food);
        }
    }

    public void ApplyWindToObject(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            obj.GetComponent<Rigidbody>().velocity += new Vector3(windPower, 0, 0);
        }
    }

    public void ResetValues()
    {
        windParticles.Stop();
        timeForNextWindChange = 120;
        windPower = 0;
    }
}
