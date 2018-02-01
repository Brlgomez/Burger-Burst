using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerBasket : MonoBehaviour
{
    float heightLimit;
    float bottomLimit;

    void Start()
    {
        bottomLimit = transform.position.y;
        heightLimit = bottomLimit + 0.325f;
    }

    public void Restart()
    {
        transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
    }
}
