using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour
{
    int neededBurgers, neededFries, neededDrinks;
    int amountOfBurgers, amountOfFries, amountOfDrinks;
    bool orderReady;
    float timeForBonus = 0;
    float maxTimeOfBonus;
    float ratioOfTime;
    float costOfMeal;
    List<GameObject> onPlatter = new List<GameObject>();
    GameObject platter;

    Transform start;
    Transform end;
    List<GameObject> tablePositions = new List<GameObject>();
    List<GameObject> startPositions = new List<GameObject>();
    Transform current;
    NavMeshAgent agent;

    float timeToWin = 0.25f;
    float currentTimeToWin;

    void Start()
    {
        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.white;
        transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = Color.green;
        platter = transform.GetChild(0).gameObject;
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
        RestartPosition();
        StartPosition();
        SetOrder();
    }

    void Update()
    {
        if (current == start)
        {
            if (agent.velocity.magnitude < 0.1f)
            {
                if (timeForBonus > 0 && !Camera.main.GetComponent<Gameplay>().IsGameOver())
                {
                    timeForBonus -= Time.deltaTime;
                    transform.GetChild(2).gameObject.GetComponent<TextMesh>().text = timeForBonus.ToString("F1");
                }
                else
                {
                    timeForBonus = 0;
                    transform.GetChild(2).gameObject.GetComponent<TextMesh>().text = timeForBonus.ToString("F1");
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
        if (orderReady)
        {
            if (CheckRigidbodyVelocities())
            {
                currentTimeToWin += Time.deltaTime;
                if (currentTimeToWin > timeToWin)
                {
                    transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "Thanks!";
                    EndPosition();
                    StickFoodToTray();
                }
            }
            else
            {
                currentTimeToWin = 0;
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
        maxAmountOfProduct = Mathf.RoundToInt(Camera.main.GetComponent<Gameplay>().GetProfit());
        maxAmountOfProduct /= 10;
        if (maxAmountOfProduct < 2)
        {
            maxAmountOfProduct = 2;
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
        transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "Burger: " + neededBurgers + "\nDrink: " + neededDrinks + "\nFries: " + neededFries;
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
        orderReady = false;
        currentTimeToWin = 0;
    }

    void CheckOrder()
    {
        if (amountOfBurgers >= neededBurgers && amountOfFries >= neededFries && amountOfDrinks >= neededDrinks)
        {
            orderReady = true;
        }
        else
        {
            orderReady = false;
            currentTimeToWin = 0;
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
        Camera.main.GetComponent<ScreenTextManagment>().SetSecondScreenText("", Color.white);
        if (ratioOfTime > 0)
        {
            float tipAmount = ratioOfTime * costOfMeal * 0.5f;
            tipAmount = Mathf.Round(tipAmount * 100f) / 100f;
            Camera.main.GetComponent<Gameplay>().AddTip(gameObject, tipAmount);
        }
        if (ratioOfTime > 0.5f)
        {
            Camera.main.GetComponent<Gameplay>().AddLife(1);
            Camera.main.GetComponent<ScreenTextManagment>().SetSecondScreenText("Excellent!", Color.white);
        }
    }

    bool CheckRigidbodyVelocities()
    {
        for (int i = 0; i < onPlatter.Count; i++)
        {
            if (onPlatter[i] != null)
            {
                if (onPlatter[i].GetComponent<Rigidbody>().velocity.magnitude > 0.05f || !onPlatter[i].GetComponent<Rigidbody>().IsSleeping())
                {
                    return false;
                }
            }
        }
        return true;
    }

    void StickFoodToTray()
    {
        for (int i = 0; i < onPlatter.Count; i++)
        {
            if (onPlatter[i] != null)
            {
                onPlatter[i].GetComponent<Rigidbody>().isKinematic = true;
                onPlatter[i].transform.parent = platter.transform;
            }
        }
    }

    public void RestartPosition()
    {
        agent.ResetPath();
        transform.position = GameObject.Find("Waiter Position").transform.position;
        transform.rotation = GameObject.Find("Waiter Position").transform.rotation;
        transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "";
    }
}
