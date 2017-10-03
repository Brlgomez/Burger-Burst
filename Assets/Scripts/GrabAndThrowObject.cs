using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    Vector3 velocity;
    GameObject target;
    GameObject invisibleWall;
    List<Vector3> positions = new List<Vector3>();
    Vector3 direction;
	GameObject[] ingredients;
    Transform initialPosition;
    bool paused;
    float timeForNewPerson = 12;
    float newPersonTime;
    int maxAmountOfPeople = 10;

    void Start()
    {
        newPersonTime = timeForNewPerson;
        invisibleWall = GameObject.FindGameObjectWithTag("Wall");
        ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
        initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
    }

    void Update()
    {
        AddMorePeople();
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

    void MouseDown()
    {
        positions.Clear();
        RaycastHit hitInfo;
        target = ReturnClickedObject(out hitInfo);
        if (target != null)
        {
            Physics.IgnoreCollision(invisibleWall.GetComponent<Collider>(), target.GetComponent<Collider>());
            target.GetComponent<Collider>().enabled = false;
			target.GetComponent<Collider>().isTrigger = false;
			target.GetComponent<BoxCollider>().enabled = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            invisibleWall.GetComponent<Collider>().enabled = true;
            foreach (GameObject obj in ingredients)
            {
                obj.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void MouseDrag()
    {
        if (target != null)
        {
            invisibleWall.GetComponent<Collider>().enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction * 1, out hit))
            {
                if (hit.transform.tag.Equals("Wall"))
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
    }

    void MouseUp()
    {
        if (target != null)
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
                    Camera.main.GetComponent<ScreenTextManagment>().ChangeBurgerCount();
					break;
				case "Drink(Clone)":
					Camera.main.GetComponent<ScreenTextManagment>().ChangeDrinkCount();
					break;
				case "Fries(Clone)":
					Camera.main.GetComponent<ScreenTextManagment>().ChangeFriesCount();
					break;
			}
            Destroy(target.GetComponent<BoxCollider>());
        }
        invisibleWall.GetComponent<Collider>().enabled = false;
        target = null;
        foreach (GameObject obj in ingredients)
        {
            obj.GetComponent<BoxCollider>().enabled = true;
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 2, out hit))
        {
            target = hit.collider.gameObject;
            if (target.tag.Equals("Ingredient") && !paused && !Camera.main.GetComponent<Gameplay>().IsGameOver())
            {
                if (target.name == "Burger" && Camera.main.GetComponent<ScreenTextManagment>().BurgerCount() < 1)
				{
                    return null;
				}
                else if(target.name == "Fries" && Camera.main.GetComponent<ScreenTextManagment>().FriesCount() < 1)
                {
                    return null;
                }
                else if (target.name == "Drink" && Camera.main.GetComponent<ScreenTextManagment>().DrinkCount() < 1)
				{
                    return null;
				}
                GameObject newIngredient = Instantiate(hit.collider.gameObject);
                return newIngredient;
            }
            Interface(target);
        }
        return null;
    }

    void Interface (GameObject target) {
		if (gameObject.GetComponent<CameraMovement>() == null)
		{
			if (target.name == "Pause Button" && !paused)
			{
                initialPosition = Camera.main.GetComponent<PositionManager>().PausePosition();
                if (GetComponent<GettingHurt>() != null) 
                {
                    Destroy(GetComponent<GettingHurt>());
                }
				target.GetComponent<Animator>().Play("ButtonClick");
				gameObject.AddComponent<CameraMovement>().MoveToPause();
				PauseGame();
			}
			else if (target.name == "Pause Button Screen" && paused)
			{
                if (!gameObject.GetComponent<Gameplay>().IsGameOver())
                {
                    initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
					gameObject.AddComponent<CameraMovement>().MoveToGameplay();
                    Camera.main.GetComponent<ScreenTextManagment>().SetSecondScreenText("", Color.white);
                } 
                else 
                {
					initialPosition = Camera.main.GetComponent<PositionManager>().GameOverPosition();
					gameObject.AddComponent<CameraMovement>().MoveToGameOver();
                    Camera.main.GetComponent<ScreenTextManagment>().SetSecondScreenText("", Color.white);
                }
			}
			else if (target.name == "Restart Button" && paused)
			{
                initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
				transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
				DeleteObjects();
				Destroy(GetComponent<Gameplay>());
				gameObject.AddComponent<CameraMovement>().MoveToGameplay();
				RestartValues();
			}
			else if (target.name == "Quit Button" && paused)
			{
                transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
				DeleteObjects();
				Destroy(GetComponent<Gameplay>());
				UnPauseGame();
				gameObject.AddComponent<CameraMovement>().MoveToMenu();
				Destroy(GetComponent<GrabAndThrowObject>());
			}
            else if (target.name == "Second Button" && !paused && !gameObject.GetComponent<Gameplay>().IsGameOver()) {
                initialPosition = Camera.main.GetComponent<PositionManager>().GrillPosition();
				gameObject.AddComponent<CameraMovement>().MoveToGrill();
            }
			else if (target.name == "Third Button" && !paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
			{
                initialPosition = Camera.main.GetComponent<PositionManager>().FryerPosition();
				gameObject.AddComponent<CameraMovement>().MoveToFryer();
			}
			else if (target.name == "Fourth Button" && !paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
			{
                initialPosition = Camera.main.GetComponent<PositionManager>().SodaPosition();
				gameObject.AddComponent<CameraMovement>().MoveToSodaMachine();
			}
			else if (target.name == "Fifth Button" && !paused && !gameObject.GetComponent<Gameplay>().IsGameOver())
			{
                initialPosition = Camera.main.GetComponent<PositionManager>().GameplayPosition();
				gameObject.AddComponent<CameraMovement>().MoveToGameplay();
			}
		}
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

    void RestartValues () 
    {
        newPersonTime = timeForNewPerson;
    }

    void DeleteObjects()
    {
        GameObject[] thrown = GameObject.FindGameObjectsWithTag("Thrown");
        GameObject[] onPlatter = GameObject.FindGameObjectsWithTag("OnPlatter");
        GameObject[] fallen = GameObject.FindGameObjectsWithTag("Fallen");
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
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
        Camera.main.GetComponent<FloatingTextManagement>().DeleteAllText();
    }

    public Transform GetInitialPosition () 
    {
        return initialPosition;
    }
}
