using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeText : MonoBehaviour
{
    static float lengthOfAnimation = 0.25f;
    static float shakeIntensity = 0.2f;
    GameObject screen;
    Vector3 initialPos;
    float time;
    bool reverse;
    Color initialColor;

    void Awake()
    {
        initialPos = transform.localPosition;
        if (GetComponent<TextMesh>() != null)
        {
            initialColor = GetComponent<TextMesh>().color;
        }
        else
        {
            initialColor = GetComponent<SpriteRenderer>().color;
        }
    }

    void Update()
    {
        if (reverse)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time += Time.deltaTime;
        }
        float newZ = initialPos.z + (Mathf.Sin(time) * shakeIntensity);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZ);
        if (time > lengthOfAnimation / 2)
        {
            reverse = true;
        }
        if (reverse && time < 0)
        {
            transform.localPosition = initialPos;
            if (GetComponent<TextMesh>() != null)
            {
                GetComponent<TextMesh>().color = initialColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = initialColor;
            }
            Destroy(GetComponent<ShakeText>());
        }
    }

    public void ChangeColor(int num)
    {
        if (num > 0)
        {
            GetComponent<TextMesh>().color = Color.green;

        }
        else
        {
            GetComponent<TextMesh>().color = Color.red;
        }
    }

    public void ChangeSpriteColor(int num)
    {
        if (num > 0)
        {
            GetComponent<SpriteRenderer>().color = Color.green;

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
