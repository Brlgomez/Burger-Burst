using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour 
{
    public void ChangeToTransparentMaterial (GameObject obj) 
    {
		if (obj.name == "Burger(Clone)" || obj.name == "Top_Bun(Clone)" || obj.name == "Bottom_Bun(Clone)")
		{
			obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().burgerClear;
		}
		if (obj.name == "Drink(Clone)")
		{
			obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().drinkClear;
		}
		if (obj.name == "Fries(Clone)" || obj.name == "Fries_No_Basket")
		{
			obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().friesClear;
		}
    }
}
