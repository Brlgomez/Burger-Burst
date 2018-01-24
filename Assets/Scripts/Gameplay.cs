using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int completedOrders;
    int foodMade;
    int points;
    bool gameOver;
    bool continued;
    int maxLife = 100;
    float life = 100;
    int burgers = 25;
    int fries = 25;
    int drinks = 25;

    //Power Ups
    static float regenMaxTime = 15;
    float regenTimer;
    float defense = 1;
    bool moreLife;
    int maxLifeWithBonus = 200;

    //Zombies
    static int minTimeForNewZombie = 6;
    float timeForNewZombie = 12;
    static float maxChanceOfDifSizedZombie = 0.25f;
    float chanceOfDifSizedZombie;
    static float maxChanceOfSpecialZombie = 0.25f;
    float chanceOfSpecialZombie;

    //SUV spawn
    static float maxChanceOfSUV = 0.5f;
    float chanceOfSUV;

    float currentSurvivalTime;

    public void IncreasePoints(GameObject obj, int multiplier)
    {
        if (!IsGameOver())
        {
            float sizeOfText = Vector3.Distance(obj.transform.position, transform.position) / 2;
            int addedPoints = (Mathf.RoundToInt(sizeOfText)) * multiplier;
            if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().doublePoints.powerUpNumber))
            {
                addedPoints *= 2;
            }
            points += addedPoints;
            GetComponent<FloatingTextManagement>().AddFloatingText(obj, addedPoints.ToString(), Color.cyan, sizeOfText + 1);
            GetComponent<LEDManager>().UpdatePointsText(points);
            GetComponent<PlayerPrefsManager>().IncreaseTotalPoints(addedPoints);
            if (sizeOfText > 12f)
            {
                GetComponent<OnlineManagement>().FarawayThrow();
            }
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
            GetComponent<SoundAndMusicManager>().PlayHealthUpSound(gameObject);
            var main = gameObject.transform.GetChild(9).GetComponent<ParticleSystem>().main;
            gameObject.transform.GetChild(9).GetComponent<ParticleSystem>().Play();
            main.maxParticles = n;
            if (life >= 25)
            {
                GetComponent<ScreenTextManagment>().RemovePingPongHeart();
            }
        }
    }

    public void ReduceHealth(int damage, GameObject zombie)
    {
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().luck.powerUpNumber))
        {
            if (Random.value > 0.125f)
            {
                if (damage > 500)
                {
                    Camera.main.GetComponent<SoundAndMusicManager>().PlayDeathPunchSound(gameObject);
                }
                else
                {
                    Camera.main.GetComponent<SoundAndMusicManager>().PlayPunchSound(gameObject);
                }
                ReduceHealthLogic(damage);
            }
            else
            {
                GetComponent<SoundAndMusicManager>().PlayWooshSound(zombie);
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
        GetComponent<VibrationManager>().Vibrate();
        GetComponent<SoundAndMusicManager>().PlayHealthDownSound(gameObject);
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
            CheckSurvivalTime();
            GetComponent<GrabAndThrowObject>().SetGameOver(gameOver);
            if (GetComponent<CameraMovement>() != null)
            {
                Destroy(GetComponent<CameraMovement>());
            }
            gameObject.AddComponent<CameraMovement>().MoveToGameOver();
            GetComponent<ZombieManager>().DeleteAllScripts();
        }
        if (life < 25 && life > 1)
        {
            GetComponent<ScreenTextManagment>().PingPongHeart();
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
        foodMade += amount;
        GetComponent<PlayerPrefsManager>().IncreaseFoodProduced(amount);
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
        foodMade += amount;
        GetComponent<PlayerPrefsManager>().IncreaseFoodProduced(amount);
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
        foodMade += amount;
        GetComponent<PlayerPrefsManager>().IncreaseFoodProduced(amount);
        GetComponent<ScreenTextManagment>().ChangeDrinkCount(amount);
    }

    public void IncreaseCompletedOrders()
    {
        completedOrders++;
        if (timeForNewZombie > minTimeForNewZombie)
        {
            timeForNewZombie -= 0.1f;
        }
        if (chanceOfSUV < maxChanceOfSUV)
        {
            chanceOfSUV += 0.01f;
        }
        if (chanceOfDifSizedZombie < maxChanceOfDifSizedZombie)
        {
            chanceOfDifSizedZombie += 0.005f;
        }
        if (chanceOfSpecialZombie < maxChanceOfSpecialZombie)
        {
            chanceOfSpecialZombie += 0.0075f;
        }
    }

    public int GetCompletedOrdersCount()
    {
        return completedOrders;
    }

    public int GetFoodProduced()
    {
        return foodMade;
    }

    public void IncreaseDefense()
    {
        defense = 0.75f;
    }

    public void StartCoinLancher(int number, GameObject obj)
    {
        int amount = number;
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().doubleCoins.powerUpNumber))
        {
            amount *= 2;
        }
        gameObject.AddComponent<CoinSpawn>().StartCoinLaunch(amount, obj.transform.position);
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
        continued = false;
        maxLifeWithBonus = 200;
        maxLife = 100;
        life = 100;
        burgers = 25;
        fries = 25;
        drinks = 25;
        completedOrders = 0;
        foodMade = 0;
        regenTimer = 0;
        points = 0;
        timeForNewZombie = 12;
        chanceOfSUV = 0;
        chanceOfDifSizedZombie = 0;
        chanceOfSpecialZombie = 0;
        currentSurvivalTime = 0;
        GetComponent<LEDManager>().ResetPointsText();
        GetComponent<WindManager>().ResetValues();
        GetComponent<DropMoreProducts>().ResetDroppedItems();
        GetComponent<ScreenTextManagment>().ChangeHeartSprite("");
        GetComponent<ScreenTextManagment>().RemovePingPongHeart();
        NotGameOver();
    }

    public void Continue()
    {
        continued = true;
        maxLifeWithBonus = 200;
        maxLife = 100;
        life = 100;
        burgers += 10;
        fries += 10;
        drinks += 10;
        regenTimer = 0;
        GetComponent<DropMoreProducts>().ResetDroppedItems();
        GetComponent<ScreenTextManagment>().ChangeHeartSprite("");
        GetComponent<ScreenTextManagment>().RemovePingPongHeart();
        NotGameOver();
    }

    void NotGameOver()
    {
        gameOver = false;
        if (GetComponent<GrabAndThrowObject>() != null)
        {
            GetComponent<GrabAndThrowObject>().SetSurvivalTime(currentSurvivalTime);
            GetComponent<GrabAndThrowObject>().SetGameOver(gameOver);
        }
    }

    public int ContinuePrice()
    {
        return Mathf.RoundToInt(completedOrders * 1.1f) + 1;
    }

    public bool GetContinued()
    {
        return continued;
    }

    public float GetTimeForNewZombie()
    {
        return timeForNewZombie;
    }

    public float GetChanceOfSUV()
    {
        return chanceOfSUV;
    }

    public float GetChanceOfDifSizedZombie()
    {
        return chanceOfDifSizedZombie;
    }

    public float GetChanceOfSpecialZombie()
    {
        return chanceOfSpecialZombie;
    }

    public void CheckSurvivalTime()
    {
        float time = GetComponent<GrabAndThrowObject>().GetSurvivalTime();
        currentSurvivalTime = time;
        GetComponent<PlayerPrefsManager>().CheckSurvivalTime(time);
    }
}
