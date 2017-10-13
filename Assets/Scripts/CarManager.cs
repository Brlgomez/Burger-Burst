using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    GameObject car;

    void Start()
    {
        car = GetComponent<ObjectManager>().Car();
    }

    public void CreateNewCarWithZombie()
    {
        int materialIndex = Random.Range(0, Camera.main.GetComponent<Materials>().cars.Length);
        GameObject newCar = Instantiate(car);
        newCar.AddComponent<Car>();
        newCar.tag = "Car";
        newCar.GetComponentInChildren<Renderer>().material = Camera.main.GetComponent<Materials>().cars[materialIndex];
        newCar.transform.position = new Vector3(38, 0, 20);
        newCar.transform.Rotate(new Vector3(0, -90, 0));
    }

    public void CreateNewCarWithNoZombie()
    {
        int materialIndex = Random.Range(0, Camera.main.GetComponent<Materials>().cars.Length);
        GameObject newCar = Instantiate(car);
        newCar.AddComponent<NonZombieCar>();
        newCar.tag = "Car";
        newCar.GetComponentInChildren<Renderer>().material = Camera.main.GetComponent<Materials>().cars[materialIndex];
        if (Random.value > 0.5f)
        {
            newCar.transform.position = new Vector3(20, 0, -40);
            newCar.transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            newCar.transform.position = new Vector3(-20, 0, 40);
            newCar.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
