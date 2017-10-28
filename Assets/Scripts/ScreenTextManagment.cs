using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
ScreenTextManagment : MonoBehaviour
{
    int initialLife, initialBurgers, initialFries, initialDrinks;
    GameObject line1, line2, line3, line4, line5, scrollView, slot1, slot2, slot3;
    List<GameObject> scrollList = new List<GameObject>();
    Color originalScreenColor;
    Color red = Color.red;
    bool pressDown;
    string menu = "Main Menu";
    public Sprite[] scrollSprites;

    void Start()
    {
        initialLife = GetComponent<Gameplay>().GetLife();
        initialBurgers = GetComponent<Gameplay>().GetBurgerCount();
        initialFries = GetComponent<Gameplay>().GetFriesCount();
        initialDrinks = GetComponent<Gameplay>().GetDrinkCount();
        originalScreenColor = new Color(0, 0.5f, 1);
        line1 = GetComponent<ObjectManager>().Phone().transform.GetChild(0).gameObject;
        line2 = GetComponent<ObjectManager>().Phone().transform.GetChild(1).gameObject;
        line3 = GetComponent<ObjectManager>().Phone().transform.GetChild(2).gameObject;
        line4 = GetComponent<ObjectManager>().Phone().transform.GetChild(3).gameObject;
        line5 = GetComponent<ObjectManager>().Phone().transform.GetChild(4).gameObject;
        scrollView = GetComponent<ObjectManager>().Phone().transform.GetChild(5).gameObject;
        slot1 = scrollView.transform.GetChild(2).GetChild(0).gameObject;
        slot2 = scrollView.transform.GetChild(2).GetChild(1).gameObject;
        slot3 = scrollView.transform.GetChild(2).GetChild(2).gameObject;
        for (int i = 0; i < GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).childCount; i++)
        {
            scrollList.Add(GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).GetChild(i).gameObject);
        }
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
        ChangeInfo(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name);
        line5.GetComponent<TextMesh>().text = "Back";
        slot1.GetComponent<TextMesh>().color = originalScreenColor;
        slot2.GetComponent<TextMesh>().color = originalScreenColor;
        slot3.GetComponent<TextMesh>().color = originalScreenColor;
        slot1.GetComponent<TextMesh>().text = "[ ]";
        slot2.GetComponent<TextMesh>().text = "[ ]";
        slot3.GetComponent<TextMesh>().text = "[ ]";
        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
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
        int n = Camera.main.GetComponent<Gameplay>().GetLife();
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
        Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialLife);
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
        line1.GetComponent<TextMesh>().text = "H : " + initialLife;
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
            float r = target.GetComponent<TextMesh>().color.r;
            float g = target.GetComponent<TextMesh>().color.g;
            float b = target.GetComponent<TextMesh>().color.b;
            target.GetComponent<TextMesh>().color = new Color(r / 2, g / 2, b / 2);
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
            float r = target.GetComponent<TextMesh>().color.r;
            float g = target.GetComponent<TextMesh>().color.g;
            float b = target.GetComponent<TextMesh>().color.b;
            target.GetComponent<TextMesh>().color = new Color(r * 2, g * 2, b * 2);
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
        ChangeInfo(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name);
    }

    public void ScaleScrollerObjects()
    {
        for (int i = 0; i < scrollList.Count; i++)
        {
            float scale = ((0.025f - Mathf.Abs(scrollList[i].transform.position.z - scrollView.transform.position.z)) / 0.05f) * 7.5f;
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

    public void ChangeSlotSprite(GameObject slot, int n, int pos)
    {
        if (GetComponent<PlayerPrefsManager>().SetUpgrades(pos, int.Parse(scrollSprites[n].name)))
        {
            slot.GetComponent<SpriteRenderer>().sprite = scrollSprites[n];
        }
    }

    public void SetSlotSprites()
    {
        slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 1")];
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 2")];
        slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = scrollSprites[PlayerPrefs.GetInt("UPGRADE 3")];
    }


    public void HighLightSlot(GameObject slot)
    {
        slot1.GetComponent<TextMesh>().color = originalScreenColor;
        slot2.GetComponent<TextMesh>().color = originalScreenColor;
        slot3.GetComponent<TextMesh>().color = originalScreenColor;
        slot.GetComponent<TextMesh>().color = Color.green;
    }

    public void ChangeScrollerItemColor(bool b)
    {
        if (b)
        {
            GetMiddleObject().GetComponent<SpriteRenderer>().color = Color.black;
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

    public void ChangeInfo(string name)
    {
        line4.GetComponent<TextMesh>().characterSize = 0.0325f;
        if (name == "0")
        {
            line4.GetComponent<TextMesh>().text = "Nothing";
        }
        else if (name == "1")
        {
            line4.GetComponent<TextMesh>().characterSize = 0.019f;
            line4.GetComponent<TextMesh>().text = "Make food quicker";
        }
        else
        {
            line4.GetComponent<TextMesh>().text = name;
        }
    }
}
