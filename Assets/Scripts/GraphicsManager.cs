using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public List<Sprite> graphicSprites;
    public List<Graphic> graphicList = new List<Graphic>();
    public Texture2D classicTexture, blueAndGreenTexture, greenAndOrange, redRetro, rainbow, grapeFruit;

    public class Graphic
    {
        public int graphicNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;

        public Graphic(int graphicNum, int cost, string info, Sprite icon)
        {
            graphicNumber = graphicNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt(PlayerPrefsManager.specificGraphics + graphicNum, 0) == 1);
            description = info;
            sprite = icon;
        }
    }

    void Awake()
    {
        PlayerPrefs.SetInt(PlayerPrefsManager.specificGraphics + 0, 1);
        graphicList.Add(new Graphic(0, 0, "Normal", graphicSprites[0]));
        graphicList.Add(new Graphic(1, 100, "Retro", graphicSprites[1]));
        graphicList.Add(new Graphic(2, 100, "Blue & Green", graphicSprites[2]));
        graphicList.Add(new Graphic(3, 100, "Green & Orange", graphicSprites[3]));
        graphicList.Add(new Graphic(4, 100, "Pixelated", graphicSprites[4]));
        graphicList.Add(new Graphic(5, 100, "Outlined", graphicSprites[5]));
		graphicList.Add(new Graphic(6, 100, "Red Retro", graphicSprites[6]));
		graphicList.Add(new Graphic(7, 100, "Grape Fruit", graphicSprites[7]));
		graphicList.Add(new Graphic(8, 100, "Lucid Dreaming", graphicSprites[8]));
		SetGraphic(GetComponent<PlayerPrefsManager>().GetGraphics());
    }

    public void SetGraphic(int graphicNum)
    {
        switch (graphicNum)
        {
            case 0:
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 1:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = classicTexture;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 2:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = blueAndGreenTexture;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 3:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = greenAndOrange;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 4:
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 5:
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = true;
                break;
            case 6:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = redRetro;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 7:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = grapeFruit;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            case 8:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = rainbow;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                break;
            default:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
                break;
        }
    }
}
