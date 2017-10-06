using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
	int life = 10;
	int burgers = 50;
	int fries = 50;
	int drinks = 50;
    int completedOrders;
    float costToMakeBurger = 0.99f;
    float costToMakeFries = 0.79f;
    float costToMakeDrink = 0.59f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    float profit;
    bool gameOver;

    public void IncreaseNumberOfLostProduct(GameObject obj)
    {
        float cost = 0;
        switch (obj.name)
        {
            case "Burger(Clone)":
                cost = costToMakeBurger;
                break;
            case "Drink(Clone)":
                cost = costToMakeDrink;
                break;
            case "Fries(Clone)":
                cost = costToMakeFries;
                break;
        }
        profit -= cost;
        profit = Mathf.Round(profit * 100f) / 100f;
        //Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(obj, -cost);
    }

    public float IncreaseNumberOfSentProduct(GameObject obj)
    {
        float cost = 0;
        switch (obj.name)
        {
            case "Burger(Clone)":
                cost = costOfBurger - costToMakeBurger;
                break;
            case "Drink(Clone)":
                cost = costOfFries - costToMakeFries;
                break;
            case "Fries(Clone)":
                cost = costOfDrink - costToMakeDrink;
                break;
        }
        profit += cost;
        profit = Mathf.Round(profit * 100f) / 100f;
        //Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(obj, cost);
        return cost;
    }

    public void AddTip (GameObject obj, float tipAmount)
    {
        profit += tipAmount;
        profit = Mathf.Round(profit * 100f) / 100f;
        //Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(obj, tipAmount);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddLife(int n)
    {
        life += n;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText();
    }

    public int GetLife ()
    {
        return life;
    }

	public int GetBurgerCount()
	{
		return burgers;
	}

	public int GetFriesCount()
	{
        return fries;
	}

	public int GetDrinkCount()
	{
		return drinks;
	}

    public void ReduceBurgers ()
    {
        burgers--;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeBurgerCount();
    }

	public void ReduceFries()
	{
        fries--;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeFriesCount();
	}

	public void ReduceDrinks()
	{
        drinks--;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeDrinkCount();
	}

    public void AddBurgers (int amount)
    {
        burgers += amount;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeBurgerCount();
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
        life--;
		Camera.main.GetComponent<ScreenTextManagment>().ChangeMistakeText();
		if (life < 1)
        {
			gameOver = true;
            if (gameObject.GetComponent<CameraMovement>() != null)
            {
                Destroy(gameObject.GetComponent<CameraMovement>());
            }
            gameObject.AddComponent<CameraMovement>().MoveToGameOver();
        }
    }
}
