using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    static int updateInterval = 30;

    GameObject target, counterWall, grillWall, fryerWall, sodaWall, phone;
    GameObject rightFryer, leftFryer, sodaFountain1, sodaFountain2, sodaFountain3;
    List<Vector3> positions = new List<Vector3>();
    Vector3 direction;
    float timeForDrag;
    static float limitForNewDragPosition = 0.01f;
    public enum Area { counter, pause, gameOver, quit, grill, fryer, sodaMachine };
    public Area currentArea;
    public Area previousArea;
    int throwingDistance = 15;
    bool frozen;
    bool gameOver, paused;

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

        if (Input.GetKeyDown("space"))
            GetComponent<ZombieManager>().Print();
        if (Input.GetKeyDown("d"))
        {
            GetComponent<Gameplay>().ReduceHealth(10, gameObject);
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
            if (!paused && !gameOver && obj.GetComponent<FadeObject>() == null)
            {
                TurnOffPhoneColliders();
                TurnOffSodaButtonColliders();
                if (obj.tag == "Pause" && gameObject.GetComponent<CameraMovement>() == null)
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.grey;
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
        if (target != null && !paused && !gameOver)
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
                GetComponent<GameplayMenu>().PhoneInterface(target);
                GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            }
            if (!paused && !gameOver)
            {
                GetComponent<GameplayMenu>().MouseUpPauseButton(target);
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
                Vector3 diff = positions[positions.Count - 1] - positions[0];
                float speed = Vector3.Distance(positions[positions.Count - 1], positions[0]);
                float xVelocity = (diff.x * 10) + ((target.transform.position.x * speed * 2));
                float yVelocity = diff.y * 3;
                float zVelocity = diff.z * 15;
                Vector3 newVelocity = new Vector3(xVelocity, yVelocity, zVelocity) * ((Screen.height / Screen.dpi) * 0.5f);
                if (newVelocity.magnitude > (throwingDistance * 0.66f))
                {
                    newVelocity *= ((throwingDistance * 0.66f) / newVelocity.magnitude);
                }
                target.GetComponent<Rigidbody>().velocity = newVelocity;
                target.GetComponent<Rigidbody>().angularVelocity = new Vector3(zVelocity, xVelocity, 0);
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

    public void DeleteEverything()
    {
        GetComponent<ZombieManager>().ResetValues();
        GetComponent<FloatingTextManagement>().DeleteAllText();
        GetComponent<WindManager>().ResetValues();
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
            sodaFountain1.GetComponent<SodaMachine>().TurnOff();
        }
        if (sodaFountain2.GetComponent<SodaMachine>() != null)
        {
            sodaFountain2.GetComponent<SodaMachine>().TurnOff();
        }
        if (sodaFountain3.GetComponent<SodaMachine>() != null)
        {
            sodaFountain3.GetComponent<SodaMachine>().TurnOff();
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

    public void SetGameOver(bool b)
    {
        gameOver = b;
    }

    public void SetPause(bool b)
    {
        paused = b;
    }
}
