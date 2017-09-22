using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    Vector3 dropOffPosition;
    Vector3 leavePosition;
    float speed = 10;
    bool dropOff = true;

	void Start () 
    {
        dropOffPosition = new Vector3(0, 0, 18);
        leavePosition = new Vector3(-25, 0, 18);
	}
	
	void Update ()
    {
        if (dropOff)
        {
            RotateTires();
            transform.position = Vector3.MoveTowards(transform.position, dropOffPosition, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, dropOffPosition) < 0.01f)
            {
                dropOff = false;
                Camera.main.GetComponent<WaiterManager>().AddNewWaiter();
            }
        }
        else
        {
            RotateTires();
            transform.position = Vector3.MoveTowards(transform.position, leavePosition, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, leavePosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
	}

    void RotateTires () 
    {
        transform.GetChild(1).RotateAround(transform.GetChild(1).transform.position, Vector3.forward, Time.deltaTime * speed * 25);
        transform.GetChild(2).RotateAround(transform.GetChild(2).transform.position, Vector3.forward, Time.deltaTime * speed * 25);
    }
}
