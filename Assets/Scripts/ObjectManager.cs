using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    GameObject counterWall, grillWall, fryerWall, sodaWall;
    GameObject rightFryer, leftFryer, sodaFountain1, sodaFountain2, sodaFountain3;
    GameObject phone, led;
    GameObject fries, burger, drink;
    GameObject meat, topBun, bottomBun, uncookedFries, friesBasket, cup, lid;
    GameObject foodTruck;
    GameObject car;
    GameObject zombie;

    void Awake()
    {
        counterWall = GameObject.Find("Counter Wall");
        grillWall = GameObject.Find("Grill Wall");
        fryerWall = GameObject.Find("Fryer Wall");
        sodaWall = GameObject.Find("Soda Wall");
        rightFryer = GameObject.Find("Fryer Basket Right");
        leftFryer = GameObject.Find("Fryer Basket Left");
        sodaFountain1 = GameObject.Find("SodaFromMachine1");
        sodaFountain2 = GameObject.Find("SodaFromMachine2");
        sodaFountain3 = GameObject.Find("SodaFromMachine3");
        phone = GameObject.Find("Phone");
        led = GameObject.Find("LED");
        fries = GameObject.Find("Fries");
        burger = GameObject.Find("Burger");
        drink = GameObject.Find("Drink");
        foodTruck = GameObject.Find("Food Truck");
        meat = GameObject.Find("Meat");
        topBun = GameObject.Find("Top_Bun");
        bottomBun = GameObject.Find("Bottom_Bun");
        uncookedFries = GameObject.Find("Fries_No_Basket");
        friesBasket = GameObject.Find("Basket");
        cup = GameObject.Find("Empty_Cup");
        lid = GameObject.Find("Lid");
        car = GameObject.Find("Car");
        zombie = GameObject.Find("Zombie");
    }

    public GameObject CounterWall() { return counterWall; }

    public GameObject GrillWall() { return grillWall; }

    public GameObject FryerWall() { return fryerWall; }

    public GameObject SodaWall() { return sodaWall; }

    public GameObject LeftFryer() { return leftFryer; }

    public GameObject RightFryer() { return rightFryer; }

    public GameObject SodaMachine1() { return sodaFountain1; }

    public GameObject SodaMachine2() { return sodaFountain2; }

    public GameObject SodaMachine3() { return sodaFountain3; }

    public GameObject Phone() { return phone; }

	public GameObject LED() { return led; }

    public GameObject Fries() { return fries; }

    public GameObject Burger() { return burger; }

    public GameObject Drink() { return drink; }

    public GameObject FoodTruck() { return foodTruck; }

    public GameObject TopBun() { return topBun; }

    public GameObject Meat() { return meat; }

    public GameObject BottomBun() { return bottomBun; }

    public GameObject UncookedFries() { return uncookedFries; }

    public GameObject FriesBasket() { return friesBasket; }

    public GameObject Cup() { return cup; }

    public GameObject Lid() { return lid; }

    public GameObject Car() { return car; }

	public GameObject Zombie() { return zombie; }
}
