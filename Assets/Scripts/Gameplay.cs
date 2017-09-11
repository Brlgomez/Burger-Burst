using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    int numberOfLostProductsAllowed = 10;
    float costToMakeBurger = 2.48f;
    float costToMakeFries = 1.98f;
    float costToMakeDrink = 1.48f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    float profit;
    bool gameOver = false;
    GameObject profitText;
    GameObject mistakeText;
    Color originalScreenColor;
    GameObject aftermathText;

    void Start()
    {
        originalScreenColor = new Color(0.117f, 0.445f, 0.773f);
        profitText = GameObject.Find("ProfitText");
        mistakeText = GameObject.Find("MistakeText");
        profitText.GetComponent<TextMesh>().text = "$0.00";
        mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n" + numberOfLostProductsAllowed.ToString();
        mistakeText.GetComponent<Renderer>().material.color = originalScreenColor;
        profitText.GetComponent<Renderer>().material.color = originalScreenColor;
        aftermathText = GameObject.Find("Aftermath Text");
    }

    public void IncreaseNumberOfLostProduct(GameObject obj)
    {
        numberOfLostProductsAllowed--;
        float cost = 0;
        if (obj.name == "Burger(Clone)")
        {
            cost = costToMakeBurger;
        }
        else if (obj.name == "Drink(Clone)")
        {
            cost = costToMakeDrink;
        }
        else if (obj.name == "Fries(Clone)")
        {
            cost = costToMakeFries;
        }
        profit -= cost;
        profit = Mathf.Round(profit * 100f) / 100f;
        profitText.GetComponent<TextMesh>().text = "$" + profit.ToString("F2");
        mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n" + numberOfLostProductsAllowed.ToString();
        mistakeText.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, originalScreenColor, (float)numberOfLostProductsAllowed/10);
        if (numberOfLostProductsAllowed < 0)
        {
            mistakeText.GetComponent<TextMesh>().text = "YOU'RE\nFIRED!";
            gameOver = true;
        }
        if (profit < 0)
        {
            profitText.GetComponent<Renderer>().material.color = Color.red;
        }
        GameObject newAftermathText = Instantiate(aftermathText);
        newAftermathText.GetComponent<Renderer>().material.color = Color.red;
        newAftermathText.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 1, obj.transform.position.z);
        newAftermathText.AddComponent<AftermathText>().GetComponent<AftermathText>().updateText("$-" + cost.ToString());
    }

    public void IncreaseNumberOfSentProduct(GameObject obj)
    {
        float cost = 0;
        if (obj.name == "Burger(Clone)")
        {
            cost = costOfBurger - costToMakeBurger;
        }
        else if (obj.name == "Drink(Clone)")
        {
            cost = costOfFries - costToMakeFries;
        }
        else if (obj.name == "Fries(Clone)")
        {
            cost = costOfDrink - costToMakeDrink;
        }
        profit += cost;
        profit = Mathf.Round(profit * 100f) / 100f;
        profitText.GetComponent<TextMesh>().text = "$" + profit.ToString("F2");
        if (profit > 0)
        {
            profitText.GetComponent<Renderer>().material.color = originalScreenColor;
        }
        GameObject newAftermathText = Instantiate(aftermathText);
        newAftermathText.GetComponent<Renderer>().material.color = Color.green;
        newAftermathText.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + Random.Range(0.0f, 1.0f), obj.transform.position.z);
        newAftermathText.AddComponent<AftermathText>().GetComponent<AftermathText>().updateText("$" + cost.ToString());
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddLife(int n) {
        numberOfLostProductsAllowed += n;
        mistakeText.GetComponent<TextMesh>().text = "Errors Left:\n" + numberOfLostProductsAllowed.ToString();
        mistakeText.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, originalScreenColor, (float)numberOfLostProductsAllowed/10);
    }
}
