using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoreProducts : MonoBehaviour
{
    GameObject madeFries, burger, madeDrink;
    GameObject meat, topBun, bottomBun, fries, basket, cup, lid;
    GameObject foodTruck;
    GameObject counterWall, grillWall, sodaWall, fryerWall;
    GameObject drink;

    void Start()
    {
        madeFries = GameObject.Find("Fries");
        burger = GameObject.Find("Burger");
        madeDrink = GameObject.Find("Drink");
        foodTruck = GameObject.Find("Food Truck");
        meat = GameObject.Find("Meat");
        topBun = GameObject.Find("Top_Bun");
        bottomBun = GameObject.Find("Bottom_Bun");
        counterWall = GameObject.Find("Counter Wall");
        grillWall = GameObject.Find("Grill Wall");
        sodaWall = GameObject.Find("Soda Wall");
        fryerWall = GameObject.Find("Fryer Wall");
        fries = GameObject.Find("Fries_No_Basket");
        basket = GameObject.Find("Basket");
        drink = GameObject.Find("Drink");
        cup = GameObject.Find("Empty_Cup");
        lid = GameObject.Find("Lid");
    }

    public void DropItems()
    {
        DropMeat();
        DropTopBun();
        DropBottomBun();
        DropFries();
        DropBasket();
        DropCup();
        DropLid();
    }

    public void DropMadeProducts()
    {
        DropMadeFries();
        DropBurger();
        DropDrink();
    }

    public void DropMadeFries()
    {
        if (Camera.main.GetComponent<Gameplay>().GetFriesCount() >= 1)
        {
            GameObject newProduct = Instantiate(madeFries);
            AddNewProduct(newProduct, "Ingredient", Camera.main.GetComponent<PositionManager>().MadeFriesPosition().position);
        }
    }

    public void DropBurger()
    {
        if (Camera.main.GetComponent<Gameplay>().GetBurgerCount() >= 1)
        {
            GameObject newProduct = Instantiate(burger);
            AddNewProduct(newProduct, "Ingredient", Camera.main.GetComponent<PositionManager>().BurgerPosition().position);
        }
    }

    public void DropDrink()
    {
        if (Camera.main.GetComponent<Gameplay>().GetDrinkCount() >= 1)
        {
            GameObject newProduct = Instantiate(madeDrink);
            AddNewProduct(newProduct, "Ingredient", Camera.main.GetComponent<PositionManager>().DrinkPosition().position);
        }
    }

    public void DropMeat()
    {
        GameObject newProduct = Instantiate(meat);
        AddNewProduct(newProduct, "GrillIngredientClone", Camera.main.GetComponent<PositionManager>().MeatSpawnPosition().position);
    }

    public void DropTopBun()
    {
        GameObject newProduct = Instantiate(topBun);
        AddNewProduct(newProduct, "GrillIngredientClone", Camera.main.GetComponent<PositionManager>().TopBunSpawnPosition().position);
    }

    public void DropBottomBun()
    {
        GameObject newProduct = Instantiate(bottomBun);
        AddNewProduct(newProduct, "GrillIngredientClone", Camera.main.GetComponent<PositionManager>().BottomBunSpawnPosition().position);
    }

    public void DropFries()
    {
        if (GameObject.FindGameObjectsWithTag("Fries").Length <= 2)
        {
            GameObject newProduct = Instantiate(fries);
            AddNewProduct(newProduct, "Fries", Camera.main.GetComponent<PositionManager>().FriesPosition().position);
            newProduct.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    public void DropBasket()
    {
        if (GameObject.FindGameObjectsWithTag("Basket").Length <= 2)
        {
            GameObject newProduct = Instantiate(basket);
            AddNewProduct(newProduct, "Basket", Camera.main.GetComponent<PositionManager>().BasketPosition().position);
        }
    }

    public void DropCup()
    {
        if (GameObject.FindGameObjectsWithTag("Soda").Length <= 2)
        {
            GameObject newProduct = Instantiate(cup);
            AddNewProduct(newProduct, "Soda", Camera.main.GetComponent<PositionManager>().CupPosition().position);
            newProduct.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    public void DropLid()
    {
        if (GameObject.FindGameObjectsWithTag("Lid").Length <= 2)
        {
            GameObject newProduct = Instantiate(lid);
            AddNewProduct(newProduct, "Lid", Camera.main.GetComponent<PositionManager>().LidPosition().position + (Random.insideUnitSphere * 0.15f));
            newProduct.transform.rotation = new Quaternion(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    public void TrasformIntoDrink(GameObject oldProduct)
    {
        GameObject newDrink = Instantiate(drink);
        newDrink.transform.parent = oldProduct.transform.parent;
        newDrink.transform.localPosition = oldProduct.transform.localPosition;
        newDrink.transform.localRotation = oldProduct.transform.localRotation;
        newDrink.transform.localScale = oldProduct.transform.localScale;
        newDrink.GetComponent<Rigidbody>().isKinematic = false;
        newDrink.GetComponent<Rigidbody>().useGravity = true;
        newDrink.tag = "Fallen";
        newDrink.AddComponent<FadeObject>();
        Destroy(oldProduct);
    }

    void AddNewProduct(GameObject newProduct, string newTag, Vector3 newPosition)
    {
        newProduct.transform.parent = foodTruck.transform;
        newProduct.GetComponent<Rigidbody>().isKinematic = false;
        newProduct.GetComponent<Rigidbody>().useGravity = true;
        newProduct.GetComponent<Collider>().enabled = true;
        newProduct.AddComponent<RemoveObjects>();
        newProduct.transform.position = newPosition;
        newProduct.tag = newTag;
        Physics.IgnoreCollision(grillWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(sodaWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(counterWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(fryerWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
    }
}
