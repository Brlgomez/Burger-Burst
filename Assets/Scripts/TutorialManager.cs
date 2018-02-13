using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameObject counterThrowing, counterTapping, counterButton;
    public Sprite[] throwing, tapping;
    bool tappingActivated, counterButtonActivated, counterButtonOn;

    int maxTimeNotPressedCounter = 30;
    float timeNotPressedCounter;

    void Start()
    {
        counterThrowing = GameObject.Find("Tutorial Counter");
        counterTapping = GameObject.Find("Tutorial Tap");
        counterButton = GameObject.Find("Tutorial Counter Button");
    }

    public void ActivateCounterThrowing()
    {
        if (GetComponent<PlayerPrefsManager>().GetTutorialThrow() == 0)
        {
            counterThrowing.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(ThrowingAnimation());
        }
    }

    public void DeactivateCounterThrowing()
    {
        if (!tappingActivated)
        {
            tappingActivated = true;
            GetComponent<PlayerPrefsManager>().SetTutorialThrow();
            counterThrowing.GetComponent<SpriteRenderer>().enabled = false;
            ActivateCounterTapping();
        }
    }

    public IEnumerator ThrowingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        counterThrowing.GetComponent<SpriteRenderer>().sprite = throwing[0];
        yield return new WaitForSeconds(0.5f);
        counterThrowing.GetComponent<SpriteRenderer>().sprite = throwing[1];
        ActivateCounterThrowing();
    }

    void ActivateCounterTapping()
    {
        StartCoroutine(PopTappingSprite());
    }

    IEnumerator PopTappingSprite()
    {
        if (GetComponent<PlayerPrefsManager>().GetTutorialTap() == 0)
        {
            yield return new WaitForSeconds(30f);
            PlayTappingAnimation();
        }
    }

    void PlayTappingAnimation()
    {
        if (GetComponent<PlayerPrefsManager>().GetTutorialTap() == 0 && GetComponent<GrabAndThrowObject>() != null && !GetComponent<Gameplay>().IsGameOver())
        {
            counterTapping.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(TappingAnimation());
        }
        else
        {
            TurnOffTappingSprite();
        }
    }

    IEnumerator TappingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        counterTapping.GetComponent<SpriteRenderer>().sprite = tapping[0];
        yield return new WaitForSeconds(0.5f);
        counterTapping.GetComponent<SpriteRenderer>().sprite = tapping[1];
        PlayTappingAnimation();
    }

    public void DeactivateCounterTapping()
    {
        GetComponent<PlayerPrefsManager>().SetTutorialTap();
        TurnOffTappingSprite();
    }

    public void TurnOffTappingSprite()
    {
        counterTapping.GetComponent<SpriteRenderer>().enabled = false;
        counterButton.GetComponent<SpriteRenderer>().enabled = false;
    }

    /* Counter Button Tutorial */

    public void IncreaseTimeNotPressedCounter(float time)
    {
        if (GetComponent<PlayerPrefsManager>().GetTutorialCounter() == 0 && !counterButtonOn)
        {
            timeNotPressedCounter += time;
            if (timeNotPressedCounter > maxTimeNotPressedCounter)
            {
                counterButtonOn = true;
                PlayCounterButtonAnimation();
            }
        }
    }

    public void DeactivateCounterButton()
    {
        GetComponent<PlayerPrefsManager>().SetTutorialCounter();
        TurnOffCounterSprite();
    }

    public void TurnOffCounterSprite()
    {
        counterButton.GetComponent<SpriteRenderer>().enabled = false;
    }

    void PlayCounterButtonAnimation()
    {
        if (GetComponent<PlayerPrefsManager>().GetTutorialCounter() == 0 && GetComponent<GrabAndThrowObject>() != null && !GetComponent<Gameplay>().IsGameOver())
        {
            counterButton.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(CounterButtonAnimation());
        }
        else
        {
            TurnOffCounterSprite();
        }
    }

    IEnumerator CounterButtonAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        counterButton.GetComponent<SpriteRenderer>().sprite = tapping[0];
        yield return new WaitForSeconds(0.5f);
        counterButton.GetComponent<SpriteRenderer>().sprite = tapping[1];
        PlayCounterButtonAnimation();
    }

    public void ResetCounterButton()
    {
        TurnOffCounterSprite();
        if (GetComponent<PlayerPrefsManager>().GetTutorialCounter() == 0)
        {
            counterButtonOn = false;
        }
    }
}
