using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour {

    public float lifetime = 3;

	void Start () 
    {
	    
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Building")
        {
            Destroy(gameObject, lifetime);
        }
    }
}
