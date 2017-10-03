using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTextManagment : MonoBehaviour
{
    int initialLostProductsAllowed = 10;
    int initialBurgers = 50;
    int initialFries = 50;
    int initialDrinks = 50;
    int burgerAmount, friesAmount, drinkAmount;
    GameObject mistakeText;
    GameObject line1, line2, line3, line4;
    GameObject secondScreenText;
    Color originalScreenColor;

    void Start()
    {
        burgerAmount = initialBurgers;
        friesAmount = initialFries;
        drinkAmount = initialDrinks;
        originalScreenColor = new Color(0.117f, 0.445f, 0.773f);
        mistakeText = GameObject.Find("MistakeText");
        line1 = GameObject.Find("Line1");
        line2 = GameObject.Find("Line2");
        line3 = GameObject.Find("Line3");
        line4 = GameObject.Find("Line4");
        secondScreenText = GameObject.Find("Pause Text");
        mistakeText.GetComponent<Renderer>().material.color = originalScreenColor;
    }

    public void ChangeMistakeText(int n)
    {
        if (n >= 1)
        {
            mistakeText.GetComponent<TextMesh>().text = "Health: " + n.ToString();
        }
        else
        {
            mistakeText.GetComponent<TextMesh>().text = "YOU'RE\nDEAD";
        }
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialLostProductsAllowed);
        mistakeText.GetComponent<Renderer>().material.color = newColor;
    }

    public void ChangeBurgerCount ()
    {
        burgerAmount--;
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)burgerAmount) / initialBurgers);
		line1.GetComponent<Renderer>().material.color = newColor;
        line1.GetComponent<TextMesh>().text = "B : " + burgerAmount;
    }

	public void ChangeFriesCount()
	{
        friesAmount--;
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)friesAmount) / initialFries);
		line2.GetComponent<Renderer>().material.color = newColor;
        line2.GetComponent<TextMesh>().text = "F : " + friesAmount;
	}

	public void ChangeDrinkCount()
	{
		drinkAmount--;
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)drinkAmount) / initialDrinks);
		line3.GetComponent<Renderer>().material.color = newColor;
		line3.GetComponent<TextMesh>().text = "D : " + drinkAmount;
	}

	public int BurgerCount()
	{
		return burgerAmount;
	}

	public int FriesCount()
	{
		return friesAmount;
	}

	public int DrinkCount()
	{
		return drinkAmount;
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
        ChangeMistakeText(initialLostProductsAllowed);
        SetSecondScreenText("", Color.white);
		burgerAmount = initialBurgers;
		friesAmount = initialFries;
		drinkAmount = initialDrinks;
        line1.GetComponent<Renderer>().material.color = originalScreenColor;
        line2.GetComponent<Renderer>().material.color = originalScreenColor;
        line3.GetComponent<Renderer>().material.color = originalScreenColor;
		line1.GetComponent<TextMesh>().text = "B : " + burgerAmount;
        line2.GetComponent<TextMesh>().text = "F : " + friesAmount;
        line3.GetComponent<TextMesh>().text = "D : " + drinkAmount;
    }
}
