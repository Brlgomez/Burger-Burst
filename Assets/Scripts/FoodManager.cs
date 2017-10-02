using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

    public void ChangeToTransparentMaterial (GameObject obj) 
    {
		if (obj.name == "Burger(Clone)")
		{
			obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().burgerClear;
		}
		else if (obj.name == "Drink(Clone)")
		{
			obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().drinkClear;
		}
		else if (obj.name == "Fries(Clone)")
		{
			obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().friesClear;
		}
    }
}
