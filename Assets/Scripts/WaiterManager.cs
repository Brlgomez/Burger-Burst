using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterManager : MonoBehaviour {

    GameObject waiter;

	void Start () {
        waiter = GameObject.Find("Waiter");
	}
	
    public void AddNewWaiter (){
        GameObject newWaiter = Instantiate(waiter);
        newWaiter.AddComponent<Waiter>();
        newWaiter.tag = "Clone";
    }
}
