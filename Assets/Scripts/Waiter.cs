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

    float speed = 2;

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
        GetComponent<Animator>().Play("Walking");
        GetComponent<Animator>().SetFloat("Speed", speed / 4);
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
        Vector3 camPosition = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, camPosition, Time.deltaTime * speed);
        if (transform.position.z < 0) 
        {
            GetComponent<Animator>().SetFloat("Speed", speed / 4);
			speed -= Time.deltaTime * 3;
        }
        if (speed < 0) {
            speed = 0;
            GetComponent<Animator>().SetFloat("Speed", speed / 4);
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
            if (obj.name == "Burger(Clone)")
            {
                amountOfBurgers++;
                if (amountOfBurgers > neededBurgers)
                {
                    obj.transform.parent = null;
                }
                else
                {
                    AddToBody(obj);
                }
            }
            else if (obj.name == "Drink(Clone)")
            {
                amountOfDrinks++;
                if (amountOfDrinks > neededDrinks)
                {
                    obj.transform.parent = null;
                }
                else
                {
                    AddToBody(obj);
                }
            }
            else if (obj.name == "Fries(Clone)")
            {
                amountOfFries++;
                if (amountOfFries > neededFries)
                {
                    obj.transform.parent = null;
                }
                else
                {
                    AddToBody(obj);
                }
            }
        }
        else if (obj.tag == "Thrown" && orderComplete)
        {
            Destroy(obj.GetComponent<Rigidbody>());
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(obj);
        }
    }

    void AddToBody(GameObject obj) {
		Destroy(obj.GetComponent<Rigidbody>());
		Destroy(obj.GetComponent<RemoveObjects>());
		costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(obj);
		obj.tag = "OnPlatter";
		onPlatter.Add(obj);
		CheckOrder();
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
            transform.GetChild(i).GetComponent<Rigidbody>().mass *= 0.1f;
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
