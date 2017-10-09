using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{

    GameObject menu, gameplay, pause, gameOver, grill, fryer, soda;
    GameObject deviceFront, deviceBack;
    GameObject meatPos, topBunPos, bottomBunPos;
    GameObject friesPos, basketPos;
    GameObject grillRange, friesRange, drinkRange;

    void Awake()
    {
        menu = GameObject.Find("Menu Camera Position");
        gameplay = GameObject.Find("Gameplay Camera Position");
        pause = GameObject.Find("Pause Camera Position");
        grill = GameObject.Find("Grill Position");
        fryer = GameObject.Find("Fryer Position");
        soda = GameObject.Find("Soda Machine Position");
        gameOver = GameObject.Find("Game Over Camera Position");
        deviceFront = GameObject.Find("Device Front Position");
        deviceBack = GameObject.Find("Device Back Position");
        meatPos = GameObject.Find("Meat Position");
        topBunPos = GameObject.Find("Top Bun Position");
        bottomBunPos = GameObject.Find("Bottom Bun Position");
        friesPos = GameObject.Find("Fries Position");
        basketPos = GameObject.Find("Basket Position");
        grillRange = GameObject.Find("Grill Range");
        friesRange = GameObject.Find("Fries Range");
        drinkRange = GameObject.Find("Drink Range");
    }

    public Transform MenuPosition()
    {
        return menu.transform;
    }

    public Transform GameplayPosition()
    {
        return gameplay.transform;
    }

    public Transform PausePosition()
    {
        return pause.transform;
    }

    public Transform GrillPosition()
    {
        return grill.transform;
    }

    public Transform FryerPosition()
    {
        return fryer.transform;
    }

    public Transform SodaPosition()
    {
        return soda.transform;
    }

    public Transform GameOverPosition()
    {
        return gameOver.transform;
    }

    public Transform DeviceFrontPosition()
    {
        return deviceFront.transform;
    }

    public Transform DeviceBackPosition()
    {
        return deviceBack.transform;
    }

    public Transform MeatSpawnPosition()
    {
        return meatPos.transform;
    }

    public Transform TopBunSpawnPosition()
    {
        return topBunPos.transform;
    }

    public Transform BottomBunSpawnPosition()
    {
        return bottomBunPos.transform;
    }

    public Transform FriesPosition()
    {
        return friesPos.transform;
    }

    public Transform BasketPosition()
    {
        return basketPos.transform;
    }

	public Transform GrillRange()
	{
        return grillRange.transform;
	}

	public Transform FriesRange()
	{
        return friesRange.transform;
	}

	public Transform DrinkRange()
	{
		return drinkRange.transform;
	}
}
