using System.Collections;
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

        flooringList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificFlooring, "C&B Floor"));
        flooringList.Add(new CustomItem(1, 50, PlayerPrefsManager.specificFlooring, "Woodland Floor"));
        flooringList.Add(new CustomItem(2, 50, PlayerPrefsManager.specificFlooring, "Lawn Floor"));
        flooringList.Add(new CustomItem(3, 50, PlayerPrefsManager.specificFlooring, "Ceramic Floor"));
        flooringList.Add(new CustomItem(4, 50, PlayerPrefsManager.specificFlooring, "Robotic Floor"));
        flooringList.Add(new CustomItem(5, 50, PlayerPrefsManager.specificFlooring, "Minimal Floor"));
        flooringList.Add(new CustomItem(6, 50, PlayerPrefsManager.specificFlooring, "Pancake Tuesday\nFloor"));
        flooringList.Add(new CustomItem(7, 50, PlayerPrefsManager.specificFlooring, "Bubbles Floor"));
        flooringList.Add(new CustomItem(8, 50, PlayerPrefsManager.specificFlooring, "Groovy Baby Floor"));
        flooringList.Add(new CustomItem(9, 50, PlayerPrefsManager.specificFlooring, "Shabby Chic Floor"));
        flooringList.Add(new CustomItem(10, 50, PlayerPrefsManager.specificFlooring, "Paw Print Floor"));
		flooringList.Add(new CustomItem(11, 50, PlayerPrefsManager.specificFlooring, "Retro Floor"));

		wallList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificWallpaper, "C&B Wallpaper"));
        wallList.Add(new CustomItem(1, 50, PlayerPrefsManager.specificWallpaper, "Woodland Wallpaper"));
        wallList.Add(new CustomItem(2, 50, PlayerPrefsManager.specificWallpaper, "Cloudy Sky Wallpaper"));
        wallList.Add(new CustomItem(3, 50, PlayerPrefsManager.specificWallpaper, "Ceramic Wallpaper"));
        wallList.Add(new CustomItem(4, 50, PlayerPrefsManager.specificWallpaper, "Robotic Wallpaper"));
        wallList.Add(new CustomItem(5, 50, PlayerPrefsManager.specificWallpaper, "Minimal Wallpaper"));
        wallList.Add(new CustomItem(6, 50, PlayerPrefsManager.specificWallpaper, "Pancake Tuesday\nWallpaper"));
        wallList.Add(new CustomItem(7, 50, PlayerPrefsManager.specificWallpaper, "Electric Fish Wallpaper"));
        wallList.Add(new CustomItem(8, 50, PlayerPrefsManager.specificWallpaper, "Grovy Baby Wallpaper"));
        wallList.Add(new CustomItem(9, 50, PlayerPrefsManager.specificWallpaper, "Shabby Chic Wallpaper"));
        wallList.Add(new CustomItem(10, 50, PlayerPrefsManager.specificWallpaper, "Bow Wow Wallpaper"));
		wallList.Add(new CustomItem(11, 50, PlayerPrefsManager.specificWallpaper, "Retro Wallpaper"));

		detailList.Add(new CustomItem(0, 0, PlayerPrefsManager.specificDetail, "C&B Detail"));
        detailList.Add(new CustomItem(1, 25, PlayerPrefsManager.specificDetail, "Woodland Detail"));
        detailList.Add(new CustomItem(2, 25, PlayerPrefsManager.specificDetail, "Outdoors Detail"));
        detailList.Add(new CustomItem(3, 25, PlayerPrefsManager.specificDetail, "Ceramic Detail"));
        detailList.Add(new CustomItem(4, 25, PlayerPrefsManager.specificDetail, "Robotic Detail"));
        detailList.Add(new CustomItem(5, 25, PlayerPrefsManager.specificDetail, "Minimal Detail"));
        detailList.Add(new CustomItem(6, 25, PlayerPrefsManager.specificDetail, "Pancake Tuesday\nDetail"));
        detailList.Add(new CustomItem(7, 25, PlayerPrefsManager.specificDetail, "Electric Blue Detail"));
        detailList.Add(new CustomItem(8, 25, PlayerPrefsManager.specificDetail, "Groovy Baby Detail"));
        detailList.Add(new CustomItem(9, 25, PlayerPrefsManager.specificDetail, "Shabby Chic Detail"));
        detailList.Add(new CustomItem(10, 25, PlayerPrefsManager.specificDetail, "Woof Detail"));
		detailList.Add(new CustomItem(11, 25, PlayerPrefsManager.specificDetail, "Retro Detail"));

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
