using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameObject target;
    Vector3 point1, point2, originalPoint;
    int lastScrollX;
    bool roundScroller;
    bool changeScrollerObjects;
    GameObject currentSlot;
    int slotPosition = 1;
    bool pressedTitle;

    void Start()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        StartCoroutine(MovePhoneUp());
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
            roundScroller = GetComponent<MenuSlider>().MoveScroller();
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
            originalPoint = hitInfo.point;
        }
    }

    void MouseUp()
    {
        if (target != null && target.name == "Phone" && GetComponent<ScreenTextManagment>().GetMenu() == Menus.Menu.Title && !pressedTitle)
        {
            pressedTitle = true;
            StartCoroutine(PhoneStartUp());
        }
        else if (target != null && target.tag == "UI")
        {
            GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            switch (GetComponent<ScreenTextManagment>().GetMenu())
            {
                case Menus.Menu.MainMenu:
                    MouseUpMainMenu();
                    break;
                case Menus.Menu.PowerUps:
                    MouseUpUpgradeMenu();
                    break;
                case Menus.Menu.Customize:
                    MouseUpCustomizeMenu();
                    break;
                case Menus.Menu.Store:
                    MouseUpStoreMenu();
                    break;
                case Menus.Menu.Setting:
                    MouseUpSettingMenu();
                    break;
                case Menus.Menu.ConfirmationPowerUp:
                    MouseUpConfirmationPowerUpMenu();
                    break;
                case Menus.Menu.Graphics:
                    MouseUpGraphicsMenu();
                    break;
                case Menus.Menu.ConfirmationGraphics:
                    MouseUpConfirmationGraphicsMenu();
                    break;
                case Menus.Menu.Theme:
                    MouseUpThemeMenu();
                    break;
                case Menus.Menu.Flooring:
                    MouseUpFlooringMenu();
                    break;
                case Menus.Menu.ConfirmationFlooring:
                    MouseUpConfirmationFlooringMenu();
                    break;
                case Menus.Menu.Wallpaper:
                    MouseUpWallpaperMenu();
                    break;
                case Menus.Menu.ConfirmationWalls:
                    MouseUpConfirmationWallpaperMenu();
                    break;
                case Menus.Menu.Detail:
                    MouseUpDetailMenu();
                    break;
                case Menus.Menu.ConfirmationDetail:
                    MouseUpConfirmationDetailMenu();
                    break;
            }
        }
        point1 = Vector3.zero;
        point2 = Vector3.zero;
    }

    void MouseDrag()
    {
        if (target.name == "Scroller")
        {
            GetComponent<MenuSlider>().EnableScroller(true);
            roundScroller = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 0.5f))
            {
                if (hit.collider.name == "Off Screen Scroller")
                {
                    point2 = hit.point;
                    float change;
                    if (transform.eulerAngles.y - 180 > 0)
                    {
                        change = (point1.z - point2.z) * -50;
                    }
                    else
                    {
                        change = (point1.z - point2.z) * 50;
                    }
                    if (change > 1)
                    {
                        change = 0.5f;
                    }
                    if (change < -1)
                    {
                        change = -0.5f;
                    }
                    if (Mathf.Abs(originalPoint.z - point2.z) > 0.002f && !changeScrollerObjects)
                    {
                        changeScrollerObjects = true;
                        GetComponent<MenuSlider>().ChangeScrollerItemColor(false);
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
                            GetComponent<MenuSlider>().MoveScrollObjects(1);
                        }
                        else
                        {
                            GetComponent<MenuSlider>().MoveScrollObjects(-1);
                        }
                        lastScrollX = newPos;
                    }
                    GetComponent<MenuSlider>().ScaleScrollerObjects();
                }
            }
        }
    }

    IEnumerator MovePhoneUp()
    {
        yield return new WaitForSeconds(0.5f);
		gameObject.AddComponent<CameraMovement>().MoveToTitle();
	}

    IEnumerator PhoneStartUp()
    {
        for (int i = 0; i < 5; i++)
        {
            target.GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().phoneLoading;
            yield return new WaitForSeconds(0.25f);
            target.GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().phoneLoading2;
            yield return new WaitForSeconds(0.25f);
        }
		target.GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().phoneOn;
		GetComponent<ScreenTextManagment>().ChangeToMenuText();
    }

    void MouseUpMainMenu()
    {
        switch (target.name)
        {
            case "First Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                GetComponent<Gameplay>().ResetValues();
                gameObject.AddComponent<CameraMovement>().MoveToGameplay("Start");
                Destroy(GetComponent<MainMenu>());
                break;
            case "Second Button":
                GetComponent<MenuSlider>().SetUpMenu(Menus.Menu.PowerUps);
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToPowerUp();
                currentSlot = GetComponent<ObjectManager>().Phone().transform.GetChild(5).GetChild(2).GetChild(0).GetChild(0).gameObject;
                slotPosition = 1;
                lastScrollX = -Mathf.RoundToInt(GetComponent<MenuSlider>().GetMiddleObject().transform.localPosition.z);
                break;
            case "Third Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToCustomize();
                break;
            case "Fourth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToStore();
                break;
            case "Fifth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToSetting();
                GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().enabled = true;
                GetComponent<ObjectManager>().Horn().GetComponent<Animator>().enabled = true;
                GetComponent<ObjectManager>().VibratingDevice().GetComponent<Animator>().enabled = true;
                if (GetComponent<PlayerPrefsManager>().GetMusic())
                {
                    GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().SetBool("Music Off", false);
                    GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().Play("MusicOn");
                    GetComponent<ObjectManager>().Stereo().GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().stereoOn;
                    GetComponent<ObjectManager>().Stereo().GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    GetComponent<ObjectManager>().Stereo().GetComponent<Renderer>().material.mainTexture = GetComponent<Textures>().stereoOff;
                }
                break;
        }
    }

    void MouseUpUpgradeMenu()
    {
        if (target.name == "Third Button")
        {
            GetComponent<MenuSlider>().ChangeSlotSprite(currentSlot, slotPosition);
        }
        else if (target.name == "Fifth Button")
        {
            roundScroller = false;
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            gameObject.AddComponent<CameraMovement>().MoveToMenu(true);
        }
        else if (target.name == "Scroller")
        {
            GetComponent<MenuSlider>().EnableScroller(false);
            if (!changeScrollerObjects)
            {
                GetComponent<MenuSlider>().ChangeSlotSprite(currentSlot, slotPosition);
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
            GetComponent<MenuSlider>().HighLightSlot(currentSlot.transform.parent.gameObject);
        }
    }

    void MouseUpCustomizeMenu()
    {
        switch (target.name)
        {
            case "First Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToTheme();
                break;
            case "Second Button":
                GetComponent<MenuSlider>().SetUpMenu(Menus.Menu.Graphics);
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToGraphics();
                lastScrollX = -Mathf.RoundToInt(GetComponent<MenuSlider>().GetMiddleObject().transform.localPosition.z);
                break;
            case "Fifth Button":
                roundScroller = false;
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToMenu(true);
                break;
        }
    }

    void MouseUpThemeMenu()
    {
        switch (target.name)
        {
            case "First Button":
                GetComponent<MenuSlider>().SetUpMenu(Menus.Menu.Flooring);
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToFlooring();
                lastScrollX = -Mathf.RoundToInt(GetComponent<MenuSlider>().GetMiddleObject().transform.localPosition.z);
                break;
            case "Second Button":
                GetComponent<MenuSlider>().SetUpMenu(Menus.Menu.Wallpaper);
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToWallpaper();
                lastScrollX = -Mathf.RoundToInt(GetComponent<MenuSlider>().GetMiddleObject().transform.localPosition.z);
                break;
            case "Third Button":
                GetComponent<MenuSlider>().SetUpMenu(Menus.Menu.Detail);
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToDetail();
                lastScrollX = -Mathf.RoundToInt(GetComponent<MenuSlider>().GetMiddleObject().transform.localPosition.z);
                break;
            case "Fifth Button":
                roundScroller = false;
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToCustomize();
                break;
        }
    }

    void MouseUpConfirmationPowerUpMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<ScreenTextManagment>().BuyUpgrade();
                GetComponent<ScreenTextManagment>().ChangeToUpgradeText();
                GetComponent<MenuSlider>().HighLightSlot(currentSlot.transform.parent.gameObject);
                break;
            case "Fourth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToStore();
                break;
            case "Fifth Button":
                GetComponent<ScreenTextManagment>().ChangeToUpgradeText();
                GetComponent<MenuSlider>().HighLightSlot(currentSlot.transform.parent.gameObject);
                break;
        }
    }

    void MouseUpStoreMenu()
    {
        if (target.name == "First Button")
        {
            //TODO: IAP
        }
        else if (target.name == "Second Button")
        {
            //TODO: IAP
        }
        else if (target.name == "Third Button")
        {
            //TODO: IAP
        }
        else if (target.name == "Fourth Button")
        {
            //TODO: IAP
        }
        else if (target.name == "Fifth Button")
        {
            GetComponent<ScreenTextManagment>().CannotPressAnything();
            Menus.Menu lastMenu = GetComponent<ScreenTextManagment>().GetLastMenu();
            switch (lastMenu)
            {
                case Menus.Menu.MainMenu:
                    gameObject.AddComponent<CameraMovement>().MoveToMenu(true);
                    break;
                case Menus.Menu.ConfirmationPowerUp:
                    gameObject.AddComponent<CameraMovement>().MoveToPowerUp();
                    break;
                case Menus.Menu.ConfirmationFlooring:
                    gameObject.AddComponent<CameraMovement>().MoveToFlooring();
                    break;
                case Menus.Menu.ConfirmationWalls:
                    gameObject.AddComponent<CameraMovement>().MoveToWallpaper();
                    break;
                case Menus.Menu.ConfirmationDetail:
                    gameObject.AddComponent<CameraMovement>().MoveToDetail();
                    break;
                case Menus.Menu.ConfirmationGraphics:
                    gameObject.AddComponent<CameraMovement>().MoveToGraphics();
                    break;
            }
        }
    }

    void MouseUpSettingMenu()
    {
        switch (target.name)
        {
            case "First Button":
                GetComponent<PlayerPrefsManager>().SetMusic();
                break;
            case "Second Button":
                GetComponent<PlayerPrefsManager>().SetSound();
                break;
            case "Third Button":
                GetComponent<PlayerPrefsManager>().SetVibration();
                break;
            case "Fourth Button":
                //TODO: achivements and leaderboards
                break;
            case "Fifth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToMenu(true);
                GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().SetBool("Music Off", true);
                GetComponent<ObjectManager>().Stereo().GetComponent<Animator>().enabled = false;
                GetComponent<ObjectManager>().Stereo().GetComponent<ParticleSystem>().Stop();
                GetComponent<ObjectManager>().Horn().GetComponent<Animator>().enabled = false;
                GetComponent<ObjectManager>().VibratingDevice().GetComponent<Animator>().enabled = false;
                break;
        }
    }

    void MouseUpGraphicsMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<MenuSlider>().ChangeSlotSpriteGraphics();
                break;
            case "Fifth Button":
                roundScroller = false;
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToCustomize();
                break;
            case "Scroller":
                GetComponent<MenuSlider>().EnableScroller(false);
                if (!changeScrollerObjects)
                {
                    GetComponent<MenuSlider>().ChangeSlotSpriteGraphics();
                }
                roundScroller = true;
                changeScrollerObjects = false;
                break;
        }
    }

    void MouseUpConfirmationGraphicsMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<ScreenTextManagment>().BuyGraphics();
                GetComponent<ScreenTextManagment>().ChangeToGraphicsScreen();
                break;
            case "Fourth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToStore();
                break;
            case "Fifth Button":
                GetComponent<GraphicsManager>().SetGraphic(GetComponent<PlayerPrefsManager>().GetGraphics());
                GetComponent<ScreenTextManagment>().ChangeToGraphicsScreen();
                break;
        }
    }

    void MouseUpFlooringMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<MenuSlider>().ChangeSlotSpriteFlooring();
                break;
            case "Fifth Button":
                roundScroller = false;
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToTheme();
                break;
            case "Scroller":
                GetComponent<MenuSlider>().EnableScroller(false);
                if (!changeScrollerObjects)
                {
                    GetComponent<MenuSlider>().ChangeSlotSpriteFlooring();
                }
                roundScroller = true;
                changeScrollerObjects = false;
                break;
        }
    }

    void MouseUpConfirmationFlooringMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<ScreenTextManagment>().BuyFlooring();
                GetComponent<ScreenTextManagment>().ChangeToFlooringScreen();
                break;
            case "Fourth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToStore();
                break;
            case "Fifth Button":
                GetComponent<ThemeManager>().SetFlooring(GetComponent<PlayerPrefsManager>().GetFlooring());
                GetComponent<ScreenTextManagment>().ChangeToFlooringScreen();
                break;
        }
    }

    void MouseUpWallpaperMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<MenuSlider>().ChangeSlotSpriteWallpaper();
                break;
            case "Fifth Button":
                roundScroller = false;
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToTheme();
                break;
            case "Scroller":
                GetComponent<MenuSlider>().EnableScroller(false);
                if (!changeScrollerObjects)
                {
                    GetComponent<MenuSlider>().ChangeSlotSpriteWallpaper();
                }
                roundScroller = true;
                changeScrollerObjects = false;
                break;
        }
    }

    void MouseUpConfirmationWallpaperMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<ScreenTextManagment>().BuyWallpaper();
                GetComponent<ScreenTextManagment>().ChangeToWallpaperScreen();
                break;
            case "Fourth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToStore();
                break;
            case "Fifth Button":
                GetComponent<ThemeManager>().SetWallpaper(GetComponent<PlayerPrefsManager>().GetWallpaper());
                GetComponent<ScreenTextManagment>().ChangeToWallpaperScreen();
                break;
        }
    }

    void MouseUpDetailMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<MenuSlider>().ChangeSlotSpriteDetail();
                break;
            case "Fifth Button":
                roundScroller = false;
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToTheme();
                break;
            case "Scroller":
                GetComponent<MenuSlider>().EnableScroller(false);
                if (!changeScrollerObjects)
                {
                    GetComponent<MenuSlider>().ChangeSlotSpriteDetail();
                }
                roundScroller = true;
                changeScrollerObjects = false;
                break;
        }
    }

    void MouseUpConfirmationDetailMenu()
    {
        switch (target.name)
        {
            case "Third Button":
                GetComponent<ScreenTextManagment>().BuyDetail();
                GetComponent<ScreenTextManagment>().ChangeToDetailScreen();
                break;
            case "Fourth Button":
                GetComponent<ScreenTextManagment>().CannotPressAnything();
                gameObject.AddComponent<CameraMovement>().MoveToStore();
                break;
            case "Fifth Button":
                GetComponent<ThemeManager>().SetDetail(GetComponent<PlayerPrefsManager>().GetDetail());
                GetComponent<ScreenTextManagment>().ChangeToDetailScreen();
                break;
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
