using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{
    float maxSize = 0.95f;
    static float speed = 2;
    float size;

    void Update()
    {
        size += Time.deltaTime * speed;
        SetGlobalScale(new Vector3(size, size, size));
        if (size > maxSize)
        {
            SetGlobalScale(new Vector3(maxSize, maxSize, maxSize));
            Destroy(GetComponent<IncreaseSize>());
        }
    }

    public void SetGlobalScale(Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x,
                                           globalScale.y / transform.lossyScale.y,
                                           globalScale.z / transform.lossyScale.z);
    }
}
