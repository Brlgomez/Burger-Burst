using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public List<Sprite> powerUpSprites;
    public List<PowerUp> powerUpList = new List<PowerUp>();
    public PowerUp nothing, throwFurther, quickerCooking, makeMoreFood, defenseIncrease, moreHealth;
    public PowerUp largerFood, regenHealth, regenBurgers, regenFries, regenDrinks, doublePoints;
    public PowerUp doubleCoins, throwMultiple, magnet, noWind, noInstantKill, luck, noPoison, noIce;
    public PowerUp freeze;

    public class PowerUp
    {
        public int powerUpNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;

        public PowerUp(int powerUpNum, int cost, string info, Sprite icon)
        {
            powerUpNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Power Up " + powerUpNum, 0) == 1);
            description = info;
            sprite = icon;
        }
    }

    void Awake()
    {
        nothing = new PowerUp(-1, 0, "", null);
        powerUpList.Add(throwFurther = new PowerUp(0, 1, "Throw further", powerUpSprites[0]));
        powerUpList.Add(quickerCooking = new PowerUp(1, 5, "Faster cooking", powerUpSprites[1]));
        powerUpList.Add(makeMoreFood = new PowerUp(2, 5, "Make more food", powerUpSprites[2]));
        powerUpList.Add(defenseIncrease = new PowerUp(3, 5, "Defense increase", powerUpSprites[3]));
        powerUpList.Add(moreHealth = new PowerUp(4, 5, "Double health", powerUpSprites[4]));
        powerUpList.Add(largerFood = new PowerUp(5, 5, "Larger food", powerUpSprites[5]));
        powerUpList.Add(regenHealth = new PowerUp(6, 5, "Health regeneration", powerUpSprites[6]));
        powerUpList.Add(regenBurgers = new PowerUp(7, 50, "Burger regeneration", powerUpSprites[7]));
        powerUpList.Add(regenFries = new PowerUp(8, 50, "Fries regeneration", powerUpSprites[8]));
        powerUpList.Add(regenDrinks = new PowerUp(9, 50, "Drink regeneration", powerUpSprites[9]));
        powerUpList.Add(doublePoints = new PowerUp(10, 50, "Double points", powerUpSprites[10]));
        powerUpList.Add(doubleCoins = new PowerUp(11, 50, "Double coins", powerUpSprites[11]));
        powerUpList.Add(throwMultiple = new PowerUp(12, 50, "Thrown food is\ndivided and scattered", powerUpSprites[12]));
        powerUpList.Add(magnet = new PowerUp(13, 50, "Food magnetic to\nzombies", powerUpSprites[13]));
        powerUpList.Add(noWind = new PowerUp(14, 50, "Food unaffected by\nwind", powerUpSprites[14]));
        powerUpList.Add(noInstantKill = new PowerUp(15, 50, "Muffle instant death\nattacks", powerUpSprites[15]));
        powerUpList.Add(luck = new PowerUp(16, 50, "Chance of dodging\nattacks", powerUpSprites[16]));
        powerUpList.Add(noPoison = new PowerUp(17, 50, "Immuned to posion\nzombies", powerUpSprites[17]));
        powerUpList.Add(noIce = new PowerUp(18, 50, "Immuned to ice\nzombies", powerUpSprites[18]));
        powerUpList.Add(freeze = new PowerUp(19, 5000, "Food freezes zombies", powerUpSprites[19]));
    }
}
