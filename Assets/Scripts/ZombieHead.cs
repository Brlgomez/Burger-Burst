using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    static float speed = 0.5f;
    static float timeForDisappear = 2;
    static int maxTime = 60;
    Vector3 startPos;
    float ellapsedTime;
    bool flying = true;
    Renderer myRenderer;
    ParticleSystem particles;

    void Awake()
    {
        transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 2.5f, Random.Range(0.0f, 5.0f));
        startPos = transform.position;
        particles = GetComponent<ParticleSystem>();
        particles.Play();
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (flying)
        {
            ellapsedTime += Time.deltaTime * speed;
            transform.position = startPos + new Vector3(Mathf.Sin(ellapsedTime * 2) * 5, Mathf.Sin(ellapsedTime), 0.0f);
            if (ellapsedTime > maxTime)
            {
                GameObject deathParticles = Instantiate(Camera.main.GetComponent<ObjectManager>().HeadDeathParticles());
                deathParticles.transform.position = transform.position;
                deathParticles.GetComponent<ParticleSystem>().Play();
                deathParticles.AddComponent<DestroySpawnedParticle>();
                Destroy(gameObject);
            }
        }
        else
        {
            ellapsedTime += Time.deltaTime;
            if (ellapsedTime > timeForDisappear)
            {
                GameObject deathParticles = Instantiate(Camera.main.GetComponent<ObjectManager>().HeadDeathParticles());
                deathParticles.transform.position = transform.position;
                deathParticles.GetComponent<ParticleSystem>().Play();
                deathParticles.AddComponent<DestroySpawnedParticle>();
                Camera.main.GetComponent<Gameplay>().StartCoinLancher(5, gameObject);
                Destroy(gameObject);
            }
        }
        if (myRenderer.isVisible && !particles.isPlaying)
        {
            particles.Play();
        }
        if (!myRenderer.isVisible && particles.isPlaying)
        {
            particles.Pause();
        }
    }

    public void Hit(GameObject product)
    {
        if (flying)
        {
            flying = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = product.GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().angularVelocity = product.GetComponent<Rigidbody>().angularVelocity;
            Destroy(product.GetComponent<RemoveObjects>());
            product.GetComponent<Rigidbody>().useGravity = false;
            product.GetComponent<Rigidbody>().isKinematic = true;
            product.transform.parent = gameObject.transform;
            product.tag = "Fallen";
            tag = "Fallen";
            Camera.main.GetComponent<ZombieHeadManager>().DecreaseCount();
            GameObject landParticles = Instantiate(Camera.main.GetComponent<ObjectManager>().LandOnZombieParticles());
            landParticles.transform.position = transform.position;
            landParticles.GetComponent<ParticleSystem>().Play();
            landParticles.AddComponent<DestroySpawnedParticle>();
            ellapsedTime = 0;
        }
    }
}
