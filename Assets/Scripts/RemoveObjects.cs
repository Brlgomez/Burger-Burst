using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{

    void OnCollisionEnter(Collision col)
    {
        if (gameObject.tag == "GrillIngredientClone")
        {
            Vector3 grillRange = Camera.main.GetComponent<PositionManager>().GrillRange().position;
            if (Vector3.Distance(gameObject.transform.position, grillRange) > 1.25f)
            {
                HasFallen();
            }
        }
		if (gameObject.tag == "Fries")
		{
            Vector3 friesRange = Camera.main.GetComponent<PositionManager>().FriesRange().position;
            if (Vector3.Distance(gameObject.transform.position, friesRange) > 1.5f)
			{
				HasFallen();
			}
		}

        if (gameObject.tag == "Soda" || gameObject.tag == "Lid")
		{
            Vector3 drinkRange = Camera.main.GetComponent<PositionManager>().DrinkRange().position;
            if (Vector3.Distance(gameObject.transform.position, drinkRange) > 1.25f)
			{
				HasFallen();
			}
		}
		if (col.gameObject.tag == "Building" && gameObject.tag != "Fallen" && gameObject.tag != "OnPlatter")
        {
            HasFallen();
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

    void HasFallen()
    {
		if (gameObject.tag == "Soda")
		{
			Destroy(gameObject.GetComponent<SodaCup>());
		}
		gameObject.tag = "Fallen";
		if (gameObject.GetComponent<FadeObject>() == null)
		{
			gameObject.AddComponent<FadeObject>();
			Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(gameObject);
			Destroy(GetComponent<RemoveObjects>());
		}
    }
}
