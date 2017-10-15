using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryFries : MonoBehaviour
{
    bool inFryer;
    float timeInFryer;
    int maxTimeInFryer = 20;
    Color initialColor;
    Color friedColor;
    Color burntColor;
    float r, g, b;
    float incR, incG, incB;
    float incR2, incG2, incB2;
    GameObject basket;

    void Start()
    {
        initialColor = gameObject.GetComponent<Renderer>().material.color;
        friedColor = new Color(0.977f, 0.875f, 0.727f);
        burntColor = new Color(0.5f, 0.375f, 0);
        r = initialColor.r;
        g = initialColor.g;
        b = initialColor.b;
        incR = (initialColor.r - friedColor.r) * (1.0f / (maxTimeInFryer / 2.0f));
        incG = (initialColor.g - friedColor.g) * (1.0f / (maxTimeInFryer / 2.0f));
        incB = (initialColor.b - friedColor.b) * (1.0f / (maxTimeInFryer / 2.0f));
        incR2 = (friedColor.r - burntColor.r) * (1.0f / (maxTimeInFryer / 2.0f));
        incG2 = (friedColor.g - burntColor.g) * (1.0f / (maxTimeInFryer / 2.0f));
        incB2 = (friedColor.b - burntColor.b) * (1.0f / (maxTimeInFryer / 2.0f));
    }

    void Update()
    {
        if (inFryer && timeInFryer < maxTimeInFryer)
        {
            timeInFryer += Time.deltaTime;
            if (timeInFryer < maxTimeInFryer / 2)
            {
                if (r > friedColor.r)
                {
                    r -= Time.deltaTime * incR;
                }
                if (g > friedColor.g)
                {
                    g -= Time.deltaTime * incG;
                }
                if (b > friedColor.b)
                {
                    b -= Time.deltaTime * incB;
                }
            }
            else
            {
                if (r > burntColor.r)
                {
                    r -= Time.deltaTime * incR2;
                }
                if (g > burntColor.g)
                {
                    g -= Time.deltaTime * incG2;
                }
                if (b > burntColor.b)
                {
                    b -= Time.deltaTime * incB2;
                }
            }
            gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Hot Oil")
        {
            inFryer = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Hot Oil")
        {
            inFryer = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Basket" && !inFryer)
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
        int worth = Mathf.RoundToInt((maxTimeInFryer / 2) - Mathf.Abs(timeInFryer - (maxTimeInFryer / 2)));
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

    public bool InFryer()
    {
        return inFryer;
    }
}
