using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPowerUp : MonoBehaviour
{
    public void CopyObject(GameObject obj)
    {
        obj.transform.localScale *= 0.5f;
        GameObject leftCopy = Instantiate(obj);
        GameObject rightCopy = Instantiate(obj);
        GameObject blastParticles = Instantiate(GetComponent<ObjectManager>().BlastParticles());
        leftCopy.name = obj.name + "Copy";
        rightCopy.name = obj.name + "Copy";
        obj.transform.rotation = Random.rotation;
        leftCopy.transform.position = obj.transform.position;
        rightCopy.transform.position = obj.transform.position;
        leftCopy.transform.rotation = Random.rotation;
        rightCopy.transform.rotation = Random.rotation;
		leftCopy.GetComponent<Rigidbody>().velocity = new Vector3(
            obj.GetComponent<Rigidbody>().velocity.x - 1,
            obj.GetComponent<Rigidbody>().velocity.y,
            obj.GetComponent<Rigidbody>().velocity.z);
        rightCopy.GetComponent<Rigidbody>().velocity = new Vector3(
            obj.GetComponent<Rigidbody>().velocity.x + 1,
            obj.GetComponent<Rigidbody>().velocity.y,
            obj.GetComponent<Rigidbody>().velocity.z);
        Physics.IgnoreCollision(leftCopy.GetComponent<Collider>(), obj.GetComponent<Collider>());
		Physics.IgnoreCollision(rightCopy.GetComponent<Collider>(), obj.GetComponent<Collider>());
        Physics.IgnoreCollision(leftCopy.GetComponent<Collider>(), rightCopy.GetComponent<Collider>());
        blastParticles.transform.position = obj.transform.position;
        blastParticles.GetComponent<ParticleSystem>().Play();
        blastParticles.AddComponent<DestroySpawnedParticle>();
	}
}
