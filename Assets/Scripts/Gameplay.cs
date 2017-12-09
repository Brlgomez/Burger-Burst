using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    static float regenMaxTime = 15;
    float regenTimer;

    int maxLifeWithBonus = 200;
    int maxLife = 100;
    float life = 100;
    int burgers = 25;
    int fries = 25;
    int drinks = 25;
    int completedOrders;
    int points;
    bool gameOver;
    float defense = 1;
    bool moreLife;

    public void IncreasePoints(GameObject obj)
    {
        if (!IsGameOver())
        {
            int addedPoints = (int)Vector3.Distance(obj.transform.position, transform.position) / 2;
            if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().doublePoints.powerUpNumber))
            {
                addedPoints *= 2;
            }
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

    public void AddLife(int n, GameObject obj)
    {
        if (life < maxLife)
        {
            float size = Vector3.Distance(obj.transform.position, transform.position) / 2;
            life += n;
            if (life > maxLife)
            {
                life = maxLife;
            }
            GetComponent<FloatingTextManagement>().AddFloatingText(obj, "+HP", Color.green, size + 1);
            GetComponent<ScreenTextManagment>().ChangeHealthCount(n);
        }
    }

    public void ReduceHealth(int damage, GameObject zombie)
    {
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().luck.powerUpNumber))
        {
            if (Random.value > 0.125f)
            {
                ReduceHealthLogic(damage);
            }
            else
            {
                GetComponent<FloatingTextManagement>().AddFloatingText(zombie, "MISS", Color.yellow, 4);
            }
        }
        else
        {
            ReduceHealthLogic(damage);
        }
    }

    void ReduceHealthLogic(int damage)
    {
        life -= Mathf.RoundToInt(damage * defense);
		if (!gameOver)
		{
			GetComponent<ScreenTextManagment>().ChangeHealthCount(-1);
			if (gameObject.GetComponent<GettingHurt>() == null)
			{
				gameObject.AddComponent<GettingHurt>();
			}
		}
        if (life < 1)
        {
            gameOver = true;
			GetComponent<GrabAndThrowObject>().SetGameOver(gameOver);
			GetComponent<ScreenTextManagment>().CannotPressAnything();
            if (GetComponent<CameraMovement>() != null)
            {
                Destroy(GetComponent<CameraMovement>());
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
        GetComponent<ScreenTextManagment>().ChangeHealthTextColor();
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
        GetComponent<ScreenTextManagment>().ChangeBurgerCount(-1);
    }

    public void ReduceFries()
    {
        fries--;
        GetComponent<ScreenTextManagment>().ChangeFriesCount(-1);
    }

    public void ReduceDrinks()
    {
        drinks--;
        GetComponent<ScreenTextManagment>().ChangeDrinkCount(-1);
    }

    public void AddBurgers(int amount)
    {
        if (burgers <= 0)
        {
            burgers += amount;
            GetComponent<DropMoreProducts>().DropBurger();
        }
        else
        {
            burgers += amount;
        }
        GetComponent<ScreenTextManagment>().ChangeBurgerCount(amount);
    }

    public void AddFries(int amount)
    {
        if (fries <= 0)
        {
            fries += amount;
            GetComponent<DropMoreProducts>().DropMadeFries();
        }
        else
        {
            fries += amount;
        }
        GetComponent<ScreenTextManagment>().ChangeFriesCount(amount);
    }

    public void AddDrinks(int amount)
    {
        if (drinks <= 0)
        {
            drinks += amount;
            GetComponent<DropMoreProducts>().DropDrink();
        }
        else
        {
            drinks += amount;
        }
        GetComponent<ScreenTextManagment>().ChangeDrinkCount(amount);
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

    public void IncreaseCoinCount(int number, GameObject obj)
    {
        int amount = number;
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().doubleCoins.powerUpNumber))
        {
            amount *= 2;
        }
        gameObject.AddComponent<CoinSpawn>().StartCoinLaunch(amount, obj.transform.position);
    }

    public void DecreaseCoinCount(int amount)
    {
        GetComponent<PlayerPrefsManager>().DecreaseCoins(amount);
        GetComponent<LEDManager>().UpdateCoinsText();
    }

    public void RegenerationUpdate(int updateInterval)
    {
        regenTimer += Time.deltaTime * updateInterval;
        if (regenTimer > regenMaxTime)
        {
            if (!GetComponent<Gameplay>().IsGameOver())
            {
                if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().regenHealth.powerUpNumber))
                {
                    GetComponent<Gameplay>().AddLife(2, gameObject);
                }
                if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().regenBurgers.powerUpNumber))
                {
                    GetComponent<Gameplay>().AddBurgers(1);
                }
                if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().regenFries.powerUpNumber))
                {
                    GetComponent<Gameplay>().AddFries(1);
                }
                if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().regenDrinks.powerUpNumber))
                {
                    GetComponent<Gameplay>().AddDrinks(1);
                }
            }
            regenTimer = 0;
        }
    }

    public void ResetValues()
    {
		maxLifeWithBonus = 200;
		maxLife = 100;
		life = 100;
		burgers = 25;
		fries = 25;
		drinks = 25;
        gameOver = false;
		completedOrders = 0;
        regenTimer = 0;
		if (GetComponent<GrabAndThrowObject>() != null)
		{
			GetComponent<GrabAndThrowObject>().SetGameOver(gameOver);
		}
    }
}
