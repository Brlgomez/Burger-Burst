using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoreProducts : MonoBehaviour
{
    GameObject madeFries, burger, madeDrink;
    GameObject meat, topBun, bottomBun, fries, cup, lid;
    GameObject foodTruck;
    GameObject counterWall, grillWall, sodaWall;
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
        fries = GameObject.Find("Fries_No_Basket");
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
            AddNewProduct(newProduct);
            newProduct.transform.position = Camera.main.GetComponent<PositionManager>().MadeFriesPosition().position;
            newProduct.tag = "Ingredient";
        }
	}

    public void DropBurger()
    {
        if (Camera.main.GetComponent<Gameplay>().GetBurgerCount() >= 1)
        {
            GameObject newProduct = Instantiate(burger);
            AddNewProduct(newProduct);
            newProduct.transform.position = Camera.main.GetComponent<PositionManager>().BurgerPosition().position;
            newProduct.tag = "Ingredient";
        }
	}

    public void DropDrink()
    {
        if (Camera.main.GetComponent<Gameplay>().GetDrinkCount() >= 1)
        {
            GameObject newProduct = Instantiate(madeDrink);
            AddNewProduct(newProduct);
            newProduct.transform.position = Camera.main.GetComponent<PositionManager>().DrinkPosition().position;
            newProduct.tag = "Ingredient";
        }
	}

    public void DropMeat()
    {
        GameObject newProduct = Instantiate(meat);
        AddNewProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().MeatSpawnPosition().position;
        newProduct.tag = "GrillIngredientClone";
    }

    public void DropTopBun()
    {
        GameObject newProduct = Instantiate(topBun);
        AddNewProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().TopBunSpawnPosition().position;
        newProduct.tag = "GrillIngredientClone";
    }

    public void DropBottomBun()
    {
        GameObject newProduct = Instantiate(bottomBun);
        AddNewProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().BottomBunSpawnPosition().position;
        newProduct.tag = "GrillIngredientClone";
    }

    public void DropFries()
    {
        GameObject newProduct = Instantiate(fries);
        AddNewProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().FriesPosition().position;
        newProduct.tag = "Fries";
    }

    public void DropCup()
    {
        if (GameObject.FindGameObjectsWithTag("Soda").Length <= 2)
        {
            GameObject newProduct = Instantiate(cup);
            AddNewProduct(newProduct);
            newProduct.transform.position = Camera.main.GetComponent<PositionManager>().CupPosition().position;
            newProduct.tag = "Soda";
        }
    }

    public void DropLid()
    {
        if (GameObject.FindGameObjectsWithTag("Lid").Length <= 2)
        {
            GameObject newProduct = Instantiate(lid);
            AddNewProduct(newProduct);
            newProduct.transform.position = Camera.main.GetComponent<PositionManager>().LidPosition().position + (Random.insideUnitSphere * 0.15f);
            newProduct.transform.rotation = new Quaternion(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            newProduct.GetComponent<Rigidbody>().freezeRotation = false;
            newProduct.tag = "Lid";
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
        newDrink.tag = "Soda";
        newDrink.AddComponent<FadeObject>();
        Destroy(oldProduct);
    }

	void AddNewProduct(GameObject newProduct)
	{
		newProduct.transform.parent = foodTruck.transform;
		newProduct.GetComponent<Rigidbody>().isKinematic = false;
		newProduct.GetComponent<Rigidbody>().useGravity = true;
		newProduct.GetComponent<Collider>().enabled = true;
		newProduct.GetComponent<Rigidbody>().freezeRotation = true;
		Physics.IgnoreCollision(grillWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(sodaWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(counterWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
	}
}
