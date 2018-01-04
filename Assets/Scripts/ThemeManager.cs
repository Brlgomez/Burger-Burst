﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Material floor, wall, details;
    public List<CustomItem> flooringList = new List<CustomItem>();
    public List<CustomItem> wallList = new List<CustomItem>();
    public List<CustomItem> detailList = new List<CustomItem>();

    public class CustomItem
    {
        public int themeNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;

        public CustomItem(int num, int cost, string itemType, string info)
        {
            themeNumber = num;
            price = cost;
            unlocked = (PlayerPrefs.GetInt(itemType + num, 0) == 1);
            description = info;
            sprite = (Sprite)Resources.Load("Sprites/" + itemType + "/" + num, typeof(Sprite));
        }
    }

    void Awake()
    {
        PlayerPrefs.SetInt("Flooring0", 1);
        PlayerPrefs.SetInt("Wallpaper0", 1);
        PlayerPrefs.SetInt("Detail0", 1);

        TextAsset t = new TextAsset();
        t = Resources.Load("Themes") as TextAsset;
        string[] allDescriptions = t.text.Split('\n');

        for (int i = 0; i <= 15; i++)
        {
            flooringList.Add(new CustomItem(i, int.Parse(allDescriptions[i].Split('*')[3]),
                                            PlayerPrefsManager.specificFlooring, allDescriptions[i].Split('*')[0] + " Floor"));
        }
        for (int i = 0; i <= 15; i++)
        {
            wallList.Add(new CustomItem(i, int.Parse(allDescriptions[i].Split('*')[4]),
                                        PlayerPrefsManager.specificWallpaper, allDescriptions[i].Split('*')[1] + " Wallpaper"));
        }
        for (int i = 0; i <= 15; i++)
        {
            detailList.Add(new CustomItem(i, int.Parse(allDescriptions[i].Split('*')[5]),
                                          PlayerPrefsManager.specificDetail, allDescriptions[i].Split('*')[2] + " Detail"));
        }

        SetFlooring(GetComponent<PlayerPrefsManager>().GetFlooring());
        SetWallpaper(GetComponent<PlayerPrefsManager>().GetWallpaper());
        SetDetail(GetComponent<PlayerPrefsManager>().GetDetail());
    }

    public void SetFlooring(int num)
    {
        floor.mainTexture = (Texture)Resources.Load("Textures/Flooring/Food Truck Floor " + (num + 1), typeof(Texture));
    }

    public void SetWallpaper(int num)
    {
        wall.mainTexture = (Texture)Resources.Load("Textures/Wallpaper/Food Truck Wall " + (num + 1), typeof(Texture));
    }

    public void SetDetail(int num)
    {
        details.mainTexture = (Texture)Resources.Load("Textures/Detail/Food Truck Detail " + (num + 1), typeof(Texture));
    }
}
