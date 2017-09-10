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

    void Start()
    {
        profitText = GameObject.Find("ProfitText");
        mistakeText = GameObject.Find("MistakeText");
        profitText.GetComponent<TextMesh>().text = "$0.00";
        mistakeText.GetComponent<TextMesh>().text = "Mistakes Allowed: " + numberOfLostProductsAllowed.ToString();
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
        mistakeText.GetComponent<TextMesh>().text = "Mistakes Allowed: " + numberOfLostProductsAllowed.ToString();
        if (numberOfLostProductsAllowed < 0)
        {
            mistakeText.GetComponent<TextMesh>().text = "YOU'RE FIRED!";
            gameOver = true;
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
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
