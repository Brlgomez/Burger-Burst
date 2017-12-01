﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    static int updateInterval = 30;

    GameObject target, counterWall, grillWall, fryerWall, sodaWall, phone;
    GameObject rightFryer, leftFryer, sodaFountain1, sodaFountain2, sodaFountain3;
    List<Vector3> positions = new List<Vector3>();
    Vector3 direction;
    Transform initialPosition;
    bool paused;
    float timeForDrag;
    static float limitForNewDragPosition = 0.01f;
    enum Area { counter, pause, gameOver, quit, grill, fryer, sodaMachine };
    Area currentArea;
    Area previousArea;
    int throwingDistance = 15;
    bool frozen = false;

    void Start()
    {
        counterWall = GetComponent<ObjectManager>().CounterWall();
        grillWall = GetComponent<ObjectManager>().GrillWall();
        fryerWall = GetComponent<ObjectManager>().FryerWall();
        sodaWall = GetComponent<ObjectManager>().SodaWall();
        rightFryer = GetComponent<ObjectManager>().RightFryer();
        leftFryer = GetComponent<ObjectManager>().LeftFryer();
        sodaFountain1 = GetComponent<ObjectManager>().SodaMachine1();
        sodaFountain2 = GetComponent<ObjectManager>().SodaMachine2();
        sodaFountain3 = GetComponent<ObjectManager>().SodaMachine3();
        phone = GetComponent<ObjectManager>().Phone();
        initialPosition = GetComponent<PositionManager>().GameplayPosition();
        currentArea = Area.counter;
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().throwFurther.powerUpNumber))
        {
            throwingDistance = 23;
        }
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().moreHealth.powerUpNumber))
        {
            GetComponent<Gameplay>().ChangeMaxHealth();
        }
        if (GetComponent<PlayerPrefsManager>().ContainsUpgrade(GetComponent<PowerUpsManager>().defenseIncrease.powerUpNumber))
        {
            GetComponent<Gameplay>().IncreaseDefense();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButton(0) && !frozen)
        {
            MouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
        if (Time.frameCount % updateInterval == 0)
        {
            GetComponent<Gameplay>().RegenerationUpdate(updateInterval);
            GetComponent<ZombieManager>().ZombieUpdate(updateInterval);
            GetComponent<WindManager>().WindUpdate(updateInterval);
        }
    }

    void MouseDown()
    {
        positions.Clear();
        counterWall.GetComponent<Collider>().enabled = false;
        grillWall.GetComponent<Collider>().enabled = false;
        fryerWall.GetComponent<Collider>().enabled = false;
        sodaWall.GetComponent<Collider>().enabled = false;
        RaycastHit hitInfo;
        target = ReturnClickedObject(out hitInfo);
        if (target != null && !paused)
        {
            switch (currentArea)
            {
                case Area.counter:
                    MouseDownCounter();
                    break;
                case Area.grill:
                    MouseDownGrill();
                    break;
                case Area.fryer:
                    MouseDownFryer();
                    break;
                case Area.sodaMachine:
                    MouseDownSodaMachine();
                    break;
            }
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject obj = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 2.5f))
        {
            obj = hit.collider.gameObject;
            if (obj.tag == "UI")
            {
                if (GetComponent<Frozen>() != null && currentArea != Area.pause)
                {
                    return null;
                }
                GetComponent<ScreenTextManagment>().PressTextDown(obj.transform.parent.gameObject);
                return obj;
            }
            if (!paused && !GetComponent<Gameplay>().IsGameOver() && obj.GetComponent<FadeObject>() == null)
            {
                TurnOffPhoneColliders();
                TurnOffSodaButtonColliders();
                if (obj.tag == "Pause" && gameObject.GetComponent<CameraMovement>() == null)
                {
                    return obj;
                }
                if (currentArea == Area.counter)
                {
                    return GetCounterObject(obj);
                }
                if (currentArea == Area.grill)
                {
                    return GetGrillObject(obj);
                }
                if (currentArea == Area.fryer)
                {
                    return GetFryerObject(obj);
                }
                if (currentArea == Area.sodaMachine)
                {
                    return GetSodaMachineObject(obj);
                }

            }
        }
        return null;
    }

    void MouseDrag()
    {
        if (target != null && !paused && !GetComponent<Gameplay>().IsGameOver())
        {
            switch (currentArea)
            {
                case Area.counter:
                    DragCounterObject();
                    break;
                case Area.grill:
                    DragGrillObject();
                    break;
                case Area.fryer:
                    DragFryerObject();
                    break;
                case Area.sodaMachine:
                    DragSodaMachineObject();
                    break;
            }
        }
    }

    void MouseUp()
    {
        TurnOnPhoneColliders();
        TurnOnSodaButtonColliders();
        if (target != null)
        {
            if (target.tag == "UI")
            {
                PhoneInterface(target);
                GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            }
            if (!paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
            {
                MouseUpPauseButton();
                if (currentArea == Area.counter)
                {
                    MouseUpCounter();
                }
                if (currentArea == Area.grill)
                {
                    MouseUpGrill();
                }
                if (currentArea == Area.fryer)
                {
                    MouseUpFryer();
                }
                if (currentArea == Area.sodaMachine)
                {
                    MouseUpSodaMachine();
                }
            }
        }
        target = null;
    }

    void PhoneInterface(GameObject obj)
    {
        if (!paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
        {
            GameplayPhoneInterface(obj);
        }
        else if (gameObject.GetComponent<Gameplay>().IsGameOver() && gameObject.GetComponent<CameraMovement>() == null)
        {
            GameOverPhoneInterface(obj);
        }
        else if (paused && gameObject.GetComponent<CameraMovement>() == null)
        {
            PauseMenuInterface(obj);
        }
    }

    void GameplayPhoneInterface(GameObject obj)
    {
        switch (obj.name)
        {
            case "Second Button":
                if (currentArea != Area.grill)
                {
                    currentArea = Area.grill;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToGrillArea();
                    initialPosition = GetComponent<PositionManager>().GrillPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                    gameObject.AddComponent<CameraMovement>().MoveToGrill();
                }
                break;
            case "Third Button":
                if (currentArea != Area.fryer)
                {
                    currentArea = Area.fryer;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToFryerArea();
                    initialPosition = GetComponent<PositionManager>().FryerPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                    gameObject.AddComponent<CameraMovement>().MoveToFryer();
                }
                break;
            case "Fourth Button":
                if (currentArea != Area.sodaMachine)
                {
                    currentArea = Area.sodaMachine;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToSodaMachineArea();
                    initialPosition = GetComponent<PositionManager>().SodaPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                }
                gameObject.AddComponent<CameraMovement>().MoveToSodaMachine();
                break;
            case "Fifth Button":
                if (currentArea != Area.counter)
                {
                    currentArea = Area.counter;
                    gameObject.GetComponent<ScreenTextManagment>().ChangeToFrontArea();
                    initialPosition = GetComponent<PositionManager>().GameplayPosition();
                    if (gameObject.GetComponent<CameraMovement>() != null)
                    {
                        Destroy(gameObject.GetComponent<CameraMovement>());
                    }
                    gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
                }
                break;
        }
    }

    void GameOverPhoneInterface(GameObject obj)
    {
        if (obj.name == "First Button")
        {
            Restart();
        }
        else if (obj.name == "Second Button")
        {
            Quit();
        }
    }

    void PauseMenuInterface(GameObject obj)
    {
        if (paused)
        {
            if (obj.name == "First Button" && !gameObject.GetComponent<Gameplay>().IsGameOver())
            {
                currentArea = previousArea;
                switch (currentArea)
                {
                    case Area.counter:
                        initialPosition = GetComponent<PositionManager>().GameplayPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
                        break;
                    case Area.grill:
                        initialPosition = GetComponent<PositionManager>().GrillPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToUnpause("Grill");
                        break;
                    case Area.fryer:
                        initialPosition = GetComponent<PositionManager>().FryerPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToUnpause("Fryer");
                        break;
                    case Area.sodaMachine:
                        initialPosition = GetComponent<PositionManager>().SodaPosition();
                        gameObject.AddComponent<CameraMovement>().MoveToUnpause("Soda Machine");
                        break;
                }
            }
            else if (obj.name == "Second Button")
            {
                Restart();
            }
            else if (obj.name == "Third Button")
            {
                Quit();
            }
        }
    }

    GameObject GetCounterObject(GameObject obj)
    {
        if (obj.tag == "Ingredient" && GetComponent<Frozen>() == null)
        {
            return obj;
        }
        return null;
    }

    GameObject GetGrillObject(GameObject obj)
    {
        if (obj.tag == "GrillIngredientClone" && GetComponent<Frozen>() == null)
        {
            return obj;
        }
        return null;
    }

    GameObject GetFryerObject(GameObject obj)
    {
        if (GetComponent<Frozen>() == null)
        {
            if (obj.name == "Right Fryer Button" || obj.name == "Left Fryer Button" || obj.tag == "Basket")
            {
                return obj;
            }
            if (obj.tag == "Fries")
            {
                if (obj.GetComponent<FryFries>())
                {
                    return null;
                }
                return obj;
            }
        }
        return null;
    }

    GameObject GetSodaMachineObject(GameObject obj)
    {
        if ((obj.tag == "Soda" || obj.tag == "Lid" || obj.name == "Soda Button") && GetComponent<Frozen>() == null)
        {
            return obj;
        }
        return null;
    }

    void MouseDownCounter()
    {
        if (target.tag == "Ingredient")
        {
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            counterWall.GetComponent<Collider>().enabled = true;
        }
    }

    void MouseDownGrill()
    {
        if (target.tag == "GrillIngredientClone")
        {
            target.transform.rotation = new Quaternion(0, 0, 0, 0);
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            grillWall.GetComponent<Collider>().enabled = true;
            if (target.GetComponent<Meat>())
            {
                target.GetComponent<Meat>().PickedUp();
            }
        }
    }

    void MouseDownFryer()
    {
        if (target.tag == "Fries" || target.tag == "Basket")
        {
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            fryerWall.GetComponent<Collider>().enabled = true;
        }
    }

    void MouseDownSodaMachine()
    {
        if (target.tag == "Soda" || target.tag == "Lid")
        {
            target.transform.rotation = new Quaternion(0, 0, 0, 0);
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            sodaWall.GetComponent<Collider>().enabled = true;
            if (target.tag == "Soda")
            {
                target.layer = 2;
            }
            else
            {
                target.GetComponent<Collider>().enabled = false;
            }
        }
    }

    void DragCounterObject()
    {
        if (target.tag == "Ingredient")
        {
            ScreenPointToRayCalc(counterWall);
        }
    }

    void DragGrillObject()
    {
        if (target.tag == "GrillIngredientClone")
        {
            ScreenPointToRayCalc(grillWall);
        }
    }

    void DragFryerObject()
    {
        if (target.tag == "Fries" || target.tag == "Basket")
        {
            ScreenPointToRayCalc(fryerWall);
        }
    }

    void DragSodaMachineObject()
    {
        if (target.tag == "Soda" || target.tag == "Lid")
        {
            ScreenPointToRayCalc(sodaWall);
        }
    }

    void MouseUpPauseButton()
    {
        if (target.name == "Pause Image")
        {
            previousArea = currentArea;
            currentArea = Area.pause;
            initialPosition = GetComponent<PositionManager>().PausePosition();
            if (GetComponent<GettingHurt>() != null)
            {
                Destroy(GetComponent<GettingHurt>());
            }
            gameObject.AddComponent<CameraMovement>().MoveToPause();
            PauseGame();
        }
    }

    void MouseUpCounter()
    {
        if (target.tag == "Ingredient")
        {
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            counterWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                float speed = Vector3.Distance(positions[positions.Count - 1], positions[0]);
                float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 7) + ((target.transform.position.x * speed) * (throwingDistance / 5));
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 3;
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * throwingDistance;
                Vector3 newVelocity = new Vector3(xVelocity, yVelocity, zVelocity) * ((Screen.height / Screen.dpi) * 0.5f);
                if (newVelocity.magnitude > (throwingDistance * 0.66f))
                {
                    newVelocity *= ((throwingDistance * 0.66f) / newVelocity.magnitude);
                }
                target.GetComponent<Rigidbody>().velocity = newVelocity;
            }
        }
    }

    void MouseUpGrill()
    {
        if (target.tag == "GrillIngredientClone")
        {
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            grillWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                target.GetComponent<Rigidbody>().velocity = GetVelocity();
            }
        }
    }

    void MouseUpFryer()
    {
        if (target.name == "Left Fryer Button")
        {
            target.GetComponent<Animator>().Play("ButtonClick");
            leftFryer.GetComponent<FryerBasket>().PressedButton();
        }
        if (target.name == "Right Fryer Button")
        {
            target.GetComponent<Animator>().Play("ButtonClick");
            rightFryer.GetComponent<FryerBasket>().PressedButton();
        }
        if (target.tag == "Fries" || target.tag == "Basket")
        {
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            fryerWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                target.GetComponent<Rigidbody>().velocity = GetVelocity();
            }
        }
    }

    void MouseUpSodaMachine()
    {
        if (target.name == "Soda Button")
        {
            target.GetComponent<Animator>().Play("ButtonClick");
            if (target.transform.GetChild(0).gameObject.GetComponent<SodaMachine>() == null)
            {
                target.transform.GetChild(0).gameObject.AddComponent<SodaMachine>();
            }
            else
            {
                target.transform.GetChild(0).gameObject.GetComponent<SodaMachine>().ButtonPressed();
            }
        }
        if (target.tag == "Soda" || target.tag == "Lid")
        {
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            target.GetComponent<Rigidbody>().useGravity = true;
            sodaWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                target.GetComponent<Rigidbody>().velocity = GetVelocity();
            }
            if (target.GetComponent<SodaCup>() == null && target.tag == "Soda")
            {
                target.AddComponent<SodaCup>();
            }
            if (target.tag == "Soda")
            {
                target.layer = 0;
            }
            else
            {
                target.GetComponent<Collider>().enabled = true;
            }
        }
    }

    Vector3 GetVelocity()
    {
        float xVelocity = (positions[positions.Count - 1].x - positions[0].x) * 5;
        float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 5;
        float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 5;
        return new Vector3(xVelocity, yVelocity, zVelocity);
    }

    void Restart()
    {
        GetComponent<ScreenTextManagment>().CannotPressAnything();
        currentArea = Area.counter;
        initialPosition = GetComponent<PositionManager>().GameplayPosition();
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        ResetEverything();
        Destroy(GetComponent<Gameplay>());
        gameObject.AddComponent<Gameplay>();
        gameObject.AddComponent<CameraMovement>().MoveToGameplay("Restart");
    }

    void Quit()
    {
        GetComponent<ScreenTextManagment>().CannotPressAnything();
        currentArea = Area.quit;
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        ResetEverything();
        Destroy(GetComponent<Gameplay>());
        UnPauseGame();
        gameObject.AddComponent<CameraMovement>().MoveToMenu(true);
        gameObject.AddComponent<Gameplay>();
        Destroy(GetComponent<GrabAndThrowObject>());
    }

    void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public bool GetPausedState()
    {
        return paused;
    }

    public Transform GetInitialPosition()
    {
        return initialPosition;
    }

    void TurnOffPhoneColliders()
    {
        phone.GetComponent<Collider>().enabled = false;
        for (int i = 0; i < phone.transform.childCount; i++)
        {
            if (phone.transform.GetChild(i).transform.childCount > 1)
            {
                phone.transform.GetChild(i).transform.GetChild(0).GetComponent<Collider>().enabled = false;
            }
        }
    }

    void TurnOnPhoneColliders()
    {
        phone.GetComponent<Collider>().enabled = true;
        for (int i = 0; i < phone.transform.childCount; i++)
        {
            if (phone.transform.GetChild(i).transform.childCount > 1)
            {
                phone.transform.GetChild(i).transform.GetChild(0).GetComponent<Collider>().enabled = true;
            }
        }
    }

    void TurnOffSodaButtonColliders()
    {
        sodaFountain1.transform.parent.GetComponent<Collider>().enabled = false;
        sodaFountain2.transform.parent.GetComponent<Collider>().enabled = false;
        sodaFountain3.transform.parent.GetComponent<Collider>().enabled = false;
    }

    void TurnOnSodaButtonColliders()
    {
        sodaFountain1.transform.parent.GetComponent<Collider>().enabled = true;
        sodaFountain2.transform.parent.GetComponent<Collider>().enabled = true;
        sodaFountain3.transform.parent.GetComponent<Collider>().enabled = true;
    }

    void ScreenPointToRayCalc(GameObject wall)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 2.5f))
        {
            if (hit.transform.gameObject == wall)
            {
                timeForDrag += Time.deltaTime;
                if (timeForDrag > limitForNewDragPosition)
                {
                    positions.Add(hit.point);
                    if (positions.Count > 5)
                    {
                        positions.RemoveAt(0);
                    }
                    timeForDrag = 0;
                }
                target.transform.position = hit.point;
            }
        }
    }

    public void ResetEverything()
    {
        GetComponent<FloatingTextManagement>().DeleteAllText();
        GetComponent<LEDManager>().ResetPointsText();
        GetComponent<WindManager>().ResetValues();
        GetComponent<ZombieManager>().ResetValues();
        GetComponent<Gameplay>().ResetValues();
        leftFryer.GetComponent<FryerBasket>().Restart();
        rightFryer.GetComponent<FryerBasket>().Restart();
        GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
        GameObject[] onPlatter = GameObject.FindGameObjectsWithTag("OnPlatter");
        GameObject[] fallen = GameObject.FindGameObjectsWithTag("Fallen");
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        GameObject[] grillIngredients = GameObject.FindGameObjectsWithTag("GrillIngredientClone");
        GameObject[] fries = GameObject.FindGameObjectsWithTag("Fries");
        GameObject[] baskets = GameObject.FindGameObjectsWithTag("Basket");
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Soda");
        GameObject[] lids = GameObject.FindGameObjectsWithTag("Lid");
        DestroyArrayOfObjects(ingredients);
        DestroyArrayOfObjects(onPlatter);
        DestroyArrayOfObjects(fallen);
        DestroyArrayOfObjects(clones);
        DestroyArrayOfObjects(cars);
        DestroyArrayOfObjects(grillIngredients);
        DestroyArrayOfObjects(fries);
        DestroyArrayOfObjects(baskets);
        DestroyArrayOfObjects(cups);
        DestroyArrayOfObjects(lids);
        if (sodaFountain1.GetComponent<SodaMachine>() != null)
        {
            sodaFountain1.GetComponent<SodaMachine>().Restart();
        }
        if (sodaFountain2.GetComponent<SodaMachine>() != null)
        {
            sodaFountain2.GetComponent<SodaMachine>().Restart();
        }
        if (sodaFountain3.GetComponent<SodaMachine>() != null)
        {
            sodaFountain3.GetComponent<SodaMachine>().Restart();
        }
        if (GetComponent<Poisoned>() != null)
        {
            GetComponent<Poisoned>().DestroyPoison();
        }
        if (GetComponent<Frozen>() != null)
        {
            GetComponent<Frozen>().DestroyFrozen();
        }
    }

    void DestroyArrayOfObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    public void SetFrozen(bool b)
    {
        frozen = b;
    }
}
