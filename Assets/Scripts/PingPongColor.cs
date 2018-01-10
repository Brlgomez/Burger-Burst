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

    void Update()
    {
        if (startLooping && pickedColors.Count > 1)
        {
            time += Time.deltaTime;
            if (time < loopTime)
            {
                switch (changeColorNumber)
                {
                    case 0:
                        GetComponent<TextMesh>().color = Color.Lerp(pickedColors[currentColorNumber], pickedColors[nextColor], time * 2);
                        break;
                    case 1:
                        GetComponent<Renderer>().material.color = Color.Lerp(pickedColors[currentColorNumber], pickedColors[nextColor], time * 2);
                        break;
                    case 2:
                        GetComponent<SpriteRenderer>().color = Color.Lerp(pickedColors[currentColorNumber], pickedColors[nextColor], time * 2);
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

    public void SetColorAndObject(Color[] colors, int changeItem)
    {
        changeColorNumber = changeItem;
        pickedColors = new List<Color>();
        for (int i = 0; i < colors.Length; i++)
        {
            pickedColors.Add(colors[i]);
        }
        startLooping = true;
    }
}
