using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftermathText : MonoBehaviour 
{
    float time;
    float maxTime = 1.5f;
    float horizontalSpeed = 2.5f;
    float horizontalDistance = 0.5f;
    float verticalSpeed = 0.1f;
    Vector3 startPosition;

    void Start () 
    {
        startPosition = transform.position;
        Vector3 v = Camera.main.transform.position - transform.position;
        v.y = v.z = 0.0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(0, 180, 0);
	}

	void Update () 
    {
        float alpha = ((maxTime / maxTime) - (time / maxTime));
        float newX = startPosition.x + (Mathf.Sin(time * horizontalSpeed) * horizontalDistance);
        float newY = transform.position.y + Time.deltaTime * verticalSpeed;
		time += Time.deltaTime;
		gameObject.GetComponent<Renderer>().material.color = new Color(
            gameObject.GetComponent<Renderer>().material.color.r,
            gameObject.GetComponent<Renderer>().material.color.g,
            gameObject.GetComponent<Renderer>().material.color.b,
            alpha
        );
        transform.position = new Vector3(newX, newY, transform.position.z);
        if (alpha < 0.01f)
        {
            Destroy(gameObject);
        }
	}

    public void UpdateText (string text) 
    {
        GetComponent<TextMesh>().text = text;    
    }
}
