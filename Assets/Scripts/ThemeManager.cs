using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public List<Sprite> themeSprites;
    public List<Material> themeMaterials;
    public List<Theme> themeList = new List<Theme>();
    public Theme normal, classic, classicColor, blackAndWhite, noir;

    public class Theme
    {
        public int themeNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;
        public Material themeMaterial;

        public Theme(int powerUpNum, int cost, string info, Sprite icon, Material mat)
        {
            themeNumber = powerUpNum;
            price = cost;
            unlocked = (PlayerPrefs.GetInt("Theme " + powerUpNum, 0) == 1);
            description = info;
            sprite = icon;
            themeMaterial = mat;
        }
    }

    void Awake()
    {
        PlayerPrefs.SetInt("Theme 0", 1);
        themeList.Add(new Theme(0, 0, "Default", themeSprites[0], themeMaterials[0]));
        themeList.Add(new Theme(1, 10, "", themeSprites[1], themeMaterials[1]));
        themeList.Add(new Theme(2, 20, "", themeSprites[2], themeMaterials[2]));
        themeList.Add(new Theme(3, 30, "", themeSprites[3], themeMaterials[3]));
        themeList.Add(new Theme(4, 40, "", themeSprites[4], themeMaterials[4]));
    }
}
