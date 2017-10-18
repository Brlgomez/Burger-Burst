using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    static float speed = 10;
    static int maxSpeed = 10;
    static int acceleration = 10;
    static int deceleration = 10;
    float randomStoppingPos;
    bool slowDown;
    bool slowingDown;
    bool droppedOff;

    void Start()
    {
        randomStoppingPos = Random.Range(-9f, 19f);
    }

    void Update()
    {
        RotateTires();
        Logic();
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }

    void Logic()
    {
        if (Mathf.Abs(randomStoppingPos - transform.position.x) < 1 && speed > 0)
        {
            slowingDown = true;

        }
        if (slowingDown)
        {
            speed -= Time.deltaTime * deceleration;
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
            speed += Time.deltaTime * acceleration;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
    }

    void AddZombie()
    {
        Camera.main.GetComponent<ZombieManager>().AddNewWaiter(new Vector3(transform.position.x, 0, transform.position.z + 4));
        droppedOff = true;
    }

    void RotateTires()
    {
        transform.GetChild(0).RotateAround(
            transform.GetChild(0).transform.position,
            -transform.right, Time.deltaTime * speed * 100
        );
        transform.GetChild(1).RotateAround(
            transform.GetChild(1).transform.position,
            -transform.right, Time.deltaTime * speed * 100
        );
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "End")
        {
            Destroy(gameObject);
        }
    }
}
