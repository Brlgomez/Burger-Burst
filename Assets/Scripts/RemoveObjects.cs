using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{
    bool hasDropped;

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
        else if ((gameObject.tag == "Fries" || gameObject.tag == "Basket") && col.gameObject.tag != "Fries" && col.gameObject.tag != "Basket")
        {
            Vector3 friesRange = Camera.main.GetComponent<PositionManager>().FriesRange().position;
            if (Vector3.Distance(gameObject.transform.position, friesRange) > 1.75f)
            {
                HasFallen();
            }
        }
        else if ((gameObject.tag == "Soda" || gameObject.tag == "Lid") && col.gameObject.tag != "Soda" && col.gameObject.tag != "Lid")
        {
            Vector3 drinkRange = Camera.main.GetComponent<PositionManager>().DrinkRange().position;
            if (Vector3.Distance(gameObject.transform.position, drinkRange) > 1.25f)
            {
                HasFallen();
            }
        }
        else if (col.gameObject.tag == "Building" && gameObject.tag != "Fallen" && gameObject.tag != "OnPlatter")
        {
            DropProduct();
            HasFallen();
        }
        else if (col.gameObject.tag == "Waiter" && gameObject.tag == "Ingredient" && !Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            Vector3 closestPointOnItem = gameObject.GetComponent<MeshCollider>().ClosestPoint(col.gameObject.transform.position);
            Vector3 closestPointOnWaiter = col.gameObject.GetComponent<Collider>().ClosestPoint(closestPointOnItem);
            if (Vector3.Distance(closestPointOnItem, closestPointOnWaiter) < 0.16f)
            {
                transform.parent = col.gameObject.transform;
                col.transform.root.gameObject.GetComponent<Zombie>().AddToPlatter(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (gameObject.tag == "Ingredient" && col.gameObject.name == "Counter Range")
        {
            DropProduct();
        }
    }

    public void DropProduct()
    {
        if (!hasDropped)
        {
            switch (gameObject.name)
            {
                case "Burger(Clone)":
                    Camera.main.GetComponent<Gameplay>().ReduceBurgers();
                    Camera.main.GetComponent<DropMoreProducts>().DropBurger();
                    hasDropped = true;
                    break;
                case "Drink(Clone)":
                    Camera.main.GetComponent<Gameplay>().ReduceDrinks();
                    Camera.main.GetComponent<DropMoreProducts>().DropDrink();
                    hasDropped = true;
                    break;
                case "Fries(Clone)":
                    Camera.main.GetComponent<Gameplay>().ReduceFries();
                    Camera.main.GetComponent<DropMoreProducts>().DropMadeFries();
                    hasDropped = true;
                    break;
            }
        }
    }

    void HasFallen()
    {
        if (gameObject.tag == "Soda")
        {
            Destroy(gameObject.GetComponent<SodaCup>());
            if (gameObject.transform.GetChild(0).gameObject.GetComponent<FadeObject>() == null)
            {
                gameObject.transform.GetChild(0).gameObject.AddComponent<FadeObject>();
            }
        }
        gameObject.tag = "Fallen";
        if (gameObject.GetComponent<FadeObject>() == null)
        {
            gameObject.AddComponent<FadeObject>();
            Destroy(GetComponent<RemoveObjects>());
        }
    }
}
