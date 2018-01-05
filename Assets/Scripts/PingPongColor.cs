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

    void Update()
    {
        if (startLooping && pickedColors.Count > 1)
        {
            time += Time.deltaTime;
            if (time < loopTime)
            {
                GetComponent<TextMesh>().color = Color.Lerp(pickedColors[currentColorNumber], pickedColors[nextColor], time * 2);
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

    public void SetColorAndObject(Color[] colors)
    {
        pickedColors = new List<Color>();
        for (int i = 0; i < colors.Length; i++)
        {
            pickedColors.Add(colors[i]);
        }
        startLooping = true;
    }
}
