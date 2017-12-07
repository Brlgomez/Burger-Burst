using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlider : MonoBehaviour
{
    List<GameObject> scrollList = new List<GameObject>();
    GameObject scrollView, slot1, slot2, slot3;
    Color textColor = new Color(0, 0.5f, 1);
    int sliderObjectCount;
    Menus.Menu lastMenu, currentMenu;

    void Awake()
    {
        scrollView = GetComponent<ObjectManager>().Phone().transform.GetChild(5).gameObject;
        slot1 = scrollView.transform.GetChild(2).GetChild(0).gameObject;
        slot2 = scrollView.transform.GetChild(2).GetChild(1).gameObject;
        slot3 = scrollView.transform.GetChild(2).GetChild(2).gameObject;
        GetComponent<PowerUpsManager>().SetPowerUpLED();
    }

    public void SetUpMenu(Menus.Menu menu)
    {
        currentMenu = menu;
        if (lastMenu != menu)
        {
            if (menu == Menus.Menu.PowerUps)
            {
                sliderObjectCount = GetComponent<PowerUpsManager>().powerUpList.Count;
                SetSlotSprites();
            }
            else if (menu == Menus.Menu.Graphics)
            {
                sliderObjectCount = GetComponent<GraphicsManager>().graphicList.Count;
                SetGraphicsSprite();
            }
            else if (menu == Menus.Menu.Flooring)
            {
                sliderObjectCount = GetComponent<ThemeManager>().flooringList.Count;
                SetFlooringSprite();
            }
            RestartScrollList();
            ScaleScrollerObjects();
        }
        lastMenu = menu;
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
                SetUpScrollObject(scrollList[0], (sliderObjectCount - 1));
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
            if ((scrollSprite + 1) <= (sliderObjectCount - 1))
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
        GetComponent<ScreenTextManagment>().ChangeSliderInfo();
    }

    void SetUpScrollObject(GameObject scrollObj, int powerUpNum)
    {
        scrollObj.GetComponent<SpriteRenderer>().sprite = GetSliderSprite(powerUpNum);
        if (GetSliderUnlock(powerUpNum))
        {
            scrollObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.clear;
            scrollObj.transform.GetChild(0).GetComponent<TextMesh>().text = "";
        }
        else
        {
            scrollObj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            scrollObj.transform.GetChild(0).GetComponent<TextMesh>().text = GetSliderPrice(powerUpNum).ToString();
        }
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

    void RestartScrollList()
    {
        scrollList.Clear();
        GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).transform.localPosition = new Vector3(
            0, GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(1).transform.localPosition.y, 0
        );
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
                SetUpScrollObject(scrollList[i], sliderObjectCount + 2 - i);
            }
            scrollList[i].transform.localPosition = new Vector3(0.5f, 0, 2 - i);
        }
    }

    public void HighLightSlot(GameObject slot)
    {
        slot1.GetComponent<SpriteRenderer>().color = textColor;
        slot2.GetComponent<SpriteRenderer>().color = textColor;
        slot3.GetComponent<SpriteRenderer>().color = textColor;
        slot.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void SetSlotSprites()
    {
        int slot1PowerUp = GetComponent<PlayerPrefsManager>().GetPowerUpFromSlot(1);
        int slot2PowerUp = GetComponent<PlayerPrefsManager>().GetPowerUpFromSlot(2);
        int slot3PowerUp = GetComponent<PlayerPrefsManager>().GetPowerUpFromSlot(3);
        if (slot1PowerUp >= 0)
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(slot1PowerUp);
        }
        else
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        if (slot2PowerUp >= 0)
        {
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(slot2PowerUp);
        }
        else
        {
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        if (slot3PowerUp >= 0)
        {
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(slot3PowerUp);
        }
        else
        {
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    public void SetGraphicsSprite()
    {
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(GetComponent<PlayerPrefsManager>().GetGraphics());
    }

    public void SetFlooringSprite()
    {
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(GetComponent<PlayerPrefsManager>().GetFlooring());
    }

    public void ChangeSlotSprite(GameObject slot, int slotPosition)
    {
        if (!GetSliderUnlock(GetMiddleObjectNumber()))
        {
            GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.Confirmation, "Get power up?");
        }
        else
        {
            if (GetComponent<PlayerPrefsManager>().ContainsUpgradeBesidesSlot(GetMiddleObjectNumber(), slotPosition))
            {
                int slotNum = GetComponent<PlayerPrefsManager>().WhichSlotContainsUpgrade(GetMiddleObjectNumber());
                GetComponent<PlayerPrefsManager>().SetPowerUpSlot(slotNum, -1);
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
                GetComponent<PlayerPrefsManager>().SetPowerUpSlot(slotPosition, -1);
                slot.GetComponent<SpriteRenderer>().sprite = null;
                slot.GetComponent<SpriteRenderer>().color = Color.clear;
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotPosition - 1).GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                GetComponent<PlayerPrefsManager>().SetPowerUpSlot(slotPosition, GetMiddleObjectNumber());
                slot.GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
                slot.GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(slotPosition - 1).GetComponent<SpriteRenderer>().sprite =
                    GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public void ChangeSlotSpriteGraphics()
    {
        if (!GetSliderUnlock(GetMiddleObjectNumber()))
        {
            GetComponent<GraphicsManager>().SetGraphic(GetMiddleObjectNumber());
            GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationGraphics, "Get graphics?");
        }
        else
        {
            GetComponent<PlayerPrefsManager>().SetGraphics(GetMiddleObjectNumber());
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            GetComponent<GraphicsManager>().SetGraphic(GetComponent<PlayerPrefsManager>().GetGraphics());
        }
    }

    public void ChangeSlotSpriteFlooring()
    {
        if (!GetSliderUnlock(GetMiddleObjectNumber()))
        {
            GetComponent<ThemeManager>().SetFlooring(GetMiddleObjectNumber());
            GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen(Menus.Menu.ConfirmationFooring, "Get flooring?");
        }
        else
        {
            GetComponent<PlayerPrefsManager>().SetFlooring(GetMiddleObjectNumber());
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            GetComponent<ThemeManager>().SetFlooring(GetComponent<PlayerPrefsManager>().GetFlooring());
		}
    }

    Sprite GetSliderSprite(int index)
    {
        if (currentMenu == Menus.Menu.PowerUps)
        {
            return GetComponent<PowerUpsManager>().powerUpList[index].sprite;
        }
        else if (currentMenu == Menus.Menu.Graphics)
        {
            return GetComponent<GraphicsManager>().graphicList[index].sprite;
        }
        else if (currentMenu == Menus.Menu.Flooring)
        {
            return GetComponent<ThemeManager>().flooringList[index].sprite;
        }
        return null;
    }

    bool GetSliderUnlock(int index)
    {
        if (currentMenu == Menus.Menu.PowerUps)
        {
            return GetComponent<PowerUpsManager>().powerUpList[index].unlocked;
        }
        else if (currentMenu == Menus.Menu.Graphics)
        {
            return GetComponent<GraphicsManager>().graphicList[index].unlocked;
        }
        else if (currentMenu == Menus.Menu.Flooring)
        {
            return GetComponent<ThemeManager>().flooringList[index].unlocked;
        }
        return false;
    }

    int GetSliderPrice(int index)
    {
        if (currentMenu == Menus.Menu.PowerUps)
        {
            return GetComponent<PowerUpsManager>().powerUpList[index].price;
        }
        else if (currentMenu == Menus.Menu.Graphics)
        {
            return GetComponent<GraphicsManager>().graphicList[index].price;
        }
        else if (currentMenu == Menus.Menu.Flooring)
        {
            return GetComponent<ThemeManager>().flooringList[index].price;
        }
        return 1000000000;
    }

    public string GetSliderDescription(int index)
    {
        if (currentMenu == Menus.Menu.PowerUps)
        {
            return GetComponent<PowerUpsManager>().powerUpList[index].description;
        }
        else if (currentMenu == Menus.Menu.Graphics)
        {
            return GetComponent<GraphicsManager>().graphicList[index].description;
        }
        else if (currentMenu == Menus.Menu.Flooring)
        {
            return GetComponent<ThemeManager>().flooringList[index].description;
        }
        return "";
    }

    public GameObject GetMiddleObject()
    {
        return scrollList[2];
    }

    public int GetMiddleObjectNumber()
    {
        return int.Parse(scrollList[2].GetComponent<SpriteRenderer>().sprite.name);
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

    public void TurnOnScrollView(Menus.Menu menu)
    {
        scrollView.transform.GetChild(0).gameObject.layer = 0;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 1);
        if (menu == Menus.Menu.PowerUps)
        {
            slot1.transform.GetChild(0).gameObject.layer = 0;
            slot2.transform.GetChild(0).gameObject.layer = 0;
            slot3.transform.GetChild(0).gameObject.layer = 0;
            slot1.GetComponent<SpriteRenderer>().color = textColor;
            slot2.GetComponent<SpriteRenderer>().color = textColor;
            slot3.GetComponent<SpriteRenderer>().color = textColor;
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            HighLightSlot(slot1);
        }
        else if (menu == Menus.Menu.Graphics || menu == Menus.Menu.Flooring)
        {
            slot1.transform.GetChild(0).gameObject.layer = 2;
            slot2.transform.GetChild(0).gameObject.layer = 0;
            slot3.transform.GetChild(0).gameObject.layer = 2;
            slot1.GetComponent<SpriteRenderer>().color = Color.clear;
            slot2.GetComponent<SpriteRenderer>().color = Color.green;
            slot3.GetComponent<SpriteRenderer>().color = Color.clear;
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }

    public void TurnOffScrollView()
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
    }

    public void TurnOffSliderColliders()
    {
        scrollView.transform.GetChild(0).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 2;
        slot2.transform.GetChild(0).gameObject.layer = 2;
        slot3.transform.GetChild(0).gameObject.layer = 2;
    }
}
