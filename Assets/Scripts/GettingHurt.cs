using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHurt : MonoBehaviour
{
    static float lengthOfAnimation = 0.25f;
    float shakeIntensity = 0.25f;
    GameObject screen;
    Transform initialTransform;
    float r, g, b, a;
    float currentTime;
    bool canShake = true;

    void Start()
    {
        initialTransform = GetComponent<GrabAndThrowObject>().GetInitialPosition();
        screen = transform.GetChild(0).gameObject;
        r = screen.GetComponent<Renderer>().material.color.r;
        g = screen.GetComponent<Renderer>().material.color.g;
        b = screen.GetComponent<Renderer>().material.color.b;
        a = 0;
        if (GetComponent<CameraMovement>() != null)
        {
            canShake = false;
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        currentTime += deltaTime;
        shakeIntensity -= deltaTime;
        if (currentTime < lengthOfAnimation / 2)
        {
            a += deltaTime * (1 / lengthOfAnimation);
        }
        else if (!Camera.main.gameObject.GetComponent<Gameplay>().IsGameOver() && currentTime > lengthOfAnimation / 2)
        {
            a -= deltaTime * (1 / lengthOfAnimation);
        }
        screen.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        if (canShake)
        {
            transform.position = initialTransform.position + Random.insideUnitSphere * shakeIntensity;
        }
        if (currentTime > lengthOfAnimation)
        {
            if (canShake)
            {
                transform.position = initialTransform.position;
            }
            Destroy(GetComponent<GettingHurt>());
        }
    }

    void OnDestroy()
    {
        a = 0;
        if (!GetComponent<Gameplay>().IsGameOver())
        {
            screen.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        }
    }
}
