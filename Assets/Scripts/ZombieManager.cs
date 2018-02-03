using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    static int maxAmountOfZombies = 5;
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
    public Texture[] outfits;
    public Texture[] specialOutfits;
    public Texture healthOutfit;

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
            if (Random.value > 0.95f && GetComponent<Gameplay>().GetCompletedOrdersCount() > 5)
            {
                GetComponent<ZombieHeadManager>().CreateZombieHead();
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
        Texture newOutfit;
        amountOfZombies++;
        PickZombieSize();
        newOutfit = PickZombieOutfit();
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
            newOutfit
        );
        newZombie.tag = "Clone";
    }

    void PickZombieSize()
    {
        if (Random.value < GetComponent<Gameplay>().GetChanceOfDifSizedZombie() / 2)
        {
            zombie.transform.localScale = Vector3.one * 1.25f;
        }
        else if (Random.value < GetComponent<Gameplay>().GetChanceOfDifSizedZombie())
        {
            zombie.transform.localScale = Vector3.one * 0.75f;
        }
        else
        {
            zombie.transform.localScale = Vector3.one;
        }
    }

    Texture PickZombieOutfit()
    {
        Texture newOutfit;
        if (Random.value < GetComponent<Gameplay>().GetChanceOfSpecialZombie())
        {
            newOutfit = specialOutfits[Random.Range(0, specialOutfits.Length)];
        }
        else
        {
            if (Random.value > 0.95f && GetComponent<Gameplay>().GetMaxLife() > GetComponent<Gameplay>().GetLife())
            {
                newOutfit = healthOutfit;

            }
            else
            {
                newOutfit = outfits[Random.Range(0, outfits.Length)];
            }
        }
        return newOutfit;
    }

    public void RemoveWaiter(GameObject waiter)
    {
        amountOfZombies--;
        if (amountOfZombies == 0 && GetComponent<CarManager>().GetNumberOfZombieCars() == 0)
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
