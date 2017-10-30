﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaCup : MonoBehaviour
{
    float maxTimeUnderFountain = 5;
    float timeUnderFountain;
    float top = 0.65f;
    float currentY;
    float maxScale = 1.5f;
    float currentScale;
    float incY, incS;
    float maxMass = 5;
    float initialMass;
    float incMass;
    int maxAmountOfDrinks = 5;
    GameObject lid;

    void Start()
    {
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(PowerUpsManager.quickerCooking))
        {
            maxTimeUnderFountain *= 0.75f;
        }
		if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(PowerUpsManager.makeMoreFood))
		{
            maxAmountOfDrinks = 6;
		}
        currentY = transform.GetChild(0).transform.localPosition.y;
        currentScale = transform.GetChild(0).transform.localScale.x;
        initialMass = gameObject.GetComponent<Rigidbody>().mass;
        incY = (top - currentY) * (1.0f / maxTimeUnderFountain);
        incS = (maxScale - currentScale) * (1.0f / maxTimeUnderFountain);
        incMass = (maxMass - initialMass) * (1.0f / maxMass);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "SodaFromMachine1" || other.name == "SodaFromMachine2" || other.name == "SodaFromMachine3")
        {
            if (other.transform.localScale.x > 1)
            {
                float angle = transform.rotation.eulerAngles.x;
                if ((angle >= -10 && angle <= 10) || (angle >= 350 && angle <= 360))
                {
                    FillCup();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "SodaFromMachine1" || other.name == "SodaFromMachine2" || other.name == "SodaFromMachine3")
        {
            if (transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().isPlaying)
            {
                transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lid")
        {
            lid = collision.gameObject;
            if (lid.transform.parent != null && gameObject.transform.parent != null)
            {
                gameObject.transform.parent = null;
                lid.transform.parent = null;
                CupReady();
                Destroy(lid);
            }
        }
    }

    void FillCup()
    {
        if (!transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().isPlaying)
        {
            transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
        }
        if (currentY < top)
        {
            timeUnderFountain += Time.deltaTime;
            currentY += Time.deltaTime * incY;
            currentScale += Time.deltaTime * incS;
            gameObject.GetComponent<Rigidbody>().mass += Time.deltaTime * incMass;
            transform.GetChild(0).transform.localPosition = new Vector3(
                transform.GetChild(0).transform.localPosition.x,
                currentY,
                transform.GetChild(0).transform.localPosition.z
            );
            transform.GetChild(0).transform.localScale = new Vector3(
                currentScale,
                currentScale,
                transform.GetChild(0).transform.localScale.z
            );
        }
    }

    void CupReady()
    {
        float percentage = (((maxTimeUnderFountain) - (Mathf.Abs(timeUnderFountain - (maxTimeUnderFountain)))) / (maxTimeUnderFountain));
        int worth = Mathf.RoundToInt(maxAmountOfDrinks * percentage);
        if (worth == 0)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drinks", Color.gray, 1);
        }
        else if (worth == 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drink", Color.green, 1);
        }
        else if (worth > 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drinks", Color.green, 1);
        }
        Camera.main.GetComponent<DropMoreProducts>().DropLid();
        Camera.main.GetComponent<DropMoreProducts>().DropCup();
        Camera.main.GetComponent<Gameplay>().AddDrinks(worth);
        Camera.main.GetComponent<DropMoreProducts>().TrasformIntoDrink(gameObject);
        Destroy(gameObject.GetComponent<SodaCup>());
    }
}
