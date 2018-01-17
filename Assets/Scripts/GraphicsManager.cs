using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public int maxGraphics;
    public List<Graphic> graphicList = new List<Graphic>();
    public Texture2D classicTexture, blueAndGreenTexture, greenAndOrange, redRetro, rainbow, grapeFruit;
    string[] allDescriptions;

    public class Graphic
    {
        public int graphicNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;

        public Graphic(int graphicNum, int cost, string info)
        {
            graphicNumber = graphicNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt(PlayerPrefsManager.specificGraphics + graphicNum, 0) == 1);
            description = info;
            sprite = (Sprite)Resources.Load("Sprites/Graphics/" + graphicNum, typeof(Sprite));
        }
    }

    void Awake()
    {
        TextAsset t = new TextAsset();
        t = Resources.Load("Graphics") as TextAsset;
        allDescriptions = t.text.Split('\n');
        maxGraphics = allDescriptions.Length;
        PlayerPrefs.SetInt(PlayerPrefsManager.specificGraphics + 0, 1);
        SetGraphicsList();
        SetGraphic(GetComponent<PlayerPrefsManager>().GetGraphics());
	}

    public void SetGraphicsList()
    {
        graphicList.Clear();
        for (int i = 0; i < maxGraphics; i++)
        {
            string description = allDescriptions[i].Replace("NEWLINE", "\n");
            if (i < GetComponent<PlayerPrefsManager>().GetGraphicsUnlocked())
            {
                graphicList.Add(new Graphic(i, int.Parse(description.Split('*')[1]), description.Split('*')[0]));
            }
        }
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
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnlyBgColor = Color.white;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesColor = Color.black;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnly = 0.25f;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().mode = UnityStandardAssets.ImageEffects.EdgeDetection.EdgeDetectMode.TriangleLuminance;
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
            case 9:
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnlyBgColor = Color.black;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesColor = Color.green;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnly = 0.75f;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().mode = UnityStandardAssets.ImageEffects.EdgeDetection.EdgeDetectMode.TriangleDepthNormals;
                break;
            default:
                GetComponent<Assets.Pixelation.Scripts.Pixelation>().enabled = false;
                GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
                break;
        }
    }
}
