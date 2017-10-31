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
    Color red = Color.red;
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
        GetComponent<ScreenTextManagment>().SetSlotSprites();
        ScaleScrollerObjects();
    }

    public void ChangeToMenuText()
    {
        RevertButtons();
        ChangeTextColorToOriginal();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line5.transform.GetChild(0).gameObject.layer = 2;
        scrollView.transform.GetChild(0).gameObject.layer = 2;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 2;
        slot2.transform.GetChild(0).gameObject.layer = 2;
        slot3.transform.GetChild(0).gameObject.layer = 2;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 0, 1);
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
        menu = "Main Menu";
    }

    public void ChangeToUpgradeText()
    {
        MakeButtonsUnpressable();
        line4.transform.GetChild(0).gameObject.layer = 0;
        line5.transform.GetChild(0).gameObject.layer = 0;
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
        slot1.GetComponent<TextMesh>().text = "[ ]";
        slot2.GetComponent<TextMesh>().text = "[ ]";
        slot3.GetComponent<TextMesh>().text = "[ ]";
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
        RevertButtons();
        line1.transform.GetChild(0).gameObject.layer = 2;
        ChangeBurgerCount();
        ChangeFriesCount();
        ChangeDrinkCount();
        ChangeMistakeText();
	}

    public void ChangeToPauseText()
    {
        RevertButtons();
        ChangeTextColorToOriginal();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line5.transform.GetChild(0).gameObject.layer = 2;
        line1.GetComponent<TextMesh>().text = "Paused";
        line2.GetComponent<TextMesh>().text = "Resume";
        line3.GetComponent<TextMesh>().text = "Restart";
        line4.GetComponent<TextMesh>().text = "Quit";
        line5.GetComponent<TextMesh>().text = "";
    }

    public void ChangeMistakeText()
    {
        int n = Mathf.RoundToInt(Camera.main.GetComponent<Gameplay>().GetLife());
        if (n >= 1)
        {
            line1.GetComponent<TextMesh>().text = "H : " + n.ToString();
        }
        else
        {
            line1.GetComponent<TextMesh>().text = "Dead";
            line2.GetComponent<TextMesh>().text = "";
            line3.GetComponent<TextMesh>().text = "Restart";
            line4.GetComponent<TextMesh>().text = "Quit";
            line5.GetComponent<TextMesh>().text = "S: " + GetComponent<Gameplay>().GetPoints();
            line2.gameObject.layer = 2;
            line5.gameObject.layer = 2;
            line3.GetComponent<TextMesh>().color = red;
            line4.GetComponent<TextMesh>().color = red;
            line5.GetComponent<TextMesh>().color = red;
        }
        Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / GetComponent<Gameplay>().GetMaxLife());
        line1.GetComponent<TextMesh>().color = newColor;
    }

    public void ChangeBurgerCount()
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            int n = Camera.main.GetComponent<Gameplay>().GetBurgerCount();
            Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialBurgers);
            line2.GetComponent<TextMesh>().color = newColor;
            line2.GetComponent<TextMesh>().text = "B : " + n;
        }
    }

    public void ChangeFriesCount()
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            int n = Camera.main.GetComponent<Gameplay>().GetFriesCount();
            Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialFries);
            line3.GetComponent<TextMesh>().color = newColor;
            line3.GetComponent<TextMesh>().text = "F : " + n;
        }
    }

    public void ChangeDrinkCount()
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            int n = Camera.main.GetComponent<Gameplay>().GetDrinkCount();
            Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialDrinks);
            line4.GetComponent<TextMesh>().color = newColor;
            line4.GetComponent<TextMesh>().text = "D : " + n;
        }
    }

    public void RestartScreens()
    {
        ChangeMistakeText();
        ChangeTextColorToOriginal();
        line1.GetComponent<TextMesh>().text = "H : " + GetComponent<Gameplay>().GetMaxLife();
        line2.GetComponent<TextMesh>().text = "B : " + initialBurgers;
        line3.GetComponent<TextMesh>().text = "F : " + initialFries;
        line4.GetComponent<TextMesh>().text = "D : " + initialDrinks;
    }

    public void ChangeToGrillArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line2.transform.GetChild(0).gameObject.layer = 2;
        line5.GetComponent<TextMesh>().text = "Counter";
    }

    public void ChangeToFryerArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line3.transform.GetChild(0).gameObject.layer = 2;
        line5.GetComponent<TextMesh>().text = "Counter";
    }

    public void ChangeToSodaMachineArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line4.transform.GetChild(0).gameObject.layer = 2;
        line5.GetComponent<TextMesh>().text = "Counter";
    }

    public void ChangeToFrontArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).gameObject.layer = 2;
        line5.transform.GetChild(0).gameObject.layer = 2;
        line5.GetComponent<TextMesh>().text = "";
    }

    public void RevertButtons()
    {
        line1.transform.GetChild(0).gameObject.layer = 0;
        line2.transform.GetChild(0).gameObject.layer = 0;
        line3.transform.GetChild(0).gameObject.layer = 0;
        line4.transform.GetChild(0).gameObject.layer = 0;
        line5.transform.GetChild(0).gameObject.layer = 0;
    }

    public void MakeButtonsUnpressable()
    {
        line1.transform.GetChild(0).gameObject.layer = 2;
        line2.transform.GetChild(0).gameObject.layer = 2;
        line3.transform.GetChild(0).gameObject.layer = 2;
        line4.transform.GetChild(0).gameObject.layer = 2;
        line5.transform.GetChild(0).gameObject.layer = 2;
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
            pressDown = true;
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
            pressDown = false;
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
            float scale = ((0.025f - Mathf.Abs(scrollList[i].transform.position.z - scrollView.transform.position.z)) / 0.05f) * 5;
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
