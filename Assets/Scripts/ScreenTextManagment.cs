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
    string menu = "Main Menu";
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
        MakeAllButtonsPressable();
        ChangeTextColorToOriginal();
        DisableButton(line1);
        DisableButton(line4);
        DisableButton(line5);
        scrollView.transform.GetChild(0).gameObject.layer = 2;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 2;
        slot2.transform.GetChild(0).gameObject.layer = 2;
        slot3.transform.GetChild(0).gameObject.layer = 2;
        line1.GetComponent<TextMesh>().text = "Game";
        line2.GetComponent<TextMesh>().text = "Play";
        line3.GetComponent<TextMesh>().text = "Upgrades";
        line4.GetComponent<TextMesh>().text = "";
        line5.GetComponent<TextMesh>().text = "";
        slot1.GetComponent<TextMesh>().text = "";
        slot2.GetComponent<TextMesh>().text = "";
        slot3.GetComponent<TextMesh>().text = "";
        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        line4.GetComponent<TextMesh>().characterSize = 0.0325f;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 0, 1);

        TurnOffGameplayImages();
        menu = "Main Menu";
    }

    public void ChangeToUpgradeText()
    {
        MakeButtonsUnpressable();
        EnableButton(line2);
        EnableButton(line3);
        EnableButton(line4);
        EnableButton(line5);
        scrollView.transform.GetChild(0).gameObject.layer = 0;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 0;
        slot2.transform.GetChild(0).gameObject.layer = 0;
        slot3.transform.GetChild(0).gameObject.layer = 0;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 1);
        line1.GetComponent<TextMesh>().text = "Upgrades";
        line2.GetComponent<TextMesh>().text = "";
        line3.GetComponent<TextMesh>().text = "";
        ChangeInfo(GetMiddleObjectNumber());
        line5.GetComponent<TextMesh>().text = "Back";
        slot1.GetComponent<TextMesh>().color = originalScreenColor;
        slot2.GetComponent<TextMesh>().color = originalScreenColor;
        slot3.GetComponent<TextMesh>().color = originalScreenColor;
        slot1.GetComponent<TextMesh>().text = "[  ]";
        slot2.GetComponent<TextMesh>().text = "[  ]";
        slot3.GetComponent<TextMesh>().text = "[  ]";
        if (GetSlotNumber(slot1) != PowerUpsManager.nothing)
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (GetSlotNumber(slot2) != PowerUpsManager.nothing)
        {
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (GetSlotNumber(slot3) != PowerUpsManager.nothing)
        {
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
        HighLightSlot(slot1);
        menu = "Upgrade";
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
        ChangeBurgerTextColor();
        ChangeFriesTextColor();
        ChangeDrinkTextColor();
        ChangeHealthTextColor();
        menu = "Gameplay";
    }

    public void ChangeToGameOverText()
    {
        MakeButtonsUnpressable();
        EnableButton(line3);
        EnableButton(line4);
        line1.GetComponent<TextMesh>().text = "Dead";
        line2.GetComponent<TextMesh>().text = "";
        line3.GetComponent<TextMesh>().text = "Restart";
        line4.GetComponent<TextMesh>().text = "Quit";
        line5.GetComponent<TextMesh>().text = "S: " + GetComponent<Gameplay>().GetPoints();
        line1.GetComponent<TextMesh>().color = Color.red;
        line3.GetComponent<TextMesh>().color = Color.red;
        line4.GetComponent<TextMesh>().color = Color.red;
        line5.GetComponent<TextMesh>().color = Color.red;
        TurnOffGameplayImages();
        menu = "Game Over";
    }

    public void ChangeToPauseText()
    {
        MakeAllButtonsPressable();
        ChangeTextColorToOriginal();
        DisableButton(line1);
        DisableButton(line5);
        line1.GetComponent<TextMesh>().text = "Paused";
        line2.GetComponent<TextMesh>().text = "Resume";
        line3.GetComponent<TextMesh>().text = "Restart";
        line4.GetComponent<TextMesh>().text = "Quit";
        line5.GetComponent<TextMesh>().text = "";
        TurnOffGameplayImages();
        menu = "Pause";
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
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToSodaMachineArea()
    {
        MakeAllButtonsPressable();
        DisableButton(line1);
        DisableButton(line4);
        line4.transform.GetChild(1).GetComponent<SpriteRenderer>().color = notPressable;
        line2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line3.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        line5.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChangeToFrontArea()
    {
        MakeAllButtonsPressable();
        DisableButton(line1);
        DisableButton(line5);
        line5.GetComponent<TextMesh>().text = "";
    }

    public void MakeAllButtonsPressable()
    {
        EnableButton(line1);
        EnableButton(line2);
        EnableButton(line3);
        EnableButton(line4);
        EnableButton(line5);
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
    }

    void EnableButton(GameObject button)
    {
        button.transform.GetChild(0).gameObject.layer = 0;
        button.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
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
            float r = target.GetComponent<TextMesh>().color.r / 2;
            float g = target.GetComponent<TextMesh>().color.g / 2;
            float b = target.GetComponent<TextMesh>().color.b / 2;
            target.GetComponent<TextMesh>().color = new Color(r, g, b);
            if (target.transform.childCount > 2)
            {
                target.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.grey;
            }
            pressDown = true;
            if (target.transform.childCount > 1 && menu == "Gameplay")
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
            float r = target.GetComponent<TextMesh>().color.r * 2;
            float g = target.GetComponent<TextMesh>().color.g * 2;
            float b = target.GetComponent<TextMesh>().color.b * 2;
            target.GetComponent<TextMesh>().color = new Color(r, g, b);
            if (target.transform.childCount > 2)
            {
                target.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
            }
            pressDown = false;
            if (target.transform.childCount > 1 && menu == "Gameplay")
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

    public string GetMenu()
    {
        return menu;
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
            float scale = ((0.025f - Mathf.Abs(scrollList[i].transform.position.z - scrollView.transform.position.z)) / 0.05f) * 3.5f;
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
        Vector3 roundedPosition = new Vector3(roundedPositionX, 0, 0);
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
        return int.Parse(slot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
    }

    public void ChangeSlotSprite(GameObject slot, int slotPosition)
    {
        if (GetComponent<PlayerPrefsManager>().SetUpgrades(slotPosition, GetMiddleObjectNumber()))
        {
            slot.GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            if (GetMiddleObjectNumber() != PowerUpsManager.nothing)
            {
                slot.GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotPosition - 1).GetComponent<SpriteRenderer>().sprite =
                    GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                slot.GetComponent<SpriteRenderer>().color = Color.clear;
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotPosition - 1).GetComponent<SpriteRenderer>().sprite =
                    null;
            }
        }
    }

    public void SetSlotSprites()
    {
        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 1")];
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 2")];
        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 3")];
        if (PlayerPrefs.GetInt("UPGRADE 1") != PowerUpsManager.nothing)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                        scrollSprites[PlayerPrefs.GetInt("UPGRADE 1")];
        }
        if (PlayerPrefs.GetInt("UPGRADE 2") != PowerUpsManager.nothing)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
                        scrollSprites[PlayerPrefs.GetInt("UPGRADE 2")];
        }
        if (PlayerPrefs.GetInt("UPGRADE 3") != PowerUpsManager.nothing)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(2).GetComponent<SpriteRenderer>().sprite =
                        scrollSprites[PlayerPrefs.GetInt("UPGRADE 3")];
        }
    }

    public void HighLightSlot(GameObject slot)
    {
        slot1.GetComponent<TextMesh>().color = originalScreenColor;
        slot2.GetComponent<TextMesh>().color = originalScreenColor;
        slot3.GetComponent<TextMesh>().color = originalScreenColor;
        slot.GetComponent<TextMesh>().color = Color.green;
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
        line4.GetComponent<TextMesh>().characterSize = 0.0325f;
        if (upgradeNum == PowerUpsManager.nothing)
        {
            line4.GetComponent<TextMesh>().text = "Remove";
        }
        else if (upgradeNum == PowerUpsManager.throwFurther)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.024f;
            line4.GetComponent<TextMesh>().text = "Throw further";
        }
        else if (upgradeNum == PowerUpsManager.quickerCooking)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.024f;
            line4.GetComponent<TextMesh>().text = "Faster cooking";
        }
        else if (upgradeNum == PowerUpsManager.makeMoreFood)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.022f;
            line4.GetComponent<TextMesh>().text = "Make more food";
        }
        else if (upgradeNum == PowerUpsManager.defenseIncrease)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.020f;
            line4.GetComponent<TextMesh>().text = "Defense increase";
        }
        else if (upgradeNum == PowerUpsManager.moreHealth)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.026f;
            line4.GetComponent<TextMesh>().text = "Double health";
        }
        else if (upgradeNum == PowerUpsManager.largerFood)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.029f;
            line4.GetComponent<TextMesh>().text = "Larger food";
        }
        else if (upgradeNum == PowerUpsManager.regenHealth)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.018f;
            line4.GetComponent<TextMesh>().text = "Health regeneration";
        }
        else if (upgradeNum == PowerUpsManager.regenBurgers)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.017f;
            line4.GetComponent<TextMesh>().text = "Burger regeneration";
        }
        else if (upgradeNum == PowerUpsManager.regenFries)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.019f;
            line4.GetComponent<TextMesh>().text = "Fries regeneration";
        }
        else if (upgradeNum == PowerUpsManager.regenDrinks)
        {
            line4.GetComponent<TextMesh>().characterSize = 0.017f;
            line4.GetComponent<TextMesh>().text = "Drink regeneration";
        }
        else if (upgradeNum == PowerUpsManager.doublePoints)
		{
			line4.GetComponent<TextMesh>().characterSize = 0.026f;
			line4.GetComponent<TextMesh>().text = "Double Points";
		}
        else
        {
            line4.GetComponent<TextMesh>().text = name.ToString();
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
}
