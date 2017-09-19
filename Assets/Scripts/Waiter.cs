using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour
{
    int neededBurgers, neededFries, neededDrinks;
    int amountOfBurgers, amountOfFries, amountOfDrinks;
    int completedOrders;
    float timeForBonus = 0;
    float maxTimeOfBonus;
    float ratioOfTime;
    float costOfMeal;
    List<GameObject> onPlatter = new List<GameObject>();

    Transform start;
    Transform end;
    List<GameObject> tablePositions = new List<GameObject>();
    List<GameObject> startPositions = new List<GameObject>();
    Transform current;
    NavMeshAgent agent;

    GameObject head, rightFoot, leftFoot;
    GameObject followRight, followLeft;
    bool left = false;
    bool leftComplete, rightComplete;

    void Start()
    {
        head = GameObject.Find("Head");
        rightFoot = GameObject.Find("Right_Foot");
        leftFoot = GameObject.Find("Left_Foot");
        followLeft = GameObject.Find("Follow Left Foot");
        followRight = GameObject.Find("Follow Right Foot");
        head.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
        head.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.green;
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Table");
        for (int i = 0; i < temp.Length; i++)
        {
            tablePositions.Add(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Start");
        for (int i = 0; i < temp.Length; i++)
        {
            startPositions.Add(temp[i]);
        }
        agent = GetComponent<NavMeshAgent>();
        //RestartPosition();
        //StartPosition();
        SetOrder();
    }

    void Update()
    {
        MoveToCashier();
        /*
        if (current == start)
        {
            if (agent.velocity.magnitude < 0.1f)
            {
                if (timeForBonus > 0 && !Camera.main.GetComponent<Gameplay>().IsGameOver())
                {
                    timeForBonus -= Time.deltaTime;
                    head.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = timeForBonus.ToString("F1");
                }
                else
                {
                    timeForBonus = 0;
                    head.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = timeForBonus.ToString("F1");
                }
                Vector3 lookPos = Camera.main.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
            }
        }
        else if (current == end)
        {
            if (Vector3.Distance(transform.position, end.position) < 3.5f)
            {
                StartPosition();
                SetOrder();
            }
        }
        */
    }

    void MoveToCashier () 
    {
        if (!left && !rightComplete)
        {
            rightFoot.transform.position = Vector3.MoveTowards(
                rightFoot.transform.position, 
                followRight.transform.position, 
                Time.deltaTime * 2
            );
            if (Vector3.Distance(rightFoot.transform.position, followRight.transform.position) < 0.1f)
            {
                int num = 0;
                if (Mathf.Abs(followLeft.transform.position.z - -2) < 1.25f)
                {
                    num = -1;
                }
                else
                {
                    num = -2;
                }
                followLeft.transform.position = new Vector3(followLeft.transform.position.x, followLeft.transform.position.y, followLeft.transform.position.z + num);       
                left = true;
                if (followRight.transform.position.z == -2)
                {
                    rightComplete = true;
                }
            }
        }
        else if (left && !leftComplete)
        {
            leftFoot.transform.position = Vector3.MoveTowards(
                leftFoot.transform.position, 
                followLeft.transform.position, 
                Time.deltaTime * 2
            );
            if (Vector3.Distance(leftFoot.transform.position, followLeft.transform.position) < 0.1f)
            {
                int num = 0;
                if (Mathf.Abs(followRight.transform.position.z - -2) < 1.25f)
                {
                    num = -1;
                }
                else
                {
                    num = -2;
                }
                followRight.transform.position = new Vector3(followRight.transform.position.x, followRight.transform.position.y, followRight.transform.position.z + num);
                left = false;
                if (followLeft.transform.position.z == -2)
                {
                    leftComplete = true;
                }
            }
        }
    }

    void SetOrder()
    {
        int maxAmountOfProduct;
        int amountOfProduct;
        CheckSentOrder();
        for (int i = 0; i < onPlatter.Count; i++)
        {
            Destroy(onPlatter[i]);
        }
        RestartCurrentPlatter();
        maxAmountOfProduct = (completedOrders/3) + 2;
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
        head.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "Burger: " + neededBurgers + "\nDrink: " + neededDrinks + "\nFries: " + neededFries;
        head.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = timeForBonus.ToString("F1");
        completedOrders += 1;
    }

    void EndPosition()
    {
        end = tablePositions[Random.Range(0, tablePositions.Count)].transform;
        current = end;
        agent.destination = current.position;
    }

    public void StartPosition()
    {
        start = startPositions[Random.Range(0, startPositions.Count)].transform;
        current = start;
        agent.destination = current.position;
    }

    public void AddToPlatter(GameObject obj)
    {
        if (obj.tag == "Thrown")
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
    }

    public void RemoveFromPlatter(GameObject obj)
    {
        if (obj.name == "Burger(Clone)")
        {
            amountOfBurgers--;
        }
        else if (obj.name == "Drink(Clone)")
        {
            amountOfDrinks--;
        }
        else if (obj.name == "Fries(Clone)")
        {
            amountOfFries--;
        }
        obj.tag = "Thrown";
        onPlatter.Remove(obj);
        CheckOrder();
    }

    void RestartCurrentPlatter()
    {
        onPlatter.Clear();
        amountOfBurgers = 0;
        amountOfDrinks = 0;
        amountOfFries = 0;
    }

    public void RestartPosition()
    {
        agent.ResetPath();
        transform.position = GameObject.Find("Waiter Position").transform.position;
        transform.rotation = GameObject.Find("Waiter Position").transform.rotation;
        head.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "";
        head.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "";
    }

    void CheckOrder()
    {
        if (amountOfBurgers >= neededBurgers && amountOfFries >= neededFries && amountOfDrinks >= neededDrinks)
        {
            EndPosition();
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
}
