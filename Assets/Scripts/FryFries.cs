using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryFries : MonoBehaviour {

	bool inFryer;
	float timeInFryer;
	int maxTimeInFryer = 20;
	Color initialColor;
	Color friedColor;
	Color burntColor;
    float iniR, iniG, iniB;

	void Start () {
		initialColor = gameObject.GetComponent<Renderer>().material.color;
        friedColor = new Color(0.977f, 0.875f, 0.727f);
        burntColor = new Color(0.5f, 0.375f, 0);
		iniR = initialColor.r;
		iniG = initialColor.g;
		iniB = initialColor.b;
	}
	
	void Update () {
        if (inFryer && timeInFryer < maxTimeInFryer)
		{
            timeInFryer += Time.deltaTime;
            if (timeInFryer < maxTimeInFryer / 2)
			{
                if (iniR > friedColor.r)
				{
					iniR -= Time.deltaTime / 50;
				}
				if (iniG > friedColor.g)
				{
					iniG -= Time.deltaTime / 40;
				}
				if (iniB > friedColor.b)
				{
					iniB -= Time.deltaTime / 30;
				}
			}
			else
			{
				if (iniR > burntColor.r)
				{
					iniR -= Time.deltaTime / 20;
				}
				if (iniG > burntColor.g)
				{
					iniG -= Time.deltaTime / 20;
				}
				if (iniB > burntColor.b)
				{
					iniB -= Time.deltaTime / 25;
				}
			}
			gameObject.GetComponent<Renderer>().material.color = new Color(iniR, iniG, iniB);
		}
	}

    void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Hot Oil")
		{
            inFryer = true;
		}

		if (col.gameObject.name == "Basket")
		{
            FriesCompleted();
        }
	}

    void OnTriggerExit(Collider col)
    {
		if (col.gameObject.name == "Hot Oil")
		{
            inFryer = false;
		}
    }

    void FriesCompleted () 
    {
        int worth = Mathf.RoundToInt((maxTimeInFryer / 2) - Mathf.Abs(timeInFryer - (maxTimeInFryer / 2)));
		if (worth == 0)
		{
			Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.gray);
		}
		else if (worth == 1)
		{
			Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fry", Color.green);
		}
		else if (worth > 1)
		{
			Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.green);
		}
		Camera.main.GetComponent<Gameplay>().AddFries(worth);
		if (gameObject.GetComponent<FadeObject>() == null)
		{
			gameObject.AddComponent<FadeObject>();
		}
		Destroy(GetComponent<CookMeat>());
    }
}
