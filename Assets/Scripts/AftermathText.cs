using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftermathText : MonoBehaviour
{
	static int updateInterval = 2;

	static float maxTime = 1.25f;
    static float fadeTime = 1;
    static float horizontalSpeed = 2.5f;
    static float horizontalDistance = 0.25f;
    static float verticalSpeed = 0.1f;
    float alpha = 1;
    float time;
    float randomX;
    float multiplier = 1;
    Vector3 startPosition;
    Material textColor;

    void Start()
    {
        startPosition = transform.position;
        Vector3 v = Camera.main.transform.position - transform.position;
        v.y = v.z = 0.0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(0, 180, 0);
        randomX = Random.Range(-2.5f, 2.5f);
        textColor = gameObject.GetComponent<Renderer>().material;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (Time.frameCount % updateInterval == 0)
        {
            ChangeAlpha();
        }
    }

    void ChangeAlpha()
    {
        if (time > fadeTime)
        {
            alpha = (((maxTime - fadeTime) / (maxTime - fadeTime)) - ((time - fadeTime) / (maxTime - fadeTime)));
        }
        float newX = startPosition.x + (Mathf.Sin((time + randomX) * horizontalSpeed) * horizontalDistance) * multiplier;
        float newY = transform.position.y + Time.deltaTime * verticalSpeed * transform.up.y * multiplier;
        textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);
        transform.position = new Vector3(newX, newY, transform.position.z);
        if (alpha < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateText(string text, float multi)
    {
        GetComponent<TextMesh>().text = text;
        multiplier = multi;
    }
}
