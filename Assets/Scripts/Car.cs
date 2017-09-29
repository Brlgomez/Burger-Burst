using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    Vector3 startPosition;
    float speed = 10;
    bool dropOff = true;
    bool dropOffZombie = false;
    bool slowDown = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (slowDown && speed > 0)
        {
            speed -= Time.deltaTime * 4.5f;
            if (speed < 0)
            {
                speed = 0;
                AddZombie();
            }
        }
        else if (!slowDown && speed < 10)
        {
            speed += Time.deltaTime * 9f;
            if (speed > 10)
            {
                speed = 10;
            }
        }
        RotateTires();
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, startPosition) > 80)
        {
            Destroy(gameObject);
        }
    }

    void AddZombie () 
    {
		if (Mathf.RoundToInt(speed) == 0 && slowDown)
		{
			if (dropOffZombie)
			{
                Camera.main.GetComponent<WaiterManager>().AddNewWaiter(new Vector3(transform.position.x, 0, transform.position.z + 4));
			}
			slowDown = false;
		} 
    }

    void RotateTires()
    {
        transform.GetChild(0).RotateAround(transform.GetChild(0).transform.position, -transform.right, Time.deltaTime * speed * 100);
        transform.GetChild(1).RotateAround(transform.GetChild(1).transform.position, -transform.right, Time.deltaTime * speed * 100);
    }

    public void MakeCarDropOffZombie()
    {
        dropOffZombie = true;
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
    }
}
