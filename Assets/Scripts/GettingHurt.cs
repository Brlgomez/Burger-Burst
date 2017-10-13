using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHurt : MonoBehaviour
{
    static float lengthOfAnimation = 0.25f;
    static float shakeIntensity = 0.25f;
    GameObject screen;
    Transform initialTransform;
    float r, g, b, a;
    float currentTime;
    bool canShake = true;

    void Start()
    {
        initialTransform = Camera.main.GetComponent<GrabAndThrowObject>().GetInitialPosition();
        screen = Camera.main.transform.GetChild(0).gameObject;
        r = screen.GetComponent<Renderer>().material.color.r;
        g = screen.GetComponent<Renderer>().material.color.g;
        b = screen.GetComponent<Renderer>().material.color.b;
        a = 0;
        if (Camera.main.GetComponent<CameraMovement>() != null)
        {
            canShake = false;
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        shakeIntensity -= Time.deltaTime;
        if (currentTime < lengthOfAnimation / 2)
        {
            a += Time.deltaTime * (1 / lengthOfAnimation);
        }
        else if (!Camera.main.gameObject.GetComponent<Gameplay>().IsGameOver() && currentTime > lengthOfAnimation / 2)
        {
            a -= Time.deltaTime * (1 / lengthOfAnimation);
        }
        screen.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        if (canShake)
        {
            Camera.main.transform.position = initialTransform.position + Random.insideUnitSphere * shakeIntensity;
        }
        if (currentTime > lengthOfAnimation)
        {
            if (canShake)
            {
                Camera.main.transform.position = initialTransform.position;
            }
            Destroy(GetComponent<GettingHurt>());
        }
    }

    void OnDestroy()
    {
        a = 0;
        if (!Camera.main.gameObject.GetComponent<Gameplay>().IsGameOver())
        {
            screen.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        }
    }
}
