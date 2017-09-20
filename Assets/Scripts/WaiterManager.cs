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
        newWaiter.transform.position = new Vector3(newWaiter.transform.position.x, newWaiter.transform.position.y, Random.Range(-4f, 0f));
        newWaiter.AddComponent<Waiter>();
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
