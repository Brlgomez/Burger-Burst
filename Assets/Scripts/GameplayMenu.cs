using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenu : MonoBehaviour
{
    bool paused;
    Transform initialPosition;

    void Start()
    {
        initialPosition = GetComponent<PositionManager>().GameplayPosition();
    }

    public void PhoneInterface(GameObject obj)
    {
        if (!paused && !GetComponent<Gameplay>().IsGameOver())
        {
            GameplayPhoneInterface(obj);
        }
        else if (GetComponent<Gameplay>().IsGameOver() && GetComponent<CameraMovement>() == null &&
                 GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.GameOver)
        {
            GameOverPhoneInterface(obj);
        }
        else if (GetComponent<Gameplay>().IsGameOver() && GetComponent<CameraMovement>() == null &&
                 GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Store)
        {
            StorePhoneInterface(obj);
        }
        else if (paused && GetComponent<CameraMovement>() == null)
        {
            PauseMenuInterface(obj);
        }
    }

    void GameplayPhoneInterface(GameObject obj)
    {
        switch (obj.name)
        {
            case "Second Button":
                if (GetComponent<GrabAndThrowObject>().currentArea != GrabAndThrowObject.Area.grill)
                {
                    GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.grill;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToGrillArea();
                    initialPosition = GetComponent<PositionManager>().GrillPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                    gameObject.AddComponent<CameraMovement>().MoveToGrill();
                }
                break;
            case "Third Button":
                if (GetComponent<GrabAndThrowObject>().currentArea != GrabAndThrowObject.Area.fryer)
                {
                    GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.fryer;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToFryerArea();
                    initialPosition = GetComponent<PositionManager>().FryerPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                    gameObject.AddComponent<CameraMovement>().MoveToFryer();
                }
                break;
            case "Fourth Button":
                if (GetComponent<GrabAndThrowObject>().currentArea != GrabAndThrowObject.Area.sodaMachine)
                {
                    GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.sodaMachine;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToSodaMachineArea();
                    initialPosition = GetComponent<PositionManager>().SodaPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                }
                gameObject.AddComponent<CameraMovement>().MoveToSodaMachine();
                break;
            case "Fifth Button":
                if (GetComponent<GrabAndThrowObject>().currentArea != GrabAndThrowObject.Area.counter)
                {
                    GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.counter;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToFrontArea();
                    initialPosition = GetComponent<PositionManager>().GameplayPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                    gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
                }
                break;
        }
    }

    void GameOverPhoneInterface(GameObject obj)
    {
        switch (obj.name)
        {
            case "Second Button":
                Restart();
                break;
            case "Third Button":
                Quit();
                break;
            case "Fourth Button":
                Continue();
                break;
        }
    }

    void StorePhoneInterface(GameObject obj)
    {
        switch (obj.name)
        {
            case "First Button":
                break;
            case "Second Button":
                break;
            case "Third Button":
                break;
            case "Fourth Button":
                break;
            case "Fifth Button":
                GetComponent<ScreenTextManagment>().ChangeToGameOverText();
                break;
        }
    }

    void PauseMenuInterface(GameObject obj)
    {
        if (paused)
        {
            if (obj.name == "First Button" && !gameObject.GetComponent<Gameplay>().IsGameOver())
            {
                GetComponent<GrabAndThrowObject>().currentArea = GetComponent<GrabAndThrowObject>().previousArea;
                switch (GetComponent<GrabAndThrowObject>().currentArea)
                {
                    case GrabAndThrowObject.Area.counter:
                        initialPosition = GetComponent<PositionManager>().GameplayPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
                        break;
                    case GrabAndThrowObject.Area.grill:
                        initialPosition = GetComponent<PositionManager>().GrillPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToUnpause("Grill");
                        break;
                    case GrabAndThrowObject.Area.fryer:
                        initialPosition = GetComponent<PositionManager>().FryerPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToUnpause("Fryer");
                        break;
                    case GrabAndThrowObject.Area.sodaMachine:
                        initialPosition = GetComponent<PositionManager>().SodaPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToUnpause("Soda Machine");
                        break;
                }
            }
            else if (obj.name == "Second Button")
            {
                Restart();
            }
            else if (obj.name == "Third Button")
            {
                Quit();
            }
        }
    }

    public void MouseUpPauseButton(GameObject target)
    {
        if (target.name == "Pause Image")
        {
            target.GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<GrabAndThrowObject>().previousArea = GetComponent<GrabAndThrowObject>().currentArea;
            GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.pause;
            initialPosition = GetComponent<PositionManager>().PausePosition();
            if (GetComponent<GettingHurt>() != null)
            {
                Destroy(GetComponent<GettingHurt>());
            }
            gameObject.AddComponent<CameraMovement>().MoveToPause();
            PauseGame();
        }
    }

    void Restart()
    {
        GetComponent<ScreenTextManagment>().CannotPressAnything();
        GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.counter;
        initialPosition = GetComponent<PositionManager>().GameplayPosition();
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        GetComponent<Gameplay>().ResetValues();
        GetComponent<GrabAndThrowObject>().DeleteEverything();
        gameObject.AddComponent<CameraMovement>().MoveToGameplay("Restart");
    }

    void Quit()
    {
        GetComponent<ScreenTextManagment>().CannotPressAnything();
        GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.quit;
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        GetComponent<Gameplay>().ResetValues();
        UnPauseGame();
        gameObject.AddComponent<CameraMovement>().MoveToMenu(true);
        Destroy(GetComponent<GrabAndThrowObject>());
    }

    void Continue()
    {
        if (GetComponent<PlayerPrefsManager>().GetCoins() >= GetComponent<Gameplay>().ContinuePrice())
        {
			GetComponent<PlayerPrefsManager>().DecreaseCoins(GetComponent<Gameplay>().ContinuePrice());
			GetComponent<ScreenTextManagment>().PressedContinue();
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            GetComponent<GrabAndThrowObject>().currentArea = GrabAndThrowObject.Area.counter;
            transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
            GetComponent<Gameplay>().Continue();
            GetComponent<GrabAndThrowObject>().DeleteEverything();
            UnPauseGame();
            gameObject.AddComponent<CameraMovement>().MoveToGameplay("Restart");
        }
        else
        {
            GetComponent<ScreenTextManagment>().ChangeToStoreScreen();
        }
    }

    void PauseGame()
    {
        paused = true;
        GetComponent<GrabAndThrowObject>().SetPause(paused);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        paused = false;
        GetComponent<GrabAndThrowObject>().SetPause(paused);
        Time.timeScale = 1;
    }

    public bool GetPausedState()
    {
        return paused;
    }

    public Transform GetInitialPosition()
    {
        return initialPosition;
    }
}
