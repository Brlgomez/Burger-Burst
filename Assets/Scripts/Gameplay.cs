using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int numberOfLostProductsAllowed = 10;
    int completedOrders;
    float costToMakeBurger = 0.99f;
    float costToMakeFries = 0.79f;
    float costToMakeDrink = 0.59f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    float profit;
    bool gameOver = false;

    void Start()
    {
        Camera.main.GetComponent<ScreenTextManagment>().RestartScreens();
    }

    public void IncreaseNumberOfLostProduct(GameObject obj)
    {
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
        Camera.main.GetComponent<ScreenTextManagment>().ChangeProfitText(profit);
        Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(obj, -cost);
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
        Camera.main.GetComponent<ScreenTextManagment>().ChangeProfitText(profit);
        Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(obj, cost);
        return cost;
    }

    public void AddTip (GameObject obj, float tipAmount)
    {
        profit += tipAmount;
        profit = Mathf.Round(profit * 100f) / 100f;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeProfitText(profit);
        Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(obj, tipAmount);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddLife(int n)
    {
        numberOfLostProductsAllowed += n;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText(numberOfLostProductsAllowed);
    }

    public float GetProfit () 
    {
        return profit;
    }

    public void IncreaseCompletedOrders () 
    {
        completedOrders++;
    }

    public int GetCompletedOrdersCount () 
    {
        return completedOrders;
    }

    public void DeductNumberOfErrors () 
    {
        numberOfLostProductsAllowed--;
        if (numberOfLostProductsAllowed < 1)
        {
            gameOver = true;
        }
        Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText(numberOfLostProductsAllowed);
    }
}
