using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int life = 10;
    int burgers = 25;
    int fries = 25;
    int drinks = 25;
    int completedOrders;
    int points;
    bool gameOver;

    public void IncreasePoints(GameObject obj)
    {
        if (!IsGameOver())
        {
            int addedPoints = (int)Vector3.Distance(obj.transform.position, transform.position) / 2;
            points += addedPoints;
            GetComponent<FloatingTextManagement>().AddFloatingText(obj, addedPoints.ToString(), Color.yellow, addedPoints);
            GetComponent<LEDManager>().UpdateText(points);
        }
    }

    public int GetPoints ()
    {
        return points;
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

    public int GetLife()
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

    public void ReduceBurgers()
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

    public void AddBurgers(int amount)
    {
        if (burgers <= 0)
        {
            burgers += amount;
            Camera.main.GetComponent<DropMoreProducts>().DropBurger();
        }
        else
        {
            burgers += amount;
        }
        Camera.main.GetComponent<ScreenTextManagment>().ChangeBurgerCount();
    }

    public void AddFries(int amount)
    {
        if (fries <= 0)
        {
            fries += amount;
            Camera.main.GetComponent<DropMoreProducts>().DropMadeFries();
        }
        else
        {
            fries += amount;
        }
        Camera.main.GetComponent<ScreenTextManagment>().ChangeFriesCount();
    }

    public void AddDrinks(int amount)
    {
        if (drinks <= 0)
        {
            drinks += amount;
            Camera.main.GetComponent<DropMoreProducts>().DropDrink();
        }
        else
        {
            drinks += amount;
        }
        Camera.main.GetComponent<ScreenTextManagment>().ChangeDrinkCount();
    }

    public void IncreaseCompletedOrders()
    {
        completedOrders++;
    }

    public int GetCompletedOrdersCount()
    {
        return completedOrders;
    }

    public void DeductNumberOfErrors()
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

    public bool SetUpgrades(int position, int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i) == upgradeNumber && upgradeNumber != 0)
            {
                return false;
            }
        }
        PlayerPrefs.SetInt("UPGRADE " + position, upgradeNumber);
		return true;
	}

    public int GetUpgrades(int n)
    {
        return PlayerPrefs.GetInt("UPGRADE " + n);
    }
}
