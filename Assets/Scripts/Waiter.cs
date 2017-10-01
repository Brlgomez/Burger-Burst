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

    GameObject head, rightFoot, leftFoot, thinkBubble;
    GameObject followRight, followLeft;
    bool left = false;
    bool leftComplete, rightComplete;
    bool firstMove = true;
    bool moving = false;

    float speed = 3;

    int maxDeathTime = 1;
    float alpha = 1;

    int timeForDamage = 3;
    float damageTime = 0;
    Vector3[] availableSpritePositions;

    void Start()
    {
        availableSpritePositions = new Vector3[3];
        availableSpritePositions[0] = new Vector3(0, -0.25f, 0.5f);
        availableSpritePositions[1] = new Vector3(-0.5f, 1, 0.4f);
        availableSpritePositions[2] = new Vector3(0.5f, 1, 0.3f);
        thinkBubble = transform.GetChild(0).gameObject;
        thinkBubble.AddComponent<IncreaseSize>();
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
        WakeUp();
        SetOrder();
    }

    void Update()
    {
        thinkBubble.transform.position = new Vector3(head.transform.position.x, thinkBubble.transform.position.y, head.transform.position.z);
        if (moving && !orderComplete)
        {
            Walk();
        }
        else if (orderComplete)
        {
            alpha -= Time.deltaTime;
            transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                transform.GetChild(0).GetChild(i).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
            }
            for (int i = 3; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
            }
            if (alpha < 0.01f)
            {
                Camera.main.GetComponent<WaiterManager>().RemoveWaiter(gameObject);
            }
        }
        else if (!moving && !orderComplete)
        {
            damageTime += Time.deltaTime;
            if (damageTime > timeForDamage)
            {
                damageTime = 0;
                Camera.main.GetComponent<Gameplay>().DeductNumberOfErrors();
            }
        }
    }

    void Walk()
    {
        int xDirection = 1;
        if (!left && !leftComplete)
        {
            rightFoot.transform.localPosition = Vector3.MoveTowards(
                rightFoot.transform.localPosition,
                followRight.transform.localPosition,
                Time.deltaTime * speed
            );
            if (Vector3.Distance(rightFoot.transform.localPosition, followRight.transform.localPosition) < 1)
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
                Vector3 nextPosition = new Vector3(
                    followLeft.transform.localPosition.x,
                    followLeft.transform.localPosition.y, 
                    followLeft.transform.localPosition.z + stepLength
                );
                if (Mathf.Abs(followLeft.transform.position.z - Camera.main.transform.position.z) < 4)
                {
                    leftComplete = true;
                }
                followLeft.transform.localPosition = nextPosition;
                left = true;
            }
        }
        else if (left && !rightComplete)
        {
            leftFoot.transform.localPosition = Vector3.MoveTowards(
                leftFoot.transform.localPosition,
                followLeft.transform.localPosition,
                Time.deltaTime * speed
            );
            if (Vector3.Distance(leftFoot.transform.localPosition, followLeft.transform.localPosition) < 1)
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
                Vector3 nextPosition = new Vector3(
                    followRight.transform.localPosition.x,
                    followRight.transform.localPosition.y, 
                    followRight.transform.localPosition.z + stepLength
                );
                if (Mathf.Abs(followRight.transform.position.z - Camera.main.transform.position.z) < 4)
                {
                    rightComplete = true;
                }
                followRight.transform.localPosition = nextPosition;
                left = false;
            }
        }
        else if (rightComplete && leftComplete)
        {
            Camera.main.GetComponent<Gameplay>().DeductNumberOfErrors();
            moving = false;
        }
    }

    void SetOrder()
    {
        int maxAmountOfProduct = Mathf.CeilToInt(Camera.main.GetComponent<Gameplay>().GetCompletedOrdersCount() / 5) + 1;
        int amountOfProduct;
        orderComplete = false;
        costOfMeal = 0;
        amountOfProduct = Random.Range(1, maxAmountOfProduct);
        if (amountOfProduct > 5)
        {
            amountOfProduct = 5;
        }
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
        SetUpSprites();
        timeForBonus = 5 + (neededBurgers * 5) + (neededFries * 5) + (neededDrinks * 5);
        maxTimeOfBonus = timeForBonus;
    }

    void SetUpSprites()
    {
        int spritePosition = 0;
        if (neededBurgers > 0)
        {
            GameObject sprite = Instantiate(Camera.main.GetComponent<WaiterManager>().burgers[neededBurgers - 1], thinkBubble.transform);
            sprite.transform.localPosition = availableSpritePositions[spritePosition];
            spritePosition++;

        }
        if (neededFries > 0)
        {
            GameObject sprite = Instantiate(Camera.main.GetComponent<WaiterManager>().fries[neededFries - 1], thinkBubble.transform);
            sprite.transform.localPosition = availableSpritePositions[spritePosition];
            spritePosition++;
        }
        if (neededDrinks > 0)
        {
            GameObject sprite = Instantiate(Camera.main.GetComponent<WaiterManager>().drinks[neededDrinks - 1], thinkBubble.transform);
            sprite.transform.localPosition = availableSpritePositions[spritePosition];
            spritePosition++;
        }
    }

    public void AddToPlatter(GameObject obj)
    {
        if (obj.tag == "Thrown" && !orderComplete)
        {
            Destroy(obj.GetComponent<Rigidbody>());
            if (obj.name == "Burger(Clone)")
            {
                amountOfBurgers++;
                if (amountOfBurgers > neededBurgers)
                {
                    Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(obj);
                }
                else
                {
                    costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(obj);
                }
            }
            else if (obj.name == "Drink(Clone)")
            {
                amountOfDrinks++;
                if (amountOfDrinks > neededDrinks)
                {
                    Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(obj);
                }
                else
                {
                    costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(obj);
                }
            }
            else if (obj.name == "Fries(Clone)")
            {
                amountOfFries++;
                if (amountOfFries > neededFries)
                {
                    Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(obj);
                }
                else
                {
                    costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(obj);
                }
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
            Camera.main.GetComponent<Gameplay>().IncreaseCompletedOrders();
            orderComplete = true;
            TurnOffForces();
            //CheckTip();
        }
    }


    void CheckTip()
    {
        ratioOfTime = timeForBonus / maxTimeOfBonus;
        if (ratioOfTime > 0)
        {
            float tipAmount = ratioOfTime * costOfMeal * 0.5f;
            tipAmount = Mathf.Round(tipAmount * 100f) / 100f;
            Camera.main.GetComponent<Gameplay>().AddTip(head, tipAmount);
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

    void TurnOffForces()
    {
        for (int i = 3; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 11;
            transform.GetChild(i).GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().zombieClear;
            if (transform.GetChild(i).GetComponent<ConstantForce>() != null)
            {
                transform.GetChild(i).GetComponent<ConstantForce>().enabled = false;
            }
        }
        for (int i = 0; i < onPlatter.Count; i++)
        {
            onPlatter[i].gameObject.layer = 11;
            onPlatter[i].AddComponent<FadeObject>();
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
