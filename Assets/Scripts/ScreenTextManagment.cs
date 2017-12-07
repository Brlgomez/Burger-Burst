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
    public Sprite playSprite, powerUpSprite, customizSprite, storeSprite, settingsSprite, backSprite, coinSprite;
    public Sprite burgerSprite, friesSprite, drinkSprite, heartSprite;
    public Sprite graphicsSprite, themeSprite, vibrationSprite, musicSprite, soundSprite, trophySprite;
    public Sprite restartSprite, quitSprite, yesSprite, pointSprite;

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
        ChangeToMenuText();
    }

    public void ChangeToMenuText()
    {
        EnableButton(line1, "Play", playSprite);
        EnableButton(line2, "Power Ups", powerUpSprite);
        EnableButton(line3, "Customize", customizSprite);
        EnableButton(line4, "Store", storeSprite);
        EnableButton(line5, "Settings", settingsSprite);
        TurnOffScrollList();
        TurnOffGameplayImages();
        currentArea = Menus.Menu.MainMenu;
    }

    public void ChangeToGamePlayText()
    {
        DisableButton(line1, "", heartSprite);
        EnableButton(line2, "", burgerSprite);
        EnableButton(line3, "", friesSprite);
        EnableButton(line4, "", drinkSprite);
        EnableButton(line5, "", null);
        line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.black;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(3).gameObject.layer = 0;
        currentArea = Menus.Menu.Gameplay;
    }

    public void ChangeToUpgradeText()
    {
        EnableButton(line1, "", null);
        EnableButton(line2, "", null);
        EnableButton(line3, "", null);
        DisableButton(line4, PlayerPrefs.GetInt("Coins", 0).ToString(), coinSprite);
        EnableButton(line5, "Back", backSprite);
        currentArea = Menus.Menu.PowerUps;
        TurnOnScrollList(currentArea);
    }

    public void ChangeToCustomizeScreen()
    {
        EnableButton(line1, "Theme", themeSprite);
        EnableButton(line2, "Graphics", graphicsSprite);
        DisableButton(line3, "", null);
        DisableButton(line4, "", null);
        EnableButton(line5, "Back", backSprite);
        TurnOffScrollList();
        currentArea = Menus.Menu.Customize;
    }

    public void ChangeToGraphicsScreen()
    {
        DisableButton(line1, "", null);
        EnableButton(line2, "", null);
        EnableButton(line3, "", null);
        DisableButton(line4, PlayerPrefs.GetInt("Coins", 0).ToString(), coinSprite);
        EnableButton(line5, "Back", backSprite);
        currentArea = Menus.Menu.Graphics;
        TurnOnScrollList(currentArea);
    }

	public void ChangeToThemeScreen()
	{
		DisableButton(line1, "", null);
		EnableButton(line2, "", null);
		EnableButton(line3, "", null);
		DisableButton(line4, PlayerPrefs.GetInt("Coins", 0).ToString(), coinSprite);
		EnableButton(line5, "Back", backSprite);
        currentArea = Menus.Menu.Theme;
		TurnOnScrollList(currentArea);
	}

    public void ChangeToStoreScreen()
    {
        EnableButton(line1, "100 coins", coinSprite);
        EnableButton(line2, "250 coins", coinSprite);
        EnableButton(line3, "1000 coins", coinSprite);
        EnableButton(line4, "2500 coins", coinSprite);
        EnableButton(line5, "Back", backSprite);
        currentArea = Menus.Menu.Store;
    }

    public void ChangeToSettingScreen()
    {
        EnableButton(line1, "Vibration", vibrationSprite);
        EnableButton(line2, "Music", musicSprite);
        EnableButton(line3, "Sound", soundSprite);
        EnableButton(line4, "Game Center", trophySprite);
        EnableButton(line5, "Back", backSprite);
        currentArea = Menus.Menu.Setting;
    }

    public void ChangeToConfirmationScreen(Menus.Menu menu, string question)
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        TurnOffScrollList();
        DisableButton(line1, middleObj.transform.GetChild(0).GetComponent<TextMesh>().text, middleObj.GetComponent<SpriteRenderer>().sprite);
        if (int.Parse(middleObj.transform.GetChild(0).GetComponent<TextMesh>().text) <= PlayerPrefs.GetInt("Coins", 0))
        {
            DisableButton(line2, question, null);
            EnableButton(line3, "Yes", yesSprite);
        }
        else
        {
            DisableButton(line2, "", null);
            line3.GetComponent<TextMesh>().characterSize = 0.025f;
            DisableButton(line3, "Coins needed:\n" +
                          (int.Parse(middleObj.transform.GetChild(0).GetComponent<TextMesh>().text) - PlayerPrefs.GetInt("Coins", 0)), null);
        }
        EnableButton(line4, "Get coins", coinSprite);
        EnableButton(line5, "Back", backSprite);
        currentArea = menu;
    }

    public void BuyGraphics()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        PlayerPrefs.SetInt("Graphic " + int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name), 1);
        GetComponent<GraphicsManager>().graphicList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
		BoughtItem(middleObj);
	}

    public void BuyUpgrade()
    {
        GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
        PlayerPrefs.SetInt("Power Up " + int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name), 1);
        GetComponent<PowerUpsManager>().powerUpList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
		BoughtItem(middleObj);
	}

	public void BuyTheme()
	{
		GameObject middleObj = GetComponent<MenuSlider>().GetMiddleObject();
		PlayerPrefs.SetInt("Theme " + int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name), 1);
        GetComponent<ThemeManager>().themeList[int.Parse(middleObj.GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        BoughtItem(middleObj);
	}

    void BoughtItem(GameObject middleObj)
    {
		GetComponent<Gameplay>().DecreaseCoinCount(int.Parse(middleObj.transform.GetChild(0).GetComponent<TextMesh>().text));
		middleObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
		middleObj.transform.GetChild(0).GetComponent<TextMesh>().text = "";
    }

    public void ChangeToGameOverText()
    {
        EnableButton(line1, "Restart", restartSprite);
        EnableButton(line2, "Quit", quitSprite);
        DisableButton(line3, GetComponent<Gameplay>().GetPoints().ToString(), pointSprite);
        DisableButton(line4, PlayerPrefs.GetInt("Coins", 0).ToString(), coinSprite);
        DisableButton(line5, "", null);
        line1.GetComponent<TextMesh>().color = Color.red;
        line2.GetComponent<TextMesh>().color = Color.red;
        line3.GetComponent<TextMesh>().color = Color.red;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(3).gameObject.layer = 2;
        TurnOffGameplayImages();
        currentArea = Menus.Menu.GameOver;
    }

    public void ChangeToPauseText()
    {
        EnableButton(line1, "Resume", playSprite);
        EnableButton(line2, "Restart", restartSprite);
        EnableButton(line3, "Quit", quitSprite);
        DisableButton(line4, "", null);
        DisableButton(line5, "", null);
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(3).gameObject.layer = 2;
        TurnOffGameplayImages();
        currentArea = Menus.Menu.Pause;
    }

    public void ChangeToGrillArea()
    {
        DisableButton(line1, "", heartSprite);
        DisableButton(line2, "", burgerSprite);
        EnableButton(line3, "", friesSprite);
        EnableButton(line4, "", drinkSprite);
        EnableButton(line5, "", null);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToFryerArea()
    {
        DisableButton(line1, "", heartSprite);
        EnableButton(line2, "", burgerSprite);
        DisableButton(line3, "", friesSprite);
        EnableButton(line4, "", drinkSprite);
        EnableButton(line5, "", null);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToSodaMachineArea()
    {
        DisableButton(line1, "", heartSprite);
        EnableButton(line2, "", burgerSprite);
        EnableButton(line3, "", friesSprite);
        DisableButton(line4, "", drinkSprite);
        EnableButton(line5, "", null);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToFrontArea()
    {
        DisableButton(line1, "", heartSprite);
        EnableButton(line2, "", burgerSprite);
        EnableButton(line3, "", friesSprite);
        EnableButton(line4, "", drinkSprite);
        DisableButton(line5, "", null);
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = notPressable;
    }

    void TurnOffGameplayImages()
    {
        line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void ChangeHealthCount(int num)
    {
        int n = Mathf.RoundToInt(Camera.main.GetComponent<Gameplay>().GetLife());
        if (n >= 1)
        {
            ChangeHealthTextColor();
            if (line1.GetComponent<ShakeText>() == null)
            {
                line1.AddComponent<ShakeText>().ChangeColor(num);
                line1.transform.GetChild(5).gameObject.AddComponent<ShakeText>().ChangeSpriteColor(num);
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
            line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = newColor;
            line1.transform.GetChild(3).transform.localScale = new Vector3((((float)n) / GetComponent<Gameplay>().GetMaxLife()) * 255, 90, 1);
            line1.transform.GetChild(4).transform.localScale = new Vector3(0, 90, 1);
        }
        else
        {
            if (n <= 100)
            {
                Color newColor = Color.Lerp(Color.red, textColor, (((float)n) / (GetComponent<Gameplay>().GetMaxLife() / 2)));
                line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = newColor;
                line1.transform.GetChild(3).transform.localScale = new Vector3(
                    (((float)n) / (GetComponent<Gameplay>().GetMaxLife() / 2)) * 255,
                    90,
                    1
                );
                line1.transform.GetChild(4).transform.localScale = new Vector3(0, 90, 1);
            }
            else
            {
                Color newColor = Color.Lerp(
                    Color.green,
                    Color.cyan,
                    ((float)n - (GetComponent<Gameplay>().GetMaxLife() / 2)) / (GetComponent<Gameplay>().GetMaxLife() / 2)
                );
                line1.transform.GetChild(3).transform.localScale = new Vector3(255, 90, 1);
                line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = textColor;
                line1.transform.GetChild(4).transform.localScale = new Vector3(
                    (((float)n - (GetComponent<Gameplay>().GetMaxLife() / 2)) / (GetComponent<Gameplay>().GetMaxLife() / 2)) * 255,
                    90,
                    1
                );
                line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = newColor;
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

    void DisableButton(GameObject button, string text, Sprite icon)
    {
        button.transform.GetChild(0).gameObject.layer = 2;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.clear;
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

    void EnableButton(GameObject button, string text, Sprite icon)
    {
        button.transform.GetChild(0).gameObject.layer = 0;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
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
            }
        }
        if (!pressDown && target == scrollView)
        {
            GetComponent<MenuSlider>().ChangeScrollerItemColor(true);
            pressDown = true;
        }
    }

    public void PressTextUp(GameObject target)
    {
        if (pressDown && target.GetComponent<TextMesh>() != null)
        {
            if (target.transform.childCount > 2)
            {
                target.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
            }
            pressDown = false;
            if (target.transform.childCount > 1 && currentArea == Menus.Menu.Gameplay)
            {
                target.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (pressDown && target == scrollView)
        {
            GetComponent<MenuSlider>().ChangeScrollerItemColor(false);
            pressDown = false;
        }
    }

    public Menus.Menu GetMenu()
    {
        return currentArea;
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
}
