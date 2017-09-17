using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{

    float lifetime = 1;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if ((col.gameObject.tag == "Building" || col.gameObject.tag == "Table") && gameObject.tag != "Fallen" && gameObject.tag != "OnPlatter")
        {
            gameObject.tag = "Fallen";
            gameObject.AddComponent<ShrinkObject>();
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(gameObject);
            Destroy(gameObject, lifetime);
        } 
        else if (col.gameObject.tag == "Waiter")
        {
            GameObject.Find("Waiter").GetComponent<Waiter>().AddToPlatter(gameObject);
            transform.parent = col.gameObject.transform;
            Destroy(GetComponent<RemoveObjects>());
        }
    }
}
