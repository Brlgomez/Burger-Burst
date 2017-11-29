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

        public powerUpStruct(int powerUpNum, int cost, bool unlock)
        {
            powerUpNumber = powerUpNum;
            price = cost;
            unlocked = unlock;
        }
    }

    public powerUpStruct nothing = new powerUpStruct(-1, 0, true);
    public powerUpStruct throwFurther = new powerUpStruct(0, 50, true);
    public powerUpStruct quickerCooking = new powerUpStruct(1, 50, true);
    public powerUpStruct makeMoreFood = new powerUpStruct(2, 50, true);
    public powerUpStruct defenseIncrease = new powerUpStruct(3, 50, true);
    public powerUpStruct moreHealth = new powerUpStruct(4, 50, true);
    public powerUpStruct largerFood = new powerUpStruct(5, 50, true);
    public powerUpStruct regenHealth = new powerUpStruct(6, 50, true);
    public powerUpStruct regenBurgers = new powerUpStruct(7, 50, true);
    public powerUpStruct regenFries = new powerUpStruct(8, 50, true);
    public powerUpStruct regenDrinks = new powerUpStruct(9, 50, true);
    public powerUpStruct doublePoints = new powerUpStruct(10, 50, true);
    public powerUpStruct doubleCoins = new powerUpStruct(11, 50, true);
    public powerUpStruct throwMultiple = new powerUpStruct(12, 50, true);
    public powerUpStruct magnet = new powerUpStruct(13, 50, true);
    public powerUpStruct noWind = new powerUpStruct(14, 50, true);
    public powerUpStruct noInstantKill = new powerUpStruct(15, 50, true);
    public powerUpStruct luck = new powerUpStruct(16, 50, true);
    public powerUpStruct noPoison = new powerUpStruct(17, 50, true);
    public powerUpStruct noIce = new powerUpStruct(18, 50, true);
    public powerUpStruct freeze = new powerUpStruct(19, 50, true);
}
