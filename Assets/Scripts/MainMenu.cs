using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameObject target;
    Vector3 point1, point2;
    int lastScrollX;
    bool roundScroller;
    bool changeScrollerObjects;
    GameObject currentSlot;
    int slotPosition = 1;

    void Start()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButton(0) && target != null)
        {
            MouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
        if (roundScroller)
        {
            roundScroller = GetComponent<ScreenTextManagment>().MoveScroller();
        }
    }

    void MouseDown()
    {
        RaycastHit hitInfo;
        target = null;
        target = ReturnClickedObject(out hitInfo);
        if (target != null && target.tag == "UI")
        {
            GetComponent<ScreenTextManagment>().PressTextDown(target.transform.parent.gameObject);
            point1 = hitInfo.point;
        }
    }

    void MouseUp()
    {
        if (target != null && target.tag == "UI")
        {
            GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.MainMenu)
            {
                MouseUpMainMenu();
            }
            else if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.PowerUps)
            {
                MouseUpUpgradeMenu();
            }
            else if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Customize)
            {
                MouseUpCustomizeMenu();
            }
            else if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Store)
            {
                MouseUpStoreMenu();
            }
            else if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Setting)
            {
                MouseUpSettingMenu();
            }
            else if (GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Confirmation)
            {
                MouseUpConfirmationMenu();
            }
        }
        point1 = Vector3.zero;
        point2 = Vector3.zero;
    }

    void MouseDrag()
    {
        if (target.name == "Scroller")
        {
            GetComponent<ScreenTextManagment>().EnableScroller(true);
            roundScroller = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 0.5f))
            {
                if (hit.collider.name == "Off Screen Scroller")
                {
                    point2 = hit.point;
                    float change = (point1.z - point2.z) * -50;
                    if (change > 1)
                    {
                        change = 0.5f;
                    }
                    if (change < -1)
                    {
                        change = -0.5f;
                    }
                    if (Mathf.Abs(change) > 0.01f && !changeScrollerObjects)
                    {
                        changeScrollerObjects = true;
                        GetComponent<ScreenTextManagment>().ChangeScrollerItemColor(false);
                    }
                    target.transform.parent.transform.GetChild(1).transform.localPosition = new Vector3(
                        target.transform.parent.transform.GetChild(1).transform.localPosition.x + change,
                        target.transform.parent.transform.GetChild(1).transform.localPosition.y,
                        0
                    );
                    point1 = hit.point;
                    if (Mathf.RoundToInt(target.transform.parent.transform.GetChild(1).transform.localPosition.x) != lastScrollX)
                    {
                        int newPos = Mathf.RoundToInt(target.transform.parent.transform.GetChild(1).transform.localPosition.x);
                        if (newPos > lastScrollX)
                        {
                            GetComponent<ScreenTextManagment>().MoveScrollObjects(1);
                        }
                        else
                        {
                            GetComponent<ScreenTextManagment>().MoveScrollObjects(-1);
                        }
                        lastScrollX = newPos;
                    }
                    GetComponent<ScreenTextManagment>().ScaleScrollerObjects();
                }
            }
        }
    }

    void MouseUpMainMenu()
    {
        if (target.name == "First Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToGameplay("Start");
            Destroy(GetComponent<MainMenu>());
        }
        else if (target.name == "Second Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToPowerUp();
            currentSlot = GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(2).GetChild(0).GetChild(0).gameObject;
            slotPosition = 1;
            lastScrollX = -Mathf.RoundToInt(GetComponent<ScreenTextManagment>().GetMiddleObject().transform.localPosition.z);
        }
        else if (target.name == "Third Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToCustomize();
        }
        else if (target.name == "Fourth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToStore();
        }
        else if (target.name == "Fifth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToSetting();
        }
    }

    void MouseUpUpgradeMenu()
    {
        if (target.name == "Third Button")
        {
            GetComponent<ScreenTextManagment>().ChangeSlotSprite(currentSlot, slotPosition);
        }
        else if (target.name == "Fourth Button")
        {
            GetComponent<ScreenTextManagment>().ChangeSlotSprite(currentSlot, slotPosition);
        }
        else if (target.name == "Fifth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToMenu(false);
        }
        else if (target.name == "Scroller")
        {
            GetComponent<ScreenTextManagment>().EnableScroller(false);
            if (!changeScrollerObjects)
            {
                GetComponent<ScreenTextManagment>().ChangeSlotSprite(currentSlot, slotPosition);
            }
            roundScroller = true;
            changeScrollerObjects = false;
        }
        else if (target.name == "Left Slot" || target.name == "Middle Slot" || target.name == "Right Slot")
        {
            switch (target.name)
            {
                case "Left Slot":
                    slotPosition = 1;
                    break;
                case "Middle Slot":
                    slotPosition = 2;
                    break;
                default:
                    slotPosition = 3;
                    break;
            }
            currentSlot = target;
            GetComponent<ScreenTextManagment>().HighLightSlot(currentSlot.transform.parent.gameObject);
        }
    }

    void MouseUpConfirmationMenu()
    {
        if (target.name == "Third Button")
        {
			GetComponent<ScreenTextManagment>().BuyUpgrade();
            GetComponent<ScreenTextManagment>().ChangeToUpgradeText();
			GetComponent<ScreenTextManagment>().HighLightSlot(currentSlot.transform.parent.gameObject);
		}
        else if (target.name == "Fourth Button")
        {
            //TODO: BUY COINS
        }
        else if (target.name == "Fifth Button")
        {
			GetComponent<ScreenTextManagment>().ChangeToUpgradeText();
			GetComponent<ScreenTextManagment>().HighLightSlot(currentSlot.transform.parent.gameObject);
		}
    }

    void MouseUpCustomizeMenu()
    {
        if (target.name == "Fifth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToMenu(false);
        }
    }

    void MouseUpStoreMenu()
    {
        if (target.name == "Fifth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToMenu(false);
        }
    }

    void MouseUpSettingMenu()
    {
        if (target.name == "Fifth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToMenu(false);
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            point1 = hit.point;
            return hit.collider.gameObject;
        }
        return null;
    }
}
