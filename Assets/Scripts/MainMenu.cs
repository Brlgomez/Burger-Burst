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
    int currentSlotNum;
    GameObject currentSlot;
    int slotPosition;

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
        if (Input.GetMouseButton(0))
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
            Camera.main.GetComponent<ScreenTextManagment>().PressTextDown(target.transform.parent.gameObject);
        }
    }

    void MouseUp()
    {
        if (target != null && target.tag == "UI")
        {
            Camera.main.GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            if (Camera.main.GetComponent<ScreenTextManagment>().GetMenu() == "Main Menu")
            {
                if (target.name == "Second Button")
                {
                    gameObject.AddComponent<CameraMovement>().MoveToGameplay("Start");
                    Destroy(GetComponent<MainMenu>());
                }
                else if (target.name == "Third Button")
                {
                    Camera.main.GetComponent<ScreenTextManagment>().ChangeToUpgradeText();
                }
            }
            else if (Camera.main.GetComponent<ScreenTextManagment>().GetMenu() == "Upgrade")
            {
                if (target.name == "Fifth Button")
                {
                    Camera.main.GetComponent<ScreenTextManagment>().ChangeToMenuText();
                    currentSlot = null;
                }
                else if (target.name == "Scroller")
                {
                    if (!changeScrollerObjects)
                    {
                        currentSlotNum = int.Parse(GetComponent<ScreenTextManagment>().GetMiddleObject().GetComponent<SpriteRenderer>().sprite.name);
                        if (currentSlot != null)
                        {
                            GetComponent<ScreenTextManagment>().ChangeSlotSprite(currentSlot, currentSlotNum, slotPosition);
                        }
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
            point1 = Vector3.zero;
            point2 = Vector3.zero;
        }
    }

    void MouseDrag()
    {
        if (target.name == "Scroller")
        {
            roundScroller = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 0.5f))
            {
                if (hit.collider.name == "Scroller")
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
					}
                    target.transform.parent.transform.GetChild(1).transform.localPosition = new Vector3(
                        target.transform.parent.transform.GetChild(1).transform.localPosition.x + change, 0, 0
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
