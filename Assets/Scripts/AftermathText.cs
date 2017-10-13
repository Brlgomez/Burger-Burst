using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftermathText : MonoBehaviour
{
    static float maxTime = 1;
    static float fadeTime = 0.75f;
    static float horizontalSpeed = 2.5f;
    static float horizontalDistance = 0.25f;
    static float verticalSpeed = 0.1f;
    float alpha = 1;
    float time;
    float randomX;
    Vector3 startPosition;

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
        if (time > fadeTime)
        {
            alpha = (((maxTime - fadeTime) / (maxTime - fadeTime)) - ((time - fadeTime) / (maxTime - fadeTime)));
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
