using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnPlatter : MonoBehaviour
{
    int burgerAmount;
    int drinkAmount;
    int friesAmount;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Thrown")
        {
            transform.parent.GetComponent<Waiter>().AddToPlatter(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "OnPlatter")
        {
            transform.parent.GetComponent<Waiter>().RemoveFromPlatter(col.gameObject);
        }
    }
}
