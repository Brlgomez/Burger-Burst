using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fry : MonoBehaviour
{
    float maxTimeInFryer = 20;
    int maxAmountOfFries = 10;
    float timeInFryer;
    GameObject basket;

    void Start()
    {
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(PowerUpsManager.quickerCooking))
        {
            maxTimeInFryer *= 0.75f;
        }
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(PowerUpsManager.makeMoreFood))
        {
            maxAmountOfFries = 12;
        }
    }

    public void SetTimeInFryer(float time)
    {
        timeInFryer = time;
    }

    public float GetTimeInFryer()
    {
        return timeInFryer;
    }

    public float GetMaxTimeInFryer()
    {
        return maxTimeInFryer;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Hot Oil")
        {
            if (GetComponent<FryFries>() == null)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                gameObject.AddComponent<FryFries>();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Hot Oil")
        {
            if (GetComponent<FryFries>())
            {
                timeInFryer = GetComponent<FryFries>().GetTimeInFryer();
                transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                Destroy(GetComponent<FryFries>());
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Basket" && GetComponent<FryFries>() == null)
        {
            basket = collision.gameObject;
            if (basket.transform.parent != null && gameObject.transform.parent != null)
            {
                gameObject.transform.parent = null;
                basket.transform.parent = null;
                basket.GetComponent<Rigidbody>().isKinematic = true;
                basket.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                FriesCompleted();
            }
        }
    }

    void FriesCompleted()
    {
        float percentage = (((maxTimeInFryer / 2) - (Mathf.Abs(timeInFryer - (maxTimeInFryer / 2)))) / (maxTimeInFryer / 2));
        int worth = Mathf.RoundToInt(maxAmountOfFries * percentage);
        if (worth == 0)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.gray, 1);
        }
        else if (worth == 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fry", Color.green, 1);
        }
        else if (worth > 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.green, 1);
        }
        Camera.main.GetComponent<Gameplay>().AddFries(worth);
        if (gameObject.GetComponent<FadeObject>() == null)
        {
            Camera.main.GetComponent<DropMoreProducts>().DropFries();
            Camera.main.GetComponent<DropMoreProducts>().DropBasket();
            gameObject.AddComponent<FadeObject>();
            basket.AddComponent<FadeObject>();
        }
        Destroy(gameObject.GetComponent<FryFries>());
    }
}
