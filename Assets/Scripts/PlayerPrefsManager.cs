using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public bool SetUpgrades(int position, int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == upgradeNumber && upgradeNumber != -1)
            {
                return false;
            }
        }
        PlayerPrefs.SetInt("UPGRADE " + position, upgradeNumber);
        return true;
    }

    public bool SlotContainsUpgrade(int pos, int upgradeNumber)
    {
        if (PlayerPrefs.GetInt("UPGRADE " + pos, -1) == upgradeNumber)
        {
            return true;
        }
        return false;
    }

    public int WhichSlotContainsUpgrade(int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == upgradeNumber && upgradeNumber != -1)
            {
                return i;
            }
        }
        return -1;
    }

    public bool ContainsUpgradeBesidesSlot(int n, int slot)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (i != slot)
            {
                if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == n)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool ContainsUpgrade(int n)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("UPGRADE " + i, -1) == n)
            {
                return true;
            }
        }
        return false;
    }

    public void BuyGraphic(int num)
    {
        PlayerPrefs.SetInt("Graphic " + num, 1);
    }

    public void BuyFlooring(int num)
    {
        PlayerPrefs.SetInt("Flooring " + num, 1);
    }

    public void BuyWallpaper(int num)
    {
        PlayerPrefs.SetInt("Wallpaper " + num, 1);
    }

    public void BuyDetail(int num)
    {
        PlayerPrefs.SetInt("Detail " + num, 1);
    }

    public void BuyPowerUp(int num)
    {
        PlayerPrefs.SetInt("Power Up " + num, 1);
    }

    public bool IsPowerUpUnlocked(int powerUp)
    {
        return (PlayerPrefs.GetInt("Power Up " + powerUp, 0) == 1);
    }

    public int GetPowerUpFromSlot(int slot)
    {
        return PlayerPrefs.GetInt("UPGRADE " + slot, -1);
    }

    public void SetPowerUpSlot(int slot, int powerUpNum)
    {
        PlayerPrefs.SetInt("UPGRADE " + slot, powerUpNum);
    }

    public int GetGraphics()
    {
        return PlayerPrefs.GetInt("GRAPHICS", 0);
    }

    public void SetGraphics(int graphicNumber)
    {
        PlayerPrefs.SetInt("GRAPHICS", graphicNumber);
    }

    public int GetFlooring()
    {
        return PlayerPrefs.GetInt("FLOORING", 0);
    }

    public void SetFlooring(int num)
    {
        PlayerPrefs.SetInt("FLOORING", num);
    }

    public int GetWallpaper()
    {
        return PlayerPrefs.GetInt("WALLPAPER", 0);
    }

    public void SetWallpaper(int num)
    {
        PlayerPrefs.SetInt("WALLPAPER", num);
    }

    public int GetDetail()
    {
        return PlayerPrefs.GetInt("DETAIL", 0);
    }

    public void SetDetail(int num)
    {
        PlayerPrefs.SetInt("DETAIL", num);
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public void DecreaseCoins(int amount)
    {
        PlayerPrefs.SetInt("Coins", GetCoins() - amount);
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
