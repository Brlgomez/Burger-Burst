using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCup : MonoBehaviour
{
    static int updateInterval = 2;

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            float angle = transform.rotation.eulerAngles.x;
            if ((angle >= -10 && angle <= 10) || (angle >= 350 && angle <= 360))
            {
                GetComponent<SodaCup>().FillCup(updateInterval);
            }
        }
    }
}
