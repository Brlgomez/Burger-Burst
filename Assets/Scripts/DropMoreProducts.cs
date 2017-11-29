using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoreProducts : MonoBehaviour
{
    GameObject counterWall, grillWall, sodaWall, fryerWall;
    GameObject fries, burger, drink;
    GameObject meat, topBun, bottomBun, uncookedFries, friesBasket, cup, lid;
    GameObject foodTruck;

    void Start()
    {
        counterWall = GetComponent<ObjectManager>().CounterWall();
        grillWall = GetComponent<ObjectManager>().GrillWall();
        sodaWall = GetComponent<ObjectManager>().SodaWall();
        fryerWall = GetComponent<ObjectManager>().FryerWall();
        fries = GetComponent<ObjectManager>().Fries();
        burger = GetComponent<ObjectManager>().Burger();
        drink = GetComponent<ObjectManager>().Drink();
        meat = GetComponent<ObjectManager>().Meat();
        topBun = GetComponent<ObjectManager>().TopBun();
        bottomBun = GetComponent<ObjectManager>().BottomBun();
        uncookedFries = GetComponent<ObjectManager>().UncookedFries();
        friesBasket = GetComponent<ObjectManager>().FriesBasket();
        cup = GetComponent<ObjectManager>().Cup();
        lid = GetComponent<ObjectManager>().Lid();
        foodTruck = GetComponent<ObjectManager>().FoodTruck();
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
            GameObject newProduct = Instantiate(fries);
            AddNewProduct(newProduct, "Ingredient", Camera.main.GetComponent<PositionManager>().MadeFriesPosition().position);
            newProduct.AddComponent<RemoveObjects>();
            if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().largerFood.powerUpNumber))
            {
                newProduct.transform.localScale = Vector3.one * 1.25f;
            }
            newProduct.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
        }
    }

    public void DropBurger()
    {
        if (Camera.main.GetComponent<Gameplay>().GetBurgerCount() >= 1)
        {
            GameObject newProduct = Instantiate(burger);
            AddNewProduct(newProduct, "Ingredient", Camera.main.GetComponent<PositionManager>().BurgerPosition().position);
            newProduct.AddComponent<RemoveObjects>();
            if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().largerFood.powerUpNumber))
            {
                newProduct.transform.localScale = Vector3.one * 1.25f;
            }
			newProduct.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
		}
    }

    public void DropDrink()
    {
        if (Camera.main.GetComponent<Gameplay>().GetDrinkCount() >= 1)
        {
            GameObject newProduct = Instantiate(drink);
            AddNewProduct(newProduct, "Ingredient", Camera.main.GetComponent<PositionManager>().DrinkPosition().position);
            newProduct.AddComponent<RemoveObjects>();
            if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().largerFood.powerUpNumber))
            {
                newProduct.transform.localScale = Vector3.one * 1.25f;
            }
			newProduct.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
		}
    }

    public void DropMeat()
    {
        GameObject newProduct = Instantiate(meat);
        AddNewProduct(newProduct, "GrillIngredientClone", Camera.main.GetComponent<PositionManager>().MeatSpawnPosition().position);
        newProduct.AddComponent<Meat>();
    }

    public void DropTopBun()
    {
        GameObject newProduct = Instantiate(topBun);
        AddNewProduct(newProduct, "GrillIngredientClone", Camera.main.GetComponent<PositionManager>().TopBunSpawnPosition().position);
        newProduct.AddComponent<RemoveObjects>();
    }

    public void DropBottomBun()
    {
        GameObject newProduct = Instantiate(bottomBun);
        AddNewProduct(newProduct, "GrillIngredientClone", Camera.main.GetComponent<PositionManager>().BottomBunSpawnPosition().position);
        newProduct.AddComponent<RemoveObjects>();
    }

    public void DropFries()
    {
        if (GameObject.FindGameObjectsWithTag("Fries").Length <= 2)
        {
            GameObject newProduct = Instantiate(uncookedFries);
            AddNewProduct(newProduct, "Fries", Camera.main.GetComponent<PositionManager>().FriesPosition().position);
            newProduct.GetComponent<Rigidbody>().freezeRotation = true;
            newProduct.AddComponent<Fry>();
        }
    }

    public void DropBasket()
    {
        if (GameObject.FindGameObjectsWithTag("Basket").Length <= 2)
        {
            GameObject newProduct = Instantiate(friesBasket);
            AddNewProduct(newProduct, "Basket", Camera.main.GetComponent<PositionManager>().BasketPosition().position);
            newProduct.AddComponent<RemoveObjects>();
        }
    }

    public void DropCup()
    {
        if (GameObject.FindGameObjectsWithTag("Soda").Length <= 2)
        {
            GameObject newProduct = Instantiate(cup);
            AddNewProduct(newProduct, "Soda", Camera.main.GetComponent<PositionManager>().CupPosition().position);
            newProduct.GetComponent<Rigidbody>().freezeRotation = true;
            newProduct.AddComponent<SodaCup>();
        }
    }

    public void DropLid()
    {
        if (GameObject.FindGameObjectsWithTag("Lid").Length <= 2)
        {
            GameObject newProduct = Instantiate(lid);
            Vector3 position = Camera.main.GetComponent<PositionManager>().LidPosition().position + (Random.insideUnitSphere * 0.15f);
            Quaternion rotation = new Quaternion(Random.value, Random.value, Random.value, Random.value);
            AddNewProduct(newProduct, "Lid", position);
            newProduct.transform.rotation = rotation;
            newProduct.AddComponent<RemoveObjects>();
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
        newProduct.transform.position = newPosition;
        newProduct.tag = newTag;
        Physics.IgnoreCollision(grillWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(sodaWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(counterWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
        Physics.IgnoreCollision(fryerWall.GetComponent<Collider>(), newProduct.GetComponent<Collider>());
    }
}
