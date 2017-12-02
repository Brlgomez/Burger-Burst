using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    GameObject car;
    public Texture[] paint;

    void Start()
    {
        car = GetComponent<ObjectManager>().Car();
    }

    public void CreateNewCarWithZombie()
    {
        GameObject newCar = Instantiate(car);
        newCar.AddComponent<Car>();
        newCar.tag = "Car";
        newCar.GetComponentInChildren<Renderer>().material.mainTexture = paint[Random.Range(0, paint.Length)];
        newCar.transform.position = new Vector3(38, 0, 20);
        newCar.transform.Rotate(new Vector3(0, -90, 0));
    }

    public void CreateNewCarWithNoZombie()
    {
        GameObject newCar = Instantiate(car);
        newCar.AddComponent<NonZombieCar>();
        newCar.tag = "Car";
        newCar.GetComponentInChildren<Renderer>().material.mainTexture = paint[Random.Range(0, paint.Length)];
        if (Random.value > 0.5f)
        {
            newCar.transform.position = new Vector3(18.5f, 0, -40);
            newCar.transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            newCar.transform.position = new Vector3(-18.5f, 0, 40);
            newCar.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
