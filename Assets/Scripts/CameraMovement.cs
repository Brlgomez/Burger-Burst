using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    GameObject menu;
    GameObject gameplay;
    GameObject pause;
    GameObject towards;
    float speed = 0;

    void Update()
    {
        if (speed < 5f)
        {
            speed += Time.unscaledDeltaTime * 10;
        }
        transform.position = Vector3.Lerp(transform.position, towards.transform.position, Time.unscaledDeltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, towards.transform.rotation, Time.unscaledDeltaTime * speed);
        if (Vector3.Distance(transform.position, towards.transform.position) < 0.005f)
        {
            transform.position = towards.transform.position;
            transform.rotation = towards.transform.rotation;
            if (towards == gameplay)
            {
                if (GetComponent<GrabAndThrowObject>() != null)
                {
                    GetComponent<GrabAndThrowObject>().UnPauseGame();
                }
                else
                {
                    gameObject.AddComponent<Gameplay>();
                    gameObject.AddComponent<GrabAndThrowObject>();
                    GameObject newWaiter = Instantiate(GameObject.Find("Waiter"));
                    newWaiter.AddComponent<Waiter>();
                }
            }
            else if (towards == menu)
            {
                gameObject.AddComponent<MainMenu>();
            } 
            else if (towards == pause) {
                Camera.main.GetComponent<ScreenTextManagment>().SetSecondScreenText("Resume\nRestart\nQuit", Color.white);
            }
            Destroy(GetComponent<CameraMovement>());
        }
    }

    public void MoveToMenu()
    {
        menu = GameObject.Find("Menu Camera Position");
        towards = menu;
    }

    public void MoveToGameplay()
    {
        gameplay = GameObject.Find("Gameplay Camera Position");
        towards = gameplay;
    }

    public void MoveToPause () 
    {
        pause = GameObject.Find("Pause Camera Position");
        towards = pause;
    }
}
