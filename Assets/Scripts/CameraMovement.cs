using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    static int updateInterval = 10;

    static int maxSpeed = 15;
    static int accelerating = 15;
    Transform title, menu, gameplay, pause, towards, gameOver, grill, fryer, soda;
    Transform powerUp, customize, setting, store, graphics, theme, flooring, wallpaper, detail;
    Transform deviceTowards;
    float speed;
    bool moveToPosition;
    bool moveDevice;
    bool unpause;
    bool moveArrows;
    string gamePlayCommand;
    GameObject phone;

    void Start()
    {
        if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Gameplay)
        {
            moveArrows = true;
            phone = GetComponent<ObjectManager>().Phone();
        }
    }

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
                    if (moveDevice && Vector3.Distance(transform.GetChild(1).localPosition, deviceTowards.transform.localPosition) < 0.001f)
                    {
                        Finished();
                    }
                    else if (!moveDevice)
                    {
                        Finished();
                    }
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
        if (moveArrows)
        {
            MoveArrows();
        }
    }

    void MoveArrows()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (towards == grill)
            {
                phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles = Vector3.Lerp(
                   phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles,
                   new Vector3(0, 0, phone.transform.GetChild(i).GetChild(3).GetComponent<ArrowDirection>().fromBurgerDirection),
                   Time.unscaledDeltaTime * speed
               );
            }
            else if (towards == fryer)
            {
                phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles = Vector3.Lerp(
                    phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles,
                    new Vector3(0, 0, phone.transform.GetChild(i).GetChild(3).GetComponent<ArrowDirection>().fromFriesDirection),
                    Time.unscaledDeltaTime * speed
                );
            }
            else if (towards == soda)
            {
                phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles = Vector3.Lerp(
                    phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles,
                    new Vector3(0, 0, phone.transform.GetChild(i).GetChild(3).GetComponent<ArrowDirection>().fromDrinkDirection),
                    Time.unscaledDeltaTime * speed
                );
            }
            else
            {
                phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles = Vector3.Lerp(
                    phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles,
                    new Vector3(0, 0, phone.transform.GetChild(i).GetChild(3).GetComponent<ArrowDirection>().fromCounterDirection),
                    Time.unscaledDeltaTime * speed
                );
            }
        }
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
        else if (towards == title)
        {
            GetComponent<ScreenTextManagment>().ChangeToTitleText();
        }
        else if (towards == gameOver)
        {
            GetComponent<GrabAndThrowObject>().DeleteEverything();
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
        Destroy(GetComponent<CameraMovement>());
    }

    public void MoveToTitle()
    {
        title = GetComponent<PositionManager>().MenuPosition();
        deviceTowards = GetComponent<PositionManager>().DeviceMiddlePosition();
        towards = title;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToMenu(bool moveDev)
    {
        menu = GetComponent<PositionManager>().MenuPosition();
        deviceTowards = GetComponent<PositionManager>().DeviceMiddlePosition();
        towards = menu;
        moveToPosition = true;
        moveDevice = moveDev;
    }

    public void MoveToPowerUp()
    {
        powerUp = GetComponent<PositionManager>().PowerUpsPosition();
        deviceTowards = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = powerUp;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToCustomize()
    {
        customize = GetComponent<PositionManager>().CustomizePosition();
        deviceTowards = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = customize;
        moveToPosition = true;
        moveDevice = true;
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
        deviceTowards = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = store;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToSetting()
    {
        setting = GetComponent<PositionManager>().SettingPosition();
        deviceTowards = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = setting;
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToGameplay(string c)
    {
        gameplay = GetComponent<PositionManager>().GameplayPosition();
        deviceTowards = GetComponent<PositionManager>().DeviceBackPosition();
        towards = gameplay;
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
        deviceTowards = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = pause;
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
        deviceTowards = GetComponent<PositionManager>().DeviceBackPosition();
        moveToPosition = true;
        moveDevice = true;
    }

    public void MoveToGameOver()
    {
        gameOver = GetComponent<PositionManager>().GameOverPosition();
        deviceTowards = GetComponent<PositionManager>().DeviceFrontPosition();
        towards = gameOver;
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
