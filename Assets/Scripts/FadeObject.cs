using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    static int updateInterval = 4;
    static int maxTime = 1;
    float time;
    float r, g, b, a;
    float newAlpha;
    Renderer myRenderer;

    void Awake()
    {
        gameObject.layer = 2;
        if (GetComponent<Renderer>().material.name.Split(' ')[0] != "Ice")
        {
            Camera.main.GetComponent<FoodManager>().ChangeToTransparentMaterial(gameObject);
        }
        r = GetComponent<Renderer>().material.color.r;
        g = GetComponent<Renderer>().material.color.g;
        b = GetComponent<Renderer>().material.color.b;
        a = GetComponent<Renderer>().material.color.a;
        myRenderer = GetComponent<Renderer>();
        AddNewItem();
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            time += Time.deltaTime * updateInterval;
            newAlpha = (a - (time / maxTime));
            if (myRenderer.isVisible)
            {
                GetComponent<Renderer>().material.color = new Color(r, g, b, newAlpha);
            }
            if (time > maxTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void AddNewItem()
    {
        switch (name)
        {
            case "Top_Bun(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropTopBun();
                break;
            case "Bottom_Bun(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropBottomBun();
                break;
            case "Meat(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropMeat();
                break;
            case "Fries_No_Basket(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropFries();
                break;
            case "Basket(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropBasket();
                break;
            case "Empty_Cup(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropCup();
                break;
            case "Lid(Clone)":
                Camera.main.GetComponent<DropMoreProducts>().DropLid();
                break;
        }
    }
}
