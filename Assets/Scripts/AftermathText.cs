using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftermathText : MonoBehaviour 
{
    float time;
    int maxTime = 1;
    Vector3 startPosition;

    void Start () 
    {
        startPosition = transform.position;
    }

	void Update () 
    {
        time += Time.deltaTime;
        float alpha = ((maxTime / maxTime) - (time / maxTime));
        gameObject.GetComponent<Renderer>().material.color = new Color(
            gameObject.GetComponent<Renderer>().material.color.r,
            gameObject.GetComponent<Renderer>().material.color.g,
            gameObject.GetComponent<Renderer>().material.color.b,
            alpha
        );
        transform.position = new Vector3(startPosition.x + Mathf.Sin(Time.time * 2), transform.position.y + Time.deltaTime, transform.position.z);
        if (alpha < 0.01f)
        {
            Destroy(gameObject);
        }
	}

    public void updateText (string text) 
    {
        GetComponent<TextMesh>().text = text;    
    }
}
