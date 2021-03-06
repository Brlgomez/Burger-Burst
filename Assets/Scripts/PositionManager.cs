﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    GameObject menu, gameplay, pause, gameOver, grill, fryer, soda, powerUp;
    GameObject customize, setting, store, graphics, theme, floor, wall, stuff, online, credits;
    GameObject deviceFront, deviceBack, deviceMiddle, deviceBackTablet, deviceBackX, deviceBackShort;
    GameObject madeFriesPos, burgerPos, drinkPos;
    GameObject meatPos, topBunPos, bottomBunPos;
    GameObject friesPos, basketPos, cupPos, lidPos;
    GameObject counterRange, grillRange, friesRange, drinkRange;

    void Awake()
    {
        menu = GameObject.Find("Menu Camera Position");
        powerUp = GameObject.Find("Power Ups Camera Position");
        customize = GameObject.Find("Customize Camera Position");
        setting = GameObject.Find("Setting Camera Position");
        store = GameObject.Find("Store Camera Position");
        graphics = GameObject.Find("Graphics Camera Position");
        theme = GameObject.Find("Theme Camera Position");
        floor = GameObject.Find("Floor Camera Position");
        wall = GameObject.Find("Wall Camera Position");
        stuff = GameObject.Find("Stuff Camera Position");
        online = GameObject.Find("Online Camera Position");
        credits = GameObject.Find("Credits Camera Position");
        gameplay = GameObject.Find("Gameplay Camera Position");
        pause = GameObject.Find("Pause Camera Position");
        grill = GameObject.Find("Grill Position");
        fryer = GameObject.Find("Fryer Position");
        soda = GameObject.Find("Soda Machine Position");
        gameOver = GameObject.Find("Game Over Camera Position");
        deviceFront = GameObject.Find("Device Front Position");
        deviceBackTablet = GameObject.Find("Device Back Tablet Position");
        deviceBack = GameObject.Find("Device Back Position");
        deviceBackX = GameObject.Find("Device Back X Position");
        deviceBackShort = GameObject.Find("Device Back Short Position");
        deviceMiddle = GameObject.Find("Device Middle Position");
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

    public Transform CustomizePosition() { return customize.transform; }

    public Transform SettingPosition() { return setting.transform; }

    public Transform StorePosition() { return store.transform; }

    public Transform GraphicsPosition() { return graphics.transform; }

    public Transform ThemePosition() { return theme.transform; }

    public Transform FloorPosition() { return floor.transform; }

    public Transform WallPosition() { return wall.transform; }

    public Transform StuffPosition() { return stuff.transform; }

    public Transform OnlinePosition() { return online.transform; }

    public Transform CreditsPosition() { return credits.transform; }

    public Transform PausePosition() { return pause.transform; }

    public Transform GrillPosition() { return grill.transform; }

    public Transform FryerPosition() { return fryer.transform; }

    public Transform SodaPosition() { return soda.transform; }

    public Transform GameOverPosition() { return gameOver.transform; }

    public Transform MadeFriesPosition() { return madeFriesPos.transform; }

    public Transform BurgerPosition() { return burgerPos.transform; }

    public Transform DrinkPosition() { return drinkPos.transform; }

    public Transform DeviceFrontPosition() { return deviceFront.transform; }

    public Transform DeviceBackPosition()
    {
        if (((float)Screen.width / Screen.height) < 1.4f)
        {
            return deviceBackTablet.transform;
        }
        if (((float)Screen.width / Screen.height) < 1.525f)
        {
            return deviceBackShort.transform;
        }
        if (((float)Screen.width / Screen.height) < 1.8f)
        {
            return deviceBack.transform;
        }
        if (((float)Screen.width / Screen.height) >= 2)
        {
            return deviceBackX.transform;
        }
        return deviceBack.transform;
    }

    public Transform DeviceMiddlePosition() { return deviceMiddle.transform; }

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
