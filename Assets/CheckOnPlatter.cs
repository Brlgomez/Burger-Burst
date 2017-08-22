using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnPlatter : MonoBehaviour 
{	
    int burgerAmount;
    int drinkAmount;
    int friesAmount;
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "Thrown")
        {
            if (col.gameObject.name == "Burger(Clone)")
            {
                burgerAmount++;
            } else if (col.gameObject.name == "Drink(Clone)")
            {
                drinkAmount++;
            } else if (col.gameObject.name == "Fries(Clone)")
            {
                friesAmount++;
            }
            //Debug.Log("Burger:"+ burgerAmount + "Drink:"+ drinkAmount + "Fries:" + friesAmount);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if(col.gameObject.tag == "Thrown")
        {
            if (col.gameObject.name == "Burger(Clone)")
            {
                burgerAmount--;
            } else if (col.gameObject.name == "Drink(Clone)")
            {
                drinkAmount--;
            } else if (col.gameObject.name == "Fries(Clone)")
            {
                friesAmount--;
            }
            //Debug.Log("Burger:"+ burgerAmount + "Drink:"+ drinkAmount + "Fries:" + friesAmount);
        }
    }
}
