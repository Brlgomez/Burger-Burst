using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Building" && gameObject.tag != "Fallen" && gameObject.tag != "OnPlatter")
        {
            gameObject.tag = "Fallen";
            if (gameObject.GetComponent<FadeObject>() == null)
            {
                gameObject.AddComponent<FadeObject>();
            }
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(gameObject);
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
            }
        }
    }
}
