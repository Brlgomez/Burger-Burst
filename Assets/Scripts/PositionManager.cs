﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour 
{

	GameObject menu, gameplay, pause, gameOver, grill, fryer, soda;
	
    void Start () 
    {
		menu = GameObject.Find("Menu Camera Position");
		gameplay = GameObject.Find("Gameplay Camera Position");
		pause = GameObject.Find("Pause Camera Position");
		grill = GameObject.Find("Grill Position");
		fryer = GameObject.Find("Fryer Position");
		soda = GameObject.Find("Soda Machine Position");
		gameOver = GameObject.Find("Game Over Camera Position");
	}

    public Transform MenuPosition()
	{
        return menu.transform;
	}

    public Transform GameplayPosition () 
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
}
