using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    GameObject menu, gameplay, pause, gameOver, grill, fryer, soda, powerUp;
    GameObject deviceFront, deviceBack;
    GameObject madeFriesPos, burgerPos, drinkPos;
    GameObject meatPos, topBunPos, bottomBunPos;
    GameObject friesPos, basketPos;
    GameObject cupPos, lidPos;
    GameObject counterRange, grillRange, friesRange, drinkRange;

    void Awake()
    {
        menu = GameObject.Find("Menu Camera Position");
        powerUp = GameObject.Find("Power Ups Camera Position");
        gameplay = GameObject.Find("Gameplay Camera Position");
        pause = GameObject.Find("Pause Camera Position");
        grill = GameObject.Find("Grill Position");
        fryer = GameObject.Find("Fryer Position");
        soda = GameObject.Find("Soda Machine Position");
        gameOver = GameObject.Find("Game Over Camera Position");
        deviceFront = GameObject.Find("Device Front Position");
        deviceBack = GameObject.Find("Device Back Position");
        madeFriesPos = GameObject.Find("Made Fries Position");
        burgerPos = GameObject.Find("Burger Position");
        drinkPos = GameObject.Find("Drink Position");
        meatPos = GameObject.Find("Meat Position");
        topBunPos = GameObject.Find("Top Bun Position");
        bottomBunPos = GameObject.Find("Bottom Bun Position");
        friesPos = GameObject.Find("Fries Position");
        basketPos = GameObject.Find("Basket Position");
        cupPos = GameObject.Find("Soda Position");
        lidPos = GameObject.Find("Lid Position");
        counterRange = GameObject.Find("Counter Range");
        grillRange = GameObject.Find("Grill Range");
        friesRange = GameObject.Find("Fries Range");
        drinkRange = GameObject.Find("Drink Range");
    }

    public Transform MenuPosition() { return menu.transform; }

    public Transform PowerUpsPosition() { return powerUp.transform; }

    public Transform GameplayPosition() { return gameplay.transform; }

    public Transform PausePosition() { return pause.transform; }

    public Transform GrillPosition() { return grill.transform; }

    public Transform FryerPosition() { return fryer.transform; }

    public Transform SodaPosition() { return soda.transform; }

    public Transform GameOverPosition() { return gameOver.transform; }

    public Transform MadeFriesPosition() { return madeFriesPos.transform; }

    public Transform BurgerPosition() { return burgerPos.transform; }

    public Transform DrinkPosition() { return drinkPos.transform; }

    public Transform DeviceFrontPosition() { return deviceFront.transform; }

    public Transform DeviceBackPosition() { return deviceBack.transform; }

    public Transform MeatSpawnPosition() { return meatPos.transform; }

    public Transform TopBunSpawnPosition() { return topBunPos.transform; }

    public Transform BottomBunSpawnPosition() { return bottomBunPos.transform; }

    public Transform FriesPosition() { return friesPos.transform; }

    public Transform BasketPosition() { return basketPos.transform; }

    public Transform CupPosition() { return cupPos.transform; }

    public Transform LidPosition() { return lidPos.transform; }

    public Transform CounterRange() { return counterRange.transform; }

    public Transform GrillRange() { return grillRange.transform; }

    public Transform FriesRange() { return friesRange.transform; }

    public Transform DrinkRange() { return drinkRange.transform; }
}
