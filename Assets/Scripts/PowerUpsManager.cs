using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public class PowerUp
    {
        public int powerUpNumber;
        public int price;
        public bool unlocked;
        public string description;

        public PowerUp(int powerUpNum, int cost, string info)
        {
            powerUpNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Power Up " + powerUpNum, 0) == 1);
            description = info;
        }
    }

    public List<PowerUp> powerUpList = new List<PowerUp>();
    public PowerUp nothing, throwFurther, quickerCooking, makeMoreFood, defenseIncrease, moreHealth;
    public PowerUp largerFood, regenHealth, regenBurgers, regenFries, regenDrinks, doublePoints;
    public PowerUp doubleCoins, throwMultiple, magnet, noWind, noInstantKill, luck, noPoison, noIce;
    public PowerUp freeze;

    void Awake()
    {
        nothing = new PowerUp(-1, 0, "");
        throwFurther = new PowerUp(0, 1, "Throw further");
        quickerCooking = new PowerUp(1, 5, "Faster cooking");
        makeMoreFood = new PowerUp(2, 5, "Make more food");
        defenseIncrease = new PowerUp(3, 5, "Defense increase");
        moreHealth = new PowerUp(4, 5, "Double health");
        largerFood = new PowerUp(5, 5, "Larger food");
        regenHealth = new PowerUp(6, 5, "Health regeneration");
        regenBurgers = new PowerUp(7, 50, "Burger regeneration");
        regenFries = new PowerUp(8, 50, "Fries regeneration");
        regenDrinks = new PowerUp(9, 50, "Drink regeneration");
        doublePoints = new PowerUp(10, 50, "Double points");
        doubleCoins = new PowerUp(11, 50, "Double coins");
        throwMultiple = new PowerUp(12, 50, "Thrown food is\ndivided and scattered");
        magnet = new PowerUp(13, 50, "Food magnetic to\nzombies");
        noWind = new PowerUp(14, 50, "Food unaffected by\nwind");
        noInstantKill = new PowerUp(15, 50, "Muffle instant death\nattacks");
        luck = new PowerUp(16, 50, "Chance of dodging\nattacks");
        noPoison = new PowerUp(17, 50, "Immuned to posion\nzombies");
        noIce = new PowerUp(18, 50, "Immuned to ice\nzombies");
        freeze = new PowerUp(19, 5000, "Food freezes zombies");
        powerUpList.Add(throwFurther);
        powerUpList.Add(quickerCooking);
        powerUpList.Add(makeMoreFood);
        powerUpList.Add(defenseIncrease);
        powerUpList.Add(moreHealth);
        powerUpList.Add(largerFood);
        powerUpList.Add(regenHealth);
        powerUpList.Add(regenBurgers);
        powerUpList.Add(regenFries);
        powerUpList.Add(regenDrinks);
        powerUpList.Add(doublePoints);
        powerUpList.Add(doubleCoins);
        powerUpList.Add(throwMultiple);
        powerUpList.Add(magnet);
        powerUpList.Add(noWind);
        powerUpList.Add(noInstantKill);
        powerUpList.Add(luck);
        powerUpList.Add(noPoison);
        powerUpList.Add(noIce);
        powerUpList.Add(freeze);
    }
}
