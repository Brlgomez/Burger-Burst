using UnityEngine;

public class CarManager : MonoBehaviour
{
    GameObject car, suv;
    public Texture[] paint;
    int numberOfZombieCars;

    void Start()
    {
        car = GetComponent<ObjectManager>().Car();
        suv = GetComponent<ObjectManager>().Suv();
    }

    public void CreateNewCarWithZombie()
    {
        GameObject newCar;
        if (Random.value > GetComponent<Gameplay>().GetChanceOfSUV())
        {
            newCar = Instantiate(car);
            newCar.AddComponent<Car>();
        }
        else
        {
            newCar = Instantiate(suv);
            newCar.AddComponent<Car>().AddMultiZombies();
            numberOfZombieCars++;
        }
        newCar.tag = "Car";
        newCar.GetComponentInChildren<Renderer>().material.mainTexture = paint[Random.Range(0, paint.Length)];
        newCar.transform.position = new Vector3(38, 0, 20);
        newCar.transform.Rotate(new Vector3(0, -90, 0));
    }

    public void CreateNewCarWithNoZombie()
    {
        GameObject newCar;
        if (Random.value > 0.5f)
        {
            newCar = Instantiate(car);
        }
        else
        {
            newCar = Instantiate(suv);
        }
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

    public int GetNumberOfZombieCars()
    {
        return numberOfZombieCars;
    }

    public void DecreaseNumberOfZombieCars()
    {
        numberOfZombieCars--;
    }

    public void ResetValues()
    {
        numberOfZombieCars = 0;
    }
}
