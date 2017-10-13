using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonZombieCar : MonoBehaviour
{
    static float speed = 10;
    static int maxSpeed = 10;
    static int acceleration = 10;
    static int deceleration = 10;
    bool slowDown;
    bool slowingDown;

    void Update()
    {
        RotateTires();
        Logic();
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }

    void Logic()
    {
        if (slowDown && speed > 0)
        {
            speed -= Time.deltaTime * deceleration;
            if (speed < 0)
            {
                speed = 0;
                slowDown = false;
            }
        }
        else if (!slowDown && speed < maxSpeed)
        {
            speed += Time.deltaTime * acceleration;
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
            -transform.right, Time.deltaTime * speed * 100
        );
        transform.GetChild(1).RotateAround(
            transform.GetChild(1).transform.position,
            -transform.right, Time.deltaTime * speed * 100
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
