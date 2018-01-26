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
    float rotationSpeed = 2.5f;

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
            chefHat.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.white, time);
            zombieHands.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.white, time);
            if (zombieHands.GetComponent<SpriteRenderer>().color.a > 0.95f)
            {
                chefHat.GetComponent<SpriteRenderer>().color = Color.white;
                zombieHands.GetComponent<SpriteRenderer>().color = Color.white;
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
            if ((Mathf.Abs(title.transform.eulerAngles.z - leftRot.z)) > 5 && rotationSpeed < 5)
            {
                rotationSpeed += Time.deltaTime;
            }
            else if (rotationSpeed > 0.1f)
            {
                rotationSpeed -= Time.deltaTime * 2;
            }
            title.transform.eulerAngles = new Vector3(
                title.transform.eulerAngles.x, title.transform.eulerAngles.y, title.transform.eulerAngles.z - Time.deltaTime * rotationSpeed);
        }
        else
        {
            if ((Mathf.Abs(title.transform.eulerAngles.z - rightRot.z)) > 5 && rotationSpeed < 5)
            {
                rotationSpeed += Time.deltaTime;
            }
            else if (rotationSpeed > 0.1f)
            {
                rotationSpeed -= Time.deltaTime * 2;
            }
            title.transform.eulerAngles = new Vector3(
                title.transform.eulerAngles.x, title.transform.eulerAngles.y, title.transform.eulerAngles.z + Time.deltaTime * rotationSpeed);
        }
    }

    void OnDestroy()
    {
        title.transform.localScale = Vector3.one * 3;
        zombieHands.transform.localScale = Vector3.one * 3;
        chefHat.transform.localScale = Vector3.one * 3;
        title.AddComponent<FadeObject>();
        zombieHands.AddComponent<FadeObject>();
        chefHat.AddComponent<FadeObject>();
    }
}
