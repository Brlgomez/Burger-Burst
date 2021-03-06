﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    float maxTimeOnGrill = 20;
    int maxAmountOfBurgers = 10;
    float timeOnGrill;
    bool touchingTop, touchingBottom;
    bool justPlayedSound;
    GameObject topBun, bottomBun;

    void Start()
    {
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().quickerCooking.powerUpNumber))
        {
            maxTimeOnGrill *= 0.5f;
        }
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().makeMoreFood.powerUpNumber))
        {
            maxAmountOfBurgers = 15;
        }
    }

    public void SetTimeOnGrill(float time)
    {
        timeOnGrill = time;
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
        Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -2);
        if (GetComponent<CookMeat>())
        {
            timeOnGrill = GetComponent<CookMeat>().GetTimeOnGrill();
            Destroy(GetComponent<CookMeat>());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!justPlayedSound)
        {
            float impactSpeed = GetComponent<Rigidbody>().velocity.magnitude;
            Camera.main.GetComponent<SoundAndMusicManager>().PlayDropPattySound(gameObject, (impactSpeed / 10));
            JustPlayedSound();
        }
        if (touchingTop && topBun == null)
        {
            touchingTop = false;
        }
        if (touchingBottom && bottomBun == null)
        {
            touchingBottom = false;
        }
        if (collision.gameObject.name == "Grill Top" && timeOnGrill < maxTimeOnGrill && collision.gameObject.tag != "Fallen")
        {
            if (GetComponent<CookMeat>() == null)
            {
                gameObject.AddComponent<CookMeat>();
            }
            Camera.main.GetComponent<SoundAndMusicManager>().PlayLoopFromSourceAndRaiseVolume(gameObject, 2, 1);
            Camera.main.GetComponent<SoundAndMusicManager>().PlaySteamSound(gameObject);
        }
        if (collision.gameObject.name == "Top_Bun(Clone)" && !touchingTop && collision.gameObject.tag != "Fallen")
        {
            if (!TouchingBottom(collision))
            {
                touchingTop = true;
                topBun = collision.gameObject;
            }
        }
        if (collision.gameObject.name == "Bottom_Bun(Clone)" && !touchingBottom && collision.gameObject.tag != "Fallen")
        {
            if (TouchingBottom(collision))
            {
                touchingBottom = true;
                bottomBun = collision.gameObject;
            }
        }
        if (touchingTop && touchingBottom && bottomBun != null && topBun != null && bottomBun.tag != "Fallen" && topBun.tag != "Fallen")
        {
            BurgerCompleted();
        }
        CheckRange();
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Bottom_Bun(Clone)" && touchingBottom)
        {
            touchingBottom = false;
            bottomBun = null;
        }
    }

    bool TouchingBottom(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                return true;
            }
        }
        return false;
    }

    void BurgerCompleted()
    {
        if (GetComponent<CookMeat>())
        {
            timeOnGrill = GetComponent<CookMeat>().GetTimeOnGrill();
        }
        float percentage = (((maxTimeOnGrill / 2) - (Mathf.Abs(timeOnGrill - (maxTimeOnGrill / 2)))) / (maxTimeOnGrill / 2));
        int worth = Mathf.RoundToInt(maxAmountOfBurgers * percentage);
        if (worth == 0)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.gray, 1);
            Camera.main.GetComponent<SoundAndMusicManager>().PlayBadFoodSound(gameObject);
        }
        else if (worth == 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burger", Color.green, 1);
        }
        else if (worth > 1)
        {
            if (worth == maxAmountOfBurgers)
            {
                Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.yellow, 1);
            }
            else
            {
                Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.green, 1);
            }
        }
        if (worth > 0)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayFoodCompleteSound(gameObject);
        }
        if (worth == maxAmountOfBurgers)
        {
            Camera.main.GetComponent<OnlineManagement>().PerfectFoodItem();
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

    void CheckRange()
    {
        Vector3 grillRange = Camera.main.GetComponent<PositionManager>().GrillRange().position;
        if (Vector3.Distance(gameObject.transform.position, grillRange) > 1.25f)
        {
            RemoveObject(gameObject);
            Destroy(GetComponent<Meat>());
        }
    }

    public void JustPlayedSound()
    {
        justPlayedSound = true;
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        justPlayedSound = false;
    }
}
