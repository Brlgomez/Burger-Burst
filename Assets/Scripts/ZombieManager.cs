using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    GameObject zombie;
    Vector3 gameplayPosition;
    int amountOfWaiters;
    public GameObject thinkBubble;
    public GameObject[] burgers;
    public GameObject[] fries;
    public GameObject[] drinks;
    public Mesh[] hair;
    public Mesh[] forearm;
    public Mesh[] hands;
    public Mesh[] feet;
    public Mesh[] legs;
    public Mesh[] rightUpperArms;
    public Mesh[] leftUpperArms;

    void Start()
    {
        gameplayPosition = GetComponent<PositionManager>().GameplayPosition().position;
        zombie = GetComponent<ObjectManager>().Zombie();
    }

    public void AddNewWaiter(Vector3 position)
    {
        amountOfWaiters++;
        GameObject newWaiter = Instantiate(zombie);
        newWaiter.transform.position = position;
		newWaiter.transform.LookAt(gameplayPosition);
		newWaiter.transform.eulerAngles = new Vector3(0, newWaiter.transform.eulerAngles.y, newWaiter.transform.eulerAngles.z);
        newWaiter.AddComponent<Zombie>().SetZombie(
            1, 
            hair[Random.Range(0, hair.Length)], 
            forearm[Random.Range(0, forearm.Length)], 
            forearm[Random.Range(0, forearm.Length)],
            hands[Random.Range(0, hands.Length)],
            hands[Random.Range(0, hands.Length)],
            feet[Random.Range(0, feet.Length)],
            feet[Random.Range(0, feet.Length)],
			legs[Random.Range(0, legs.Length)],
			legs[Random.Range(0, legs.Length)],
            rightUpperArms[Random.Range(0, rightUpperArms.Length)],
            leftUpperArms[Random.Range(0, leftUpperArms.Length)]
        );
        newWaiter.tag = "Clone";
    }

    public void RemoveWaiter(GameObject waiter)
    {
        amountOfWaiters--;
        Destroy(waiter);
    }

    public int GetCount()
    {
        return amountOfWaiters;
    }

    public void DeleteAllScripts()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject obj in clones)
        {
            if (obj.GetComponent<Zombie>() != null)
            {
                obj.GetComponent<Zombie>().DestroyScript();
            }
        }
    }
}

