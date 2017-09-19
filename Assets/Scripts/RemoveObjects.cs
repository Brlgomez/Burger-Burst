using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{

    float lifetime = 1;

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Building" || col.gameObject.tag == "Table") && gameObject.tag != "Fallen" && gameObject.tag != "OnPlatter")
        {
            gameObject.tag = "Fallen";
            gameObject.AddComponent<ShrinkObject>();
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(gameObject);
            Destroy(gameObject, lifetime);
            Destroy(GetComponent<RemoveObjects>());
        }
        else if (col.gameObject.tag == "Waiter")
        {
            Vector3 closestPointOnItem = gameObject.GetComponent<MeshCollider>().ClosestPoint(col.gameObject.transform.position);
            Vector3 closestPointOnWaiter = col.gameObject.GetComponent<Collider>().ClosestPoint(closestPointOnItem);
            if (Vector3.Distance(closestPointOnItem, closestPointOnWaiter) < 0.15f)
            {
                col.transform.root.gameObject.GetComponent<Waiter>().AddToPlatter(gameObject);
                transform.parent = col.gameObject.transform;
                Destroy(GetComponent<RemoveObjects>());
            }
        }
    }
}
