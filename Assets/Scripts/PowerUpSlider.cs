using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlider : MonoBehaviour
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
        SetPowerUpLED();
        SetGraphic(PlayerPrefs.GetInt("GRAPHICS"));
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
        if (PlayerPrefs.GetInt("UPGRADE 1", -1) > -1)
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(PlayerPrefs.GetInt("UPGRADE 1"));
        }
        else
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        if (PlayerPrefs.GetInt("UPGRADE 2", -1) > -1)
        {
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(PlayerPrefs.GetInt("UPGRADE 2"));
        }
        else
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        if (PlayerPrefs.GetInt("UPGRADE 3", -1) > -1)
        {
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(PlayerPrefs.GetInt("UPGRADE 3"));
        }
        else
        {
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    void SetPowerUpLED()
    {
        if (PlayerPrefs.GetInt("UPGRADE 1", -1) > -1)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 1")].sprite;
        }
        if (PlayerPrefs.GetInt("UPGRADE 2", -1) > -1)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 2")].sprite;
        }
        if (PlayerPrefs.GetInt("UPGRADE 3", -1) > -1)
        {
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(2).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 3")].sprite;
        }
    }

    public void SetGraphic(int graphicNum)
    {
        if (graphicNum == 0)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
		}
        else if (graphicNum == 1)
        {
            GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = true;
        }
        else
        {
			GetComponent<Assets.Pixelation.Scripts.Chunky>().enabled = false;
		}
    }

    public void SetGraphicsSprite()
    {
        slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSliderSprite(PlayerPrefs.GetInt("GRAPHICS"));
    }

    public void ChangeSlotSprite(GameObject slot, int slotPosition)
    {
        if (!GetSliderUnlock(int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name)))
        {
            GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen();
        }
        else
        {
            if (GetComponent<PlayerPrefsManager>().ContainsUpgradeBesidesSlot(GetMiddleObjectNumber(), slotPosition))
            {
                int slotNum = GetComponent<PlayerPrefsManager>().WhichSlotContainsUpgrade(GetMiddleObjectNumber());
                PlayerPrefs.SetInt("UPGRADE " + slotNum, -1);
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
                PlayerPrefs.SetInt("UPGRADE " + slotPosition, -1);
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

    public void ChangeSlotSpriteGraphics()
    {
        if (!GetSliderUnlock(int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name)))
        {
            SetGraphic(int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name));
            GetComponent<ScreenTextManagment>().ChangeToConfirmationGraphicsScreen();
        }
        else
        {
            PlayerPrefs.SetInt("GRAPHICS", GetMiddleObjectNumber());
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetMiddleObject().GetComponent<SpriteRenderer>().sprite;
            SetGraphic(PlayerPrefs.GetInt("GRAPHICS"));
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

    public void TurnOnScrollView()
    {
        scrollView.transform.GetChild(0).gameObject.layer = 0;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 1);
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

    public void TurnOnGraphicsScrollView()
    {
        scrollView.transform.GetChild(0).gameObject.layer = 0;
        scrollView.transform.GetChild(3).gameObject.layer = 2;
        scrollView.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 1);
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

    public void TurnOffColliders()
    {
        scrollView.transform.GetChild(0).gameObject.layer = 2;
        slot1.transform.GetChild(0).gameObject.layer = 2;
        slot2.transform.GetChild(0).gameObject.layer = 2;
        slot3.transform.GetChild(0).gameObject.layer = 2;
    }
}
