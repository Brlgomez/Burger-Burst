using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookMeat : MonoBehaviour
{
    bool onGrill;
    float timeOnGrill;
    float maxTimeOnGrill = 20;
    int maxAmountOfBurgers = 10;
    Color initialColor;
    Color cookedColor;
    Color burntColor;
    float r, g, b;
    float incR, incG, incB;
    float incR2, incG2, incB2;
    GameObject topBun, bottomBun;
    bool touchingTop, touchingBottom;
    ParticleSystem particleSyst;
    ParticleSystem.MainModule mainModule;

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
        initialColor = gameObject.GetComponent<Renderer>().material.color;
        cookedColor = new Color(0.480f, 0.613f, 0.266f);
        burntColor = new Color(cookedColor.r * 0.25f, cookedColor.g * 0.25f, cookedColor.b * 0.25f);
        r = initialColor.r;
        g = initialColor.g;
        b = initialColor.b;
        incR = (initialColor.r - cookedColor.r) * (1.0f / (maxTimeOnGrill / 2.0f));
        incG = (initialColor.g - cookedColor.g) * (1.0f / (maxTimeOnGrill / 2.0f));
        incB = (initialColor.b - cookedColor.b) * (1.0f / (maxTimeOnGrill / 2.0f));
        incR2 = (cookedColor.r - burntColor.r) * (1.0f / (maxTimeOnGrill / 2.0f));
        incG2 = (cookedColor.g - burntColor.g) * (1.0f / (maxTimeOnGrill / 2.0f));
        incB2 = (cookedColor.b - burntColor.b) * (1.0f / (maxTimeOnGrill / 2.0f));
        particleSyst = transform.GetChild(0).GetComponent<ParticleSystem>();
        mainModule = particleSyst.main;
    }

    void Update()
    {
        if (onGrill && timeOnGrill < maxTimeOnGrill)
        {
            timeOnGrill += Time.deltaTime;
            if (timeOnGrill > (maxTimeOnGrill * 0.25f) && !transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
            if (transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
            {
                float percentage = ((timeOnGrill - (maxTimeOnGrill * 0.25f)) / (maxTimeOnGrill - (maxTimeOnGrill * 0.25f)));
                Color newColor = Color.Lerp(Color.white, Color.black, percentage);
                mainModule.startColor = newColor;
            }
            if (timeOnGrill < maxTimeOnGrill / 2)
            {
                if (r > cookedColor.r)
                {
                    r -= Time.deltaTime * incR;
                }
                if (g > cookedColor.g)
                {
                    g -= Time.deltaTime * incG;
                }
                if (b > cookedColor.b)
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Grill Top")
        {
            if (timeOnGrill > (maxTimeOnGrill * 0.25f))
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
            onGrill = true;
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
    }

    public void PickedUp()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        onGrill = false;
        touchingTop = false;
        touchingBottom = false;
        topBun = null;
        bottomBun = null;
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
        float percantage = (((maxTimeOnGrill / 2) - (Mathf.Abs(timeOnGrill - (maxTimeOnGrill / 2)))) / (maxTimeOnGrill / 2));
        int worth = Mathf.RoundToInt(maxAmountOfBurgers * percantage);
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
        if (topBun.GetComponent<FadeObject>() == null)
        {
            topBun.AddComponent<FadeObject>();
            topBun.tag = "Fallen";
        }
        if (bottomBun.GetComponent<FadeObject>() == null)
        {
            bottomBun.AddComponent<FadeObject>();
            bottomBun.tag = "Fallen";
        }
        if (gameObject.GetComponent<FadeObject>() == null)
        {
            gameObject.AddComponent<FadeObject>();
            gameObject.tag = "Fallen";
        }
        Destroy(GetComponent<CookMeat>());
    }
}
