using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    static int maxAmountOfZombies = 10;
    float time;

    GameObject zombie;
    Vector3 gameplayPosition;
    int amountOfZombies;
    public GameObject thinkBubble;
    public Sprite[] burgers;
    public Sprite[] fries;
    public Sprite[] drinks;
    public Mesh[] hair;
    public Mesh[] forearm;
    public Mesh[] hands;
    public Mesh[] feet;
    public Mesh[] legs;
    public Mesh[] rightUpperArms;
    public Mesh[] leftUpperArms;
    public Mesh[] thighs;
    public Mesh[] heads;
    public Mesh[] lowerBody;
    public Mesh[] upperBody;
    public Texture[] outfit;

    void Start()
    {
        gameplayPosition = GetComponent<PositionManager>().GameplayPosition().position;
        zombie = GetComponent<ObjectManager>().Zombie();
        time = GetComponent<Gameplay>().GetTimeForNewZombie();
    }

    public void ZombieUpdate(int updateInterval)
    {
        time += Time.deltaTime * updateInterval;
        if (time > GetComponent<Gameplay>().GetTimeForNewZombie())
        {
            if (GetCount() < maxAmountOfZombies && !GetComponent<Gameplay>().IsGameOver())
            {
                GetComponent<CarManager>().CreateNewCarWithZombie();
                if (Random.value < 0.25f)
                {
                    GetComponent<CarManager>().CreateNewCarWithNoZombie();
                }
            }
            time = 0;
        }
    }

    public void Print()
    {
        Debug.Log(GetCount());
        Debug.Log(time);
        Debug.Log(GetComponent<Gameplay>().IsGameOver());
    }

    public void AddNewZombie(Vector3 position)
    {
        amountOfZombies++;
        if (Random.value < 0.05f)
        {
            zombie.transform.localScale = Vector3.one * 1.25f;
        }
        else if (Random.value < 0.1f)
        {
            zombie.transform.localScale = Vector3.one * 0.75f;
        }
        else
        {
            zombie.transform.localScale = Vector3.one;
        }
        GameObject newZombie = Instantiate(zombie);
        newZombie.transform.position = position;
        newZombie.transform.LookAt(gameplayPosition);
        newZombie.transform.eulerAngles = new Vector3(0, newZombie.transform.eulerAngles.y, newZombie.transform.eulerAngles.z);
        newZombie.AddComponent<Zombie>().SetZombie(
            1,
            hair[Random.Range(0, hair.Length)],
            forearm[Random.Range(0, forearm.Length)],
            forearm[Random.Range(0, forearm.Length)],
            hands[Random.Range(0, hands.Length)],
            hands[Random.Range(0, hands.Length)],
            feet[Random.Range(0, feet.Length)],
            feet[Random.Range(0, feet.Length)],
            legs[Random.Range(0, legs.Length)],
            legs[Random.Range(0, legs.Length)],
            rightUpperArms[Random.Range(0, rightUpperArms.Length)],
            leftUpperArms[Random.Range(0, leftUpperArms.Length)],
            thighs[Random.Range(0, thighs.Length)],
            thighs[Random.Range(0, thighs.Length)],
            heads[Random.Range(0, heads.Length)],
            lowerBody[Random.Range(0, lowerBody.Length)],
            upperBody[Random.Range(0, upperBody.Length)],
            outfit[Random.Range(0, outfit.Length)]
        );
        newZombie.tag = "Clone";
    }

    public void RemoveWaiter(GameObject waiter)
    {
        amountOfZombies--;
        if (amountOfZombies == 0)
        {
            time = GetComponent<Gameplay>().GetTimeForNewZombie();
        }
        Destroy(waiter);
    }

    public int GetCount()
    {
        return amountOfZombies;
    }

    public void DeleteAllScripts()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject obj in clones)
        {
            if (obj.GetComponent<Zombie>() != null)
            {
                obj.GetComponent<Zombie>().DestroyScript();
            }
        }
    }

    public void ResetValues()
    {
        amountOfZombies = 0;
        time = GetComponent<Gameplay>().GetTimeForNewZombie();
    }
}
