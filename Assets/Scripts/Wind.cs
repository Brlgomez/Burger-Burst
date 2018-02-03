﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float windStrength;
    static int windRadius = 15;
    static float timeToNextBurst = 0.1f;
    float time;

    void Update()
    {
        time += Time.timeScale;
        if (time > timeToNextBurst)
        {
            Debug.Log("Hi");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, windRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<Rigidbody>() != null && (hitColliders[i].tag == "Ingredient" || hitColliders[i].tag == "Waiter"))
                {
                    hitColliders[i].GetComponent<Rigidbody>().AddForce(transform.forward * windStrength, ForceMode.Acceleration);
                }
            }
            time = 0;
        }
    }
}
