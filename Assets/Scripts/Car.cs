using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Vector3 startPosition;
    float speed = 10;
    bool dropOffZombie = false;
    bool slowDown = false;
    float randomPos;
    bool slowingDown = false;

    void Start()
    {
        startPosition = transform.position;
        randomPos = Random.Range(-15f, 15f);
    }

    void Update()
    {
        if (!dropOffZombie)
        {
            NonZombieLogic();
        }
        else 
        {
            ZombieLogic();
        }
        RotateTires();
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, startPosition) > 80)
        {
            Destroy(gameObject);
        }
    }

    void ZombieLogic () 
    {
        if (Mathf.Abs(randomPos - transform.position.x) < 1 && speed > 0)
		{
            slowingDown = true;

		}
        if (slowingDown) 
        {
			speed -= Time.deltaTime * 10;
			if (speed < 0)
			{
				speed = 0;
				slowDown = false;
                slowingDown = false;
				dropOffZombie = false;
                if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
                {
                    AddZombie();
                }
			}
        }
	}

    void NonZombieLogic () 
    {
		if (slowDown && speed > 0)
		{
			speed -= Time.deltaTime * 10;
			if (speed < 0)
			{
				speed = 0;
				slowDown = false;
			}
		}
		else if (!slowDown && speed < 10)
		{
			speed += Time.deltaTime * 10;
			if (speed > 10)
			{
				speed = 10;
			}
		}  
    }

    void AddZombie () 
    {
        Camera.main.GetComponent<WaiterManager>().AddNewWaiter(new Vector3(transform.position.x, 0, transform.position.z + 4));
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
