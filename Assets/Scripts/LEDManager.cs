﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDManager : MonoBehaviour
{
    GameObject pointsLed;
    GameObject coinsLed;
    GameObject highScoreLed;
    GameObject totalScoreLed;
    GameObject nextUnlockLed;
    GameObject unlockedLed;

    void Start()
    {
        highScoreLed = GetComponent<ObjectManager>().HighScoreLED().transform.GetChild(0).gameObject;
        highScoreLed.GetComponent<Renderer>().material.color = Color.cyan;
        pointsLed = GetComponent<ObjectManager>().LED().transform.GetChild(0).gameObject;
        pointsLed.GetComponent<Renderer>().material.color = Color.cyan;
        coinsLed = GetComponent<ObjectManager>().CoinsLED().transform.GetChild(0).gameObject;
        coinsLed.GetComponent<Renderer>().material.color = Color.yellow;
        totalScoreLed = GetComponent<ObjectManager>().HighScoreLED().transform.GetChild(1).gameObject;
        totalScoreLed.GetComponent<Renderer>().material.color = Color.cyan;
        nextUnlockLed = GetComponent<ObjectManager>().TotalPointsLED().transform.GetChild(0).gameObject;
        nextUnlockLed.GetComponent<Renderer>().material.color = Color.cyan;
        unlockedLed = GetComponent<ObjectManager>().TotalPointsLED().transform.GetChild(1).gameObject;
        unlockedLed.GetComponent<Renderer>().material.color = Color.cyan;

        highScoreLed.GetComponent<TextMesh>().fontSize = 0;
        pointsLed.GetComponent<TextMesh>().fontSize = 0;
        coinsLed.GetComponent<TextMesh>().fontSize = 0;
        totalScoreLed.GetComponent<TextMesh>().fontSize = 0;
        nextUnlockLed.GetComponent<TextMesh>().fontSize = 0;
        unlockedLed.GetComponent<TextMesh>().fontSize = 0;

        ResetPointsText();
        UpdateCoinsText();
    }

    public void UpdatePointsText(float number)
    {
        if (pointsLed.GetComponent<ShakeText>() == null)
        {
            pointsLed.AddComponent<ShakeText>();
        }
        pointsLed.GetComponent<TextMesh>().text = "P " + number.ToString("n0");
    }

    public void ResetPointsText()
    {
        pointsLed.GetComponent<TextMesh>().text = "P 0";
    }

    public void UpdateCoinsText()
    {
        if (coinsLed.GetComponent<ShakeText>() == null)
        {
            coinsLed.AddComponent<ShakeText>();
        }
        coinsLed.GetComponent<TextMesh>().text = "    " + GetComponent<PlayerPrefsManager>().GetCoins().ToString("n0");
    }

    void UpdateHighScoreText()
    {
        highScoreLed.GetComponent<TextMesh>().text = "High Score\n" + GetComponent<PlayerPrefsManager>().GetHighScore().ToString("n0");
        totalScoreLed.GetComponent<TextMesh>().text = "Total Points\n" + GetComponent<PlayerPrefsManager>().GetTotalPoints().ToString("n0");
        NextUnlockText();
    }

    public void NextUnlockText()
    {
        if (GetComponent<PlayerPrefsManager>().PointsToNextUpgrade() > -1)
        {
            nextUnlockLed.GetComponent<TextMesh>().text = "Next Item\n" + GetComponent<PlayerPrefsManager>().PointsToNextUpgrade().ToString("n0");
        }
        else
        {
            float percentage = GetComponent<PlayerPrefsManager>().NotBoughtItemsPercentage();
            nextUnlockLed.GetComponent<TextMesh>().text = "All Items\nAvailable";
            unlockedLed.GetComponent<TextMesh>().text = percentage.ToString("F1") + " %\nUnlocked";
        }
    }

    public void ShowWhatIsUnlocked(string text)
    {
        if (text != "")
        {
            unlockedLed.GetComponent<TextMesh>().text = "New " + text + " \nAvailable";
        }
    }

    public void EraseUnlockText()
    {
        unlockedLed.GetComponent<TextMesh>().text = "";
    }

    public string GetUnlockLedText()
    {
        if (unlockedLed.GetComponent<TextMesh>().text != "")
        {
            return unlockedLed.GetComponent<TextMesh>().text.Split(' ')[1];
        }
        return "";
    }

    public void MakeUnlockTextBlink()
    {
        if (unlockedLed.GetComponent<TextMesh>().text != "" && !unlockedLed.GetComponent<TextMesh>().text.Contains("%"))
        {
            if (unlockedLed.GetComponent<PingPongColor>() == null)
            {
                Color[] ledColors = new Color[2];
                ledColors[0] = Color.cyan;
                ledColors[1] = Color.clear;
                unlockedLed.AddComponent<PingPongColor>().SetColors(ledColors);
            }
        }
    }

    public void RemoveBlinkingLED()
    {
        if (unlockedLed.GetComponent<PingPongColor>() != null)
        {
            Destroy(unlockedLed.GetComponent<PingPongColor>());
        }
    }

    public void CheckIfAnythingUnlocked()
    {
        ShowWhatIsUnlocked(GetComponent<PlayerPrefsManager>().CheckIfAnythingUnlocked());
        UpdateHighScoreText();
    }
}
