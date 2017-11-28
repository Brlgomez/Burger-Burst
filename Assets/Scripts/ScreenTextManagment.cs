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
    int middleSpriteIndex;

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
        MakeButtonsUnpressable();
        ChangeTextColorToOriginal();
        EnableButton(line1, "Play");
        EnableButton(line2, "Power Ups");
        TurnOffScrollList();
        TurnOffGameplayImages();
        currentArea = Menus.Menu.MainMenu;
    }

    public void ChangeToUpgradeText()
    {
        MakeButtonsUnpressable();
        EnableButton(line1, "");
        EnableButton(line2, "");
        EnableButton(line3, "");
        EnableButton(line4, "");
        EnableButton(line5, "Back");
        TurnOnScrollList();
        currentArea = Menus.Menu.PowerUps;
    }

    public void ChangeToGamePlayText()
    {
        MakeAllButtonsPressable();
        DisableButton(line1);
        line1.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line1.transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.black;
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line5.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(3).gameObject.layer = 0;
		currentArea = Menus.Menu.Gameplay;
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
        ChangeTextColorToOriginal();
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
        DisableButton(line1);
        DisableButton(line2);
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
        DisableButton(line1);
        DisableButton(line3);
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
        DisableButton(line1);
        DisableButton(line4);
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
        DisableButton(line1);
        DisableButton(line5);
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
        DisableButton(line1);
        DisableButton(line2);
        DisableButton(line3);
        DisableButton(line4);
        DisableButton(line5);
    }


    void DisableButton(GameObject button)
    {
        button.transform.GetChild(0).gameObject.layer = 2;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.clear;
        button.GetComponent<TextMesh>().text = "";
    }

    void EnableButton(GameObject button, string text)
    {
        button.transform.GetChild(0).gameObject.layer = 0;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
        button.GetComponent<TextMesh>().text = text;
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
                scrollList[0].GetComponent<SpriteRenderer>().sprite = scrollSprites[(scrollSprite - 1)];
            }
            else
            {
                scrollList[0].GetComponent<SpriteRenderer>().sprite = scrollSprites[(scrollSprites.Length - 1)];
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
                scrollList[scrollList.Count - 1].GetComponent<SpriteRenderer>().sprite = scrollSprites[(scrollSprite + 1)];
            }
            else
            {
                scrollList[scrollList.Count - 1].GetComponent<SpriteRenderer>().sprite = scrollSprites[0];
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
        if (GetComponent<PlayerPrefsManager>().ContainsUpgradeBesidesSlot(GetMiddleObjectNumber(), slotPosition))
        {
            int slotNum = GetComponent<PlayerPrefsManager>().WhichSlotContainsUpgrade(GetMiddleObjectNumber());
            PlayerPrefs.SetInt("UPGRADE " + slotNum, PowerUpsManager.nothing);
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotNum - 1).GetComponent<SpriteRenderer>().sprite = null;
            if (slotNum == 1)
            {
                slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (slotNum == 2)
            {
                slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (slotNum == 3)
            {
                slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        if (GetComponent<PlayerPrefsManager>().SlotContainsUpgrade(slotPosition, GetMiddleObjectNumber()))
        {
            PlayerPrefs.SetInt("UPGRADE " + slotPosition, PowerUpsManager.nothing);
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
        int upgradeNum = name;
        line3.GetComponent<TextMesh>().characterSize = 0.0325f;
        if (upgradeNum == PowerUpsManager.nothing)
        {
            line3.GetComponent<TextMesh>().text = "Remove";
        }
        else if (upgradeNum == PowerUpsManager.throwFurther)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.024f;
            line3.GetComponent<TextMesh>().text = "Throw further";
        }
        else if (upgradeNum == PowerUpsManager.quickerCooking)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.024f;
            line3.GetComponent<TextMesh>().text = "Faster cooking";
        }
        else if (upgradeNum == PowerUpsManager.makeMoreFood)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.022f;
            line3.GetComponent<TextMesh>().text = "Make more food";
        }
        else if (upgradeNum == PowerUpsManager.defenseIncrease)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.020f;
            line3.GetComponent<TextMesh>().text = "Defense increase";
        }
        else if (upgradeNum == PowerUpsManager.moreHealth)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.026f;
            line3.GetComponent<TextMesh>().text = "Double health";
        }
        else if (upgradeNum == PowerUpsManager.largerFood)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.029f;
            line3.GetComponent<TextMesh>().text = "Larger food";
        }
        else if (upgradeNum == PowerUpsManager.regenHealth)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.018f;
            line3.GetComponent<TextMesh>().text = "Health regeneration";
        }
        else if (upgradeNum == PowerUpsManager.regenBurgers)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.017f;
            line3.GetComponent<TextMesh>().text = "Burger regeneration";
        }
        else if (upgradeNum == PowerUpsManager.regenFries)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.019f;
            line3.GetComponent<TextMesh>().text = "Fries regeneration";
        }
        else if (upgradeNum == PowerUpsManager.regenDrinks)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.019f;
            line3.GetComponent<TextMesh>().text = "Drink regeneration";
        }
        else if (upgradeNum == PowerUpsManager.doublePoints)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.026f;
            line3.GetComponent<TextMesh>().text = "Double points";
        }
        else if (upgradeNum == PowerUpsManager.doubleCoins)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.028f;
            line3.GetComponent<TextMesh>().text = "Double coins";
        }
        else if (upgradeNum == PowerUpsManager.throwMultiple)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.015f;
            line3.GetComponent<TextMesh>().text = "Thrown food is divided\nand scattered";
        }
        else if (upgradeNum == PowerUpsManager.magnet)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.022f;
            line3.GetComponent<TextMesh>().text = "Food magnetic\nto zombies";
        }
        else if (upgradeNum == PowerUpsManager.noWind)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.022f;
            line3.GetComponent<TextMesh>().text = "Food unaffected\nby wind";
        }
        else if (upgradeNum == PowerUpsManager.noInstantKill)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.021f;
            line3.GetComponent<TextMesh>().text = "Muffle instant\ndeath attacks";
        }
        else if (upgradeNum == PowerUpsManager.luck)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.019f;
            line3.GetComponent<TextMesh>().text = "Chance of dodging\nattacks";
        }
        else if (upgradeNum == PowerUpsManager.noPoison)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.019f;
            line3.GetComponent<TextMesh>().text = "Immuned to posion\nzombies";
        }
        else if (upgradeNum == PowerUpsManager.noIce)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.019f;
            line3.GetComponent<TextMesh>().text = "Immuned to ice\nzombies";
        }
        else if (upgradeNum == PowerUpsManager.freeze)
        {
            line3.GetComponent<TextMesh>().characterSize = 0.019f;
            line3.GetComponent<TextMesh>().text = "Food freezes\nzombies";
        }
        else
        {
            line3.GetComponent<TextMesh>().text = name.ToString();
        }
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
                GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    scrollSprites[2 - i];
            }
            else
            {
                GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    scrollSprites[scrollSprites.Length + 2 - i];
            }
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
