using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour
{
    int neededBurgers, neededFries, neededDrinks;
    int amountOfBurgers, amountOfFries, amountOfDrinks;
    float timeForBonus = 0;
    float maxTimeOfBonus;
    float ratioOfTime;
    float costOfMeal;
    bool orderComplete;
    List<GameObject> onPlatter = new List<GameObject>();

    GameObject head, rightFoot, leftFoot, text;
    GameObject followRight, followLeft;
    bool left = false;
    bool leftComplete, rightComplete;
    bool firstMove = true;
    bool moving = false;

    void Start()
    {
        text = transform.GetChild(0).gameObject;
        followRight = transform.GetChild(1).gameObject;
        followLeft = transform.GetChild(2).gameObject;
        for (int i = 3; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Right_Foot")
            {
                rightFoot = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "Left_Foot")
            {
                leftFoot = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name == "Head")
            {
                head = transform.GetChild(i).gameObject;
            }
        }
        text.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
        text.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.green;
        WakeUp();
        SetOrder();
    }

    void Update()
    {
        if (moving)
        {
            //TODO: DECREASE TIMER
            text.transform.position = new Vector3(head.transform.position.x, text.transform.position.y, head.transform.position.z);
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        int xDirection = -1;
        if (!left && !leftComplete)
        {
            rightFoot.transform.position = Vector3.MoveTowards(
                rightFoot.transform.position, 
                followRight.transform.position, 
                Time.deltaTime * 4
            );
            if (Vector3.Distance(rightFoot.transform.position, followRight.transform.position) < 0.5f)
            {
                int stepLength = 0;
                if (firstMove)
                {
                    stepLength = 1 * xDirection;
                    firstMove = false;
                }
                else
                {
                    stepLength = 2 * xDirection;
                }
                Vector3 nextPosition = new Vector3(followLeft.transform.position.x + stepLength, followLeft.transform.position.y, followLeft.transform.position.z);
                if (followLeft.transform.position.x + stepLength < -9)
                {
                    leftComplete = true;
                }
                followLeft.transform.position = nextPosition;
                left = true;
            }
        }
        else if (left && !rightComplete)
        {
            leftFoot.transform.position = Vector3.MoveTowards(
                leftFoot.transform.position, 
                followLeft.transform.position, 
                Time.deltaTime * 4
            );
            if (Vector3.Distance(leftFoot.transform.position, followLeft.transform.position) < 0.5f)
            {
                int stepLength = 0;
                if (firstMove)
                {
                    stepLength = 1 * xDirection;
                    firstMove = false;
                }
                else
                {
                    stepLength = 2 * xDirection;
                }
                Vector3 nextPosition = new Vector3(followRight.transform.position.x + stepLength, followRight.transform.position.y, followRight.transform.position.z);
                if (followRight.transform.position.x + stepLength < -9)
                {
                    rightComplete = true;
                }
                followRight.transform.position = nextPosition;
                left = false;
            }
        }
        else if (rightComplete && leftComplete && !orderComplete)
        {
            //TODO: ADD PUNISHMENT FOR NOT A COMPLETE ORDER
            Camera.main.GetComponent<WaiterManager>().AddNewWaiter();
            Destroy(gameObject);
        }
        else if (rightComplete && leftComplete && orderComplete)
        {
            Camera.main.GetComponent<WaiterManager>().AddNewWaiter();
            Destroy(gameObject);
        }
    }

    void SetOrder()
    {
        //TODO: ADD WAY TO INCREASE MAX AMOUNT
        int maxAmountOfProduct = 2;
        int amountOfProduct;
        orderComplete = false;
        for (int i = 0; i < onPlatter.Count; i++)
        {
            Destroy(onPlatter[i]);
        }
        amountOfProduct = Random.Range(1, maxAmountOfProduct);
        if (amountOfProduct > 10)
        {
            amountOfProduct = 10;
        }
        neededBurgers = 0;
        neededFries = 0;
        neededDrinks = 0;
        for (int i = 0; i < amountOfProduct; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.33f)
            {
                neededBurgers++;
                continue;
            }
            else if (rand > 0.33f && rand < 0.66f)
            {
                neededFries++;
                continue;
            }
            else
            {
                neededDrinks++;
            }
        }
        timeForBonus = 5 + (neededBurgers * 5) + (neededFries * 5) + (neededDrinks * 5);
        maxTimeOfBonus = timeForBonus;
        text.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "Burger: " + neededBurgers + "\nDrink: " + neededDrinks + "\nFries: " + neededFries;
        text.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = timeForBonus.ToString("F1");
    }

    public void AddToPlatter(GameObject obj)
    {
        if (obj.tag == "Thrown" && !orderComplete)
        {
            Destroy(obj.GetComponent<Rigidbody>());
            if (obj.name == "Burger(Clone)")
            {
                amountOfBurgers++;
            }
            else if (obj.name == "Drink(Clone)")
            {
                amountOfDrinks++;
            }
            else if (obj.name == "Fries(Clone)")
            {
                amountOfFries++;
            }
            obj.tag = "OnPlatter";
            onPlatter.Add(obj);
            CheckOrder();
        }
        else if (obj.tag == "Thrown" && orderComplete)
        {
            Destroy(obj.GetComponent<Rigidbody>());
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(obj);
        }
    }

    void CheckOrder()
    {
        if (amountOfBurgers >= neededBurgers && amountOfFries >= neededFries && amountOfDrinks >= neededDrinks)
        {
            text.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "Thanks";
            orderComplete = true;
            CheckSentOrder();
        } 
    }

    void CheckSentOrder()
    {
        int tempBurger = 0; 
        int tempFries = 0; 
        int tempDrink = 0;
        costOfMeal = 0;
        for (int i = 0; i < onPlatter.Count; i++)
        {
            if (onPlatter[i] != null)
            {
                if (onPlatter[i].name == "Burger(Clone)")
                {
                    tempBurger++;
                    if (tempBurger <= neededBurgers)
                    {
                        costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(onPlatter[i]);
                    }
                    else
                    {
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(onPlatter[i]);
                    }
                }
                else if (onPlatter[i].name == "Drink(Clone)")
                {
                    tempDrink++;
                    if (tempDrink <= neededDrinks)
                    {
                        costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(onPlatter[i]);
                    }
                    else
                    {
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(onPlatter[i]);
                    }
                }
                else if (onPlatter[i].name == "Fries(Clone)")
                {
                    tempFries++;
                    if (tempFries <= neededFries)
                    {
                        costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(onPlatter[i]);
                    }
                    else
                    {
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(onPlatter[i]);
                    }
                }
            }
        }
        ratioOfTime = timeForBonus / maxTimeOfBonus;
        if (ratioOfTime > 0)
        {
            float tipAmount = ratioOfTime * costOfMeal * 0.5f;
            tipAmount = Mathf.Round(tipAmount * 100f) / 100f;
            Camera.main.GetComponent<Gameplay>().AddTip(gameObject, tipAmount);
        }
        if (ratioOfTime > 0.5f)
        {
            Camera.main.GetComponent<Gameplay>().AddLife(1);
        }
    }

    void WakeUp()
    {
        moving = true;
        for (int i = 3; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Rigidbody>() != null)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            }
            if (transform.GetChild(i).GetComponent<ConstantForce>() != null)
            {
                transform.GetChild(i).GetComponent<ConstantForce>().enabled = true;
            }
            if (transform.GetChild(i).GetComponent<Collider>() != null)
            {
                transform.GetChild(i).GetComponent<Collider>().enabled = true;
            }
        }
    }

    void Sleep()
    {
        moving = false;
        for (int i = 3; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Rigidbody>() != null)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = false;
                transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
            }
            if (transform.GetChild(i).GetComponent<ConstantForce>() != null)
            {
                transform.GetChild(i).GetComponent<ConstantForce>().enabled = false;
            }
        }
    }
}
