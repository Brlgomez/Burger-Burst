using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    int updateInterval = 1;

    static float speed = 10;
    static int maxSpeed = 10;
    static int acceleration = 10;
    static int deceleration = 10;
    float randomStoppingPos;
    bool slowDown;
    bool slowingDown;
    bool droppedOff;
    bool multiZombies;
    Renderer myRenderer;

    void Start()
    {
        randomStoppingPos = Random.Range(-7.5f, 17.5f);
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            if (myRenderer.isVisible)
            {
                updateInterval = 1;
                RotateTires();
            }
            else
            {
                updateInterval = 10;
            }
            Logic();
            transform.Translate(-Vector3.forward * Time.deltaTime * speed * updateInterval);
        }
    }

    void Logic()
    {
        if (Mathf.Abs(randomStoppingPos - transform.position.x) < 2 && speed > 0)
        {
            slowingDown = true;
        }
        if (slowingDown)
        {
            speed -= Time.deltaTime * deceleration * updateInterval;
            if (speed < 0)
            {
                speed = 0;
                slowDown = false;
                slowingDown = false;
                if (!Camera.main.GetComponent<Gameplay>().IsGameOver() && !droppedOff)
                {
                    AddZombie();
                }
            }
        }
        else if (!slowDown && droppedOff)
        {
            speed += Time.deltaTime * acceleration * updateInterval;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
    }

    void AddZombie()
    {
        if (multiZombies)
        {
            Camera.main.GetComponent<ZombieManager>().AddNewZombie(new Vector3(transform.position.x - 1, 0, transform.position.z + 4.25f));
            Camera.main.GetComponent<ZombieManager>().AddNewZombie(new Vector3(transform.position.x + 1, 0, transform.position.z + 3.75f));
        }
        else 
        {
            Camera.main.GetComponent<ZombieManager>().AddNewZombie(new Vector3(transform.position.x, 0, transform.position.z + 4));
		}
        droppedOff = true;
    }

    void RotateTires()
    {
        transform.GetChild(0).RotateAround(
            transform.GetChild(0).transform.position,
            -transform.right, Time.deltaTime * speed * 100 * updateInterval
        );
        transform.GetChild(1).RotateAround(
            transform.GetChild(1).transform.position,
            -transform.right, Time.deltaTime * speed * 100 * updateInterval
        );
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "End")
        {
            Destroy(gameObject);
        }
    }

    public void AddMultiZombies()
    {
        multiZombies = true;
    }
}
