using System.Collections;
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
    bool justPlayedSound;
    GameObject lid;
    Renderer myRenderer;

    void Start()
    {
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().quickerCooking.powerUpNumber))
        {
            maxTimeUnderFountain *= 0.75f;
        }
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().makeMoreFood.powerUpNumber))
        {
            maxAmountOfDrinks = 6;
        }
        currentY = transform.GetChild(0).transform.localPosition.y;
        currentScale = transform.GetChild(0).transform.localScale.x;
        initialMass = gameObject.GetComponent<Rigidbody>().mass;
        incY = (top - currentY) * (1.0f / maxTimeUnderFountain);
        incS = (maxScale - currentScale) * (1.0f / maxTimeUnderFountain);
        incMass = (maxMass - initialMass) * (1.0f / maxMass);
        myRenderer = GetComponent<Renderer>();
        Camera.main.GetComponent<SoundAndMusicManager>().PlayFromSource(gameObject);
        Camera.main.GetComponent<SoundAndMusicManager>().PauseFromSource(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "SodaFromMachine1" || other.name == "SodaFromMachine2" || other.name == "SodaFromMachine3")
        {
            if (GetComponent<FillCup>() == null && other.transform.localScale.x > 0 && currentY < top)
            {
                Camera.main.GetComponent<SoundAndMusicManager>().UnPauseFromSource(gameObject);
                gameObject.AddComponent<FillCup>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "SodaFromMachine1" || other.name == "SodaFromMachine2" || other.name == "SodaFromMachine3")
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PauseFromSource(gameObject);
            if (GetComponent<FillCup>())
            {
                Destroy(GetComponent<FillCup>());
            }
            if (transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().isPlaying ||
                transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().isPaused)
            {
                transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
                transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!justPlayedSound)
        {
            float impactSpeed = GetComponent<Rigidbody>().velocity.magnitude;
            float fill = (((maxTimeUnderFountain) - (Mathf.Abs(timeUnderFountain - (maxTimeUnderFountain)))) / (maxTimeUnderFountain));
            float pitch = (1 - (fill / 4));
            Camera.main.GetComponent<SoundAndMusicManager>().PlayDropCupSound(gameObject, (impactSpeed / 10), pitch);
            JustPlayedSound();
        }
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
        else if (collision.gameObject.tag != "Lid" && collision.gameObject.tag != "Soda" && collision.gameObject.tag != "Fallen")
        {
            CheckRange();
        }
    }

    public void FillCup(int updateInterval)
    {
        if (!transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().isPlaying && myRenderer.isVisible)
        {
            transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
        }
        if (transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().isPlaying && !myRenderer.isVisible)
        {
            transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Pause();
        }
        if (currentY < top)
        {
            timeUnderFountain += Time.deltaTime * updateInterval;
            currentY += Time.deltaTime * incY * updateInterval;
            currentScale += Time.deltaTime * incS * updateInterval;
            gameObject.GetComponent<Rigidbody>().mass += Time.deltaTime * incMass * updateInterval;
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
            Camera.main.GetComponent<SoundAndMusicManager>().PlayBadFoodSound(gameObject);
        }
        else if (worth == 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drink", Color.green, 1);
        }
        else if (worth > 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drinks", Color.green, 1);
        }
        if (worth > 0)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayFoodCompleteSound(gameObject);
        }
        Camera.main.GetComponent<DropMoreProducts>().DropLid();
        Camera.main.GetComponent<DropMoreProducts>().DropCup();
        Camera.main.GetComponent<Gameplay>().AddDrinks(worth);
        Camera.main.GetComponent<DropMoreProducts>().TrasformIntoDrink(gameObject);
        Destroy(gameObject.GetComponent<SodaCup>());
    }

    void CheckRange()
    {
        if (GetComponent<FadeObject>() == null)
        {
            Vector3 drinkRange = Camera.main.GetComponent<PositionManager>().DrinkRange().position;
            if (Vector3.Distance(gameObject.transform.position, drinkRange) > 1.25f)
            {
                if (gameObject.transform.GetChild(0).gameObject.GetComponent<FadeObject>() == null)
                {
                    gameObject.transform.GetChild(0).gameObject.AddComponent<FadeObject>();
                }
                gameObject.AddComponent<FadeObject>();
                gameObject.tag = "Fallen";
                Destroy(GetComponent<SodaCup>());
            }
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
