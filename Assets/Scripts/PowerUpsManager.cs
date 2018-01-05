using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    int maxPowerUps;
    public List<PowerUp> powerUpList = new List<PowerUp>();
    public PowerUp throwFurther, quickerCooking, makeMoreFood, defenseIncrease, moreHealth;
    public PowerUp largerFood, regenHealth, regenBurgers, regenFries, regenDrinks, doublePoints;
    public PowerUp doubleCoins, throwMultiple, magnet, noWind, noInstantKill, luck, noPoison, noIce;
    public PowerUp freeze;
    string[] allDescriptions;
    int powerUpInc;
    List<PowerUp> powerUpListTemp = new List<PowerUp>();

    public class PowerUp
    {
        public int powerUpNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;

        public PowerUp(int powerUpNum, int cost, string info)
        {
            powerUpNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Power Up " + powerUpNum, 0) == 1);
            description = info;
            sprite = (Sprite)Resources.Load("Sprites/Power Ups/" + powerUpNum, typeof(Sprite));
        }
    }

    void Awake()
    {
        TextAsset t = new TextAsset();
        t = Resources.Load("Power Ups") as TextAsset;
        allDescriptions = t.text.Split('\n');
        maxPowerUps = allDescriptions.Length;

        CreatePowerUp(throwFurther);
        CreatePowerUp(quickerCooking);
        CreatePowerUp(makeMoreFood);
        CreatePowerUp(defenseIncrease);
        CreatePowerUp(moreHealth);
        CreatePowerUp(largerFood);
        CreatePowerUp(regenHealth);
        CreatePowerUp(regenBurgers);
        CreatePowerUp(regenFries);
        CreatePowerUp(regenDrinks);
        CreatePowerUp(doublePoints);
        CreatePowerUp(doubleCoins);
        CreatePowerUp(throwMultiple);
        CreatePowerUp(magnet);
        CreatePowerUp(noWind);
        CreatePowerUp(noInstantKill);
        CreatePowerUp(luck);
        CreatePowerUp(noPoison);
        CreatePowerUp(noIce);
        CreatePowerUp(freeze);

        for (int i = 0; i < maxPowerUps; i++)
        {
            powerUpList.Add(powerUpListTemp[i]);
        }
    }

    void CreatePowerUp(PowerUp powerUp)
    {
        string description = allDescriptions[powerUpInc].Replace("NEWLINE", "\n");
        powerUp = new PowerUp(powerUpInc, int.Parse(description.Split('*')[1]), description.Split('*')[0]);
        powerUpListTemp.Add(powerUp);
        powerUpInc++;
    }

    public void SetPowerUpLED()
    {
        int slot1PowerUp = GetComponent<PlayerPrefsManager>().GetPowerUpFromSlot(1);
        int slot2PowerUp = GetComponent<PlayerPrefsManager>().GetPowerUpFromSlot(2);
        int slot3PowerUp = GetComponent<PlayerPrefsManager>().GetPowerUpFromSlot(3);
        if (slot1PowerUp >= 0)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[slot1PowerUp].sprite;
        }
        if (slot2PowerUp >= 0)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[slot2PowerUp].sprite;
        }
        if (slot3PowerUp >= 0)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(2).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[slot3PowerUp].sprite;
        }
    }
}
