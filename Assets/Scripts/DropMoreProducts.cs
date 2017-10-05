using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoreProducts : MonoBehaviour
{
    GameObject meat, topBun, bottomBun;
    GameObject foodTruck;
    GameObject grillWall;

    void Start()
    {
        foodTruck = GameObject.Find("Food Truck");
        meat = GameObject.Find("Meat");
        topBun = GameObject.Find("Top_Bun");
        bottomBun = GameObject.Find("Bottom_Bun");
        grillWall = GameObject.Find("Grill Wall");
    }

    public void DropGrillItems()
    {
        DropMeat();
        DropTopBun();
        DropBottomBun();
    }

    public void DropMeat()
    {
        GameObject newProduct = Instantiate(meat);
        AddNewGrillProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().MeatSpawnPosition().position;
    }

    public void DropTopBun()
    {
        GameObject newProduct = Instantiate(topBun);
        AddNewGrillProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().TopBunSpawnPosition().position;
    }

    public void DropBottomBun()
    {
        GameObject newProduct = Instantiate(bottomBun);
        AddNewGrillProduct(newProduct);
        newProduct.transform.position = Camera.main.GetComponent<PositionManager>().BottomBunSpawnPosition().position;
    }

    void AddNewGrillProduct(GameObject newProduct)
    {
        newProduct.transform.parent = foodTruck.transform;
        newProduct.GetComponent<Rigidbody>().isKinematic = false;
        newProduct.GetComponent<Rigidbody>().useGravity = true;
        newProduct.GetComponent<Collider>().enabled = true;
        newProduct.tag = "GrillIngredientClone";
        Physics.IgnoreCollision(grillWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
    }
}
