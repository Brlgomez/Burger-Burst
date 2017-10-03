using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Transform menu, gameplay, pause, towards, gameOver, grill, fryer, soda;
    float speed;
    int maxSpeed = 10;
    int accelerating = 10;
    bool moveToPosition;

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
            if (Vector3.Distance(transform.position, towards.transform.position) < 0.001f)
            {
                transform.position = towards.transform.position;
                transform.rotation = towards.transform.rotation;
                if (towards == gameplay)
                {
                    // restart game
                    if (GetComponent<GrabAndThrowObject>() != null && GetComponent<Gameplay>() == null)
                    {
                        GetComponent<GrabAndThrowObject>().UnPauseGame();
                        gameObject.AddComponent<Gameplay>();
                    }
                    // start game
                    else if (GetComponent<Gameplay>() == null)
                    {
                        gameObject.AddComponent<Gameplay>();
                        gameObject.AddComponent<GrabAndThrowObject>();
                    }
                    // unpause or from other places
                    else if (GetComponent<Gameplay>() != null)
                    {
                        GetComponent<GrabAndThrowObject>().UnPauseGame();
                    } 
                }
                else if (towards == menu)
                {
                    gameObject.AddComponent<MainMenu>();
                }
                else if (towards == pause)
                {
                    Camera.main.GetComponent<ScreenTextManagment>().SetSecondScreenText("Resume\nRestart\nQuit", Color.white);
                }
                else if (towards == gameOver)
                {
                    GetComponent<GrabAndThrowObject>().UnPauseGame();
                }
                Destroy(GetComponent<CameraMovement>());
            }
        }
    }

    public void MoveToMenu()
    {
        menu = Camera.main.GetComponent<PositionManager>().MenuPosition();
        towards = menu;
        moveToPosition = true;
    }

    public void MoveToGameplay()
    {
        gameplay = Camera.main.GetComponent<PositionManager>().GameplayPosition();
        towards = gameplay;
        moveToPosition = true;
    }

    public void MoveToPause()
    {
        pause = Camera.main.GetComponent<PositionManager>().PausePosition();
        towards = pause;
        moveToPosition = true;
    }

    public void MoveToGameOver()
    {
        gameOver = Camera.main.GetComponent<PositionManager>().GameOverPosition();
        towards = gameOver;
        moveToPosition = true;
    }

	public void MoveToGrill()
	{
        grill = Camera.main.GetComponent<PositionManager>().GrillPosition();
        towards = grill;
		moveToPosition = true;
	}

	public void MoveToFryer()
	{
        fryer = Camera.main.GetComponent<PositionManager>().FryerPosition();
        towards = fryer;
		moveToPosition = true;
	}

	public void MoveToSodaMachine()
	{
        soda = Camera.main.GetComponent<PositionManager>().SodaPosition();
        towards = soda;
		moveToPosition = true;
	}
}
