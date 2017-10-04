using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
ScreenTextManagment : MonoBehaviour
{
    int initialLife;
    int initialBurgers;
    int initialFries;
    int initialDrinks;
    GameObject line1, line2, line3, line4, line5;
    Color originalScreenColor;
    Color unpressable;

    void Start()
    {
        initialLife = gameObject.GetComponent<Gameplay>().GetLife();
        initialBurgers = gameObject.GetComponent<Gameplay>().GetBurgerCount();
        initialFries = gameObject.GetComponent<Gameplay>().GetFriesCount();
        initialDrinks = gameObject.GetComponent<Gameplay>().GetDrinkCount();
        originalScreenColor = new Color(0.117f, 0.445f, 0.773f);
        unpressable = new Color(0.9f, 0.9f, 0.9f);
        line1 = GameObject.Find("Line1");
        line2 = GameObject.Find("Line2");
        line3 = GameObject.Find("Line3");
        line4 = GameObject.Find("Line4");
        line5 = GameObject.Find("Line5");
        line1.GetComponent<Renderer>().material.color = originalScreenColor;
        ChangeToMenuText();
    }

    public void ChangeToMenuText()
    {
        line1.GetComponent<Renderer>().material.color = originalScreenColor;
        line2.GetComponent<Renderer>().material.color = originalScreenColor;
        line3.GetComponent<Renderer>().material.color = originalScreenColor;
        line4.GetComponent<Renderer>().material.color = originalScreenColor;
        line5.GetComponent<Renderer>().material.color = originalScreenColor;
        line1.GetComponent<TextMesh>().text = "GAME";
        line2.GetComponent<TextMesh>().text = "Play";
        line3.GetComponent<TextMesh>().text = "Scores";
        line4.GetComponent<TextMesh>().text = "Settings";
        line5.GetComponent<TextMesh>().text = "";
    }

    public void ChangeToGamePlayText()
    {
        ChangeMistakeText();
        ChangeBurgerCount();
        ChangeFriesCount();
        ChangeDrinkCount();
        line5.GetComponent<TextMesh>().text = "Back";
    }

    public void ChangeToPauseText()
    {
        line1.GetComponent<Renderer>().material.color = originalScreenColor;
        line2.GetComponent<Renderer>().material.color = originalScreenColor;
        line3.GetComponent<Renderer>().material.color = originalScreenColor;
        line4.GetComponent<Renderer>().material.color = originalScreenColor;
        line5.GetComponent<Renderer>().material.color = originalScreenColor;
        line1.GetComponent<TextMesh>().text = "PAUSED";
        line2.GetComponent<TextMesh>().text = "Resume";
        line3.GetComponent<TextMesh>().text = "Restart";
        line4.GetComponent<TextMesh>().text = "Quit";
        line5.GetComponent<TextMesh>().text = "";
    }

    public void ChangeMistakeText()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetLife();
        if (n >= 1)
        {
            line1.GetComponent<TextMesh>().text = "Health: " + n.ToString();
        }
        else
        {
            line1.GetComponent<TextMesh>().text = "YOU'RE DEAD";
        }
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialLife);
        line1.GetComponent<Renderer>().material.color = newColor;
    }

    public void ChangeBurgerCount()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetBurgerCount();
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialBurgers);
        line2.GetComponent<Renderer>().material.color = newColor;
        line2.GetComponent<TextMesh>().text = "B : " + n;
    }

    public void ChangeFriesCount()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetFriesCount();
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialFries);
        line3.GetComponent<Renderer>().material.color = newColor;
        line3.GetComponent<TextMesh>().text = "F : " + n;
    }

    public void ChangeDrinkCount()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetDrinkCount();
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialDrinks);
        line4.GetComponent<Renderer>().material.color = newColor;
        line4.GetComponent<TextMesh>().text = "D : " + n;
    }

    public void RestartScreens()
    {
        ChangeMistakeText();
        line1.GetComponent<Renderer>().material.color = originalScreenColor;
        line2.GetComponent<Renderer>().material.color = originalScreenColor;
        line3.GetComponent<Renderer>().material.color = originalScreenColor;
        line4.GetComponent<Renderer>().material.color = originalScreenColor;
        line1.GetComponent<TextMesh>().text = "Health: " + initialLife;
        line2.GetComponent<TextMesh>().text = "B : " + initialBurgers;
        line3.GetComponent<TextMesh>().text = "F : " + initialFries;
        line4.GetComponent<TextMesh>().text = "D : " + initialDrinks;
        line5.GetComponent<TextMesh>().text = "Back";
    }

    public void PressTextDown(GameObject target)
    {
        float r = target.GetComponent<Renderer>().material.color.r;
        float g = target.GetComponent<Renderer>().material.color.g;
        float b = target.GetComponent<Renderer>().material.color.b;
        target.GetComponent<Renderer>().material.color = new Color(r / 2, g / 2, b / 2);
    }

    public void PressTextUp(GameObject target)
    {
        float r = target.GetComponent<Renderer>().material.color.r;
        float g = target.GetComponent<Renderer>().material.color.g;
        float b = target.GetComponent<Renderer>().material.color.b;
        target.GetComponent<Renderer>().material.color = new Color(r * 2, g * 2, b * 2);
    }

    public void RevertButtons()
    {
        line1.transform.GetChild(0).tag = "UI";
        line2.transform.GetChild(0).tag = "UI";
        line3.transform.GetChild(0).tag = "UI";
        line4.transform.GetChild(0).tag = "UI";
        line5.transform.GetChild(0).tag = "UI";
        line1.GetComponent<Renderer>().material.color = originalScreenColor;
        line2.GetComponent<Renderer>().material.color = originalScreenColor;
        line3.GetComponent<Renderer>().material.color = originalScreenColor;
        line4.GetComponent<Renderer>().material.color = originalScreenColor;
        line5.GetComponent<Renderer>().material.color = originalScreenColor;
    }

    public void MakeGrillUnpressable()
    {
        RevertButtons();
        line2.transform.GetChild(0).tag = "Untagged";
        line2.GetComponent<Renderer>().material.color = unpressable;
    }

    public void MakeFryerUnpressable()
    {
        RevertButtons();
        line3.transform.GetChild(0).tag = "Untagged";
        line3.GetComponent<Renderer>().material.color = unpressable;
    }

    public void MakeSodaUnpressable()
    {
        RevertButtons();
        line4.transform.GetChild(0).tag = "Untagged";
        line4.GetComponent<Renderer>().material.color = unpressable;
    }

    public void MakeFrontUnpressable()
    {
        RevertButtons();
        line5.transform.GetChild(0).tag = "Untagged";
        line5.GetComponent<Renderer>().material.color = unpressable;
    }
}
