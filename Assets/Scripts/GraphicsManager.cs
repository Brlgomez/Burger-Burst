using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public int maxGraphics;
    public List<Graphic> graphicList = new List<Graphic>();
    public Texture2D offWhite, rainbow, blue;
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
        if (PlayerPrefs.GetInt(PlayerPrefsManager.specificGraphics + "0") == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsManager.specificGraphics + 0, 1);
        }
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
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                break;
            case 1:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                break;
            case 2:
                GetComponent<Pixelate>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                break;
            case 3:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                break;
            case 4:
                GetComponent<Pixelate>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                break;
            case 5:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnlyBgColor = Color.white;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesColor = Color.black;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnly = 0.25f;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().mode = UnityStandardAssets.ImageEffects.EdgeDetection.EdgeDetectMode.TriangleLuminance;
                break;
            case 6:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().textureRamp = offWhite;
                break;
            case 7:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().textureRamp = blue;
                break;
            case 8:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().textureRamp = rainbow;
                break;
            case 9:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.Tonemapping>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionRamp>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = true;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnlyBgColor = Color.black;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesColor = Color.green;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().edgesOnly = 0.75f;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().mode = UnityStandardAssets.ImageEffects.EdgeDetection.EdgeDetectMode.TriangleDepthNormals;
                break;
            default:
                GetComponent<Pixelate>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.SepiaTone>().enabled = false;
                GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>().enabled = false;
                break;
        }
    }
}
