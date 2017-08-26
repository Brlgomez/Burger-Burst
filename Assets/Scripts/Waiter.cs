using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour {

    Transform start;
    Transform end;
    List<GameObject> tablePositions = new List<GameObject>();
    List<GameObject> startPositions = new List<GameObject>();
    Transform current;
    NavMeshAgent agent;

	void Start () {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Table");
        for (int i = 0; i < temp.Length; i++)
        {
            tablePositions.Add(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Start");
        for (int i = 0; i < temp.Length; i++)
        {
            startPositions.Add(temp[i]);
        }
        agent = GetComponent<NavMeshAgent>();
        StartPosition();
	}

    void Update () {
        if (current == start)
        {
            if (agent.velocity.magnitude < 0.1f)
            {
                Vector3 lookPos = Camera.main.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
            }
        } 
        else if (current == end)
        {
            if (Vector3.Distance(transform.position, end.position) < 3.5f)
            {
                StartPosition();
                Camera.main.GetComponent<Gameplay>().SetOrder();
            }
        }
    }

    public void EndPosition() {
        //tag = "Waiter";
        end = tablePositions[Random.Range(0, tablePositions.Count)].transform;
        current = end;
        agent.destination = current.position;

    }

    public void StartPosition() {
        start = startPositions[Random.Range(0, startPositions.Count)].transform;
        current = start;
        agent.destination = current.position;
    }
}
