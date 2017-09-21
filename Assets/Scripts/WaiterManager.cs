﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterManager : MonoBehaviour
{

    GameObject waiter;
    int amountOfWaiters;

    void Start()
    {
        waiter = GameObject.Find("Waiter");
    }

    public void AddNewWaiter()
    {
        amountOfWaiters++;
        GameObject newWaiter = Instantiate(waiter);
        newWaiter.transform.position = new Vector3(Random.Range(-2f, 2f), 0, 13);
        newWaiter.AddComponent<Waiter>().SetSpeed(Random.Range(2.5f, 3.5f));
        newWaiter.tag = "Clone";
    }

    public void RemoveWaiter(GameObject waiter)
    {
        amountOfWaiters--;
        Destroy(waiter);
    }

    public int GetCount () 
    {
        return amountOfWaiters;
    }
}
