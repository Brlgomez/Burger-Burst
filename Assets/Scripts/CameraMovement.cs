using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Transform menu, gameplay, pause, towards, gameOver, grill, fryer, soda;
    Transform deviceFront, deviceBack, deviceTowards;
    float speed;
    int maxSpeed = 15;
    int accelerating = 15;
    bool moveToPosition;
    bool moveDevice;
    string command;

    void Update()
    {
        if (moveToPosition)
        {
            if (speed < maxSpeed)
            {
                speed += Time.unscaledDeltaTime * accelerating;
            }
            transform.position = Vector3.Lerp(transform.position, towards.transform.position, Time.unscaledDeltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, towards.transform.rotation, Time.unscaledDeltaTime * speed);
            if (moveDevice)
            {
                transform.GetChild(1).localPosition = Vector3.Lerp(transform.GetChild(1).localPosition, deviceTowards.transform.localPosition, Time.unscaledDeltaTime * speed);
                transform.GetChild(1).localRotation = Quaternion.Lerp(transform.GetChild(1).localRotation, deviceTowards.transform.localRotation, Time.unscaledDeltaTime * speed);
            }
            if (Vector3.Distance(transform.position, towards.transform.position) < 0.001f)
            {
                transform.position = towards.transform.position;
                transform.rotation = towards.transform.rotation;
                if (moveDevice)
                {
                    transform.GetChild(1).localPosition = deviceTowards.localPosition;
                    transform.GetChild(1).localRotation = deviceTowards.localRotation;
                }
                if (towards == gameplay)
                {
                    switch (command)
                    {
                        case "Restart":
                            GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                            GetComponent<GrabAndThrowObject>().UnPauseGame();
                            Camera.main.GetComponent<DropMoreProducts>().DropGrillItems();
                            break;
                        case "Start":
                            GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                            gameObject.AddComponent<GrabAndThrowObject>();
                            Camera.main.GetComponent<DropMoreProducts>().DropGrillItems();
                            break;
                        case "Unpause":
                            GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                            GetComponent<GrabAndThrowObject>().UnPauseGame();
                            break;
                    }
                    GetComponent<ScreenTextManagment>().ChangeToFrontArea();
                }
                else if (towards == menu)
                {
                    GetComponent<ScreenTextManagment>().ChangeToMenuText();
                    gameObject.AddComponent<MainMenu>();
                }
                else if (towards == pause)
                {
                    GetComponent<ScreenTextManagment>().ChangeToPauseText();
                }
                else if (towards == gameOver)
                {
                    GetComponent<GrabAndThrowObject>().DeleteObjects();
                    GetComponent<GrabAndThrowObject>().UnPauseGame();
                    GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                }
                else if (towards == grill)
                {
                    GetComponent<ScreenTextManagment>().ChangeToGrillArea();
                }
                else if (towards == fryer)
                {
                    GetComponent<ScreenTextManagment>().ChangeToFryerArea();
                }
                else if (towards == soda)
                {
                    GetComponent<ScreenTextManagment>().ChangeToSodaMachineArea();
                }
                Destroy(GetComponent<CameraMovement>());
            }
        }
    }

    public void MoveToMenu()
    {
        menu = GetComponent<PositionManager>().MenuPosition();
        deviceFront = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = menu;
        deviceTowards = deviceFront;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToGameplay(string c)
    {
        gameplay = GetComponent<PositionManager>().GameplayPosition();
        deviceBack = GetComponent<PositionManager>().DeviceBackPosition();
        towards = gameplay;
        deviceTowards = deviceBack;
        moveToPosition = true;
        moveDevice = true;
        command = c;
		switch (command)
		{
			case "Restart":
				Camera.main.GetComponent<DropMoreProducts>().DropGrillItems();
				break;
			case "Start":
				Camera.main.GetComponent<DropMoreProducts>().DropGrillItems();
				break;
		}
    }

    public void MoveToPause()
    {
        pause = GetComponent<PositionManager>().PausePosition();
        deviceFront = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = pause;
        deviceTowards = deviceFront;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToGameOver()
    {
        gameOver = GetComponent<PositionManager>().GameOverPosition();
        deviceFront = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = gameOver;
        deviceTowards = deviceFront;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToGrill()
    {
        grill = Camera.main.GetComponent<PositionManager>().GrillPosition();
        towards = grill;
        moveToPosition = true;
    }

    public void MoveToFryer()
    {
        fryer = GetComponent<PositionManager>().FryerPosition();
        towards = fryer;
        moveToPosition = true;
    }

    public void MoveToSodaMachine()
    {
        soda = GetComponent<PositionManager>().SodaPosition();
        towards = soda;
        moveToPosition = true;
    }
}
