using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookMeat : MonoBehaviour
{
    bool onGrill;
    float timeOnGrill;
    int maxTimeOnGrill = 20;
    Color initialColor;
    Color cookedColor;
    Color burntColor;
    float iniR, iniG, iniB;
    GameObject topBun, bottomBun;
    bool touchingTop, touchingBottom;

    void Start()
    {
        initialColor = gameObject.GetComponent<Renderer>().material.color;
        cookedColor = new Color(0.480f, 0.613f, 0.266f);
        burntColor = new Color(cookedColor.r * 0.25f, cookedColor.g * 0.25f, cookedColor.b * 0.25f);
        iniR = initialColor.r;
        iniG = initialColor.g;
        iniB = initialColor.b;
    }

    void Update()
    {
        if (onGrill && timeOnGrill < maxTimeOnGrill)
        {
            timeOnGrill += Time.deltaTime;
            if (timeOnGrill < maxTimeOnGrill / 2)
            {
                if (iniR > cookedColor.r)
                {
                    iniR -= Time.deltaTime / 20;
                }
                if (iniG > cookedColor.g)
                {
                    iniG -= Time.deltaTime / 15;
                }
                if (iniB > cookedColor.b)
                {
                    iniB -= Time.deltaTime / 10;
                }
            }
            else
            {
                if (iniR > burntColor.r)
                {
                    iniR -= Time.deltaTime / 25;
                }
                if (iniG > burntColor.g)
                {
                    iniG -= Time.deltaTime / 20;
                }
                if (iniB > burntColor.b)
                {
                    iniB -= Time.deltaTime / 30;
                }
            }
            gameObject.GetComponent<Renderer>().material.color = new Color(iniR, iniG, iniB);
        }
        if (touchingTop && touchingBottom)
        {
            int worth = Mathf.RoundToInt((maxTimeOnGrill / 2) - Mathf.Abs(timeOnGrill - (maxTimeOnGrill / 2)));
            if (worth == 0)
			{
                Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.gray);
			}
            else if (worth == 1) {
                Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burger", Color.green);
            } 
            else if (worth > 1){
				Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Burgers", Color.green);
			}
            Camera.main.GetComponent<Gameplay>().AddBurgers(worth);
            if (topBun.GetComponent<FadeObject>() == null)
            {
				topBun.AddComponent<FadeObject>();
			}
            if (bottomBun.GetComponent<FadeObject>() == null)
            {
				bottomBun.AddComponent<FadeObject>();
			}
            if (gameObject.GetComponent<FadeObject>() == null)
            {
				gameObject.AddComponent<FadeObject>();
			}
            Destroy(GetComponent<CookMeat>());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Grill Top")
        {
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
    }

    public void PickedUp()
    {
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
}
