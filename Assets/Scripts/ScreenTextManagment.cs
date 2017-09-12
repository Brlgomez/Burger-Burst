using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTextManagment : MonoBehaviour
{
    GameObject profitText;
    GameObject mistakeText;
    Color originalScreenColor;
    GameObject secondScreenText;

    void Start()
    {
        originalScreenColor = new Color(0.117f, 0.445f, 0.773f);
        profitText = GameObject.Find("ProfitText");
        mistakeText = GameObject.Find("MistakeText");
        secondScreenText = GameObject.Find("Pause Text");
        profitText.GetComponent<TextMesh>().text = "$0.00";
        mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n10";
        mistakeText.GetComponent<Renderer>().material.color = originalScreenColor;
        profitText.GetComponent<Renderer>().material.color = originalScreenColor;
    }

    public void ChangeProfitText(string text, float profit)
    {
        profitText.GetComponent<TextMesh>().text = text;
        if (profit > 0)
        {
            profitText.GetComponent<Renderer>().material.color = originalScreenColor;
        }
        else
        {
            profitText.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void ChangeMistakeText(string text, int n, int max)
    {
        mistakeText.GetComponent<TextMesh>().text = text;
        Color newColor = Color.Lerp(Color.red, originalScreenColor, (float)n / max);
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
}
