using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public List<Sprite> themeSprites;
    public List<Texture> themeTexture;
    public List<Theme> themeList = new List<Theme>();
    public Theme normal, classic, classicColor, blackAndWhite, noir;

    public class Theme
    {
        public int themeNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;
        public Texture texture;

        public Theme(int powerUpNum, int cost, string info, Sprite icon, Texture tex)
        {
            themeNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Theme " + powerUpNum, 0) == 1);
            description = info;
            sprite = icon;
            texture = tex;
        }
    }

    void Awake()
    {
        themeList.Add(new Theme(0, 0, "Default", themeSprites[0], themeTexture[0]));
        themeList.Add(new Theme(1, 10, "One", themeSprites[1], themeTexture[1]));
        themeList.Add(new Theme(2, 20, "Two", themeSprites[2], themeTexture[2]));
        themeList.Add(new Theme(3, 30, "Three", themeSprites[3], themeTexture[3]));
        themeList.Add(new Theme(4, 40, "Four", themeSprites[4], themeTexture[4]));
    }
}
