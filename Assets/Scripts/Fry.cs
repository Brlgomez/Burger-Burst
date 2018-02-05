using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fry : MonoBehaviour
{
    float maxTimeInFryer = 20;
    int maxAmountOfFries = 10;
    float timeInFryer;
    bool justPlayedSound;
    GameObject basket;

    void Start()
    {
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().quickerCooking.powerUpNumber))
        {
            maxTimeInFryer *= 0.75f;
        }
        if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().makeMoreFood.powerUpNumber))
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
            if (GetComponent<FryFries>() == null && timeInFryer < maxTimeInFryer)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                gameObject.AddComponent<FryFries>();
                Camera.main.GetComponent<SoundAndMusicManager>().PlayLoopFromSourceAndRaiseVolume(gameObject, 1, 1);
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
                Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -1);
                Destroy(GetComponent<FryFries>());
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!justPlayedSound)
        {
            float impactSpeed = GetComponent<Rigidbody>().velocity.magnitude;
            Camera.main.GetComponent<SoundAndMusicManager>().PlayDropFriesSound(gameObject, (impactSpeed / 10));
            JustPlayedSound();
        }
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
        else if (collision.gameObject.tag != gameObject.tag)
        {
            CheckRange();
        }
    }

    void FriesCompleted()
    {
        float percentage = (((maxTimeInFryer / 2) - (Mathf.Abs(timeInFryer - (maxTimeInFryer / 2)))) / (maxTimeInFryer / 2));
        int worth = Mathf.RoundToInt(maxAmountOfFries * percentage);
        if (worth == 0)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.gray, 1);
            Camera.main.GetComponent<SoundAndMusicManager>().PlayBadFoodSound(gameObject);
        }
        else if (worth == 1)
        {
            Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fry", Color.green, 1);
        }
        else if (worth > 1)
        {
            if (worth == maxAmountOfFries)
            {
                Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.yellow, 1);
            }
            else
            {
                Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Fries", Color.green, 1);
            }
        }
        if (worth > 0)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayFoodCompleteSound(gameObject);
        }
        if (worth == maxAmountOfFries)
        {
            Camera.main.GetComponent<OnlineManagement>().PerfectFoodItem();
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

    void CheckRange()
    {
        if (gameObject.GetComponent<FadeObject>() == null)
        {
            Vector3 friesRange = Camera.main.GetComponent<PositionManager>().FriesRange().position;
            if (Vector3.Distance(gameObject.transform.position, friesRange) > 1.75f)
            {
                gameObject.tag = "Fallen";
                gameObject.AddComponent<FadeObject>();
                Destroy(GetComponent<Fry>());
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
