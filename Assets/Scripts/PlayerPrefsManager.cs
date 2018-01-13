﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    static string totalOrdersCompleted = "TOTAL ORDERS COMPLETED";
    static string totalFoodLanded = "TOTAL FOOD LANDED";
    static string totalPoints = "TOTAL POINTS";
    static string highScore = "HIGH SCORE";
    static string longestSurvivalTime = "LONGEST SURVIVAL TIME";
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
    static string powerUpsUnlocked = "PowerUpsUnlocked";
    static string graphicsUnlocked = "GraphicsUnlocked";
    static string floorsUnlocked = "FlooringUnlocked";
    static string wallsUnlocked = "WallpapersUnlocked";
    static string detailsUnlocked = "DetailsUnlocked";
    static string nextUnlock = "NextUnlock";
    int floorsLeft, wallsLeft, detailsLeft, powerUpsLeft, graphicsLeft;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        GetUnlockValues();
    }

    public int GetOrdersCompleted()
    {
        return PlayerPrefs.GetInt(totalOrdersCompleted, 0);
    }

    public void IncreaseOrdersCompleted()
    {
        PlayerPrefs.SetInt(totalOrdersCompleted, (GetOrdersCompleted() + 1));
    }

    public int GetFoodLanded()
    {
        return PlayerPrefs.GetInt(totalFoodLanded, 0);
    }

    public void IncreaseFoodLanded()
    {
        PlayerPrefs.SetInt(totalFoodLanded, (GetFlooring() + 1));
    }

    public int GetTotalPoints()
    {
        return PlayerPrefs.GetInt(totalPoints, 0);
    }

    public void IncreaseTotalPoints(int n)
    {
        PlayerPrefs.SetInt(totalPoints, (GetTotalPoints() + n));
    }

    public bool CheckHighScore(int points)
    {
        if (points > PlayerPrefs.GetInt(highScore, 0))
        {
            PlayerPrefs.SetInt(highScore, points);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highScore, 0);
    }

    public void CheckSurvivalTime(float time)
    {
        if (time > PlayerPrefs.GetFloat(longestSurvivalTime, 0))
        {
            PlayerPrefs.SetFloat(longestSurvivalTime, time);
            PlayerPrefs.Save();
        }
    }

    public float ConvertSurvivalTimeToMin()
    {
        float time = PlayerPrefs.GetFloat(longestSurvivalTime, 0);
        float min = Mathf.FloorToInt(time / 60);
        min += ((time % 60) / 60);
        min = Mathf.RoundToInt(min * 100);
        min /= 100;
        return min;
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
        PlayerPrefs.Save();
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
        PlayerPrefs.Save();
    }

    public void BuyFlooring(int num)
    {
        PlayerPrefs.SetInt(specificFlooring + num, 1);
        PlayerPrefs.Save();
    }

    public void BuyWallpaper(int num)
    {
        PlayerPrefs.SetInt(specificWallpaper + num, 1);
        PlayerPrefs.Save();
    }

    public void BuyDetail(int num)
    {
        PlayerPrefs.SetInt(specificDetail + num, 1);
        PlayerPrefs.Save();
    }

    public void BuyPowerUp(int powerUpNum)
    {
        PlayerPrefs.SetInt(specificPowerUp + powerUpNum, 1);
        PlayerPrefs.Save();
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
        PlayerPrefs.Save();
    }

    public int GetGraphics()
    {
        return PlayerPrefs.GetInt(graphics, 0);
    }

    public void SetGraphics(int graphicNumber)
    {
        PlayerPrefs.SetInt(graphics, graphicNumber);
        PlayerPrefs.Save();
    }

    public int GetFlooring()
    {
        return PlayerPrefs.GetInt(flooring, 0);
    }

    public void SetFlooring(int num)
    {
        PlayerPrefs.SetInt(flooring, num);
        PlayerPrefs.Save();
    }

    public int GetWallpaper()
    {
        return PlayerPrefs.GetInt(wallpaper, 0);
    }

    public void SetWallpaper(int num)
    {
        PlayerPrefs.SetInt(wallpaper, num);
        PlayerPrefs.Save();
    }

    public int GetDetail()
    {
        return PlayerPrefs.GetInt(detail, 0);
    }

    public void SetDetail(int num)
    {
        PlayerPrefs.SetInt(detail, num);
        PlayerPrefs.Save();
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt(coins, 0);
    }

    public void IncreaseCoins(int amount)
    {
        PlayerPrefs.SetInt(coins, (GetCoins() + amount));
        GetComponent<LEDManager>().UpdateCoinsText();
        PlayerPrefs.Save();
    }

    public void DecreaseCoins(int amount)
    {
        PlayerPrefs.SetInt(coins, GetCoins() - amount);
        GetComponent<LEDManager>().UpdateCoinsText();
        PlayerPrefs.Save();
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
            GetComponent<SoundAndMusicManager>().ChangeSoundSetting(false);
        }
        else
        {
            PlayerPrefs.SetInt(sound, 1);
            GetComponent<ObjectManager>().Horn().GetComponent<Animator>().Play("Horn");
            GetComponent<ObjectManager>().Horn().GetComponent<ParticleSystem>().Play();
            GetComponent<SoundAndMusicManager>().ChangeSoundSetting(true);
            GetComponent<SoundAndMusicManager>().PlayHornSound();
        }
        GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
        PlayerPrefs.Save();
    }

    public void SetMusic()
    {
        GetComponent<SoundAndMusicManager>().PlayStereoSwitchSound();
        if (GetMusic())
        {
            PlayerPrefs.SetInt(music, 0);
            GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().SetBool("Music Off", true);
            GetComponent<ObjectManager>().Stereo().GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().stereoOff;
            GetComponent<ObjectManager>().Stereo().GetComponent<ParticleSystem>().Stop();
            GetComponent<SoundAndMusicManager>().StopMusic();
        }
        else
        {
            PlayerPrefs.SetInt(music, 1);
            GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().SetBool("Music Off", false);
            GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().Play("MusicOn");
            GetComponent<ObjectManager>().Stereo().GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().stereoOn;
            GetComponent<ObjectManager>().Stereo().GetComponent<ParticleSystem>().Play();
            GetComponent<SoundAndMusicManager>().CanPlayMusic();
        }
        GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
        PlayerPrefs.Save();
    }

    public void SetVibration()
    {
        if (GetVibration())
        {
            PlayerPrefs.SetInt(vibration, 0);
            GetComponent<VibrationManager>().ChangeVibration(false);
        }
        else
        {
            PlayerPrefs.SetInt(vibration, 1);
            GetComponent<ObjectManager>().VibratingDevice().GetComponent<Animator>().Play("Vibrating");
            GetComponent<ObjectManager>().VibratingDevice().GetComponent<ParticleSystem>().Play();
            GetComponent<VibrationManager>().ChangeVibration(true);
            GetComponent<VibrationManager>().Vibrate();
            GetComponent<SoundAndMusicManager>().PlayVibrateSound();
        }
        GetComponent<ScreenTextManagment>().ChangeToSettingScreen();
        PlayerPrefs.Save();
    }

    public int GetPowerUpsUnlocked()
    {
        return PlayerPrefs.GetInt(powerUpsUnlocked, 5);
    }

    public void IncreasePowerUpsUnlocked()
    {
        PlayerPrefs.SetInt(powerUpsUnlocked, (GetPowerUpsUnlocked() + 1));
    }

    public int GetGraphicsUnlocked()
    {
        return PlayerPrefs.GetInt(graphicsUnlocked, 5);
    }

    public void IncreaseGraphicsUnlocked()
    {
        PlayerPrefs.SetInt(graphicsUnlocked, (GetGraphicsUnlocked() + 1));
    }

    public int GetFloorsUnlocked()
    {
        return PlayerPrefs.GetInt(floorsUnlocked, 5);
    }

    public void IncreaseFloorsUnlocked()
    {
        PlayerPrefs.SetInt(floorsUnlocked, (GetFloorsUnlocked() + 1));
    }

    public int GetWallsUnlocked()
    {
        return PlayerPrefs.GetInt(wallsUnlocked, 5);
    }

    public void IncreaseWallsUnlocked()
    {
        PlayerPrefs.SetInt(wallsUnlocked, (GetWallsUnlocked() + 1));
    }

    public int GetDetailUnlocked()
    {
        return PlayerPrefs.GetInt(detailsUnlocked, 5);
    }

    public void IncreaseDetailUnlocked()
    {
        PlayerPrefs.SetInt(detailsUnlocked, (GetDetailUnlocked() + 1));
    }

    int GetNextUnlock()
    {
        return PlayerPrefs.GetInt(nextUnlock, 100);
    }

    void IncreaseNextUnlock()
    {
        PlayerPrefs.SetInt(nextUnlock, Mathf.RoundToInt((1.025f * GetNextUnlock()) + 100));
    }

    public string CheckIfAnythingUnlocked()
    {
        string somethingUnlocked = "";
        while (GetTotalPoints() >= GetNextUnlock())
        {
            if (floorsLeft + wallsLeft + detailsLeft + powerUpsLeft + graphicsLeft > 0)
            {
                IncreaseNextUnlock();
                somethingUnlocked = UnlockItem();
            }
            else
            {
                break;
            }
        }
        return somethingUnlocked;
    }

    public string UnlockItem()
    {
        string description = "";
        int randomValue = Random.Range(0, (floorsLeft + wallsLeft + detailsLeft + (powerUpsLeft * 2) + graphicsLeft));
        if (randomValue >= 0 && randomValue < floorsLeft)
        {
            description = "Floor";
            IncreaseFloorsUnlocked();
            GetComponent<ThemeManager>().SetThemeLists();
        }
        else if (randomValue >= floorsLeft && randomValue < (floorsLeft + wallsLeft))
        {
            description = "Wall";
            IncreaseWallsUnlocked();
            GetComponent<ThemeManager>().SetThemeLists();
        }
        else if (randomValue >= (floorsLeft + wallsLeft) && randomValue < (floorsLeft + wallsLeft + detailsLeft))
        {
            description = "Detail";
            IncreaseDetailUnlocked();
            GetComponent<ThemeManager>().SetThemeLists();
        }
        else if (randomValue >= (floorsLeft + wallsLeft + detailsLeft) && randomValue < (floorsLeft + wallsLeft + detailsLeft + (powerUpsLeft * 2)))
        {
            description = "Power";
            IncreasePowerUpsUnlocked();
            GetComponent<PowerUpsManager>().SetPowerUpLists();
        }
        else
        {
            description = "Graphic";
            IncreaseGraphicsUnlocked();
            GetComponent<GraphicsManager>().SetGraphicsList();
        }
        GetUnlockValues();
        return description;
    }

    public int PointsToNextUpgrade()
    {
        if (floorsLeft + wallsLeft + detailsLeft + powerUpsLeft + graphicsLeft > 0)
        {
            return GetNextUnlock() - GetTotalPoints();
        }
        return -1;
    }

    void GetUnlockValues()
    {
        int themeCount = GetComponent<ThemeManager>().maxThemes;
        int powerUpCount = GetComponent<PowerUpsManager>().maxPowerUps;
        int graphicsCount = GetComponent<GraphicsManager>().maxGraphics;
        floorsLeft = themeCount - GetFloorsUnlocked();
        wallsLeft = themeCount - GetWallsUnlocked();
        detailsLeft = themeCount - GetDetailUnlocked();
        powerUpsLeft = powerUpCount - GetPowerUpsUnlocked();
        graphicsLeft = graphicsCount - GetGraphicsUnlocked();
    }
}
