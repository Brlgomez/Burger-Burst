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
            transform.position = Vector3.MoveTowards(transform.position, dropOffPosition, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, dropOffPosition) < 0.01f)
            {
                dropOff = false;
                Camera.main.GetComponent<WaiterManager>().AddNewWaiter();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leavePosition, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, leavePosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
	}
}
