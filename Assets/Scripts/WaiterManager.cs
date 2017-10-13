﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterManager : MonoBehaviour
{
    GameObject zombie;
    Vector3 gameplayPosition;
    int amountOfWaiters;
    public GameObject thinkBubble;
    public GameObject[] burgers;
    public GameObject[] fries;
    public GameObject[] drinks;

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
        newWaiter.AddComponent<Waiter>().SetSpeed(1);
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
            if (obj.GetComponent<Waiter>() != null)
            {
                obj.GetComponent<Waiter>().DestroyScript();
            }
        }
    }

}
