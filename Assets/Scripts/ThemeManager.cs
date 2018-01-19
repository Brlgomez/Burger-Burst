using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public int maxThemes;
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
        if (PlayerPrefs.GetInt(PlayerPrefsManager.specificFlooring + "0") == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsManager.specificFlooring + "0", 1);
            GetComponent<OnlineManagement>().CheckFlooring(1);
        }
        if (PlayerPrefs.GetInt(PlayerPrefsManager.specificWallpaper + "0") == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsManager.specificWallpaper + "0", 1);
            GetComponent<OnlineManagement>().CheckWallpaper(1);
        }
        if (PlayerPrefs.GetInt(PlayerPrefsManager.specificDetail + "0") == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsManager.specificDetail + "0", 1);
            GetComponent<OnlineManagement>().CheckDetail(1);
        }
        SetThemeLists();
        SetFlooring(GetComponent<PlayerPrefsManager>().GetFlooring());
        SetWallpaper(GetComponent<PlayerPrefsManager>().GetWallpaper());
        SetDetail(GetComponent<PlayerPrefsManager>().GetDetail());
    }

    public void SetThemeLists()
    {
        TextAsset t = new TextAsset();
        t = Resources.Load("Themes") as TextAsset;
        string[] allDescriptions = t.text.Split('\n');
        maxThemes = allDescriptions.Length;
        flooringList.Clear();
        wallList.Clear();
        detailList.Clear();
        for (int i = 0; i < maxThemes; i++)
        {
            string description = allDescriptions[i].Replace("NEWLINE", "\n");
            if (i < GetComponent<PlayerPrefsManager>().GetFloorsUnlocked())
            {
                flooringList.Add(new CustomItem(i, int.Parse(description.Split('*')[3]),
                                                PlayerPrefsManager.specificFlooring, description.Split('*')[0] + "Floor"));
            }
            if (i < GetComponent<PlayerPrefsManager>().GetWallsUnlocked())
            {
                wallList.Add(new CustomItem(i, int.Parse(description.Split('*')[4]),
                                            PlayerPrefsManager.specificWallpaper, description.Split('*')[1] + "Wallpaper"));
            }
            if (i < GetComponent<PlayerPrefsManager>().GetDetailUnlocked())
            {
                detailList.Add(new CustomItem(i, int.Parse(description.Split('*')[5]),
                                              PlayerPrefsManager.specificDetail, description.Split('*')[2] + "Detail"));
            }
        }
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
