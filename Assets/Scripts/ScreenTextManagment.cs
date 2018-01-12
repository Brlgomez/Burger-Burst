using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
ScreenTextManagment : MonoBehaviour
{
    int initialBurgers, initialFries, initialDrinks;
    GameObject line1, line2, line3, line4, line5, scrollView;
    Color textColor = new Color(0, 0.5f, 1);
    Color notPressable = new Color(1, 1, 1, 0.25f);
    bool pressDown;
    Menus.Menu currentArea;
    Menus.Menu lastArea;
    public Sprite playSprite, powerUpSprite, customizSprite, storeSprite, settingsSprite, backSprite, coinSprite;
    public Sprite burgerSprite, friesSprite, drinkSprite, heartSprite;
    public Sprite graphicsSprite, themeSprite, vibrationSprite, musicSprite, soundSprite, trophySprite;
    public Sprite restartSprite, quitSprite, yesSprite, pointSprite, floorSprite, wallSprite, detailSprite;
    public Sprite arrowSprite, currentSprite, frozen, poisoned, frozenAndPoisoned;
    Sprite currentHeartSprite;

    void Start()
    {
        initialBurgers = GetComponent<Gameplay>().GetBurgerCount();
        initialFries = GetComponent<Gameplay>().GetFriesCount();
        initialDrinks = GetComponent<Gameplay>().GetDrinkCount();
        line1 = GetComponent<ObjectManager>().Phone().transform.GetChild(0).gameObject;
        line2 = GetComponent<ObjectManager>().Phone().transform.GetChild(1).gameObject;
        line3 = GetComponent<ObjectManager>().Phone().transform.GetChild(2).gameObject;
        line4 = GetComponent<ObjectManager>().Phone().transform.GetChild(3).gameObject;
        line5 = GetComponent<ObjectManager>().Phone().transform.GetChild(4).gameObject;
        scrollView = GetComponent<ObjectManager>().Phone().transform.GetChild(5).gameObject;
        TurnOffScrollList();
        TurnOffGameplayImages();
        currentArea = Menus.Menu.PhoneDown;
        currentHeartSprite = heartSprite;
    }

    public void ChangeToTitleText()
    {
        DisableButton(line1, "", null, Color.white);
        DisableButton(line2, "", null, Color.white);
        DisableButton(line3, "", null, Color.white);
        DisableButton(line4, "", null, Color.white);
        DisableButton(line5, "", null, Color.white);
        lastArea = currentArea;
        currentArea = Menus.Menu.Title;
    }

    public void ChangeToMenuText()
    {
        EnableButton(line1, "Play", playSprite, Color.white);
        EnableButton(line2, "Power Ups", powerUpSprite, Color.white);
        EnableButton(line3, "Customize", customizSprite, Color.white);
        EnableButton(line4, "Store", storeSprite, Color.white);
        EnableButton(line5, "Settings", settingsSprite, Color.white);
        TurnOffScrollList();
        TurnOffGameplayImages();
        GetComponent<LEDManager>().MakeUnlockTextBlink();
        line1.GetComponent<TextMesh>().characterSize = 0.025f;
        line2.GetComponent<TextMesh>().characterSize = 0.025f;
        line3.GetComponent<TextMesh>().characterSize = 0.025f;
        line4.GetComponent<TextMesh>().characterSize = 0.025f;
        lastArea = currentArea;
        currentArea = Menus.Menu.MainMenu;
    }

    public void ChangeToGamePlayText()
    {
        DisableButton(line1, "", currentHeartSprite, Color.white);
        EnableButton(line2, "", burgerSprite, Color.white);
        EnableButton(line3, "", friesSprite, Color.white);
        EnableButton(line4, "", drinkSprite, Color.white);
        EnableButton(line5, "", null, Color.white);
        line1.transform.GetChild(6).GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<LEDManager>().RemoveBlinkingLED();
        lastArea = currentArea;
        currentArea = Menus.Menu.Gameplay;
    }

    public void ChangeToUpgradeText()
    {
        if (GetComponent<LEDManager>().GetUnlockLedText() == "Power")
        {
            GetComponent<LEDManager>().EraseUnlockText();
            GetComponent<LEDManager>().RemoveBlinkingLED();
        }
        EnableButton(line1, "", null, Color.white);
        EnableButton(line2, "", null, Color.white);
        EnableButton(line3, "", null, Color.white);
        DisableButton(line4, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        lastArea = currentArea;
        currentArea = Menus.Menu.PowerUps;
        TurnOnScrollList(currentArea);
    }

    public void ChangeToCustomizeScreen()
    {
        EnableButton(line1, "Paint", themeSprite, Color.white);
        EnableButton(line2, "Graphics", graphicsSprite, Color.white);
        DisableButton(line3, "", null, Color.white);
        DisableButton(line4, "", null, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        TurnOffScrollList();
        lastArea = currentArea;
        GetComponent<LEDManager>().RemoveBlinkingLED();
        currentArea = Menus.Menu.Customize;
    }

    public void ChangeToThemeScreen()
    {
        EnableButton(line1, "Flooring", floorSprite, Color.white);
        EnableButton(line2, "Walls", wallSprite, Color.white);
        EnableButton(line3, "Detail", detailSprite, Color.white);
        DisableButton(line4, "", null, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        TurnOffScrollList();
        lastArea = currentArea;
        currentArea = Menus.Menu.Theme;
    }

    public void ChangeToGraphicsScreen()
    {
        if (GetComponent<LEDManager>().GetUnlockLedText() == "Graphic")
        {
            GetComponent<LEDManager>().EraseUnlockText();
            GetComponent<LEDManager>().RemoveBlinkingLED();
        }
        DisableButton(line1, "", null, Color.white);
        EnableButton(line2, "", null, Color.white);
        EnableButton(line3, "", null, Color.white);
        DisableButton(line4, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        lastArea = currentArea;
        currentArea = Menus.Menu.Graphics;
        TurnOnScrollList(currentArea);
    }

    public void ChangeToFlooringScreen()
    {
        if (GetComponent<LEDManager>().GetUnlockLedText() == "Floor")
        {
            GetComponent<LEDManager>().EraseUnlockText();
        }
        DisableButton(line1, "", null, Color.white);
        EnableButton(line2, "", null, Color.white);
        EnableButton(line3, "", null, Color.white);
        DisableButton(line4, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        lastArea = currentArea;
        currentArea = Menus.Menu.Flooring;
        TurnOnScrollList(currentArea);
    }

    public void ChangeToWallpaperScreen()
    {
        if (GetComponent<LEDManager>().GetUnlockLedText() == "Wall")
        {
            GetComponent<LEDManager>().EraseUnlockText();
        }
        DisableButton(line1, "", null, Color.white);
        EnableButton(line2, "", null, Color.white);
        EnableButton(line3, "", null, Color.white);
        DisableButton(line4, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        lastArea = currentArea;
        currentArea = Menus.Menu.Wallpaper;
        TurnOnScrollList(currentArea);
    }

    public void ChangeToDetailScreen()
    {
        if (GetComponent<LEDManager>().GetUnlockLedText() == "Detail")
        {
            GetComponent<LEDManager>().EraseUnlockText();
        }
        DisableButton(line1, "", null, Color.white);
        EnableButton(line2, "", null, Color.white);
        EnableButton(line3, "", null, Color.white);
        DisableButton(line4, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        lastArea = currentArea;
        currentArea = Menus.Menu.Detail;
        TurnOnScrollList(currentArea);
    }

    public void ChangeToStoreScreen()
    {
        EnableButton(line1, "100 coins", coinSprite, Color.white);
        EnableButton(line2, "250 coins", coinSprite, Color.white);
        EnableButton(line3, "1000 coins", coinSprite, Color.white);
        EnableButton(line4, "2500 coins", coinSprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        GetComponent<LEDManager>().RemoveBlinkingLED();
        lastArea = currentArea;
        currentArea = Menus.Menu.Store;
    }

    public void ChangeToSettingScreen()
    {
        if (GetComponent<PlayerPrefsManager>().GetMusic())
        {
            EnableButton(line1, " Music: On", musicSprite, Color.white);
        }
        else
        {
            EnableButton(line1, " Music: Off", musicSprite, Color.white);
        }
        if (GetComponent<PlayerPrefsManager>().GetSound())
        {
            EnableButton(line2, " Sounds: On", soundSprite, Color.white);
        }
        else
        {
            EnableButton(line2, " Sounds: Off", soundSprite, Color.white);
        }
        if (GetComponent<PlayerPrefsManager>().GetVibration())
        {
            EnableButton(line3, " Vibration: On", vibrationSprite, Color.white);
        }
        else
        {
            EnableButton(line3, " Vibration: Off", vibrationSprite, Color.white);
        }
        EnableButton(line4, " Leaderboards\n      & achievments", trophySprite, Color.white);
        EnableButton(line5, "Back", backSprite, Color.white);
        line1.GetComponent<TextMesh>().characterSize = 0.02f;
        line2.GetComponent<TextMesh>().characterSize = 0.02f;
        line3.GetComponent<TextMesh>().characterSize = 0.02f;
        line4.GetComponent<TextMesh>().characterSize = 0.02f;
        GetComponent<LEDManager>().RemoveBlinkingLED();
        lastArea = currentArea;
        currentArea = Menus.Menu.Setting;
    }

    public void ChangeToConfirmationScreen(Menus.Menu menu)
    {
        int currentCoinCount = GetComponent<PlayerPrefsManager>().GetCoins();
        string question = "";
        switch (menu)
        {
            case Menus.Menu.ConfirmationPowerUp:
                question = "Get power up?";
                break;
            case Menus.Menu.ConfirmationGraphics:
                question = "Get graphics?";
                break;
            case Menus.Menu.ConfirmationFlooring:
                question = "Get flooring?";
                break;
            case Menus.Menu.ConfirmationWalls:
                question = "Get wallpaper?";
                break;
            case Menus.Menu.ConfirmationDetail:
                question = "Get detail?";
                break;
        }
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        TurnOffScrollList();
        DisableButton(line1, "Price: " + middleObj.transform.GetChild(0).GetComponent<TextMesh>().text, middleObj.GetComponent<SpriteRenderer>().sprite, Color.white);
        if (int.Parse(middleObj.transform.GetChild(0).GetComponent<TextMesh>().text) <= GetComponent<PlayerPrefsManager>().GetCoins())
        {
            DisableButton(line2, question, null, Color.white);
            EnableButton(line3, "Yes", yesSprite, Color.white);
            DisableButton(line4, currentCoinCount.ToString(), coinSprite, Color.white);
        }
        else
        {
            int middleObjectPrice = int.Parse(middleObj.transform.GetChild(0).GetComponent<TextMesh>().text);
            DisableButton(line2, "Not enough\ncoins. Need", null, Color.white);
            if ((middleObjectPrice - currentCoinCount) == 1)
            {
                DisableButton(line3, (middleObjectPrice - currentCoinCount) + " more\ncoin", null, Color.white);
            }
            else
            {
                DisableButton(line3, (middleObjectPrice - currentCoinCount) + " more\ncoins", null, Color.white);
            }
            line3.GetComponent<TextMesh>().characterSize = 0.025f;
            EnableButton(line4, "Get coins", coinSprite, Color.white);
        }
        EnableButton(line5, "Back", backSprite, Color.white);
        lastArea = currentArea;
        currentArea = menu;
    }

    public void BuyGraphics()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        GetComponent<PlayerPrefsManager>().BuyGraphic(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<PlayerPrefsManager>().SetGraphics(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<GraphicsManager>().graphicList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        GetComponent<GraphicsManager>().SetGraphic(GetComponent<PlayerPrefsManager>().GetGraphics());
        GetComponent<MenuSlider>().ChangeSlotSpriteGraphics();
        BoughtItem(middleObj);
    }

    public void BuyUpgrade()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        GetComponent<PlayerPrefsManager>().BuyPowerUp(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<PowerUpsManager>().powerUpList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        BoughtItem(middleObj);
    }

    public void BuyFlooring()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        GetComponent<PlayerPrefsManager>().BuyFlooring(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<PlayerPrefsManager>().SetFlooring(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<ThemeManager>().flooringList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        GetComponent<MenuSlider>().ChangeSlotSpriteFlooring();
        BoughtItem(middleObj);
    }

    public void BuyWallpaper()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        GetComponent<PlayerPrefsManager>().BuyWallpaper(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<PlayerPrefsManager>().SetWallpaper(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<ThemeManager>().wallList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        GetComponent<MenuSlider>().ChangeSlotSpriteWallpaper();
        BoughtItem(middleObj);
    }

    public void BuyDetail()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        GetComponent<PlayerPrefsManager>().BuyDetail(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<PlayerPrefsManager>().SetDetail(GetComponent<MenuSlider>().GetMiddleObjectNumber());
        GetComponent<ThemeManager>().detailList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        GetComponent<MenuSlider>().ChangeSlotSpriteDetail();
        BoughtItem(middleObj);
    }

    void BoughtItem(GameObject middleObj)
    {
        GetComponent<PlayerPrefsManager>().DecreaseCoins(int.Parse(middleObj.transform.GetChild(0).GetComponent<TextMesh>().text));
        middleObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        middleObj.transform.GetChild(0).GetComponent<TextMesh>().text = "";
        GetComponent<VibrationManager>().SuccessTapticFeedback();
        GetComponent<SoundAndMusicManager>().PlayBoughtItemWithCoinsSound();
    }

    public void ChangeToGameOverText()
    {
        CheckShakeText();
        RemovePingPongHeart();
        if (GetComponent<PlayerPrefsManager>().CheckHighScore(GetComponent<Gameplay>().GetPoints()))
        {
            Color[] highScoreColors = new Color[3];
            highScoreColors[0] = new Color(1, 0.25f, 1);
            highScoreColors[1] = new Color(1, 1, 0.25f);
            highScoreColors[2] = new Color(0.25f, 1, 1);
            line1.AddComponent<PingPongColor>().SetColorAndObject(highScoreColors, 0);
            DisableButton(line1, "NEW HIGH\n     " + GetComponent<Gameplay>().GetPoints().ToString(), pointSprite, Color.white);
            GetComponent<SoundAndMusicManager>().PlayHighScoreSound();
        }
        else
        {
            DisableButton(line1, GetComponent<Gameplay>().GetPoints().ToString(), pointSprite, Color.white);
        }
        EnableButton(line2, "Restart", restartSprite, Color.white);
        EnableButton(line3, "Quit", quitSprite, Color.white);
        if (!GetComponent<Gameplay>().GetContinued())
        {
            if (GetComponent<Gameplay>().ContinuePrice() > 1)
            {
                EnableButton(line4, "Continue for\n" + GetComponent<Gameplay>().ContinuePrice() + " coins?", null, Color.white);
            }
            else
            {
                EnableButton(line4, "Continue for\n" + GetComponent<Gameplay>().ContinuePrice() + " coin?", null, Color.white);
            }
            DisableButton(line5, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
        }
        else
        {
            DisableButton(line4, "", null, Color.white);
            DisableButton(line5, "", null, Color.white);
        }
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        TurnOffGameplayImages();
        lastArea = currentArea;
        currentArea = Menus.Menu.GameOver;
    }

    public void PressedContinue()
    {
        DisableButton(line5, GetComponent<PlayerPrefsManager>().GetCoins().ToString(), coinSprite, Color.white);
    }

    public void ChangeToPauseText()
    {
        RemovePingPongHeart();
        CheckShakeText();
        EnableButton(line1, "Resume", playSprite, Color.white);
        EnableButton(line2, "Restart", restartSprite, Color.white);
        EnableButton(line3, "Quit", quitSprite, Color.white);
        DisableButton(line4, "", null, Color.white);
        DisableButton(line5, "", null, Color.white);
        TurnOffGameplayImages();
        lastArea = currentArea;
        currentArea = Menus.Menu.Pause;
    }

    public void TurnOnExtraButtonImages()
    {
        line2.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.white;
        line1.transform.GetChild(6).transform.localScale = Vector3.one * 8;
        line2.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = arrowSprite;
        line3.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = arrowSprite;
        line4.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = arrowSprite;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = arrowSprite;
    }

    public void ChangeToGrillArea()
    {
        DisableButton(line1, "", currentHeartSprite, Color.white);
        DisableButton(line2, "", burgerSprite, notPressable);
        EnableButton(line3, "", friesSprite, Color.white);
        EnableButton(line4, "", drinkSprite, Color.white);
        EnableButton(line5, "", null, Color.white);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        TurnOnExtraButtonImages();
        line2.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void ChangeToFryerArea()
    {
        DisableButton(line1, "", currentHeartSprite, Color.white);
        EnableButton(line2, "", burgerSprite, Color.white);
        DisableButton(line3, "", friesSprite, notPressable);
        EnableButton(line4, "", drinkSprite, Color.white);
        EnableButton(line5, "", null, Color.white);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        TurnOnExtraButtonImages();
        line3.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void ChangeToSodaMachineArea()
    {
        DisableButton(line1, "", currentHeartSprite, Color.white);
        EnableButton(line2, "", burgerSprite, Color.white);
        EnableButton(line3, "", friesSprite, Color.white);
        DisableButton(line4, "", drinkSprite, notPressable);
        EnableButton(line5, "", null, Color.white);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        TurnOnExtraButtonImages();
        line4.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void ChangeToFrontArea()
    {
        DisableButton(line1, "", currentHeartSprite, Color.white);
        EnableButton(line2, "", burgerSprite, Color.white);
        EnableButton(line3, "", friesSprite, Color.white);
        EnableButton(line4, "", drinkSprite, Color.white);
        DisableButton(line5, "", null, Color.white);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        TurnOnExtraButtonImages();
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = currentSprite;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = notPressable;
    }

    void TurnOffGameplayImages()
    {
        line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(6).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(6).transform.localScale = Vector3.zero;
        line2.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line3.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line4.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void ChangeHealthCount(int num)
    {
        int n = Mathf.RoundToInt(Camera.main.GetComponent<Gameplay>().GetLife());
        if (n >= 1)
        {
            ChangeHealthTextColor();
            if (line1.GetComponent<ShakeText>() == null && currentArea != Menus.Menu.GameOver)
            {
                line1.AddComponent<ShakeText>().ChangeColor(num);
                line1.transform.GetChild(6).gameObject.AddComponent<ShakeText>().ChangeSpriteColor(num);
            }
        }
        else
        {
            ChangeToGameOverText();
        }
    }

    public void ChangeHealthTextColor()
    {
        int n = Mathf.RoundToInt(Camera.main.GetComponent<Gameplay>().GetLife());
        line1.GetComponent<TextMesh>().text = "";
        if (!GetComponent<Gameplay>().HaveMoreLife())
        {
            Color newColor = Color.Lerp(Color.red, textColor, ((float)n) / GetComponent<Gameplay>().GetMaxLife());
            line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = newColor;
            line1.transform.GetChild(4).transform.localScale = new Vector3((((float)n) / GetComponent<Gameplay>().GetMaxLife()) * 255, 90, 1);
            line1.transform.GetChild(5).transform.localScale = new Vector3(0, 90, 1);
        }
        else
        {
            if (n <= 100)
            {
                Color newColor = Color.Lerp(Color.red, textColor, (((float)n) / (GetComponent<Gameplay>().GetMaxLife() / 2)));
                line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = newColor;
                line1.transform.GetChild(4).transform.localScale = new Vector3(
                    (((float)n) / (GetComponent<Gameplay>().GetMaxLife() / 2)) * 255,
                    90,
                    1
                );
                line1.transform.GetChild(5).transform.localScale = new Vector3(0, 90, 1);
            }
            else
            {
                Color newColor = Color.Lerp(
                    Color.green,
                    Color.cyan,
                    ((float)n - (GetComponent<Gameplay>().GetMaxLife() / 2)) / (GetComponent<Gameplay>().GetMaxLife() / 2)
                );
                line1.transform.GetChild(4).transform.localScale = new Vector3(255, 90, 1);
                line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = textColor;
                line1.transform.GetChild(5).transform.localScale = new Vector3(
                    (((float)n - (GetComponent<Gameplay>().GetMaxLife() / 2)) / (GetComponent<Gameplay>().GetMaxLife() / 2)) * 255,
                    90,
                    1
                );
                line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = newColor;
            }
        }
    }

    public void ChangeBurgerCount(int num)
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            ChangeBurgerTextColor();
            if (line2.GetComponent<ShakeText>() == null)
            {
                line2.AddComponent<ShakeText>().ChangeColor(num);
            }
        }
    }

    void ChangeBurgerTextColor()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetBurgerCount();
        Color newColor = Color.Lerp(Color.red, textColor, ((float)n) / initialBurgers);
        line2.GetComponent<TextMesh>().color = newColor;
        line2.GetComponent<TextMesh>().text = "      " + n;
    }

    public void ChangeFriesCount(int num)
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            ChangeFriesTextColor();
            if (line3.GetComponent<ShakeText>() == null)
            {
                line3.AddComponent<ShakeText>().ChangeColor(num);
            }
        }
    }

    void ChangeFriesTextColor()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetFriesCount();
        Color newColor = Color.Lerp(Color.red, textColor, ((float)n) / initialFries);
        line3.GetComponent<TextMesh>().color = newColor;
        line3.GetComponent<TextMesh>().text = "      " + n;
    }

    public void ChangeDrinkCount(int num)
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            ChangeDrinkTextColor();
            if (line4.GetComponent<ShakeText>() == null)
            {
                line4.AddComponent<ShakeText>().ChangeColor(num);
            }
        }
    }

    void ChangeDrinkTextColor()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetDrinkCount();
        Color newColor = Color.Lerp(Color.red, textColor, ((float)n) / initialDrinks);
        line4.GetComponent<TextMesh>().color = newColor;
        line4.GetComponent<TextMesh>().text = "      " + n;
    }

    public void RestartScreens()
    {
        ChangeHealthTextColor();
        ChangeTextColorToOriginal();
        line2.GetComponent<TextMesh>().text = "      " + initialBurgers;
        line3.GetComponent<TextMesh>().text = "      " + initialFries;
        line4.GetComponent<TextMesh>().text = "      " + initialDrinks;
    }

    void DisableButton(GameObject button, string text, Sprite icon, Color iconColor)
    {
        button.transform.GetChild(0).gameObject.layer = 2;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.clear;
        button.transform.GetChild(1).GetComponent<SpriteRenderer>().color = iconColor;
        button.GetComponent<TextMesh>().color = textColor;
        if (icon != null)
        {
            button.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = icon;
            button.GetComponent<TextMesh>().text = "     " + text;
        }
        else
        {
            button.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
            button.GetComponent<TextMesh>().text = text;
        }
    }

    void EnableButton(GameObject button, string text, Sprite icon, Color iconColor)
    {
        button.transform.GetChild(0).gameObject.layer = 0;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
        button.transform.GetChild(1).GetComponent<SpriteRenderer>().color = iconColor;
        button.GetComponent<TextMesh>().color = textColor;
        if (icon != null)
        {
            button.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = icon;
            button.GetComponent<TextMesh>().text = "     " + text;
        }
        else
        {
            button.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
            button.GetComponent<TextMesh>().text = text;
        }
        button.GetComponent<TextMesh>().color = textColor;
    }

    void ChangeTextColorToOriginal()
    {
        line1.GetComponent<TextMesh>().color = textColor;
        line2.GetComponent<TextMesh>().color = textColor;
        line3.GetComponent<TextMesh>().color = textColor;
        line4.GetComponent<TextMesh>().color = textColor;
        line5.GetComponent<TextMesh>().color = textColor;
    }

    public void PressTextDown(GameObject target)
    {
        if (!pressDown && target.GetComponent<TextMesh>() != null)
        {
            if (target.transform.childCount > 2)
            {
                target.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.grey;
            }
            pressDown = true;
            if (target.transform.childCount > 1 && currentArea == Menus.Menu.Gameplay)
            {
                target.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.grey;
                if (target.name != "Line1")
                {
                    target.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }
        }
        if (!pressDown && target == scrollView && currentArea != Menus.Menu.Gameplay)
        {
            GetComponent<MenuSlider>().ChangeScrollerItemColor(true);
            pressDown = true;
        }
    }

    public void PressTextUp(GameObject target)
    {
        if (pressDown && target.GetComponent<TextMesh>() != null)
        {
            if (target.transform.childCount > 2 && currentArea != Menus.Menu.Gameplay)
            {
                target.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
            }
            pressDown = false;
            if (target.transform.childCount > 1 && currentArea == Menus.Menu.Gameplay)
            {
                //target.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
                if (target.name != "Line1")
                {
                    target.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        if (pressDown && target == scrollView && currentArea != Menus.Menu.Gameplay)
        {
            GetComponent<MenuSlider>().ChangeScrollerItemColor(false);
            pressDown = false;
        }
    }

    public Menus.Menu GetMenu()
    {
        return currentArea;
    }

    public Menus.Menu GetLastMenu()
    {
        return lastArea;
    }

    public void ChangeSliderInfo()
    {
        int n = GetComponent<MenuSlider>().GetMiddleObjectNumber();
        line3.GetComponent<TextMesh>().text = GetComponent<MenuSlider>().GetSliderDescription(n);
    }

    void TurnOffScrollList()
    {
        GetComponent<MenuSlider>().TurnOffScrollView();
        line3.GetComponent<TextMesh>().characterSize = 0.025f;
    }

    void TurnOnScrollList(Menus.Menu menu)
    {
        GetComponent<MenuSlider>().TurnOnScrollView(menu);
        ChangeSliderInfo();
        line2.transform.GetChild(0).gameObject.layer = 2;
        line3.GetComponent<TextMesh>().characterSize = 0.01625f;
    }

    public void CannotPressAnything()
    {
        GetComponent<MenuSlider>().TurnOffSliderColliders();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line2.transform.GetChild(0).gameObject.layer = 2;
        line3.transform.GetChild(0).gameObject.layer = 2;
        line4.transform.GetChild(0).gameObject.layer = 2;
        line5.transform.GetChild(0).gameObject.layer = 2;
    }

    void CheckShakeText()
    {
        if (GetComponent<GettingHurt>())
        {

        }
        if (line1.GetComponent<ShakeText>())
        {
            line1.GetComponent<ShakeText>().Finished();
        }
        if (line2.GetComponent<ShakeText>())
        {
            line2.GetComponent<ShakeText>().Finished();
        }
        if (line3.GetComponent<ShakeText>())
        {
            line3.GetComponent<ShakeText>().Finished();
        }
        if (line4.GetComponent<ShakeText>())
        {
            line4.GetComponent<ShakeText>().Finished();
        }
        if (line5.GetComponent<ShakeText>())
        {
            line5.GetComponent<ShakeText>().Finished();
        }
    }

    public void ChangeHeartSprite(string justChanged)
    {
        bool ice = false;
        bool poison = false;
        currentHeartSprite = heartSprite;
        if (GetComponent<Poisoned>() != null && justChanged != "Poisoned")
        {
            poison = true;
            currentHeartSprite = poisoned;
        }
        if (GetComponent<Frozen>() != null && justChanged != "Frozen")
        {
            ice = true;
            currentHeartSprite = frozen;
        }
        if (poison && ice)
        {
            currentHeartSprite = frozenAndPoisoned;
        }
        if (!GetComponent<Gameplay>().IsGameOver())
        {
            line1.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = currentHeartSprite;
        }
    }

    public void PingPongHeart()
    {
        if (GetComponent<Gameplay>().GetLife() < 25)
        {
            if (line1.transform.GetChild(1).gameObject.GetComponent<PingPongColor>() == null)
            {
                Color[] colors = new Color[2];
                colors[0] = new Color(1, 1, 1, 1);
                colors[1] = new Color(1, 1, 1, 0);
                line1.transform.GetChild(1).gameObject.AddComponent<PingPongColor>().SetColorAndObject(colors, 2);
            }
        }
    }

    public void RemovePingPongHeart()
    {
        if (line1.transform.GetChild(1).gameObject.GetComponent<PingPongColor>() != null)
        {
            Destroy(line1.transform.GetChild(1).gameObject.GetComponent<PingPongColor>());
            line1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
