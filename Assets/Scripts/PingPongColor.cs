using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongColor : MonoBehaviour
{
    static float loopTime = 0.5f;
    List<Color> pickedColors;
    bool startLooping;
    float time;
    int currentColorNumber;
    int nextColor = 1;
    int changeColorNumber;

    void Awake()
    {
        pickedColors = new List<Color>();
        if (gameObject.GetComponent<TextMesh>())
        {
            changeColorNumber = 0;
        }
        if (gameObject.GetComponent<Renderer>())
        {
            changeColorNumber = 1;
        }
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            changeColorNumber = 2;
        }
    }

    void Update()
    {
        if (startLooping && pickedColors.Count > 1)
        {
            time += Time.deltaTime;
            if (time < loopTime)
            {
                Color newColor = Color.Lerp(pickedColors[currentColorNumber], pickedColors[nextColor], (time / loopTime));
                switch (changeColorNumber)
                {
                    case 0:
                        GetComponent<TextMesh>().color = newColor;
                        break;
                    case 1:
                        GetComponent<Renderer>().material.color = newColor;
                        break;
                    case 2:
                        GetComponent<SpriteRenderer>().color = newColor;
                        break;
                }
            }
            if (time > loopTime)
            {
                time = 0;
                if (nextColor + 1 < pickedColors.Count)
                {
                    nextColor++;
                }
                else
                {
                    nextColor = 0;
                }
                if (currentColorNumber + 1 < pickedColors.Count)
                {
                    currentColorNumber++;
                }
                else
                {
                    currentColorNumber = 0;
                }
            }
        }
    }

    public void SetColors(Color[] colors)
    {
        if (colors != null)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                pickedColors.Add(colors[i]);
            }
        }
        else
        {
            pickedColors.Add(Color.white);
            pickedColors.Add(Color.clear);
        }
        startLooping = true;
    }
}
