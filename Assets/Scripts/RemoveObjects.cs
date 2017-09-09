using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour {

    float lifetime = 3;

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Building" && gameObject.tag != "Fallen")
        {
            gameObject.tag = "Fallen";
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(gameObject);
            Destroy(gameObject, lifetime);
        }
    }
}
