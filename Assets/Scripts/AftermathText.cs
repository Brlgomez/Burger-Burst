using UnityEngine;

public class AftermathText : MonoBehaviour
{
    static int updateInterval = 2;
    static float timeToFade = 1;
    static float fadeTime = 0.25f;
    static float horizontalSpeed = 2.5f;
    static float horizontalDistance = 0.2f;

    float alpha = 1;
    float maxTime;
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
        maxTime = timeToFade + fadeTime;
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            time += Time.deltaTime * updateInterval;
            ChangeAlpha();
        }
    }

    void ChangeAlpha()
    {
        float newX = startPosition.x + (Mathf.Sin((time) * horizontalSpeed) * horizontalDistance) * distance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if (time > timeToFade)
        {
            alpha = 1 - ((time - timeToFade) / fadeTime);
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
