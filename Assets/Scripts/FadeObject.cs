using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    float time;
    int maxTime = 1;
    float r, g, b;

    void Start () {
        Camera.main.GetComponent<FoodManager>().ChangeToTransparentMaterial(gameObject);
        r = GetComponent<Renderer>().material.color.r;
        g = GetComponent<Renderer>().material.color.g;
        b = GetComponent<Renderer>().material.color.b;
		AddNewItem();
	}

    void Update()
    {
        time += Time.deltaTime;
        float alpha = ((maxTime / maxTime) - (time / maxTime));
        GetComponent<Renderer>().material.color = new Color(r, g, b, alpha);
        if (alpha < 0.01f) {
            Destroy(gameObject);
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
