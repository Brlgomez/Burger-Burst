﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    float windPower;
    float timer;
    float timeForNextWindChange = 30;
    ParticleSystem windDust;

    void Start()
    {
        ResetValues();
    }

    public void WindUpdate()
    {
        timer += Time.deltaTime;
        if (timer > timeForNextWindChange)
        {
            PickNextWindChangeTime();
            timer = 0;
        }
    }

    void PickNextWindChangeTime()
    {
        timeForNextWindChange = Random.Range(30, 60);
        windPower = Random.Range(-2.5f, 2.5f);
        Debug.Log(windPower);
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
		timeForNextWindChange = 30;
		windPower = 0;
    }
}