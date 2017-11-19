using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachine : MonoBehaviour
{
	static int updateInterval = 2;

	int maxOnTime = 5;
    float currentTimeOn;
    bool turnOn = true;
    float sodaMaxScale = 2.22f;
    float sodaCurrentScale;

    void Update()
    {
        currentTimeOn += Time.deltaTime;
        if (Time.frameCount % updateInterval == 0)
        {
            if (turnOn && sodaCurrentScale < sodaMaxScale)
            {
                sodaCurrentScale += Time.deltaTime * updateInterval * 50;
                transform.localScale = Vector3.one * sodaCurrentScale;
                if (transform.localScale.x > sodaMaxScale)
                {
                    transform.localScale = Vector3.one * sodaMaxScale;
                }
            }
            if (!turnOn || currentTimeOn > maxOnTime)
			{
				turnOn = false;
				sodaCurrentScale -= Time.deltaTime * updateInterval * 50;
				transform.localScale = Vector3.one * sodaCurrentScale;
				if (sodaCurrentScale < 0)
				{
					transform.localScale = Vector3.zero;
					Destroy(gameObject.GetComponent<SodaMachine>());
				}
			}
        }
    }

    public void ButtonPressed()
    {
        turnOn = false;
    }

    public void Restart()
    {
        transform.localScale = Vector3.zero;
        Destroy(gameObject.GetComponent<SodaMachine>());
    }
}
