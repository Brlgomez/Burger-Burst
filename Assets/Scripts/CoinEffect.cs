using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    float upwardSpeed = 1;
    float rotatingSpeed = 0.5f;
    float lengthOfAnimation = 1;
    float time;

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        time += Time.deltaTime;
        RaiseAndSpin();
        if (time < lengthOfAnimation * 0.25f)
        {
            transform.localScale = Vector3.one * (time / (lengthOfAnimation * 0.25f));
        }
        else if (time > lengthOfAnimation * 0.75f)
        {
            //transform.localScale = Vector3.one * (time / (lengthOfAnimation * 0.75f));
        }
        else if (time > lengthOfAnimation)
        {
            Destroy(gameObject);
        }
    }

    void RaiseAndSpin()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y * upwardSpeed * Time.deltaTime,
            transform.position.z
        );
        transform.RotateAround(
            transform.transform.position,
            transform.forward, rotatingSpeed * Time.deltaTime
        );
    }
}
