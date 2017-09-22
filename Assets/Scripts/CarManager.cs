using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

    GameObject car;

    void Start () 
    {
        car = GameObject.Find("Car");
    }

    public void CreateNewCar () 
    {
        GameObject newCar = Instantiate(car);
        newCar.AddComponent<Car>();
    }
}
