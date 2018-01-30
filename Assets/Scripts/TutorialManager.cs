using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameObject counterThrowing;
    GameObject counterTapping;
    public Sprite[] throwing;
    public Sprite[] tapping;

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
        GetComponent<PlayerPrefsManager>().SetTutorialThrow();
        counterThrowing.GetComponent<SpriteRenderer>().enabled = false;
        ActivateCounterTapping();
    }


    public IEnumerator ThrowingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        counterThrowing.GetComponent<SpriteRenderer>().sprite = throwing[0];
        yield return new WaitForSeconds(0.5f);
        counterThrowing.GetComponent<SpriteRenderer>().sprite = throwing[1];
        ActivateCounterThrowing();
    }

    public void ActivateCounterTapping()
    {
        if (GetComponent<PlayerPrefsManager>().GetTutorialTap() == 0)
        {
            counterTapping.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(TappingAnimation());
        }
    }

    public void DeactivateCounterTapping()
    {
        GetComponent<PlayerPrefsManager>().SetTutorialTap();
        counterTapping.GetComponent<SpriteRenderer>().enabled = false;
    }

    public IEnumerator TappingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        counterTapping.GetComponent<SpriteRenderer>().sprite = tapping[0];
        yield return new WaitForSeconds(0.5f);
        counterTapping.GetComponent<SpriteRenderer>().sprite = tapping[1];
        ActivateCounterTapping();
    }
}
