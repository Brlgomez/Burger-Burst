using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerUp : MonoBehaviour
{
    static float magnetDistance = 7.5f;
    static float searchTimeMax = 0.1f;
    float searchTimer;
    float magnetPower;
    GameObject target = null;

    void Start()
    {
        searchTimer = searchTimeMax;
    }

    void FixedUpdate()
    {
        searchTimer += Time.deltaTime;
        if (searchTimer > searchTimeMax)
        {
            target = SearchNearestTarget();
			if (target != null)
			{
				Vector3 direction = (target.transform.position - transform.position).normalized * magnetPower;
				GetComponent<Rigidbody>().velocity += direction;
			}
            searchTimer = 0;
        }
    }

    GameObject SearchNearestTarget()
    {
        float closestObject = Mathf.Infinity;
        GameObject nearest = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetDistance);
        foreach (Collider hit in hitColliders)
        {
            if (hit.tag == "Waiter" && (hit.name == "Upper_Body" || hit.name == "Lower_Body" || hit.name == "Head"))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < closestObject)
                {
                    closestObject = Vector3.Distance(transform.position, hit.transform.position);
                    magnetPower = (magnetDistance - closestObject);
                    nearest = hit.gameObject;
                }
            }
        }
        return nearest;
    }
}
