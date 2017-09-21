using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{

    float time;
    int maxTime = 1;
    float r, g, b;

    void Start () {
        if (name == "Burger(Clone)")
        {
            GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().burgerClear;
        }
        else if (name == "Drink(Clone)")
        {
            GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().drinkClear;
        }
        else if (name == "Fries(Clone)")
        {
            GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().friesClear;
        }
        r = GetComponent<Renderer>().material.color.r;
        g = GetComponent<Renderer>().material.color.g;
        b = GetComponent<Renderer>().material.color.b;
    }

    void Update()
    {
        time += Time.deltaTime;
        float alpha = ((maxTime / maxTime) - (time / maxTime));
        GetComponent<Renderer>().material.color = new Color(r, g, b, alpha);
    }
}
