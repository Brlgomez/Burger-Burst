﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour 
{
    int neededBurgers;
    int neededFries;
    int neededDrinks;
    bool orderReady;
    List<GameObject> onPlatter = new List<GameObject>();

	void Start () 
    {
        SetOrder();
	}
	
    public void SetOrder () 
    {
        for (int i = 0; i < onPlatter.Count; i++)
        {
            Destroy(onPlatter[i]);
        }
        onPlatter.Clear();
        GameObject.Find("OnPlatter").GetComponent<CheckOnPlatter>().restartAmounts();
        Camera.main.GetComponent<GrabAndThrowObject>().newOrder();
        orderReady = false;
        neededBurgers = Random.Range(0, 2);
        neededFries = Random.Range(0, 2);
        neededDrinks = Random.Range(0, 2);
        if (neededBurgers + neededDrinks + neededFries == 0)
        {
            neededBurgers = 1;
        }
        Debug.Log("OBJECTIVE - Burger:"+ neededBurgers + "Drink:"+ neededDrinks + "Fries:" + neededFries);
    }

    public void checkOrder (int b, int d, int f) 
    {
        if (b >= neededBurgers && f >= neededFries && d >= neededDrinks)
        {
            orderReady = true;
        } else
        {
            orderReady = false;
        }
    }

    public bool isOrderReady () {
        return orderReady;
    }

    public void addToPlatter (GameObject obj) {
        onPlatter.Add(obj);
    }

    public void removeFromPlatter (GameObject obj) {
        onPlatter.Remove(obj);
    }

    public bool checkRigidbodyVelocities () {
        for (int i = 0; i < onPlatter.Count; i++)
        {
            if (onPlatter[i].GetComponent<Rigidbody>().velocity.magnitude > 0.05f || !onPlatter[i].GetComponent<Rigidbody>().IsSleeping())
            {
                return false;
            }
        }
        return true;
    }
}
