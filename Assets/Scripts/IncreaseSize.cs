using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour 
{
    float maxSize;
    float size;

	void Start () 
    {
        maxSize = 0.25f;
	}
	
	void Update () 
    {
        size += Time.deltaTime;
        gameObject.transform.localScale = new Vector3(size, size, size);
        if (size > maxSize) 
        {
            gameObject.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            Destroy(GetComponent<IncreaseSize>());
        }
	}
}
