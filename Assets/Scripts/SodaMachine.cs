﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachine : MonoBehaviour {

    int maxOnTime = 10;
    float currentTimeOn;
    bool turnOn = true;
    float sodaMaxScale = 2.22f;
    float sodaCurrentScale;
	
	void Update () {
        currentTimeOn += Time.deltaTime;
        if (currentTimeOn > maxOnTime)
        {
            turnOn = false;
        }
        if (turnOn && sodaCurrentScale < sodaMaxScale)
        {
            sodaCurrentScale += Time.deltaTime * 20;
            transform.localScale = Vector3.one * sodaCurrentScale;
        }
        if (!turnOn && sodaCurrentScale > 0)
        {
            sodaCurrentScale -= Time.deltaTime * 20;
            transform.localScale = Vector3.one * sodaCurrentScale;
            if (sodaCurrentScale < 0)
            {
                Destroy(gameObject.GetComponent<SodaMachine>());
            }
        }
	}

    public void ButtonPressed ()
    {
        turnOn = false;
    }
}
