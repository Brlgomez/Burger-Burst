using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int numberOfLostProductsAllowed = 10;
    float costToMakeBurger = 2.48f;
    float costToMakeFries = 1.98f;
    float costToMakeDrink = 0.98f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    float profit;
    bool gameOver = false;
    GameObject profitText;
    GameObject mistakeText;
    Color originalScreenColor;

    void Start()
    {
        originalScreenColor = new Color(0.117f, 0.445f, 0.773f);
        profitText = GameObject.Find("ProfitText");
        mistakeText = GameObject.Find("MistakeText");
        profitText.GetComponent<TextMesh>().text = "$0.00";
        mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n" + numberOfLostProductsAllowed.ToString();
        mistakeText.GetComponent<Renderer>().material.color = originalScreenColor;
        profitText.GetComponent<Renderer>().material.color = originalScreenColor;
    }

    public void IncreaseNumberOfLostProduct(GameObject obj)
    {
        numberOfLostProductsAllowed--;
        if (obj.name == "Burger(Clone)")
        {
            profit -= costToMakeBurger;
        }
        else if (obj.name == "Drink(Clone)")
        {
            profit -= costToMakeFries;
        }
        else if (obj.name == "Fries(Clone)")
        {
            profit -= costToMakeDrink;
        }
        profit = Mathf.Round(profit * 100f) / 100f;
        profitText.GetComponent<TextMesh>().text = "$" + profit.ToString("F2");
        mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n" + numberOfLostProductsAllowed.ToString();
        mistakeText.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, originalScreenColor, (float)numberOfLostProductsAllowed/10);
        if (numberOfLostProductsAllowed < 0)
        {
            mistakeText.GetComponent<TextMesh>().text = "YOU'RE\nFIRED!";
            gameOver = true;
        }
        if (profit < 0)
        {
            profitText.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void IncreaseNumberOfSentProduct(GameObject obj)
    {
        if (obj.name == "Burger(Clone)")
        {
            profit += costOfBurger - costToMakeBurger;
        }
        else if (obj.name == "Drink(Clone)")
        {
            profit += costOfFries - costToMakeFries;
        }
        else if (obj.name == "Fries(Clone)")
        {
            profit += costOfDrink - costToMakeDrink;
        }
        profit = Mathf.Round(profit * 100f) / 100f;
        profitText.GetComponent<TextMesh>().text = "$" + profit.ToString("F2");
        if (profit > 0)
        {
            profitText.GetComponent<Renderer>().material.color = originalScreenColor;
        }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
