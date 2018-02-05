using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;

public class PlayerPrefsManager : MonoBehaviour
{
    static string totalOrdersCompleted = "TOTAL ORDERS COMPLETED";
    static string totalFoodLanded = "TOTAL FOOD LANDED";
    static string totalPoints = "TOTAL POINTS";
    static string totalFoodProduced = "TOTAL FOOD PRODUCED";
    static string highScore = "HIGH SCORE";
    static string longestSurvivalTime = "LONGEST SURVIVAL TIME";
    static string totalPlayTime = "TOTAL PLAY TIME";
    static string coins = "COINS";
    static string powerUp = "POWERUP";
    static string graphics = "GRAPHICS";
    static string flooring = "FLOORING";
    static string wallpaper = "WALLPAPER";
    static string detail = "DETAIL";
    static string sound = "SOUND";
    static string music = "MUSIC";
    static string vibration = "VIBRATION";
    static string haptic = "HAPTIC";
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
    static string tutorialThrow = "TutorialThrow";
    static string tutorialTap = "TutorialTap";
    static string thingsUnlocked = "ThingsUnlocked";
    int floorsLeft, wallsLeft, detailsLeft, powerUpsLeft, graphicsLeft;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        GetUnlockValues();
    }

    public void SetOrdersCompleted(int num)
    {
        PlayerPrefs.SetInt(totalOrdersCompleted, num);
    }

    public int GetOrdersCompleted()
    {
        return PlayerPrefs.GetInt(totalOrdersCompleted, 0);
    }

    public void IncreaseOrdersCompleted()
    {
        PlayerPrefs.SetInt(totalOrdersCompleted, (GetOrdersCompleted() + 1));
    }

    public void SetFoodProduced(int num)
    {
        PlayerPrefs.SetInt(totalFoodProduced, num);
    }

    public int GetFoodProduced()
    {
        return PlayerPrefs.GetInt(totalFoodProduced, 0);
    }

    public void IncreaseFoodProduced(int num)
    {
        PlayerPrefs.SetInt(totalFoodProduced, (GetFoodProduced() + num));
    }

    public void SetFoodLanded(int num)
    {
        PlayerPrefs.SetInt(totalFoodLanded, num);
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

    public void SetTotalPoints(int n)
    {
        PlayerPrefs.SetInt(totalPoints, n);
    }

    public void IncreaseTotalPoints(int n)
    {
        PlayerPrefs.SetInt(totalPoints, (GetTotalPoints() + n));
    }

    public bool CheckHighScore(int points)
    {
        if (points > PlayerPrefs.GetInt(highScore, 0))
        {
            GetComponent<OnlineManagement>().CheckScore(points);
            GetComponent<OnlineManagement>().PushHighScore(points);
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
        int milliseconds;
        if (Application.platform == RuntimePlatform.Android)
        {
            milliseconds = Mathf.RoundToInt(time * 1000);
        }
        else
        {
            milliseconds = Mathf.RoundToInt(time * 100);
        }
        IncreasePlayTimeInSeconds(Mathf.RoundToInt(time));
        if (milliseconds > PlayerPrefs.GetInt(longestSurvivalTime, 0))
        {
            GetComponent<OnlineManagement>().CheckTime(milliseconds);
            GetComponent<OnlineManagement>().PushLongestSurvivalTime(milliseconds);
            PlayerPrefs.SetInt(longestSurvivalTime, milliseconds);
            PlayerPrefs.Save();
        }
    }

    public int GetLongestSurvivalTime()
    {
        return PlayerPrefs.GetInt(longestSurvivalTime, 0);
    }

    void SetPlayTimeInSeconds(int seconds)
    {
        PlayerPrefs.GetInt(totalPlayTime, seconds);
    }

    public int GetPlayTimeInSeconds()
    {
        return PlayerPrefs.GetInt(totalPlayTime, 0);
    }

    void IncreasePlayTimeInSeconds(int n)
    {
        PlayerPrefs.SetInt(totalPlayTime, (GetPlayTimeInSeconds() + n));
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
        GetComponent<OnlineManagement>().CheckGraphics(CheckHowManyBought(GetComponent<GraphicsManager>().maxGraphics, specificGraphics));
    }

    public void BuyFlooring(int num)
    {
        PlayerPrefs.SetInt(specificFlooring + num, 1);
        PlayerPrefs.Save();
        GetComponent<OnlineManagement>().CheckFlooring(CheckHowManyBought(GetComponent<ThemeManager>().maxThemes, specificFlooring));
    }

    public void BuyWallpaper(int num)
    {
        PlayerPrefs.SetInt(specificWallpaper + num, 1);
        PlayerPrefs.Save();
        GetComponent<OnlineManagement>().CheckWallpaper(CheckHowManyBought(GetComponent<ThemeManager>().maxThemes, specificWallpaper));
    }

    public void BuyDetail(int num)
    {
        PlayerPrefs.SetInt(specificDetail + num, 1);
        PlayerPrefs.Save();
        GetComponent<OnlineManagement>().CheckDetail(CheckHowManyBought(GetComponent<ThemeManager>().maxThemes, specificDetail));
    }

    public void BuyPowerUp(int powerUpNum)
    {
        PlayerPrefs.SetInt(specificPowerUp + powerUpNum, 1);
        PlayerPrefs.Save();
        GetComponent<OnlineManagement>().CheckPowerUps(CheckHowManyBought(GetComponent<PowerUpsManager>().maxPowerUps, specificPowerUp));
    }

    int CheckHowManyBought(int max, string type)
    {
        int count = 0;
        for (int i = 0; i < max; i++)
        {
            if (PlayerPrefs.GetInt(type + i, 0) == 1)
            {
                count++;
            }
        }
        return count;
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

    public void SetCoins(int num)
    {
        PlayerPrefs.SetInt(coins, num);
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

    public bool GetHaptic()
    {
        return (PlayerPrefs.GetInt(haptic, 1) == 1);
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

    void SaveSetSounds(int n)
    {
        PlayerPrefs.SetInt(sound, n);
        if (n == 0)
        {
            GetComponent<SoundAndMusicManager>().ChangeSoundSetting(false);
        }
        else
        {
            GetComponent<SoundAndMusicManager>().ChangeSoundSetting(true);
        }
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

    void SaveSetMusic(int n)
    {
        PlayerPrefs.SetInt(music, n);
        if (n == 0)
        {
            GetComponent<SoundAndMusicManager>().StopMusic();
        }
        else
        {
            GetComponent<SoundAndMusicManager>().CanPlayMusic();
        }
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

    void SaveSetVibration(int n)
    {
        PlayerPrefs.SetInt(vibration, n);
        if (n == 0)
        {
            GetComponent<VibrationManager>().ChangeVibration(false);
        }
        else
        {
            GetComponent<VibrationManager>().ChangeVibration(true);
        }
    }

    public void SetHaptic()
    {
        if (GetHaptic())
        {
            PlayerPrefs.SetInt(haptic, 0);
            GetComponent<VibrationManager>().ChangeHaptic(false);
        }
        else
        {
            PlayerPrefs.SetInt(haptic, 1);
            GetComponent<VibrationManager>().ChangeHaptic(true);
            GetComponent<VibrationManager>().SuccessTapticFeedback();
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
        return PlayerPrefs.GetInt(nextUnlock, 1000);
    }

    void IncreaseNextUnlock()
    {
        IncreaseThingsUnlocked();
        PlayerPrefs.SetInt(nextUnlock, Mathf.RoundToInt((GetNextUnlock()) + 1000 + (GetThingsUnlocked() * 10)));
    }

    public int GetThingsUnlocked()
    {
        return PlayerPrefs.GetInt(thingsUnlocked, 0);
    }

    public void IncreaseThingsUnlocked()
    {
        PlayerPrefs.SetInt(thingsUnlocked, GetThingsUnlocked() + 1);
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
        int randomValue = UnityEngine.Random.Range(0, (floorsLeft + wallsLeft + detailsLeft + (powerUpsLeft * 2) + graphicsLeft));
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

    public float NotBoughtItemsPercentage()
    {
        int allItems = (GetComponent<ThemeManager>().maxThemes * 3);
        allItems += GetComponent<PowerUpsManager>().maxPowerUps;
        allItems += GetComponent<GraphicsManager>().maxGraphics;
        int itemsLeft = GetComponent<ThemeManager>().ItemsNotBought();
        itemsLeft += GetComponent<PowerUpsManager>().PowerUpsNotBought();
        itemsLeft += GetComponent<GraphicsManager>().GraphicsNotBought();
        float percentage = ((float)itemsLeft / allItems) * 100;
        return percentage;
    }

    public void SetTutorialThrow()
    {
        PlayerPrefs.SetInt(tutorialThrow, 1);
    }

    public int GetTutorialThrow()
    {
        return PlayerPrefs.GetInt(tutorialThrow, 0);
    }

    public void SetTutorialTap()
    {
        PlayerPrefs.SetInt(tutorialTap, 1);
    }

    public int GetTutorialTap()
    {
        return PlayerPrefs.GetInt(tutorialTap, 0);
    }

    public void LoadPlayerPrefsFromSave(byte[] gameSave)
    {
        string saveString = Encoding.ASCII.GetString(gameSave);
        string[] saveStringSplit = saveString.Split('*');
        CheckHighScore(int.Parse(saveStringSplit[0]));
        SetTotalPoints(int.Parse(saveStringSplit[1]));
        CheckSurvivalTime(int.Parse(saveStringSplit[2]));
        SetOrdersCompleted(int.Parse(saveStringSplit[3]));
        SetFoodLanded(int.Parse(saveStringSplit[4]));
        SetFoodProduced(int.Parse(saveStringSplit[5]));
        SetCoins(int.Parse(saveStringSplit[6]));
        SaveSetMusic(int.Parse(saveStringSplit[7]));
        SaveSetSounds(int.Parse(saveStringSplit[8]));
        SaveSetVibration(int.Parse(saveStringSplit[9]));
        PlayerPrefs.SetInt(powerUp + 1, int.Parse(saveStringSplit[10]));
        PlayerPrefs.SetInt(powerUp + 2, int.Parse(saveStringSplit[11]));
        PlayerPrefs.SetInt(powerUp + 3, int.Parse(saveStringSplit[12]));
        GetComponent<ThemeManager>().SetWallpaper(int.Parse(saveStringSplit[13]));
        GetComponent<ThemeManager>().SetFlooring(int.Parse(saveStringSplit[14]));
        GetComponent<ThemeManager>().SetDetail(int.Parse(saveStringSplit[15]));
        GetComponent<GraphicsManager>().SetGraphic(int.Parse(saveStringSplit[16]));
        SetPlayTimeInSeconds(int.Parse(saveStringSplit[17]));
        for (int i = 0; i < 50; i++)
        {
            PlayerPrefs.SetInt(specificPowerUp + i, int.Parse(saveStringSplit[i + 18]));
            PlayerPrefs.SetInt(specificWallpaper + i, int.Parse(saveStringSplit[i + 68]));
            PlayerPrefs.SetInt(specificFlooring + i, int.Parse(saveStringSplit[i + 118]));
            PlayerPrefs.SetInt(specificDetail + i, int.Parse(saveStringSplit[i + 168]));
            PlayerPrefs.SetInt(specificGraphics + i, int.Parse(saveStringSplit[i + 218]));
        }
        GetComponent<PowerUpsManager>().SetPowerUpLists();
        GetComponent<ThemeManager>().SetThemeLists();
        GetComponent<GraphicsManager>().SetGraphicsList();
        GetComponent<PowerUpsManager>().SetPowerUpLED();
        GetComponent<LEDManager>().UpdateCoinsText();
        GetComponent<LEDManager>().CheckIfAnythingUnlocked();
    }

    public byte[] SavePlayerPrefsToByteArray()
    {
        List<Byte[]> data = new List<byte[]>();
        data.Add(CovertToByteArray(GetHighScore()));
        data.Add(CovertToByteArray(GetTotalPoints()));
        data.Add(CovertToByteArray(GetLongestSurvivalTime()));
        data.Add(CovertToByteArray(GetOrdersCompleted()));
        data.Add(CovertToByteArray(GetFoodLanded()));
        data.Add(CovertToByteArray(GetFoodProduced()));
        data.Add(CovertToByteArray(GetCoins()));
        data.Add(CovertToByteArray(GetMusic() ? 1 : 0));
        data.Add(CovertToByteArray(GetSound() ? 1 : 0));
        data.Add(CovertToByteArray(GetVibration() ? 1 : 0));
        data.Add(CovertToByteArray(PlayerPrefs.GetInt(powerUp + 1, -1)));
        data.Add(CovertToByteArray(PlayerPrefs.GetInt(powerUp + 2, -1)));
        data.Add(CovertToByteArray(PlayerPrefs.GetInt(powerUp + 3, -1)));
        data.Add(CovertToByteArray(GetWallpaper()));
        data.Add(CovertToByteArray(GetFlooring()));
        data.Add(CovertToByteArray(GetDetail()));
        data.Add(CovertToByteArray(GetGraphics()));
        data.Add(CovertToByteArray(GetPlayTimeInSeconds()));
        for (int i = 0; i < 50; i++)
        {
            data.Add(CovertToByteArray(PlayerPrefs.GetInt(specificPowerUp + i, 0)));
        }
        for (int i = 0; i < 50; i++)
        {
            data.Add(CovertToByteArray(PlayerPrefs.GetInt(specificWallpaper + i, 0)));
        }
        for (int i = 0; i < 50; i++)
        {
            data.Add(CovertToByteArray(PlayerPrefs.GetInt(specificFlooring + i, 0)));
        }
        for (int i = 0; i < 50; i++)
        {
            data.Add(CovertToByteArray(PlayerPrefs.GetInt(specificDetail + i, 0)));
        }
        for (int i = 0; i < 50; i++)
        {
            data.Add(CovertToByteArray(PlayerPrefs.GetInt(specificGraphics + i, 0)));
        }
        byte[] dataArray = data.SelectMany(a => a).ToArray();
        return dataArray;
    }

    byte[] CovertToByteArray(int number)
    {
        return Encoding.ASCII.GetBytes(number.ToString() + "*");
    }

    ulong ConvertLittleEndian(byte[] array)
    {
        int pos = 0;
        ulong result = 0;
        foreach (byte by in array)
        {
            result |= ((ulong)by) << pos;
            pos += 8;
        }
        return result;
    }
}
