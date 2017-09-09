using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    int numberOfLostProducts = 0;
    int numberOfSentProducts = 0;
    float costToMakeBurger = 2.48f;
    float costToMakeFries = 1.98f;
    float costToMakeDrink = 0.98f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    float profit;
    GameObject profitText;

    void Start()
    {
        profitText = GameObject.Find("ProfitText");
    }

    public void IncreaseNumberOfLostProduct(GameObject obj)
    {
        numberOfLostProducts++;
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
    }

    public void IncreaseNumberOfSentProduct(GameObject obj)
    {
        numberOfSentProducts++;
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
}
