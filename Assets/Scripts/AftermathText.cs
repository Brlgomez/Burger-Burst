using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftermathText : MonoBehaviour
{
    float time;
    float maxTime = 1;
    float timeToStartFading = 0.75f;
    float horizontalSpeed = 2.5f;
    float horizontalDistance = 0.25f;
    float verticalSpeed = 0.1f;
    float alpha = 1;
    Vector3 startPosition;
    float randomX;

    void Start()
    {
        startPosition = transform.position;
        Vector3 v = Camera.main.transform.position - transform.position;
        v.y = v.z = 0.0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(0, 180, 0);
        randomX = Random.Range(-2.5f, 2.5f);
    }

    void Update()
    {
		time += Time.deltaTime;
		if (time > timeToStartFading)
        {
            alpha = (((maxTime - timeToStartFading) / (maxTime - timeToStartFading)) - ((time - timeToStartFading) / (maxTime - timeToStartFading)));
        }
        float newX = startPosition.x + (Mathf.Sin((time + randomX) * horizontalSpeed) * horizontalDistance);
        float newY = transform.position.y + Time.deltaTime * verticalSpeed * transform.up.y;
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

    public void UpdateText(string text)
    {
        GetComponent<TextMesh>().text = text;
    }
}
