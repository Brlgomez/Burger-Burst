using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHurt : MonoBehaviour
{

    GameObject screen;
    float r, g, b, a;
    float lengthOfAnimation = 0.25f;
    float currentTime = 0;
    Transform initialTransform;
    float shakeIntensity = 0.25f;

    void Start()
    {
        initialTransform = GameObject.Find("Gameplay Camera Position").transform;
        screen = Camera.main.transform.GetChild(0).gameObject;
        r = screen.GetComponent<Renderer>().material.color.r;
        g = screen.GetComponent<Renderer>().material.color.g;
        b = screen.GetComponent<Renderer>().material.color.b;
        a = 0;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        shakeIntensity -= Time.deltaTime;
        if (currentTime < lengthOfAnimation / 2)
        {
            a += Time.deltaTime * 4;
        }
        else
        {
            a -= Time.deltaTime * 4;
        }
        screen.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
        Camera.main.transform.position = initialTransform.position + Random.insideUnitSphere * shakeIntensity;
        if (currentTime > lengthOfAnimation)
        {
            Camera.main.transform.position = initialTransform.position;
            Destroy(GetComponent<GettingHurt>());
        }
    }

    void OnDestroy()
    {
        a = 0;
        screen.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
    }
}
