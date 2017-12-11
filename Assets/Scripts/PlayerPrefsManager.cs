﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    static string highScore = "HIGH SCORE";
    static string coins = "COINS";
    static string powerUp = "POWERUP";
    static string graphics = "GRAPHICS";
    static string flooring = "FLOORING";
    static string wallpaper = "WALLPAPER";
    static string detail = "DETAIL";
    static string sound = "SOUND";
    static string music = "MUSIC";
    static string vibration = "VIBRATION";
    public static string specificPowerUp = "PowerUp";
    public static string specificGraphics = "Graphic";
    public static string specificFlooring = "Flooring";
    public static string specificWallpaper = "Wallpaper";
    public static string specificDetail = "Detail";

    public void CheckHighScore(int points)
    {
        if (points > PlayerPrefs.GetInt(highScore, 0))
        {
            PlayerPrefs.SetInt(highScore, points);
            GetComponent<LEDManager>().UpdateHighScoreText();
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highScore, 0);
    }

    public bool SetUpgrades(int slotPosition, int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt(powerUp + i, -1) == upgradeNumber && upgradeNumber != -1)
            {
                return false;
            }
        }
        PlayerPrefs.SetInt(powerUp + slotPosition, upgradeNumber);
        return true;
    }

    public bool SlotContainsUpgrade(int slotPosition, int upgradeNumber)
    {
        if (PlayerPrefs.GetInt(powerUp + slotPosition, -1) == upgradeNumber)
        {
            return true;
        }
        return false;
    }

    public int WhichSlotContainsUpgrade(int upgradeNumber)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt(powerUp + i, -1) == upgradeNumber && upgradeNumber != -1)
            {
                return i;
            }
        }
        return -1;
    }

    public bool ContainsUpgradeBesidesSlot(int powerUpNumber, int slot)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (i != slot)
            {
                if (PlayerPrefs.GetInt(powerUp + i, -1) == powerUpNumber)
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
            if (PlayerPrefs.GetInt(powerUp + i, -1) == n)
            {
                return true;
            }
        }
        return false;
    }

    public void BuyGraphic(int num)
    {
        PlayerPrefs.SetInt(specificGraphics + num, 1);
    }

    public void BuyFlooring(int num)
    {
        PlayerPrefs.SetInt(specificFlooring + num, 1);
    }

    public void BuyWallpaper(int num)
    {
        PlayerPrefs.SetInt(specificWallpaper + num, 1);
    }

    public void BuyDetail(int num)
    {
        PlayerPrefs.SetInt(specificDetail + num, 1);
    }

    public void BuyPowerUp(int powerUpNum)
    {
        PlayerPrefs.SetInt(specificPowerUp + powerUpNum, 1);
    }

    public bool IsPowerUpUnlocked(int powerUpNum)
    {
        return (PlayerPrefs.GetInt(specificPowerUp + powerUpNum, 0) == 1);
    }

    public int GetPowerUpFromSlot(int slot)
    {
        return PlayerPrefs.GetInt(powerUp + slot, -1);
    }

    public void SetPowerUpSlot(int slot, int powerUpNum)
    {
        PlayerPrefs.SetInt(powerUp + slot, powerUpNum);
    }

    public int GetGraphics()
    {
        return PlayerPrefs.GetInt(graphics, 0);
    }

    public void SetGraphics(int graphicNumber)
    {
        PlayerPrefs.SetInt(graphics, graphicNumber);
    }

    public int GetFlooring()
    {
        return PlayerPrefs.GetInt(flooring, 0);
    }

    public void SetFlooring(int num)
    {
        PlayerPrefs.SetInt(flooring, num);
    }

    public int GetWallpaper()
    {
        return PlayerPrefs.GetInt(wallpaper, 0);
    }

    public void SetWallpaper(int num)
    {
        PlayerPrefs.SetInt(wallpaper, num);
    }

    public int GetDetail()
    {
        return PlayerPrefs.GetInt(detail, 0);
    }

    public void SetDetail(int num)
    {
        PlayerPrefs.SetInt(detail, num);
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt(coins, 0);
    }

    public void IncreaseCoins()
    {
        PlayerPrefs.SetInt(coins, (GetCoins() + 1));
        GetComponent<LEDManager>().UpdateCoinsText();
    }

    public void DecreaseCoins(int amount)
    {
        PlayerPrefs.SetInt(coins, GetCoins() - amount);
        GetComponent<LEDManager>().UpdateCoinsText();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool GetSound()
    {
        return (PlayerPrefs.GetInt(sound, 1) == 1);
    }

    public bool GetMusic()
    {
        return (PlayerPrefs.GetInt(music, 1) == 1);
    }

    public bool GetVibration()
    {
        return (PlayerPrefs.GetInt(vibration, 1) == 1);
    }

    public void SetSound()
    {
        if (GetSound())
        {
            PlayerPrefs.SetInt(sound, 0);
        }
        else
        {
            PlayerPrefs.SetInt(sound, 1);
        }
        GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
    }

    public void SetMusic()
    {
        if (GetMusic())
        {
            PlayerPrefs.SetInt(music, 0);
        }
        else
        {
            PlayerPrefs.SetInt(music, 1);
        }
        GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
    }

    public void SetVibration()
    {
        if (GetVibration())
        {
            PlayerPrefs.SetInt(vibration, 0);
        }
        else
        {
            PlayerPrefs.SetInt(vibration, 1);
        }
        GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
    }
}
