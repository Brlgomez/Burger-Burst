using UnityEngine;

public class Liquid : MonoBehaviour
{
    ParticleSystem particleSyst;
    ParticleSystem.MainModule mainModule;
    float velocityMagnitude;

    void Start()
    {
        particleSyst = transform.GetChild(0).GetComponent<ParticleSystem>();
        mainModule = particleSyst.main;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name != "Bottom Collider")
        {
            velocityMagnitude = col.GetComponent<Rigidbody>().velocity.magnitude;
            GetComponent<Animator>().Play("Oil");
            if (velocityMagnitude / 3 > 0.20f)
            {
                Camera.main.GetComponent<SoundAndMusicManager>().PlayFromSourceWithSelectedVolume(gameObject, velocityMagnitude / 3);
            }
            if (col.GetComponent<Rigidbody>() != null && velocityMagnitude > 1)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                mainModule.startSpeed = velocityMagnitude;
            }
        }
    }
}

