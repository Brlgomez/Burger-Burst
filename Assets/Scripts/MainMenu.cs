using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameObject target;

    void Start()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    void MouseDown()
    {
        RaycastHit hitInfo;
        target = null;
        target = ReturnClickedObject(out hitInfo);
        if (target != null && target.tag == "UI")
        {
            Camera.main.GetComponent<ScreenTextManagment>().PressTextDown(target.transform.parent.gameObject);
        }
    }

    void MouseUp()
    {
        if (target != null && target.tag == "UI")
        {
            Camera.main.GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            if (target.name == "Second Button")
            {
                gameObject.AddComponent<CameraMovement>().MoveToGameplay("Start");
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
