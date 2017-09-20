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
        if (col.gameObject.tag == "Waiter")
        {
            transform.parent.GetComponent<Waiter>().AddToPlatter(gameObject);
        }
    }
}
