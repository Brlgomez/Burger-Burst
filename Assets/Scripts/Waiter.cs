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
    List<GameObject> onPlatter = new List<GameObject>();
    GameObject platter;

    Transform start;
    Transform end;
    List<GameObject> tablePositions = new List<GameObject>();
    List<GameObject> startPositions = new List<GameObject>();
    Transform current;
    NavMeshAgent agent;

    float currentTimeToWin;
    float timeToWin = 0.5f;

    void Start()
    {
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
                if (timeForBonus > 0)
                {
                    timeForBonus -= Time.deltaTime;
                }
                else
                {
                    timeForBonus = 0;
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
        CheckSentOrder();
        for (int i = 0; i < onPlatter.Count; i++)
        {
            Destroy(onPlatter[i]);
        }
        RestartCurrentPlatter();
        neededBurgers = Random.Range(0, 3);
        neededFries = Random.Range(0, 3);
        neededDrinks = Random.Range(0, 3);
        timeForBonus = (neededBurgers * 5) + (neededFries * 5) + (neededDrinks * 5);
        if (neededBurgers + neededDrinks + neededFries == 0)
        {
            neededBurgers = 1;
        }
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
        for (int i = 0; i < onPlatter.Count; i++)
        {
            if (onPlatter[i] != null)
            {
                if (onPlatter[i].name == "Burger(Clone)")
                {
                    tempBurger++;
                    if (tempBurger <= neededBurgers)
                    {
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(onPlatter[i]);
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
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(onPlatter[i]);
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
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(onPlatter[i]);
                    }
                    else
                    {
                        Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(onPlatter[i]);
                    }
                }
            }
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
