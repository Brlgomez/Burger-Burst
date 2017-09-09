using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour {

    float lifetime = 3;

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Building")
        {
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(1);
            Destroy(gameObject, lifetime);
        }
    }
}
