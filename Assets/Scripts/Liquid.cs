using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{

    ParticleSystem particleSyst;
    ParticleSystem.MainModule mainModule;

    void Start()
    {
        particleSyst = transform.GetChild(0).GetComponent<ParticleSystem>();
        mainModule = particleSyst.main;
    }

    void OnTriggerEnter(Collider col)
    {
        GetComponent<Animator>().Play("Oil");
        if (col.GetComponent<Rigidbody>() != null && col.GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            mainModule.startSpeed = col.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }
}

