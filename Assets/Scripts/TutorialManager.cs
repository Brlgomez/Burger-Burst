using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameObject counterThrowing;
    GameObject counterTapping;
    public Sprite[] throwing;
    public Sprite[] tapping;

    bool tappingActivated;

    void Start()
    {
        counterThrowing = GameObject.Find("Tutorial Counter");
        counterTapping = GameObject.Find("Tutorial Tap");
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
        yield return new WaitForSeconds(30f);
        PlayTappingAnimation();
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
        counterTapping.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TurnOffTappingSprite()
    {
        counterTapping.GetComponent<SpriteRenderer>().enabled = false;
    }
}
