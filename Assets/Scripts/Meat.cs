using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    float maxTimeOnGrill = 20;
    int maxAmountOfBurgers = 10;
    float timeOnGrill;
    bool touchingTop, touchingBottom;
    GameObject topBun, bottomBun;

    void Start()
    {
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(PowerUpsManager.quickerCooking))
        {
            maxTimeOnGrill *= 0.75f;
        }
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(PowerUpsManager.makeMoreFood))
        {
            maxAmountOfBurgers = 12;
        }
    }

    public void AddTimeOnGrill(float time)
    {
        timeOnGrill += time;
    }

    public float GetTimeOnGrill()
    {
        return timeOnGrill;
    }

    public float GetMaxTimeOnGrill()
    {
        return maxTimeOnGrill;
    }

    public void PickedUp()
    {
        touchingTop = false;
        touchingBottom = false;
        topBun = null;
        bottomBun = null;
        transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        if (GetComponent<CookMeat>())
        {
            Destroy(GetComponent<CookMeat>());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Grill Top" && timeOnGrill < maxTimeOnGrill)
        {
            gameObject.AddComponent<CookMeat>();
        }
        if (collision.gameObject.name == "Top_Bun(Clone)" && !touchingTop)
        {
            if (CheckDistance(collision.gameObject))
            {
                touchingTop = true;
                topBun = collision.gameObject;
            }
        }
        if (collision.gameObject.name == "Bottom_Bun(Clone)" && !touchingBottom)
        {
            if (CheckDistance(collision.gameObject))
            {
                touchingBottom = true;
                bottomBun = collision.gameObject;
            }
        }
        if (touchingTop && touchingBottom)
        {
            BurgerCompleted();
        }
        Vector3 grillRange = Camera.main.GetComponent<PositionManager>().GrillRange().position;
        if (Vector3.Distance(gameObject.transform.position, grillRange) > 1.25f)
        {
            RemoveObject(gameObject);
            Destroy(GetComponent<Meat>());
        }
    }

    bool CheckDistance(GameObject obj)
    {
        if (Vector3.Distance(gameObject.transform.position, obj.transform.position) < 0.1f)
        {
            return true;
        }
        return false;
    }
    void BurgerCompleted()
    {
        float percentage = (((maxTimeOnGrill / 2) - (Mathf.Abs(timeOnGrill - (maxTimeOnGrill / 2)))) / (maxTimeOnGrill / 2));
        int worth = Mathf.RoundToInt(maxAmountOfBurgers * percentage);
        if (worth == 0)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.gray, 1);
        }
        else if (worth == 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burger", Color.green, 1);
        }
        else if (worth > 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.green, 1);
        }
        Camera.main.GetComponent<Gameplay>().AddBurgers(worth);
        RemoveObject(topBun);
        RemoveObject(bottomBun);
        RemoveObject(gameObject);
        Destroy(GetComponent<CookMeat>());
        Destroy(GetComponent<Meat>());
    }

    void RemoveObject(GameObject obj)
    {
        obj.tag = "Fallen";
        if (obj.GetComponent<FadeObject>() == null)
        {
            obj.AddComponent<FadeObject>();
        }
    }
}
