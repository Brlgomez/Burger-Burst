using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonZombieCar : MonoBehaviour
{
    int updateInterval = 1;

    static float speed = 10;
    static int maxSpeed = 10;
    static int acceleration = 10;
    static int deceleration = 10;
    bool slowDown;
    bool slowingDown;
    Renderer myRenderer;

    void Start()
    {
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
                updateInterval = 30;
            }
            Logic();
            transform.Translate(-Vector3.forward * Time.deltaTime * speed * updateInterval);
        }
    }

    void Logic()
    {
        if (slowDown && speed > 0)
        {
            speed -= Time.deltaTime * deceleration * updateInterval;
            if (speed < 0)
            {
                speed = 0;
                slowDown = false;
            }
        }
        else if (!slowDown && speed < maxSpeed)
        {
            speed += Time.deltaTime * acceleration * updateInterval;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
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
        if (col.name == "SlowDown")
        {
            if (speed > 0)
            {
                slowDown = true;
            }
        }
        if (col.name == "End")
        {
            Destroy(gameObject);
        }
    }
}
