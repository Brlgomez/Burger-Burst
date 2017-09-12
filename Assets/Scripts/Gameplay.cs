using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int initialLostProductsAllowed = 10;
    int numberOfLostProductsAllowed = 10;
    float costToMakeBurger = 2.48f;
    float costToMakeFries = 1.98f;
    float costToMakeDrink = 1.48f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    float profit;
    bool gameOver = false;
    GameObject aftermathText;

    void Start()
    {
        aftermathText = GameObject.Find("Aftermath Text");
    }

    public void IncreaseNumberOfLostProduct(GameObject obj)
    {
        numberOfLostProductsAllowed--;
        float cost = 0;
        if (obj.name == "Burger(Clone)")
        {
            cost = costToMakeBurger;
        }
        else if (obj.name == "Drink(Clone)")
        {
            cost = costToMakeDrink;
        }
        else if (obj.name == "Fries(Clone)")
        {
            cost = costToMakeFries;
        }
        profit -= cost;
        profit = Mathf.Round(profit * 100f) / 100f;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeProfitText("$" + profit.ToString("F2"), profit);
        Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText("Errors Left:\n" + numberOfLostProductsAllowed.ToString(), numberOfLostProductsAllowed, initialLostProductsAllowed);
        if (numberOfLostProductsAllowed < 0)
        {
            Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText("YOU'RE\nFIRED", numberOfLostProductsAllowed, initialLostProductsAllowed);
            gameOver = true;
        }
        AddFloatingText(obj, Color.red, "-$" + cost.ToString());
    }

    public float IncreaseNumberOfSentProduct(GameObject obj)
    {
        float cost = 0;
        if (obj.name == "Burger(Clone)")
        {
            cost = costOfBurger - costToMakeBurger;
        }
        else if (obj.name == "Drink(Clone)")
        {
            cost = costOfFries - costToMakeFries;
        }
        else if (obj.name == "Fries(Clone)")
        {
            cost = costOfDrink - costToMakeDrink;
        }
        profit += cost;
        profit = Mathf.Round(profit * 100f) / 100f;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeProfitText("$" + profit.ToString("F2"), profit);
        AddFloatingText(obj, Color.green, "+$" + cost.ToString());
        return cost;
    }

    public void AddTip (GameObject obj, float tipAmount)
    {
        profit += tipAmount;
        profit = Mathf.Round(profit * 100f) / 100f;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeProfitText("$" + profit.ToString("F2"), profit);
        AddFloatingText(obj, Color.green, "TIP +$" + tipAmount);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddLife(int n)
    {
        numberOfLostProductsAllowed += n;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText("Errors Left:\n" + numberOfLostProductsAllowed.ToString(), numberOfLostProductsAllowed, initialLostProductsAllowed);
    }

    public void AddFloatingText (GameObject obj, Color c, string text)
    {
        GameObject newAftermathText = Instantiate(aftermathText);
        newAftermathText.GetComponent<Renderer>().material.color = c;
        newAftermathText.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + Random.Range(0.0f, 1.0f), obj.transform.position.z);
        newAftermathText.AddComponent<AftermathText>().GetComponent<AftermathText>().updateText(text);
    }

    public float GetProfit () 
    {
        return profit;
    }
}
