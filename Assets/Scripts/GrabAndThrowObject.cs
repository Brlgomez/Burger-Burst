using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    Vector3 velocity;
    GameObject target;
    GameObject invisibleWall;
    List<Vector3> positions = new List<Vector3>();
    float deltaTime;
    float timeForPositions = 0.01f;

    void Start()
    {
        invisibleWall = GameObject.FindGameObjectWithTag("Wall");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        } 
        if (Input.GetMouseButton(0))
        {
            MouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    void MouseDown()
    {
        positions.Clear();
        RaycastHit hitInfo;
        target = ReturnClickedObject(out hitInfo);
        if (target != null)
        {
            Physics.IgnoreCollision(invisibleWall.GetComponent<Collider>(), target.GetComponent<Collider>());
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            invisibleWall.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void MouseDrag()
    {
        if (target != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                if (hit.transform.tag.Equals("Wall"))
                {
                    deltaTime += Time.deltaTime;
                    if (deltaTime > timeForPositions)
                    {
                        positions.Add(hit.point);
                        if (positions.Count > 9)
                        {
                            positions.RemoveAt(0);
                        }
                        deltaTime = 0;
                    }
                    target.transform.position = hit.point;
                }
            }
        }
    }

    void MouseUp()
    {
        if (target != null)
        {
            if (positions.Count > 1)
            {
                float xVelocity = (positions[positions.Count - 1].x - positions[0].x) * 2;
                float yVelocity = (positions[positions.Count - 1].y - positions[0].y) * 0.75f;
                float zVelocity = (positions[positions.Count - 1].z - positions[0].z) * 10;
                target.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
            }
            target.GetComponent<Rigidbody>().useGravity = true;
            target.GetComponent<Collider>().enabled = true;
            target.tag = "Thrown";
            target.AddComponent<RemoveObjects>();
        }
        invisibleWall.GetComponent<BoxCollider>().enabled = false;
        target = null;
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            if (target.tag.Equals("Ingredient"))
            {
                GameObject newIngredient = Instantiate(hit.collider.gameObject);
                return newIngredient;
            }
        }
        return null;
    }
}
