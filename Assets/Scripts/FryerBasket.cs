using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerBasket : MonoBehaviour
{
    float bottomLimit;

    void Start()
    {
        bottomLimit = transform.position.y;
    }

    public void Restart()
    {
        transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
    }
}
