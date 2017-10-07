using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoreProducts : MonoBehaviour
{
    GameObject meat, topBun, bottomBun, fries;
    GameObject foodTruck;
    GameObject grillWall;

    void Start()
    {
        foodTruck = GameObject.Find("Food Truck");
        meat = GameObject.Find("Meat");
        topBun = GameObject.Find("Top_Bun");
        bottomBun = GameObject.Find("Bottom_Bun");
        grillWall = GameObject.Find("Grill Wall");
        fries = GameObject.Find("Fries_No_Basket");
    }

    public void DropItems()
    {
        DropMeat();
        DropTopBun();
        DropBottomBun();
		DropFries();
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

    void AddNewProduct(GameObject newProduct)
    {
        newProduct.transform.parent = foodTruck.transform;
        newProduct.GetComponent<Rigidbody>().isKinematic = false;
        newProduct.GetComponent<Rigidbody>().useGravity = true;
        newProduct.GetComponent<Collider>().enabled = true;
        Physics.IgnoreCollision(grillWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
    }
}
