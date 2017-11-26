﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    static float upwardSpeed = 2;
    static float rotatingSpeed = 360;
    static float lengthOfAnimation = 0.75f;
    static float lengthOfScaleUp = 0.25f;
    static float lengthOfScaleDown = 0.25f;
    float time;

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        time += Time.deltaTime;
        RaiseAndSpin();
        if (time < lengthOfScaleUp)
        {
            transform.localScale = Vector3.one * (time / lengthOfScaleUp);
        }
        else if (time > (lengthOfAnimation - lengthOfScaleDown))
        {
            transform.localScale = Vector3.one * ((lengthOfAnimation / lengthOfScaleDown) - (time / lengthOfScaleDown));
        }
        if (time > lengthOfAnimation)
        {
            Destroy(gameObject);
        }
    }

    void RaiseAndSpin()
    {
        float newY = transform.position.y + (upwardSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.RotateAround(
            transform.transform.position,
            transform.up, rotatingSpeed * Time.deltaTime
        );
    }
}
