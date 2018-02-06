using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    static int updateInterval = 10;
    static int maxSpeed = 15;
    static int accelerating = 15;
    float speed;
    Transform towards, deviceTowards;
    bool moveToPosition, moveDevice, moveArrows;
    bool unpause, gameplay, title, gameOver, grill, fryer, soda;
    string gamePlayCommand;
    GameObject phone;
    bool gotCalculation;
    float distance;

    void Start()
    {
        if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Gameplay)
        {
            moveArrows = true;
            phone = GetComponent<ObjectManager>().Phone();
        }
        GetComponent<ObjectManager>().PostProcess().GetComponent<PostProcessing>().PlayMotionBlur();
    }

    void Update()
    {
        if (moveToPosition)
        {
            if (!gotCalculation)
            {
                distance = Vector3.Distance(towards.position, transform.position);
                if (distance / 4 > 0.6f)
                {
                    GetComponent<SoundAndMusicManager>().PlayMovementWoosh(gameObject, distance / 4);
                }
                gotCalculation = true;
            }
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
                    if (Quaternion.Angle(transform.rotation, towards.transform.rotation) < 0.1f)
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
            if (grill)
            {
                phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles = Vector3.Lerp(
                   phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles,
                   new Vector3(0, 0, phone.transform.GetChild(i).GetChild(3).GetComponent<ArrowDirection>().fromBurgerDirection),
                   Time.unscaledDeltaTime * speed
               );
            }
            else if (fryer)
            {
                phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles = Vector3.Lerp(
                    phone.transform.GetChild(i).GetChild(3).transform.localEulerAngles,
                    new Vector3(0, 0, phone.transform.GetChild(i).GetChild(3).GetComponent<ArrowDirection>().fromFriesDirection),
                    Time.unscaledDeltaTime * speed
                );
            }
            else if (soda)
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
        if (GetComponent<GrabAndThrowObject>() != null)
        {
            GetComponent<GrabAndThrowObject>().SetRotation(towards.transform.rotation);
        }
        if (GetComponent<MainMenu>() != null)
        {
            GetComponent<MainMenu>().SetRotation(towards.transform.rotation);
        }
        if (moveDevice)
        {
            transform.GetChild(1).localPosition = deviceTowards.localPosition;
            transform.GetChild(1).localRotation = deviceTowards.localRotation;
        }
        if (unpause)
        {
            GetComponent<GameplayMenu>().UnPauseGame();
            GetComponent<ScreenTextManagment>().PingPongHeart();
        }
        if (gameplay)
        {
            GetComponent<TutorialManager>().ActivateCounterThrowing();
            switch (gamePlayCommand)
            {
                case "Restart":
                    GetComponent<GameplayMenu>().UnPauseGame();
                    GetComponent<DropMoreProducts>().DropMadeProducts();
                    break;
                case "Start":
                    gameObject.AddComponent<GrabAndThrowObject>();
                    GetComponent<DropMoreProducts>().DropMadeProducts();
                    break;
                case "Unpause":
                    GetComponent<GameplayMenu>().UnPauseGame();
                    GetComponent<ScreenTextManagment>().PingPongHeart();
                    break;
            }
        }
        else if (title)
        {
            GetComponent<ScreenTextManagment>().ChangeToTitleText();
            GetComponent<MainMenu>().LoadingAnimation();
        }
        else if (gameOver)
        {
            GetComponent<GrabAndThrowObject>().DeleteEverything();
            GetComponent<GameplayMenu>().UnPauseGame();
        }
        GetComponent<ObjectManager>().PostProcess().GetComponent<PostProcessing>().StopMotionBlur();
        Destroy(GetComponent<CameraMovement>());
    }

    public void MoveToTitle()
    {
        title = true;
        SetCameraAndDevicePosition(GetComponent<PositionManager>().MenuPosition(), GetComponent<PositionManager>().DeviceMiddlePosition());
    }

    public void MoveToMenu(bool moveDev)
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().MenuPosition(), GetComponent<PositionManager>().DeviceMiddlePosition());
        moveDevice = moveDev;
    }

    public void MoveToPowerUp()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().PowerUpsPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToCustomize()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().CustomizePosition(), GetComponent<PositionManager>().DeviceMiddlePosition());
    }

    public void MoveToTheme()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().CustomizePosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToGraphics()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().GraphicsPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToFlooring()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().FloorPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToWallpaper()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().WallPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToDetail()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().ThemePosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToStore()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().StorePosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToStuff()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().StuffPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToSettings()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().SettingPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToOnline()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().OnlinePosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToCredits()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().CreditsPosition(), GetComponent<PositionManager>().DeviceMiddlePosition());
    }

    public void MoveToGameplay(string c)
    {
        gameplay = true;
        SetCameraAndDevicePosition(GetComponent<PositionManager>().GameplayPosition(), GetComponent<PositionManager>().DeviceBackPosition());
        gamePlayCommand = c;
    }

    public void MoveToPause()
    {
        SetCameraAndDevicePosition(GetComponent<PositionManager>().PausePosition(), GetComponent<PositionManager>().DeviceMiddlePosition());
    }

    public void MoveToUnpause(string area)
    {
        unpause = true;
        switch (area)
        {
            case "Front":
                SetCameraAndDevicePosition(GetComponent<PositionManager>().GameplayPosition(), GetComponent<PositionManager>().DeviceBackPosition());
                break;
            case "Grill":
                grill = true;
                SetCameraAndDevicePosition(GetComponent<PositionManager>().GrillPosition(), GetComponent<PositionManager>().DeviceBackPosition());
                break;
            case "Fryer":
                fryer = true;
                SetCameraAndDevicePosition(GetComponent<PositionManager>().FryerPosition(), GetComponent<PositionManager>().DeviceBackPosition());
                break;
            case "Soda Machine":
                soda = true;
                SetCameraAndDevicePosition(GetComponent<PositionManager>().SodaPosition(), GetComponent<PositionManager>().DeviceBackPosition());
                break;
        }
    }

    public void MoveToGameOver()
    {
        gameOver = true;
        SetCameraAndDevicePosition(GetComponent<PositionManager>().GameOverPosition(), GetComponent<PositionManager>().DeviceFrontPosition());
    }

    public void MoveToGrill()
    {
        grill = true;
        SetCameraPosition(GetComponent<PositionManager>().GrillPosition());
        if (!GetComponent<DropMoreProducts>().GetDroppedBurgers())
        {
            GetComponent<DropMoreProducts>().SetDroppedBurgers();
            GetComponent<DropMoreProducts>().DropBurgerProducts();
        }
    }

    public void MoveToFryer()
    {
        fryer = true;
        SetCameraPosition(GetComponent<PositionManager>().FryerPosition());
        if (!GetComponent<DropMoreProducts>().GetDroppedFries())
        {
            GetComponent<DropMoreProducts>().SetDroppedFries();
            GetComponent<DropMoreProducts>().DropFryProducts();
        }
    }

    public void MoveToSodaMachine()
    {
        soda = true;
        SetCameraPosition(GetComponent<PositionManager>().SodaPosition());
        if (!GetComponent<DropMoreProducts>().GetDroppedDrinks())
        {
            GetComponent<DropMoreProducts>().SetDroppedDrinks();
            GetComponent<DropMoreProducts>().DropDrinkProducts();
        }
    }

    void SetCameraPosition(Transform movingTowards)
    {
        towards = movingTowards;
        moveToPosition = true;
    }

    void SetCameraAndDevicePosition(Transform movingTowards, Transform deviceMovingTowards)
    {
        towards = movingTowards;
        deviceTowards = deviceMovingTowards;
        moveToPosition = true;
        moveDevice = true;
    }
}
