﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public List<Sprite> graphicSprites;
    public List<Graphic> graphicList = new List<Graphic>();
    public Graphic normal, classic, classicColor, blackAndWhite, noir;
    public Texture2D classicTexture, blueAndGreenTexture, blackAndWhiteTexture;

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
            unlocked = (PlayerPrefs.GetInt("Graphic " + graphicNum, 0) == 1);
            description = info;
            sprite = icon;
        }
    }

    void Awake()
    {
        PlayerPrefs.SetInt("Graphic 0", 1);
        graphicList.Add(normal = new Graphic(0, 0, "Normal", graphicSprites[0]));
        graphicList.Add(classic = new Graphic(1, 0, "Classic", graphicSprites[1]));
        graphicList.Add(classicColor = new Graphic(2, 10, "Blue-green gradient", graphicSprites[2]));
        graphicList.Add(blackAndWhite = new Graphic(3, 20, "Black & white", graphicSprites[3]));
        graphicList.Add(noir = new Graphic(4, 30, "Pixelated", graphicSprites[4]));
        SetGraphic(PlayerPrefs.GetInt("GRAPHICS"));
    }

    public void SetGraphic(int graphicNum)
    {
        if (graphicNum == 0)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
            GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
        }
        else if (graphicNum == 1)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
            GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = classicTexture;
        }
        else if (graphicNum == 2)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
            GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = blueAndGreenTexture;
        }
        else if (graphicNum == 3)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
            GetComponent<Assets.Pixelation.Scripts.Chunky>().SprTex = blackAndWhiteTexture;
        }
        else if (graphicNum == 4)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
            GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = true;
        }
        else
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
        }
    }
}
