using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    static int speed = 3;
    GameObject title, zombieHands, chefHat;
    float time;
    bool titleZoomed, imagesZoomed, goingLeft;
    Vector3 leftRot, rightRot;

    void Start()
    {
        title = GetComponent<ObjectManager>().TitleSign().transform.GetChild(1).gameObject;
        chefHat = GetComponent<ObjectManager>().TitleSign().transform.GetChild(2).gameObject;
        zombieHands = GetComponent<ObjectManager>().TitleSign().transform.GetChild(3).gameObject;
        leftRot = new Vector3(title.transform.eulerAngles.x, title.transform.eulerAngles.y, 340);
        rightRot = new Vector3(title.transform.eulerAngles.x, title.transform.eulerAngles.y, 20);
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        if (!titleZoomed)
        {
            title.transform.localScale = Vector3.one * time;
            if (title.transform.localScale.x > 3)
            {
                title.transform.localScale = Vector3.one * 3;
                titleZoomed = true;
                time = 0;
            }
        }
        else if (titleZoomed && !imagesZoomed)
        {
            chefHat.transform.localScale = Vector3.one * time;
            zombieHands.transform.localScale = Vector3.one * time;
            if (chefHat.transform.localScale.x > 3)
            {
                chefHat.transform.localScale = Vector3.one * 3;
                zombieHands.transform.localScale = Vector3.one * 3;
                imagesZoomed = true;
                time = 0;
            }
        }
        RotateTitle();
    }

    void RotateTitle()
    {
        if ((Mathf.Abs(title.transform.eulerAngles.z - leftRot.z) < 1) && goingLeft)
        {
            goingLeft = false;
        }
        if ((Mathf.Abs(title.transform.eulerAngles.z - rightRot.z) < 1) && !goingLeft)
        {
            goingLeft = true;
        }
        if (goingLeft)
        {
            title.transform.eulerAngles = new Vector3(
                title.transform.eulerAngles.x, title.transform.eulerAngles.y, title.transform.eulerAngles.z - Time.deltaTime * 20);
        }
        else
        {
            title.transform.eulerAngles = new Vector3(
                title.transform.eulerAngles.x, title.transform.eulerAngles.y, title.transform.eulerAngles.z + Time.deltaTime * 20);
        }
    }

    void OnDestroy()
    {
        title.transform.localScale = Vector3.one * 3;
        zombieHands.transform.localScale = Vector3.one * 3;
        chefHat.transform.localScale = Vector3.one * 3;
    }
}
