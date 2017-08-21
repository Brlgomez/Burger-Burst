using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class GrabAndThrowObject : MonoBehaviour
{
    Vector3 velocity;
    GameObject target;
    GameObject wall;
    List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        wall = GameObject.FindGameObjectWithTag("Wall");
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown();
        } 
        if (Input.GetMouseButton(0)) 
        {
            mouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseUp();
        }
    }

    void mouseDown()
    {
        positions.Clear();
        RaycastHit hitInfo;
        target = returnClickedObject(out hitInfo);
        if (target != null)
        {
            Physics.IgnoreCollision(wall.GetComponent<Collider>(), target.GetComponent<Collider>());
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            wall.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void mouseDrag()
    {
        if (target != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                if (hit.transform.tag.Equals("Wall"))
                {
                    positions.Add(hit.point);
                    if (positions.Count > 9)
                    {
                        positions.RemoveAt(0);
                    }
                    target.transform.position = hit.point;
                }
            }
        }
    }

    void mouseUp()
    {
        if (target != null)
        {
            if (positions.Count > 1)
            {
                float xVelocity = (positions[positions.Count - 1].x - positions[0].x) * 2;
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y);
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 5;
                target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
            }
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
        }
        wall.GetComponent<BoxCollider>().enabled = false;
        target = null;
    }

    GameObject returnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            if (target.tag.Equals("Ingredient"))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
