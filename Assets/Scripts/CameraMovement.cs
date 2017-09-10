using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    GameObject menu;
    GameObject gameplay;
    GameObject towards;
    float speed = 2f;

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, towards.transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, towards.transform.rotation, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, towards.transform.position) < 0.005f)
        {
            transform.position = towards.transform.position;
            transform.rotation = towards.transform.rotation;
            if (towards == gameplay)
            {
                gameObject.AddComponent<Gameplay>();
                gameObject.AddComponent<GrabAndThrowObject>();
            } else if (towards == menu) {

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
}
