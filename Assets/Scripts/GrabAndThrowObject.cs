﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    GameObject target;
    GameObject counterWall, grillWall, fryerWall, sodaWall;
    GameObject rightFryer, leftFryer, sodaFountain1, sodaFountain2, sodaFountain3;
    List<Vector3> positions = new List<Vector3>();
    Vector3 direction;
    GameObject phone;
    Transform initialPosition;
    bool paused;
    float timeForNewPerson = 12;
    float newPersonTime;
    int maxAmountOfPeople = 10;
    enum Area { counter, pause, gameOver, quit, grill, fryer, sodaMachine };
    Area currentArea;

    void Start()
    {
        newPersonTime = timeForNewPerson;
        counterWall = GameObject.Find("Counter Wall");
        grillWall = GameObject.Find("Grill Wall");
        rightFryer = GameObject.Find("Fryer Basket Right");
        leftFryer = GameObject.Find("Fryer Basket Left");
        fryerWall = GameObject.Find("Fryer Wall");
        sodaWall = GameObject.Find("Soda Wall");
        phone = GameObject.Find("Phone");
        sodaFountain1 = GameObject.Find("SodaFromMachine1");
        sodaFountain2 = GameObject.Find("SodaFromMachine2");
        sodaFountain3 = GameObject.Find("SodaFromMachine3");
        initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
        currentArea = Area.counter;
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
        AddMorePeople();
    }

    void MouseDown()
    {
        positions.Clear();
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
        if (Physics.Raycast(ray.origin, ray.direction * 2, out hit) && gameObject.GetComponent<CameraMovement>() == null)
        {
            obj = hit.collider.gameObject;
            if (obj.tag == "UI")
            {
                Camera.main.GetComponent<ScreenTextManagment>().PressTextDown(obj.transform.parent.gameObject);
                return obj;
            }
            if (!paused && !Camera.main.GetComponent<Gameplay>().IsGameOver())
            {
                TurnOffPhoneColliders();
                TurnOffSodaButtonColliders();
				if (currentArea == Area.counter && obj.tag == "Pause")
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
        if (target != null && !paused && !Camera.main.GetComponent<Gameplay>().IsGameOver())
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
                Camera.main.GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            }
            if (!paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
            {
                if (currentArea == Area.counter)
                {
                    MouseUpPauseButton();
                }
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
        else if (gameObject.GetComponent<Gameplay>().IsGameOver())
        {
            GameOverPhoneInterface(obj);
        }
        else
        {
            PauseMenuInterface(obj);
        }
    }

    void GameplayPhoneInterface(GameObject obj)
    {
        switch (obj.name)
        {
            case "Second Button":
                currentArea = Area.grill;
                initialPosition = Camera.main.GetComponent<PositionManager>().GrillPosition();
                gameObject.AddComponent<CameraMovement>().MoveToGrill();
                break;
            case "Third Button":
                currentArea = Area.fryer;
                initialPosition = Camera.main.GetComponent<PositionManager>().FryerPosition();
                gameObject.AddComponent<CameraMovement>().MoveToFryer();
                break;
            case "Fourth Button":
                currentArea = Area.sodaMachine;
                initialPosition = Camera.main.GetComponent<PositionManager>().SodaPosition();
                gameObject.AddComponent<CameraMovement>().MoveToSodaMachine();
                break;
            case "Fifth Button":
                currentArea = Area.counter;
                initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
                gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
                break;
        }
    }

    void GameOverPhoneInterface(GameObject obj)
    {
        if (obj.name == "Third Button")
        {
            Restart();
        }
        else if (obj.name == "Fourth Button")
        {
            Quit();
        }
    }

    void PauseMenuInterface(GameObject obj)
    {
        if (paused)
        {
            if (obj.name == "Second Button" && !gameObject.GetComponent<Gameplay>().IsGameOver())
            {
                currentArea = Area.counter;
                initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
                gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
            }
            else if (obj.name == "Third Button")
            {
                Restart();
            }
            else if (obj.name == "Fourth Button")
            {
                Quit();
            }
        }
    }

    GameObject GetCounterObject(GameObject obj)
    {
        if (obj.tag == "Ingredient")
        {
            return obj;
        }
        return null;
    }

    GameObject GetGrillObject(GameObject obj)
    {
        if (obj.tag == "GrillIngredientClone")
        {
            return obj;
        }
        return null;
    }

    GameObject GetFryerObject(GameObject obj)
    {
        if (obj.name == "Right Fryer Button" || obj.name == "Left Fryer Button" || obj.tag == "Fries")
        {
            return obj;
        }
        return null;
    }

    GameObject GetSodaMachineObject(GameObject obj)
    {
        if (obj.tag == "Soda" || obj.tag == "Lid" || obj.name == "Soda Button")
        {
            return obj;
        }
        return null;
    }

    void MouseDownCounter()
    {
        counterWall.GetComponent<Collider>().enabled = false;
        if (target.tag == "Ingredient")
        {
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            counterWall.GetComponent<Collider>().enabled = true;
        }
    }

    void MouseDownGrill()
    {
        grillWall.GetComponent<Collider>().enabled = false;
        if (target.tag == "GrillIngredientClone")
        {
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            grillWall.GetComponent<Collider>().enabled = true;
            if (target.GetComponent<CookMeat>())
            {
                target.GetComponent<CookMeat>().PickedUp();
            }
        }
    }

    void MouseDownFryer()
    {
        fryerWall.GetComponent<Collider>().enabled = false;
        if (target.tag == "Fries")
        {
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            fryerWall.GetComponent<Collider>().enabled = true;
        }
    }

    void MouseDownSodaMachine()
    {
        sodaWall.GetComponent<Collider>().enabled = false;
        if (target.tag == "Soda" || target.tag == "Lid")
        {
            target.transform.rotation = new Quaternion(0, 0, 0, 0);
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            sodaWall.GetComponent<Collider>().enabled = true;
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
        if (target.tag == "Fries")
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
        if (target.name == "Pause Button")
        {
            currentArea = Area.pause;
            initialPosition = Camera.main.GetComponent<PositionManager>().PausePosition();
            if (GetComponent<GettingHurt>() != null)
            {
                Destroy(GetComponent<GettingHurt>());
            }
            target.GetComponent<Animator>().Play("ButtonClick");
            gameObject.AddComponent<CameraMovement>().MoveToPause();
            PauseGame();
        }
    }

    void MouseUpCounter()
    {
        if (target.tag == "Ingredient")
        {
            target.GetComponent<Rigidbody>().freezeRotation = false;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            target.AddComponent<RemoveObjects>();
            counterWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                float speed = Vector3.Distance(positions[positions.Count - 1], positions[0]);
                float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 7) + ((target.transform.position.x) * speed * 3);
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 3;
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 20;
                target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
            }
        }
    }

    void MouseUpGrill()
    {
        if (target.tag == "GrillIngredientClone")
        {
            target.GetComponent<Rigidbody>().freezeRotation = false;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            grillWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 5);
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 5;
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 5;
                target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
            }
            if (target.GetComponent<CookMeat>() == null && target.name == "Meat(Clone)")
            {
                target.AddComponent<CookMeat>();
            }
            if (target.GetComponent<RemoveObjects>() == null)
            {
                target.AddComponent<RemoveObjects>();
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
        if (target.tag == "Fries")
        {
            target.GetComponent<Rigidbody>().freezeRotation = false;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            fryerWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 5);
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 5;
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 5;
                target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
            }
            if (target.GetComponent<FryFries>() == null)
            {
                target.AddComponent<FryFries>();
            }
            if (target.GetComponent<RemoveObjects>() == null)
            {
                target.AddComponent<RemoveObjects>();
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
            target.GetComponent<Collider>().enabled = true;
            sodaWall.GetComponent<Collider>().enabled = false;
            if (positions.Count > 1)
            {
                float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 5);
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 5;
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 5;
                target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
            }
            if (target.GetComponent<RemoveObjects>() == null)
            {
                target.AddComponent<RemoveObjects>();
            }
            if (target.GetComponent<SodaCup>() == null && target.tag == "Soda")
            {
                target.AddComponent<SodaCup>();
            }
        }
    }

    void Restart()
    {
        currentArea = Area.counter;
        initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        DeleteObjects();
        Destroy(GetComponent<Gameplay>());
        gameObject.AddComponent<Gameplay>();
        gameObject.AddComponent<CameraMovement>().MoveToGameplay("Restart");
        RestartValues();
    }

    void Quit()
    {
        currentArea = Area.quit;
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        DeleteObjects();
        Destroy(GetComponent<Gameplay>());
        UnPauseGame();
        gameObject.AddComponent<CameraMovement>().MoveToMenu();
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

    void RestartValues()
    {
        newPersonTime = timeForNewPerson;
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
            phone.transform.GetChild(i).transform.GetChild(0).GetComponent<Collider>().enabled = false;
        }
    }

    void TurnOnPhoneColliders()
    {
        phone.GetComponent<Collider>().enabled = true;
        for (int i = 0; i < phone.transform.childCount; i++)
        {
            phone.transform.GetChild(i).transform.GetChild(0).GetComponent<Collider>().enabled = true;
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

    void AddMorePeople()
    {
        if (!paused && !Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            newPersonTime += Time.deltaTime;
            if (newPersonTime > timeForNewPerson)
            {
                if (Camera.main.GetComponent<WaiterManager>().GetCount() < maxAmountOfPeople)
                {
                    Camera.main.GetComponent<CarManager>().CreateNewCarWithZombie();
                    if (Random.value < 0.25f)
                    {
                        Camera.main.GetComponent<CarManager>().CreateNewCarWithNoZombie();
                    }
                    newPersonTime = 0;
                }
            }
        }
    }

    void ScreenPointToRayCalc(GameObject wall)
    {
        wall.GetComponent<Collider>().enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction * 2, out hit))
        {
            if (hit.transform.gameObject == wall)
            {
                positions.Add(hit.point);
                if (positions.Count > 9)
                {
                    positions.RemoveAt(0);
                }
                target.transform.position = hit.point;
            }
        }
    }

    public void DeleteObjects()
    {
        GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
        GameObject[] onPlatter = GameObject.FindGameObjectsWithTag("OnPlatter");
        GameObject[] fallen = GameObject.FindGameObjectsWithTag("Fallen");
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        GameObject[] grillIngredients = GameObject.FindGameObjectsWithTag("GrillIngredientClone");
        GameObject[] fries = GameObject.FindGameObjectsWithTag("Fries");
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Soda");
        GameObject[] lids = GameObject.FindGameObjectsWithTag("Lid");
        DestroyArrayOfObjects(ingredients);
        DestroyArrayOfObjects(onPlatter);
        DestroyArrayOfObjects(fallen);
        DestroyArrayOfObjects(clones);
        DestroyArrayOfObjects(cars);
        DestroyArrayOfObjects(grillIngredients);
        DestroyArrayOfObjects(fries);
        DestroyArrayOfObjects(cups);
        DestroyArrayOfObjects(lids);
        Camera.main.GetComponent<FloatingTextManagement>().DeleteAllText();
        leftFryer.GetComponent<FryerBasket>().Restart();
        rightFryer.GetComponent<FryerBasket>().Restart();
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
    }

    void DestroyArrayOfObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}
