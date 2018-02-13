using UnityEngine;

public class AftermathText : MonoBehaviour
{
    static int updateInterval = 2;
    static float maxTime = 1.25f;
    static float fadeTime = 1;
    static float horizontalSpeed = 2.5f;
    static float horizontalDistance = 0.2f;

    float alpha = 1;
    float time;
    float distance;
    Vector3 startPosition;
    Material textColor;

    void Start()
    {
        startPosition = transform.position;
        Vector3 v = Camera.main.transform.position - transform.position;
        v.y = v.z = 0.0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(0, 180, 0);
        textColor = GetComponent<Renderer>().material;
        distance = (Vector3.Distance(Camera.main.transform.position, transform.position)) / 4;
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            ChangeAlpha();
        }
    }

    void ChangeAlpha()
    {
        time += Time.deltaTime * updateInterval;
        float newX = startPosition.x + (Mathf.Sin((time) * horizontalSpeed) * horizontalDistance) * distance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if (time > fadeTime)
        {
            alpha = 1 - ((time - fadeTime) / (maxTime - fadeTime));
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);
        }
        if (time > maxTime)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateText(string text)
    {
        GetComponent<TextMesh>().text = text;
    }
}
