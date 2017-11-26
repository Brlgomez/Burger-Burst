﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDManager : MonoBehaviour
{
    GameObject pointsLed;
    GameObject coinsLed;

    void Start()
    {
        pointsLed = GetComponent<ObjectManager>().LED().transform.GetChild(0).gameObject;
        pointsLed.GetComponent<Renderer>().material.color = Color.cyan;
        coinsLed = GetComponent<ObjectManager>().CoinsLED().transform.GetChild(0).gameObject;
        coinsLed.GetComponent<Renderer>().material.color = Color.yellow;
        ResetPointsText();
        UpdateCoinsText();
    }

    public void UpdatePointsText(float number)
    {
        pointsLed.GetComponent<TextMesh>().text = "P " + number.ToString("n0");
    }

    public void ResetPointsText()
    {
        pointsLed.GetComponent<TextMesh>().text = "P 0";
    }

    public void UpdateCoinsText()
    {
        coinsLed.GetComponent<TextMesh>().text = "    " + PlayerPrefs.GetInt("Coins", 0).ToString("n0");
    }
}
