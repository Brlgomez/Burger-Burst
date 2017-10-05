using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    Vector3 velocity;
    GameObject target;
    GameObject counterWall, grillWall;
    List<Vector3> positions = new List<Vector3>();
    Vector3 direction;
    GameObject[] ingredients;
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
        ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
        phone = GameObject.Find("Phone");
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
            if (currentArea == Area.counter && target.tag == "Ingredient")
            {
                MouseDownCounter();
            }
            if (currentArea == Area.grill && (target.tag == "GrillIngredient" || target.tag == "GrillIngredientClone"))
            {
                MouseDownGrill();
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
            if (obj.tag == "Pause" && currentArea == Area.counter && !Camera.main.GetComponent<Gameplay>().IsGameOver())
            {
                return obj;
            }
            if (obj.tag == "UI")
            {
                Camera.main.GetComponent<ScreenTextManagment>().PressTextDown(obj.transform.parent.gameObject);
                return obj;
            }
            if (currentArea == Area.counter && !paused && !Camera.main.GetComponent<Gameplay>().IsGameOver() && obj.tag == "Ingredient")
            {
                return GetCounterObject(obj);
            }
            if (currentArea == Area.grill && !paused && !Camera.main.GetComponent<Gameplay>().IsGameOver())
            {
                return GetGrillObject(obj);
            }
        }
        return null;
    }

    void MouseDrag()
    {
        if (target != null && !paused)
        {
            if (currentArea == Area.counter && target.tag == "Ingredient")
            {
                DragCounterObject();
            }
            if (currentArea == Area.grill && (target.tag == "GrillIngredient" || target.tag == "GrillIngredientClone"))
            {
                DragGrillObject();
            }
        }
    }

    void MouseUp()
    {
        if (target != null)
        {
            TurnOnPhoneColliders();
            if (target.tag == "UI")
            {
                PhoneInterface(target);
                Camera.main.GetComponent<ScreenTextManagment>().PressTextUp(target.transform.parent.gameObject);
            }
            if (!paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
            {
                if (currentArea == Area.counter && target.name == "Pause Button")
                {
                    MouseUpPauseButton();
                }
                if (currentArea == Area.counter && target.tag == "Ingredient")
                {
                    MouseUpCounter();
                }
                if (currentArea == Area.grill && (target.tag == "GrillIngredient" || target.tag == "GrillIngredientClone"))
                {
                    MouseUpGrill();
                }
            }
        }
        foreach (GameObject obj in ingredients)
        {
            obj.GetComponent<BoxCollider>().enabled = true;
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
        if (obj.name == "Second Button")
		{
			currentArea = Area.grill;
			initialPosition = Camera.main.GetComponent<PositionManager>().GrillPosition();
			gameObject.AddComponent<CameraMovement>().MoveToGrill();
		}
		else if (obj.name == "Third Button")
		{
			currentArea = Area.fryer;
			initialPosition = Camera.main.GetComponent<PositionManager>().FryerPosition();
			gameObject.AddComponent<CameraMovement>().MoveToFryer();
		}
		else if (obj.name == "Fourth Button")
		{
			currentArea = Area.sodaMachine;
			initialPosition = Camera.main.GetComponent<PositionManager>().SodaPosition();
			gameObject.AddComponent<CameraMovement>().MoveToSodaMachine();
		}
		else if (obj.name == "Fifth Button")
		{
			currentArea = Area.counter;
			initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
			gameObject.AddComponent<CameraMovement>().MoveToGameplay("Unpause");
		}
    }

    void GameOverPhoneInterface (GameObject obj)
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

    void PauseMenuInterface (GameObject obj) 
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

    void MouseDownCounter()
    {
        Physics.IgnoreCollision(counterWall.GetComponent<Collider>(), target.GetComponent<Collider>());
        target.GetComponent<Collider>().enabled = false;
        target.GetComponent<Collider>().isTrigger = false;
        target.GetComponent<BoxCollider>().enabled = false;
        target.GetComponent<Rigidbody>().isKinematic = false;
        target.GetComponent<Rigidbody>().useGravity = false;
        counterWall.GetComponent<Collider>().enabled = true;
        foreach (GameObject obj in ingredients)
        {
            obj.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void MouseDownGrill()
    {
        Physics.IgnoreCollision(grillWall.GetComponent<Collider>(), target.GetComponent<Collider>());
        target.GetComponent<Collider>().enabled = false;
        target.GetComponent<Collider>().isTrigger = false;
        target.GetComponent<Rigidbody>().isKinematic = false;
        target.GetComponent<Rigidbody>().useGravity = false;
        grillWall.GetComponent<Collider>().enabled = true;
    }

    GameObject GetCounterObject(GameObject obj)
    {
        if (obj.name == "Burger" && Camera.main.GetComponent<Gameplay>().GetBurgerCount() < 1)
        {
            return null;
        }
        if (obj.name == "Fries" && Camera.main.GetComponent<Gameplay>().GetFriesCount() < 1)
        {
            return null;
        }
        if (obj.name == "Drink" && Camera.main.GetComponent<Gameplay>().GetDrinkCount() < 1)
        {
            return null;
        }
        TurnOffPhoneColliders();
        GameObject newIngredient = Instantiate(obj);
        return newIngredient;
    }

    GameObject GetGrillObject(GameObject obj)
    {
        if (obj.tag == "GrillIngredient")
        {
            TurnOffPhoneColliders();
            GameObject newIngredient = Instantiate(obj);
            return newIngredient;
        }
        if (obj.tag == "GrillIngredientClone")
        {
            TurnOffPhoneColliders();
            return obj;
        }
        return null;
    }

    void DragCounterObject()
    {
        counterWall.GetComponent<Collider>().enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction * 1, out hit))
        {
            if (hit.transform.gameObject == counterWall)
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

    void DragGrillObject()
    {
        grillWall.GetComponent<Collider>().enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction * 1, out hit))
        {
            if (hit.transform.gameObject == grillWall)
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

    void MouseUpPauseButton()
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

    void MouseUpCounter()
    {
        if (positions.Count > 1)
        {
            float speed = Vector3.Distance(positions[positions.Count - 1], positions[0]);
            float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 7) + ((target.transform.position.x) * speed * 3);
            float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 3;
            float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 20;
            target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
        }
        target.GetComponent<Rigidbody>().useGravity = true;
        target.GetComponent<Collider>().enabled = true;
        target.tag = "Thrown";
        target.AddComponent<RemoveObjects>();
        switch (target.name)
        {
            case "Burger(Clone)":
                Camera.main.GetComponent<Gameplay>().ReduceBurgers();
                break;
            case "Drink(Clone)":
                Camera.main.GetComponent<Gameplay>().ReduceDrinks();
                break;
            case "Fries(Clone)":
                Camera.main.GetComponent<Gameplay>().ReduceFries();
                break;
        }
        Destroy(target.GetComponent<BoxCollider>());
        counterWall.GetComponent<Collider>().enabled = false;
    }

    void MouseUpGrill()
    {
        if (positions.Count > 1)
        {
            float xVelocity = ((positions[positions.Count - 1].x - positions[0].x) * 5);
            float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 5;
            float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 5;
            target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
        }
        target.tag = "GrillIngredientClone";
        target.GetComponent<Rigidbody>().useGravity = true;
        target.GetComponent<Collider>().enabled = true;
        target.AddComponent<RemoveObjects>();
        grillWall.GetComponent<Collider>().enabled = false;
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

    public void DeleteObjects()
    {
        GameObject[] thrown = GameObject.FindGameObjectsWithTag("Thrown");
        GameObject[] onPlatter = GameObject.FindGameObjectsWithTag("OnPlatter");
        GameObject[] fallen = GameObject.FindGameObjectsWithTag("Fallen");
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        GameObject[] grillIngredients = GameObject.FindGameObjectsWithTag("GrillIngredientClone");
        foreach (GameObject obj in thrown)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in onPlatter)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in fallen)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in clones)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in cars)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in grillIngredients)
        {
            Destroy(obj);
        }
        Camera.main.GetComponent<FloatingTextManagement>().DeleteAllText();
    }
}
