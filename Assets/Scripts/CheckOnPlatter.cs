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
            col.gameObject.tag = "OnPlatter";
            Camera.main.GetComponent<Gameplay>().addToPlatter(col.gameObject);
            Camera.main.GetComponent<Gameplay>().checkOrder(burgerAmount, drinkAmount, friesAmount);
            //Debug.Log("Burger:"+ burgerAmount + "Drink:"+ drinkAmount + "Fries:" + friesAmount);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if(col.gameObject.tag == "OnPlatter")
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
            col.gameObject.tag = "Thrown";
            Camera.main.GetComponent<Gameplay>().removeFromPlatter(col.gameObject);
            Camera.main.GetComponent<Gameplay>().checkOrder(burgerAmount, drinkAmount, friesAmount);
            //Debug.Log("Burger:"+ burgerAmount + "Drink:"+ drinkAmount + "Fries:" + friesAmount);
        }
    }

    public void restartAmounts () {
        burgerAmount = 0;
        drinkAmount = 0;
        friesAmount = 0;
    }
}
