using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public struct powerUpStruct
    {
        public int powerUpNumber;
        public int price;
        public bool unlocked;
        public string description;

        public powerUpStruct(int powerUpNum, int cost, string info)
        {
            powerUpNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Power Up " + powerUpNum, 0) == 1);
            description = info;
        }
    }

    public List<powerUpStruct> powerUpList = new List<powerUpStruct>();
    public powerUpStruct nothing, throwFurther, quickerCooking, makeMoreFood, defenseIncrease, moreHealth;
    public powerUpStruct largerFood, regenHealth, regenBurgers, regenFries, regenDrinks, doublePoints;
    public powerUpStruct doubleCoins, throwMultiple, magnet, noWind, noInstantKill, luck, noPoison, noIce;
    public powerUpStruct freeze;

    void Awake()
    {
        nothing = new powerUpStruct(-1, 0, "");
        throwFurther = new powerUpStruct(0, 60, "Throw further");
        quickerCooking = new powerUpStruct(1, 70, "Faster cooking");
        makeMoreFood = new powerUpStruct(2, 80, "Make more food");
        defenseIncrease = new powerUpStruct(3, 90, "Defense increase");
        moreHealth = new powerUpStruct(4, 50, "Double health");
        largerFood = new powerUpStruct(5, 50, "Larger food");
        regenHealth = new powerUpStruct(6, 50, "Health regeneration");
        regenBurgers = new powerUpStruct(7, 50, "Burger regeneration");
        regenFries = new powerUpStruct(8, 50, "Fries regeneration");
        regenDrinks = new powerUpStruct(9, 50, "Drink regeneration");
        doublePoints = new powerUpStruct(10, 50, "Double points");
        doubleCoins = new powerUpStruct(11, 50, "Double coins");
        throwMultiple = new powerUpStruct(12, 50, "Thrown food is\ndivided and scattered");
        magnet = new powerUpStruct(13, 50, "Food magnetic to\nzombies");
        noWind = new powerUpStruct(14, 50, "Food unaffected by\nwind");
        noInstantKill = new powerUpStruct(15, 50, "Muffle instant death\nattacks");
        luck = new powerUpStruct(16, 50, "Chance of dodging\nattacks");
        noPoison = new powerUpStruct(17, 50, "Immuned to posion\nzombies");
        noIce = new powerUpStruct(18, 50, "Immuned to ice\nzombies");
        freeze = new powerUpStruct(19, 50, "Food freezes zombies");
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
