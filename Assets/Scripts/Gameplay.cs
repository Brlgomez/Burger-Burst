using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int maxLifeWithBonus = 200;

    int maxLife = 100;
    float life = 100;
    int burgers = 25;
    int fries = 25;
    int drinks = 25;
    int completedOrders;
    int points;
    bool gameOver;
    float regularZombieDamage = 10;
    float defense = 1;
    bool moreLife;

    public void IncreasePoints(GameObject obj)
    {
        if (!IsGameOver())
        {
            int addedPoints = (int)Vector3.Distance(obj.transform.position, transform.position) / 2;
            points += addedPoints;
            GetComponent<FloatingTextManagement>().AddFloatingText(obj, addedPoints.ToString(), Color.cyan, addedPoints + 1);
            GetComponent<LEDManager>().UpdatePointsText(points);
        }
    }

    public int GetPoints()
    {
        return points;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddLife(int n)
    {
        if (life < maxLife)
        {
            life += n;
            Camera.main.GetComponent<ScreenTextManagment>().ChangeHealthCount(n);
        }
    }

    public void ReduceHealth()
    {
        life -= Mathf.RoundToInt(regularZombieDamage * defense);
        Camera.main.GetComponent<ScreenTextManagment>().ChangeHealthCount(-1);
        if (life < 1)
        {
            gameOver = true;
            if (gameObject.GetComponent<CameraMovement>() != null)
            {
                Destroy(gameObject.GetComponent<CameraMovement>());
            }
            gameObject.AddComponent<CameraMovement>().MoveToGameOver();
            GetComponent<ZombieManager>().DeleteAllScripts();
        }
    }

    public void ChangeMaxHealth()
    {
        maxLife = maxLifeWithBonus;
        life = maxLifeWithBonus;
        moreLife = true;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeHealthTextColor();
    }

    public float GetLife()
    {
        return life;
    }

    public int GetMaxLife()
    {
        return maxLife;
    }

    public bool HaveMoreLife()
    {
        return moreLife;
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
        Camera.main.GetComponent<ScreenTextManagment>().ChangeBurgerCount(-1);
    }

    public void ReduceFries()
    {
        fries--;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeFriesCount(-1);
    }

    public void ReduceDrinks()
    {
        drinks--;
        Camera.main.GetComponent<ScreenTextManagment>().ChangeDrinkCount(-1);
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
        Camera.main.GetComponent<ScreenTextManagment>().ChangeBurgerCount(amount);
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
        Camera.main.GetComponent<ScreenTextManagment>().ChangeFriesCount(amount);
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
        Camera.main.GetComponent<ScreenTextManagment>().ChangeDrinkCount(amount);
    }

    public void IncreaseCompletedOrders()
    {
        completedOrders++;
    }

    public int GetCompletedOrdersCount()
    {
        return completedOrders;
    }

    public void IncreaseDefense()
    {
        defense = 0.75f;
    }

    public void IncreaseCoinCount(int amount)
    {
        PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins", 0) + amount));
        GetComponent<LEDManager>().UpdateCoinsText();
    }

    public void DecreaseCoinCount(int amount)
    {
        PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins", 0) - amount));
        GetComponent<LEDManager>().UpdateCoinsText();
    }
}
