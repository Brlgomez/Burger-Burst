using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadManager : MonoBehaviour
{
    GameObject zombieHead;
    public Texture coinHead;
    int count;

    void Start()
    {
        zombieHead = GetComponent<ObjectManager>().ZombieHead();
	}

    public void CreateZombieHead()
    {
        if (count == 0)
        {
            count++;
            GameObject newHead = Instantiate(zombieHead);
            newHead.AddComponent<ZombieHead>();
            newHead.GetComponent<Renderer>().material.mainTexture = coinHead;
            newHead.tag = "Zombie Head";
            GameObject blastParticles = Instantiate(GetComponent<ObjectManager>().BlastParticles());
            blastParticles.transform.position = newHead.transform.position;
            blastParticles.transform.localScale = Vector3.one * 2;
            blastParticles.GetComponent<ParticleSystem>().Play();
            blastParticles.AddComponent<DestroySpawnedParticle>();
        }
    }

    public void DeleteAll()
    {
        count = 0;
        GameObject[] allHeads = GameObject.FindGameObjectsWithTag("Zombie Head");
        foreach (GameObject obj in allHeads)
        {
            Destroy(obj);
        }
    }

    public void DecreaseCount()
    {
        count--;
    }
}
