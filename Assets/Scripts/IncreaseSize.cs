using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{
    static float maxSize = 0.95f;
    static float speed = 2;
    float size;

    void Update()
    {
        size += Time.deltaTime * speed;
        gameObject.transform.localScale = new Vector3(size, size, size);
        if (size > maxSize)
        {
            gameObject.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            Destroy(GetComponent<IncreaseSize>());
        }
    }
}
