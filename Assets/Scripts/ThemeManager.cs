using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Material floor, wall, details;

    public List<Sprite> flooringSprites;
    public List<Texture> flooringTexture;
    public List<CustomItem> flooringList = new List<CustomItem>();

    public List<Sprite> wallSprites;
    public List<Texture> wallTexture;
    public List<CustomItem> wallList = new List<CustomItem>();

    public List<Sprite> detailSprites;
    public List<Texture> detailTexture;
    public List<CustomItem> detailList = new List<CustomItem>();

    public class CustomItem
    {
        public int themeNumber;
        public int price;
        public bool unlocked;
        public string description;
        public Sprite sprite;
        public Texture texture;

        public CustomItem(int num, int cost, string itemType, string info, Sprite icon, Texture tex)
        {
            themeNumber = num;
            price = cost;
            unlocked = (PlayerPrefs.GetInt(itemType + num, 0) == 1);
            description = info;
            sprite = icon;
            texture = tex;
        }
    }

    void Awake()
    {
        PlayerPrefs.SetInt("Flooring0", 1);
        PlayerPrefs.SetInt("Wallpaper0", 1);
        PlayerPrefs.SetInt("Detail0", 1);

        flooringList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificFlooring, "C&B", flooringSprites[0], flooringTexture[0]));
        flooringList.Add(new CustomItem(1, 50, PlayerPrefsManager.specificFlooring, "Woodland", flooringSprites[1], flooringTexture[1]));
        flooringList.Add(new CustomItem(2, 50, PlayerPrefsManager.specificFlooring, "Lawn", flooringSprites[2], flooringTexture[2]));
        flooringList.Add(new CustomItem(3, 50, PlayerPrefsManager.specificFlooring, "Ceramic", flooringSprites[3], flooringTexture[3]));
        flooringList.Add(new CustomItem(4, 50, PlayerPrefsManager.specificFlooring, "Robotic", flooringSprites[4], flooringTexture[4]));
		flooringList.Add(new CustomItem(5, 50, PlayerPrefsManager.specificFlooring, "Minimal", flooringSprites[5], flooringTexture[5]));

		wallList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificWallpaper, "C&B", wallSprites[0], wallTexture[0]));
        wallList.Add(new CustomItem(1, 50, PlayerPrefsManager.specificWallpaper, "Woodland", wallSprites[1], wallTexture[1]));
        wallList.Add(new CustomItem(2, 50, PlayerPrefsManager.specificWallpaper, "Cloudy Sky", wallSprites[2], wallTexture[2]));
        wallList.Add(new CustomItem(3, 50, PlayerPrefsManager.specificWallpaper, "Ceramic", wallSprites[3], wallTexture[3]));
        wallList.Add(new CustomItem(4, 50, PlayerPrefsManager.specificWallpaper, "Robotic", wallSprites[4], wallTexture[4]));
		wallList.Add(new CustomItem(5, 50, PlayerPrefsManager.specificWallpaper, "Minimal", wallSprites[5], wallTexture[5]));

		detailList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificDetail, "C&B", detailSprites[0], detailTexture[0]));
        detailList.Add(new CustomItem(1, 25, PlayerPrefsManager.specificDetail, "Woodland", detailSprites[1], detailTexture[1]));
        detailList.Add(new CustomItem(2, 25, PlayerPrefsManager.specificDetail, "The Great Outdoors", detailSprites[2], detailTexture[2]));
        detailList.Add(new CustomItem(3, 25, PlayerPrefsManager.specificDetail, "Ceramic", detailSprites[3], detailTexture[3]));
        detailList.Add(new CustomItem(4, 25, PlayerPrefsManager.specificDetail, "Robotic", detailSprites[4], detailTexture[4]));
		detailList.Add(new CustomItem(5, 25, PlayerPrefsManager.specificDetail, "Minimal", detailSprites[5], detailTexture[5]));

		SetFlooring(GetComponent<PlayerPrefsManager>().GetFlooring());
        SetWallpaper(GetComponent<PlayerPrefsManager>().GetWallpaper());
        SetDetail(GetComponent<PlayerPrefsManager>().GetDetail());
    }

    public void SetFlooring(int num)
    {
        floor.mainTexture = flooringList[num].texture;
    }

    public void SetWallpaper(int num)
    {
        wall.mainTexture = wallList[num].texture;
    }

    public void SetDetail(int num)
    {
        details.mainTexture = detailList[num].texture;
    }
}
