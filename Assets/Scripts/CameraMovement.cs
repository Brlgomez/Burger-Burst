﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    static int updateInterval = 10;

    static int maxSpeed = 15;
    static int accelerating = 15;
    Transform menu, gameplay, pause, towards, gameOver, grill, fryer, soda;
    Transform powerUp, customize, setting, store, graphics, theme, flooring, wallpaper, detail;
    Transform deviceFront, deviceBack, deviceTowards;
    float speed;
    bool moveToPosition;
    bool moveDevice;
    bool unpause;
    string gamePlayCommand;

    void Update()
    {
        if (moveToPosition)
        {
            if (speed < maxSpeed)
            {
                speed += Time.unscaledDeltaTime * accelerating;
            }
            MoveCamera();
            if (moveDevice)
            {
                MoveDevice();
            }
            if (Time.frameCount % updateInterval == 0)
            {
                if (Vector3.Distance(transform.position, towards.transform.position) < 0.001f)
                {
                    Finished();
                }
            }
        }
    }

    void MoveCamera()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            towards.transform.position,
            Time.unscaledDeltaTime * speed
        );
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            towards.transform.rotation,
            Time.unscaledDeltaTime * speed
        );
    }

    void MoveDevice()
    {
        transform.GetChild(1).localPosition = Vector3.Lerp(
            transform.GetChild(1).localPosition,
            deviceTowards.transform.localPosition,
            Time.unscaledDeltaTime * speed
        );
        transform.GetChild(1).localRotation = Quaternion.Lerp(
            transform.GetChild(1).localRotation,
            deviceTowards.transform.localRotation,
            Time.unscaledDeltaTime * speed
        );
    }

    void Finished()
    {
        transform.position = towards.transform.position;
        transform.rotation = towards.transform.rotation;
        if (moveDevice)
        {
            transform.GetChild(1).localPosition = deviceTowards.localPosition;
            transform.GetChild(1).localRotation = deviceTowards.localRotation;
        }
        if (unpause)
        {
            GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
            GetComponent<GameplayMenu>().UnPauseGame();
        }
        if (towards == gameplay)
        {
            switch (gamePlayCommand)
            {
                case "Restart":
                    GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                    GetComponent<GameplayMenu>().UnPauseGame();
                    GetComponent<DropMoreProducts>().DropItems();
                    GetComponent<DropMoreProducts>().DropMadeProducts();
                    break;
                case "Start":
                    GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                    gameObject.AddComponent<GrabAndThrowObject>();
                    GetComponent<DropMoreProducts>().DropItems();
                    GetComponent<DropMoreProducts>().DropMadeProducts();
                    break;
                case "Unpause":
                    GetComponent<ScreenTextManagment>().ChangeToGamePlayText();
                    GetComponent<GameplayMenu>().UnPauseGame();
                    break;
            }
            GetComponent<ScreenTextManagment>().ChangeToFrontArea();
        }
        else if (towards == menu)
        {
            GetComponent<ScreenTextManagment>().ChangeToMenuText();
            if (GetComponent<MainMenu>() == null)
            {
                gameObject.AddComponent<MainMenu>();
            }
        }
        else if (towards == pause)
        {
            GetComponent<ScreenTextManagment>().ChangeToPauseText();
        }
        else if (towards == customize)
        {
            GetComponent<ScreenTextManagment>().ChangeToCustomizeScreen();
        }
        else if (towards == setting)
        {
            GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
        }
        else if (towards == store)
        {
            GetComponent<ScreenTextManagment>().ChangeToStoreScreen();
        }
        else if (towards == gameOver)
        {
            GetComponent<GrabAndThrowObject>().ResetEverything();
            GetComponent<GameplayMenu>().UnPauseGame();
            GetComponent<ScreenTextManagment>().ChangeToGameOverText();
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
        else if (towards == powerUp)
        {
            if (GetComponent<ScreenTextManagment>().GetLastMenu() == Menus.Menu.ConfirmationPowerUp)
            {
                GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationPowerUp);
            }
            else
            {
                GetComponent<ScreenTextManagment>().ChangeToUpgradeText();
            }
        }
        else if (towards == theme)
        {
            GetComponent<ScreenTextManagment>().ChangeToThemeScreen();
        }
        else if (towards == graphics)
        {
            if (GetComponent<ScreenTextManagment>().GetLastMenu() == Menus.Menu.ConfirmationGraphics)
            {
                GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationGraphics);
            }
            else
            {
                GetComponent<ScreenTextManagment>().ChangeToGraphicsScreen();
            }
        }
        else if (towards == flooring)
        {
            if (GetComponent<ScreenTextManagment>().GetLastMenu() == Menus.Menu.ConfirmationFlooring)
            {
                GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationFlooring);
            }
            else
            {
                GetComponent<ScreenTextManagment>().ChangeToFlooringScreen();
            }
        }
        else if (towards == wallpaper)
        {
            if (GetComponent<ScreenTextManagment>().GetLastMenu() == Menus.Menu.ConfirmationWalls)
            {
                GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationWalls);
            }
            else
            {
                GetComponent<ScreenTextManagment>().ChangeToWallpaperScreen();
            }
        }
        else if (towards == detail)
        {
            if (GetComponent<ScreenTextManagment>().GetLastMenu() == Menus.Menu.ConfirmationDetail)
            {
                GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationDetail);
            }
            else
            {
                GetComponent<ScreenTextManagment>().ChangeToDetailScreen();
            }
        }
        Destroy(GetComponent<CameraMovement>());
    }

    public void MoveToMenu(bool moveDev)
    {
        menu = GetComponent<PositionManager>().MenuPosition();
        deviceFront = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = menu;
        deviceTowards = deviceFront;
        moveToPosition = true;
        moveDevice = moveDev;
    }

    public void MoveToPowerUp()
    {
        powerUp = GetComponent<PositionManager>().PowerUpsPosition();
        towards = powerUp;
        moveToPosition = true;
    }

    public void MoveToCustomize()
    {
        customize = GetComponent<PositionManager>().CustomizePosition();
        towards = customize;
        moveToPosition = true;
    }

    public void MoveToTheme()
    {
        theme = GetComponent<PositionManager>().CustomizePosition();
        towards = theme;
        moveToPosition = true;
    }

    public void MoveToGraphics()
    {
        graphics = GetComponent<PositionManager>().GraphicsPosition();
        towards = graphics;
        moveToPosition = true;
    }

    public void MoveToFlooring()
    {
        flooring = GetComponent<PositionManager>().ThemePosition();
        towards = flooring;
        moveToPosition = true;
    }

    public void MoveToWallpaper()
    {
        wallpaper = GetComponent<PositionManager>().ThemePosition();
        towards = wallpaper;
        moveToPosition = true;
    }

    public void MoveToDetail()
    {
        detail = GetComponent<PositionManager>().ThemePosition();
        towards = detail;
        moveToPosition = true;
    }

    public void MoveToStore()
    {
        store = GetComponent<PositionManager>().StorePosition();
        towards = store;
        moveToPosition = true;
    }

    public void MoveToSetting()
    {
        setting = GetComponent<PositionManager>().SettingPosition();
        towards = setting;
        moveToPosition = true;
    }

    public void MoveToGameplay(string c)
    {
        gameplay = GetComponent<PositionManager>().GameplayPosition();
        deviceBack = GetComponent<PositionManager>().DeviceBackPosition();
        towards = gameplay;
        deviceTowards = deviceBack;
        moveToPosition = true;
        moveDevice = true;
        gamePlayCommand = c;
        switch (gamePlayCommand)
        {
            case "Restart":
                GetComponent<GameplayMenu>().UnPauseGame();
                GetComponent<DropMoreProducts>().DropItems();
                break;
            case "Start":
                GetComponent<DropMoreProducts>().DropItems();
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

    public void MoveToUnpause(string area)
    {
        unpause = true;
        switch (area)
        {
            case "Front":
                gameplay = GetComponent<PositionManager>().GameplayPosition();
                towards = gameplay;
                break;
            case "Grill":
                grill = GetComponent<PositionManager>().GrillPosition();
                towards = grill;
                break;
            case "Fryer":
                fryer = GetComponent<PositionManager>().FryerPosition();
                towards = fryer;
                break;
            case "Soda Machine":
                soda = GetComponent<PositionManager>().SodaPosition();
                towards = soda;
                break;
        }
        deviceBack = GetComponent<PositionManager>().DeviceBackPosition();
        deviceTowards = deviceBack;
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
        grill = GetComponent<PositionManager>().GrillPosition();
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
