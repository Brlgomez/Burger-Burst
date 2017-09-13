using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTextManagment : MonoBehaviour
{
    int initialLostProductsAllowed = 10;
    GameObject profitText;
    GameObject mistakeText;
    GameObject secondScreenText;
    Color originalScreenColor;

    void Start()
    {
        originalScreenColor = new Color(0.117f, 0.445f, 0.773f);
        profitText = GameObject.Find("ProfitText");
        mistakeText = GameObject.Find("MistakeText");
        secondScreenText = GameObject.Find("Pause Text");
        mistakeText.GetComponent<Renderer>().material.color = originalScreenColor;
        profitText.GetComponent<Renderer>().material.color = originalScreenColor;
    }

    public void ChangeProfitText(float profit)
    {
        profitText.GetComponent<TextMesh>().text = "$" + profit.ToString("F2");
        if (profit >= 0)
        {
            profitText.GetComponent<Renderer>().material.color = originalScreenColor;
        }
        else
        {
            profitText.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void ChangeMistakeText(int n)
    {
        if (n >= 0)
        {
            mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n" + n.ToString();
        }
        else
        {
            mistakeText.GetComponent<TextMesh>().text = "YOU'RE\nFIRED!";
        }
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialLostProductsAllowed);
        mistakeText.GetComponent<Renderer>().material.color = newColor;
    }

    public void SetSecondScreenText(string text, Color c)
    {
        secondScreenText.GetComponent<TextMesh>().text = text;
        if (c != Color.white)
        {
            secondScreenText.GetComponent<Renderer>().material.color = c;
        }
    }

    public void RestartScreens () {
        ChangeProfitText(0);
        ChangeMistakeText(initialLostProductsAllowed);
        SetSecondScreenText("", Color.white);
    }
}
