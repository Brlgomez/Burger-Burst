using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkObject : MonoBehaviour {

    float time;
    int maxTime = 3;
	
	void Update () 
    {
        time += Time.deltaTime;
        float scale = ((maxTime / maxTime) - (time / maxTime));
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
	}
}
