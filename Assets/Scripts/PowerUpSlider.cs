using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlider : MonoBehaviour
{
    List<GameObject> scrollList = new List<GameObject>();
    GameObject scrollView, slot1, slot2, slot3;
    Color textColor = new Color(0, 0.5f, 1);

    void Start()
    {
        scrollView = GetComponent<ObjectManager>().Phone().transform.GetChild(5).gameObject;
        slot1 = scrollView.transform.GetChild(2).GetChild(0).gameObject;
        slot2 = scrollView.transform.GetChild(2).GetChild(1).gameObject;
        slot3 = scrollView.transform.GetChild(2).GetChild(2).gameObject;
        RestartScrollList();
        SetSlotSprites();
        ScaleScrollerObjects();
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
                SetUpScrollObject(scrollList[0], (GetComponent<PowerUpsManager>().powerUpList.Count - 1));
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
            if ((scrollSprite + 1) <= (GetComponent<PowerUpsManager>().powerUpList.Count - 1))
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
        GetComponent<ScreenTextManagment>().ChangeSliderInfo(GetMiddleObjectNumber());
    }

    void SetUpScrollObject(GameObject scrollObj, int powerUpNum)
    {
        scrollObj.GetComponent<SpriteRenderer>().sprite = GetComponent<PowerUpsManager>().powerUpList[powerUpNum].sprite;
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
                SetUpScrollObject(scrollList[i], GetComponent<PowerUpsManager>().powerUpList.Count + 2 - i);
            }
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
            slot1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 1")].sprite;
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 1")].sprite;
        }
        if (PlayerPrefs.GetInt("UPGRADE 2", -1) > -1)
        {
            slot2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 2")].sprite;
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 2")].sprite;
        }
        if (PlayerPrefs.GetInt("UPGRADE 3", -1) > -1)
        {
            slot3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 3")].sprite;
            GetComponent<ObjectManager>().PowerUpsLed().transform.GetChild(2).GetComponent<SpriteRenderer>().sprite =
                GetComponent<PowerUpsManager>().powerUpList[PlayerPrefs.GetInt("UPGRADE 3")].sprite;
        }
    }

    public void ChangeSlotSprite(GameObject slot, int slotPosition)
    {
        if (!GetComponent<PowerUpsManager>().powerUpList[int.Parse(GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name)].unlocked)
        {
            GetComponent<ScreenTextManagment>().ChangeToConfirmationScreen();
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

    public GameObject GetMiddleObject()
    {
        return scrollList[2];
    }

    public int GetMiddleObjectNumber()
    {
        return int.Parse(scrollList[2].GetComponent<SpriteRenderer>().sprite.name);
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
