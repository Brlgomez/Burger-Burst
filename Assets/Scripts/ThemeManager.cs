﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Material floor, wall, details;
    public List<Sprite> flooringSprites;
    public List<Texture> flooringTexture;
    public List<Flooring> flooringList = new List<Flooring>();

    public class Flooring
    {
        public int themeNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;
        public Texture texture;

        public Flooring(int powerUpNum, int cost, string info, Sprite icon, Texture tex)
        {
            themeNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Flooring " + powerUpNum, 0) == 1);
            description = info;
            sprite = icon;
            texture = tex;
        }
    }

    void Awake()
    {
		PlayerPrefs.SetInt("Flooring 0", 1);
		flooringList.Add(new Flooring(0, 0, "Default", flooringSprites[0], flooringTexture[0]));
        flooringList.Add(new Flooring(1, 10, "One", flooringSprites[1], flooringTexture[1]));
        flooringList.Add(new Flooring(2, 20, "Two", flooringSprites[2], flooringTexture[2]));
        flooringList.Add(new Flooring(3, 30, "Three", flooringSprites[3], flooringTexture[3]));
        flooringList.Add(new Flooring(4, 40, "Four", flooringSprites[4], flooringTexture[4]));
        SetFlooring(GetComponent<PlayerPrefsManager>().GetFlooring());
	}

    public void SetFlooring(int num)
    {
        floor.mainTexture = flooringList[num].texture;
    }
}
