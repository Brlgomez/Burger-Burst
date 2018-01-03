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

        flooringList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificFlooring, "C&B Floor", flooringSprites[0], flooringTexture[0]));
        flooringList.Add(new CustomItem(1, 50, PlayerPrefsManager.specificFlooring, "Woodland Floor", flooringSprites[1], flooringTexture[1]));
        flooringList.Add(new CustomItem(2, 50, PlayerPrefsManager.specificFlooring, "Lawn Floor", flooringSprites[2], flooringTexture[2]));
        flooringList.Add(new CustomItem(3, 50, PlayerPrefsManager.specificFlooring, "Ceramic Floor", flooringSprites[3], flooringTexture[3]));
        flooringList.Add(new CustomItem(4, 50, PlayerPrefsManager.specificFlooring, "Robotic Floor", flooringSprites[4], flooringTexture[4]));
        flooringList.Add(new CustomItem(5, 50, PlayerPrefsManager.specificFlooring, "Minimal Floor", flooringSprites[5], flooringTexture[5]));
        flooringList.Add(new CustomItem(6, 50, PlayerPrefsManager.specificFlooring, "Pancake Tuesday\nFloor", flooringSprites[6], flooringTexture[6]));
        flooringList.Add(new CustomItem(7, 50, PlayerPrefsManager.specificFlooring, "Bubbles Floor", flooringSprites[7], flooringTexture[7]));
		flooringList.Add(new CustomItem(8, 50, PlayerPrefsManager.specificFlooring, "Groovy Floor", flooringSprites[8], flooringTexture[8]));

		wallList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificWallpaper, "C&B Wallpaper", wallSprites[0], wallTexture[0]));
        wallList.Add(new CustomItem(1, 50, PlayerPrefsManager.specificWallpaper, "Woodland Wallpaper", wallSprites[1], wallTexture[1]));
        wallList.Add(new CustomItem(2, 50, PlayerPrefsManager.specificWallpaper, "Cloudy Sky Wallpaper", wallSprites[2], wallTexture[2]));
        wallList.Add(new CustomItem(3, 50, PlayerPrefsManager.specificWallpaper, "Ceramic Wallpaper", wallSprites[3], wallTexture[3]));
        wallList.Add(new CustomItem(4, 50, PlayerPrefsManager.specificWallpaper, "Robotic Wallpaper", wallSprites[4], wallTexture[4]));
        wallList.Add(new CustomItem(5, 50, PlayerPrefsManager.specificWallpaper, "Minimal Wallpaper", wallSprites[5], wallTexture[5]));
        wallList.Add(new CustomItem(6, 50, PlayerPrefsManager.specificWallpaper, "Pancake Tuesday\nWallpaper", wallSprites[6], wallTexture[6]));
        wallList.Add(new CustomItem(7, 50, PlayerPrefsManager.specificWallpaper, "Electric Fish Wallpaper", wallSprites[7], wallTexture[7]));
		wallList.Add(new CustomItem(8, 50, PlayerPrefsManager.specificWallpaper, "Grovy Wallpaper", wallSprites[8], wallTexture[8]));

		detailList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificDetail, "C&B Detail", detailSprites[0], detailTexture[0]));
        detailList.Add(new CustomItem(1, 25, PlayerPrefsManager.specificDetail, "Woodland Detail", detailSprites[1], detailTexture[1]));
        detailList.Add(new CustomItem(2, 25, PlayerPrefsManager.specificDetail, "Outdoors Detail", detailSprites[2], detailTexture[2]));
        detailList.Add(new CustomItem(3, 25, PlayerPrefsManager.specificDetail, "Ceramic Detail", detailSprites[3], detailTexture[3]));
        detailList.Add(new CustomItem(4, 25, PlayerPrefsManager.specificDetail, "Robotic Detail", detailSprites[4], detailTexture[4]));
        detailList.Add(new CustomItem(5, 25, PlayerPrefsManager.specificDetail, "Minimal Detail", detailSprites[5], detailTexture[5]));
        detailList.Add(new CustomItem(6, 25, PlayerPrefsManager.specificDetail, "Pancake Tuesday\nDetail", detailSprites[6], detailTexture[6]));
        detailList.Add(new CustomItem(7, 25, PlayerPrefsManager.specificDetail, "Electric Blue Detail", detailSprites[7], detailTexture[7]));
		detailList.Add(new CustomItem(8, 25, PlayerPrefsManager.specificDetail, "Groovy Detail", detailSprites[8], detailTexture[8]));

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
