using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
ScreenTextManagment : MonoBehaviour
{
    int initialBurgers, initialFries, initialDrinks;
    GameObject line1, line2, line3, line4, line5, scrollView, slot1, slot2, slot3;
    List<GameObject> scrollList = new List<GameObject>();
    Color originalScreenColor = new Color(0, 0.5f, 1);
    Color notPressable = new Color(1, 1, 1, 0.25f);
    bool pressDown;
    Menus.Menu currentArea;
    public Sprite[] scrollSprites;
    public Sprite coinSprite, drinkSprite, heartSprite;

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
        slot1 = scrollView.transform.GetChild(2).GetChild(0).gameObject;
        slot2 = scrollView.transform.GetChild(2).GetChild(1).gameObject;
        slot3 = scrollView.transform.GetChild(2).GetChild(2).gameObject;
        RestartScrollList();
        ChangeToMenuText();
        SetSlotSprites();
        ScaleScrollerObjects();
    }

    /* Main Menu */

    public void ChangeToMenuText()
    {
        EnableButton(line1, "Play");
        EnableButton(line2, "Power Ups");
        EnableButton(line3, "Customize");
        EnableButton(line4, "Store");
        EnableButton(line5, "Settings");
        TurnOffScrollList();
        TurnOffGameplayImages();
        currentArea = Menus.Menu.MainMenu;
    }

    public void ChangeToGamePlayText()
    {
        MakeAllButtonsPressable();
        DisableButton(line1, "");
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.black;
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = drinkSprite;
        line5.transform.GetChild(3).gameObject.layer = 0;
        currentArea = Menus.Menu.Gameplay;
    }

    public void ChangeToUpgradeText()
    {
        EnableButton(line1, "");
        EnableButton(line2, "");
        EnableButton(line3, "");
        EnableButton(line4, "    " + PlayerPrefs.GetInt("Coins", 0).ToString());
        EnableButton(line5, "Back");
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = heartSprite;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = coinSprite;
        TurnOnScrollList();
        currentArea = Menus.Menu.PowerUps;
    }

	public void ChangeToCustomizeScreen()
	{
		EnableButton(line1, "Theme");
		EnableButton(line2, "Graphics");
        DisableButton(line3, "");
		DisableButton(line4, "");
		EnableButton(line5, "Back");
        currentArea = Menus.Menu.Customize;
	}

	public void ChangeToSettingScreen()
	{
		EnableButton(line1, "Vibration");
		EnableButton(line2, "Music");
		EnableButton(line3, "Sound");
        EnableButton(line4, "Game Center");
		EnableButton(line5, "Back");
        currentArea = Menus.Menu.Setting;
	}

    public void ChangeToConfirmationScreen()
    {
        TurnOffScrollList();
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
        DisableButton(line1, "");
        line2.GetComponent<TextMesh>().characterSize = 0.024375f;
        DisableButton(line2, "Get power up?");
        if (int.Parse(GetMiddleObject().transform.GetChild(0).GetComponent<TextMesh>().text) <= PlayerPrefs.GetInt("Coins", 0))
        {
            EnableButton(line3, "Yes");
        }
        else
        {
            line3.GetComponent<TextMesh>().characterSize = 0.024375f;
            DisableButton(line3,
                "Coins needed:\n" +
                (int.Parse(GetMiddleObject().transform.GetChild(0).GetComponent<TextMesh>().text) - PlayerPrefs.GetInt("Coins", 0)));
        }
        EnableButton(line4, "Get coins");
        EnableButton(line5, "Back");
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        currentArea = Menus.Menu.Confirmation;
    }

    public void BuyUpgrade()
    {
        GetComponent<Gameplay>().DecreaseCoinCount(int.Parse(GetMiddleObject().transform.GetChild(0).GetComponent<TextMesh>().text));
        PlayerPrefs.SetInt("Power Up " + int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name), 1);
        GetComponent<PowerUpsManager>().powerUpList[int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name)].unlocked = true;
        GetMiddleObject().transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        GetMiddleObject().transform.GetChild(0).GetComponent<TextMesh>().text = "";
    }

    public void ChangeToGameOverText()
    {
        MakeButtonsUnpressable();
        EnableButton(line1, "Restart");
        EnableButton(line2, "Quit");
        line3.GetComponent<TextMesh>().text = "S: " + GetComponent<Gameplay>().GetPoints();
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
        MakeButtonsUnpressable();
        EnableButton(line1, "Resume");
        EnableButton(line2, "Restart");
        EnableButton(line3, "Quit");
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(3).gameObject.layer = 2;
        TurnOffGameplayImages();
        currentArea = Menus.Menu.Pause;
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
            Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / GetComponent<Gameplay>().GetMaxLife());
            line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = newColor;
            line1.transform.GetChild(3).transform.localScale = new Vector3((((float)n) / GetComponent<Gameplay>().GetMaxLife()) * 255, 90, 1);
            line1.transform.GetChild(4).transform.localScale = new Vector3(0, 90, 1);
        }
        else
        {
            if (n <= 100)
            {
                Color newColor = Color.Lerp(Color.red, originalScreenColor, (((float)n) / (GetComponent<Gameplay>().GetMaxLife() / 2)));
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
                line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = originalScreenColor;
                line1.transform.GetChild(4).transform.localScale = new Vector3(
                    (((float)n - (GetComponent<Gameplay>().GetMaxLife() / 2)) / (GetComponent<Gameplay>().GetMaxLife() / 2)) * 255,
                    90,
                    1
                );
                line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = newColor;
            }
        }
    }

    void TurnOffGameplayImages()
    {
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.clear;
        line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.clear;
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
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
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialBurgers);
        line2.GetComponent<TextMesh>().color = newColor;
        line2.GetComponent<TextMesh>().text = "     " + n;
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
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialFries);
        line3.GetComponent<TextMesh>().color = newColor;
        line3.GetComponent<TextMesh>().text = "     " + n;
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
        Color newColor = Color.Lerp(Color.red, originalScreenColor, ((float)n) / initialDrinks);
        line4.GetComponent<TextMesh>().color = newColor;
        line4.GetComponent<TextMesh>().text = "     " + n;
    }

    public void RestartScreens()
    {
        ChangeHealthTextColor();
        ChangeTextColorToOriginal();
        line1.GetComponent<TextMesh>().text = "     " + GetComponent<Gameplay>().GetMaxLife();
        line2.GetComponent<TextMesh>().text = "     " + initialBurgers;
        line3.GetComponent<TextMesh>().text = "     " + initialFries;
        line4.GetComponent<TextMesh>().text = "     " + initialDrinks;
    }

    public void ChangeToGrillArea()
    {
        MakeAllButtonsPressable();
        DisableButton(line1, "");
        DisableButton(line2, "");
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToFryerArea()
    {
        MakeAllButtonsPressable();
        DisableButton(line1, "");
        DisableButton(line3, "");
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToSodaMachineArea()
    {
        MakeAllButtonsPressable();
        DisableButton(line1, "");
        DisableButton(line4, "");
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToFrontArea()
    {
        MakeAllButtonsPressable();
        DisableButton(line1, "");
        DisableButton(line5, "");
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        line5.GetComponent<TextMesh>().text = "";
    }

    public void MakeAllButtonsPressable()
    {
        EnableButton(line1, "");
        EnableButton(line2, "");
        EnableButton(line3, "");
        EnableButton(line4, "");
        EnableButton(line5, "");
    }

    public void MakeButtonsUnpressable()
    {
        DisableButton(line1, "");
        DisableButton(line2, "");
        DisableButton(line3, "");
        DisableButton(line4, "");
        DisableButton(line5, "");
    }

    void DisableButton(GameObject button, string text)
    {
        button.transform.GetChild(0).gameObject.layer = 2;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.clear;
        button.GetComponent<TextMesh>().text = text;
    }

    void EnableButton(GameObject button, string text)
    {
        button.transform.GetChild(0).gameObject.layer = 0;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
        button.GetComponent<TextMesh>().text = text;
        button.GetComponent<TextMesh>().color = originalScreenColor;
    }

    void ChangeTextColorToOriginal()
    {
        line1.GetComponent<TextMesh>().color = originalScreenColor;
        line2.GetComponent<TextMesh>().color = originalScreenColor;
        line3.GetComponent<TextMesh>().color = originalScreenColor;
        line4.GetComponent<TextMesh>().color = originalScreenColor;
        line5.GetComponent<TextMesh>().color = originalScreenColor;
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
            ChangeScrollerItemColor(true);
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
            ChangeScrollerItemColor(false);
            pressDown = false;
        }
    }

    public Menus.Menu GetMenu()
    {
        return currentArea;
    }

    public void MoveScrollObjects(int dir)
    {
        if (dir > 0)
        {
            int scrollSprite = int.Parse(scrollList[scrollList.Count - 1].GetComponent<SpriteRenderer>().sprite.name);
            if ((scrollSprite - 1) >= 0)
            {
                SetUpScrollObject(scrollList[0], (scrollSprite - 1));
            }
            else
            {
                SetUpScrollObject(scrollList[0], (scrollSprites.Length - 1));
            }
            scrollList[0].transform.localPosition = new Vector3(
                scrollList[0].transform.localPosition.x,
                scrollList[0].transform.localPosition.y,
                scrollList[0].transform.localPosition.z - 5
            );
            scrollList.Insert((scrollList.Count), scrollList[0]);
            scrollList.RemoveAt(0);
        }
        else
        {
            int scrollSprite = int.Parse(scrollList[0].GetComponent<SpriteRenderer>().sprite.name);
            if ((scrollSprite + 1) <= (scrollSprites.Length - 1))
            {
                SetUpScrollObject(scrollList[scrollList.Count - 1], (scrollSprite + 1));
            }
            else
            {
                SetUpScrollObject(scrollList[scrollList.Count - 1], 0);
            }
            scrollList[scrollList.Count - 1].transform.localPosition = new Vector3(
                scrollList[scrollList.Count - 1].transform.localPosition.x,
                scrollList[scrollList.Count - 1].transform.localPosition.y,
                scrollList[scrollList.Count - 1].transform.localPosition.z + 5
            );
            scrollList.Insert(0, scrollList[scrollList.Count - 1]);
            scrollList.RemoveAt(scrollList.Count - 1);
        }
        ChangeInfo(GetMiddleObjectNumber());
    }

    public void ScaleScrollerObjects()
    {
        for (int i = 0; i < scrollList.Count; i++)
        {
            float scale = ((0.025f - Mathf.Abs(scrollList[i].transform.position.z - scrollView.transform.position.z)) / 0.05f) * 14;
            if (scale < 0)
            {
                scale = 0;
            }
            scrollList[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public bool MoveScroller()
    {
        int roundedPositionX = Mathf.RoundToInt(scrollView.transform.GetChild(1).localPosition.x);
        Vector3 roundedPosition = new Vector3(
            roundedPositionX,
            scrollView.transform.GetChild(1).localPosition.y,
            scrollView.transform.GetChild(1).localPosition.z
        );
        scrollView.transform.GetChild(1).localPosition = Vector3.Slerp(
            scrollView.transform.GetChild(1).localPosition,
            roundedPosition,
            Time.deltaTime * 5
        );
        ScaleScrollerObjects();
        if (Mathf.Abs(roundedPositionX - scrollView.transform.GetChild(1).localPosition.x) < 0.001f)
        {
            return false;
        }
        return true;
    }

    public GameObject GetMiddleObject()
    {
        return scrollList[2];
    }

    int GetMiddleObjectNumber()
    {
        return int.Parse(scrollList[2].GetComponent<SpriteRenderer>().sprite.name);
    }

    int GetSlotNumber(GameObject slot)
    {
        if (slot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite != null)
        {
            return int.Parse(slot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
        }
        return -1;
    }

    public void ChangeSlotSprite(GameObject slot, int slotPosition)
    {
        if (!GetComponent<PowerUpsManager>().powerUpList[int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name)].unlocked)
        {
            ChangeToConfirmationScreen();
        }
        else
        {
            if (GetComponent<PlayerPrefsManager>().ContainsUpgradeBesidesSlot(GetMiddleObjectNumber(), slotPosition))
            {
                int slotNum = GetComponent<PlayerPrefsManager>().WhichSlotContainsUpgrade(GetMiddleObjectNumber());
                PlayerPrefs.SetInt("UPGRADE " + slotNum, GetComponent<PowerUpsManager>().nothing.powerUpNumber);
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotNum - 1).GetComponent<SpriteRenderer>().sprite = null;
                switch (slotNum)
                {
                    case 1:
                        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    case 2:
                        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    case 3:
                        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                        break;
                }
            }
            if (GetComponent<PlayerPrefsManager>().SlotContainsUpgrade(slotPosition, GetMiddleObjectNumber()))
            {
                PlayerPrefs.SetInt("UPGRADE " + slotPosition, GetComponent<PowerUpsManager>().nothing.powerUpNumber);
                slot.GetComponent<SpriteRenderer>().sprite = null;
                slot.GetComponent<SpriteRenderer>().color = Color.clear;
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotPosition - 1).GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                PlayerPrefs.SetInt("UPGRADE " + slotPosition, GetMiddleObjectNumber());
                slot.GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
                slot.GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotPosition - 1).GetComponent<SpriteRenderer>().sprite =
                    GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public void SetSlotSprites()
    {
        if (PlayerPrefs.GetInt("UPGRADE 1", -1) > -1)
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 1")];
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                        scrollSprites[PlayerPrefs.GetInt("UPGRADE 1")];
        }
        if (PlayerPrefs.GetInt("UPGRADE 2", -1) > -1)
        {
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 2")];
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
                        scrollSprites[PlayerPrefs.GetInt("UPGRADE 2")];
        }
        if (PlayerPrefs.GetInt("UPGRADE 3", -1) > -1)
        {
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 3")];
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(2).GetComponent<SpriteRenderer>().sprite =
                        scrollSprites[PlayerPrefs.GetInt("UPGRADE 3")];
        }
    }

    public void HighLightSlot(GameObject slot)
    {
        slot1.GetComponent<SpriteRenderer>().color = originalScreenColor;
        slot2.GetComponent<SpriteRenderer>().color = originalScreenColor;
        slot3.GetComponent<SpriteRenderer>().color = originalScreenColor;
        slot.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void ChangeScrollerItemColor(bool pressDown)
    {
        if (pressDown)
        {
            GetMiddleObject().GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else
        {
            GetMiddleObject().GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void EnableScroller(bool b)
    {
        if (b)
        {
            scrollView.transform.GetChild(3).gameObject.layer = 0;
        }
        else
        {
            scrollView.transform.GetChild(3).gameObject.layer = 2;
        }
    }

    public void ChangeInfo(int name)
    {
        line3.GetComponent<TextMesh>().text = GetComponent<PowerUpsManager>().powerUpList[name].description;
    }

    void RestartScrollList()
    {
        scrollList.Clear();
        for (int i = 0; i < GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).childCount; i++)
        {
            scrollList.Add(GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).GetChild(i).gameObject);
            GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).GetChild(i).GetComponent<SpriteRenderer>().color =
                Color.white;
            if (i == 0 || i == 1 || i == 2)
            {
                SetUpScrollObject(scrollList[i], 2 - i);
            }
            else
            {
                SetUpScrollObject(scrollList[i], scrollSprites.Length + 2 - i);
            }
        }
    }

    void SetUpScrollObject(GameObject scrollObj, int powerUpNum)
    {
        scrollObj.GetComponent<SpriteRenderer>().sprite = scrollSprites[powerUpNum];
        if (GetComponent<PowerUpsManager>().powerUpList[powerUpNum].unlocked)
        {
            scrollObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
            scrollObj.transform.GetChild(0).GetComponent<TextMesh>().text = "";
        }
        else
        {
            scrollObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            scrollObj.transform.GetChild(0).GetComponent<TextMesh>().text =
                GetComponent<PowerUpsManager>().powerUpList[powerUpNum].price.ToString();
        }
    }

    void TurnOffScrollList()
    {
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 0, 1);
        scrollView.transform.GetChild(0).gameObject.layer = 2;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 2;
        slot2.transform.GetChild(0).gameObject.layer = 2;
        slot3.transform.GetChild(0).gameObject.layer = 2;
        slot1.GetComponent<SpriteRenderer>().color = Color.clear;
        slot2.GetComponent<SpriteRenderer>().color = Color.clear;
        slot3.GetComponent<SpriteRenderer>().color = Color.clear;
        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        line3.GetComponent<TextMesh>().characterSize = 0.0325f;
    }

    void TurnOnScrollList()
    {
        line2.transform.GetChild(0).gameObject.layer = 2;
        scrollView.transform.GetChild(0).gameObject.layer = 0;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 1);
        slot1.transform.GetChild(0).gameObject.layer = 0;
        slot2.transform.GetChild(0).gameObject.layer = 0;
        slot3.transform.GetChild(0).gameObject.layer = 0;
        ChangeInfo(GetMiddleObjectNumber());
        slot1.GetComponent<SpriteRenderer>().color = originalScreenColor;
        slot2.GetComponent<SpriteRenderer>().color = originalScreenColor;
        slot3.GetComponent<SpriteRenderer>().color = originalScreenColor;
        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        line2.GetComponent<TextMesh>().characterSize = 0.0325f;
        line3.GetComponent<TextMesh>().characterSize = 0.01625f;
        HighLightSlot(slot1);
    }

    public void CannotPressAnything()
    {
        scrollView.transform.GetChild(0).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 2;
        slot2.transform.GetChild(0).gameObject.layer = 2;
        slot3.transform.GetChild(0).gameObject.layer = 2;
        line1.transform.GetChild(0).gameObject.layer = 2;
        line2.transform.GetChild(0).gameObject.layer = 2;
        line3.transform.GetChild(0).gameObject.layer = 2;
        line4.transform.GetChild(0).gameObject.layer = 2;
        line5.transform.GetChild(0).gameObject.layer = 2;
    }
}
