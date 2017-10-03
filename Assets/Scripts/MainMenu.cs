using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    void Start () 
    { 
        Application.targetFrameRate = 60;
        Screen.orientation = ScreenOrientation.Landscape;
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        } 
    }

    void MouseDown()
    {
        RaycastHit hitInfo;
        GameObject Button = ReturnClickedObject(out hitInfo);
        if (Button != null)
        {
            if (Button.name == "Play Button")
            {
                Camera.main.GetComponent<ScreenTextManagment>().RestartScreens();
                gameObject.AddComponent<CameraMovement>().MoveToGameplay();
                Destroy(GetComponent<MainMenu>());
            }
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
