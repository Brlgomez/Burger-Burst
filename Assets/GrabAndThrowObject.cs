using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrowObject : MonoBehaviour
{
    Vector3 velocity;
    GameObject target;

    void Start()
    {

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
        RaycastHit hitInfo;
        target = returnClickedObject(out hitInfo);
        if (target != null)
        {
            Debug.Log("Touching");
        }
    }

    void mouseUp()
    {
        target = null;
    }

    void mouseDrag(){
        if (target != null)
        {

        }
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
