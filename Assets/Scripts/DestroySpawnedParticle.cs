using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpawnedParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
