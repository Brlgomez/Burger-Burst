using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public void ChangeToTransparentMaterial(GameObject obj)
    {
        if (obj.name == "Burger(Clone)" || obj.name == "Top_Bun(Clone)" || obj.name == "Bottom_Bun(Clone)" || obj.name == "Burger(Clone)Copy")
        {
            obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().burgerClear;
        }
        if (obj.name == "Drink(Clone)" || obj.name == "Empty_Cup(Clone)" || obj.name == "Lid(Clone)" || obj.name == "Drink(Clone)Copy")
        {
            obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().drinkClear;
        }
        if (obj.name == "Fries(Clone)" || obj.name == "Fries_No_Basket" || obj.name == "Basket(Clone)" || obj.name == "Fries(Clone)Copy")
        {
            obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().friesClear;
        }
    }
}
