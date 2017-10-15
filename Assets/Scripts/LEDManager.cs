using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDManager : MonoBehaviour 
{
    GameObject led;

	void Start () 
    {
		led = GetComponent<ObjectManager>().LED().transform.GetChild(0).gameObject;
	}

    public void UpdateText(float number)
    {
        led.GetComponent<TextMesh>().text = "S: " + number;
    }

    public void ResetText()
    {
        led.GetComponent<TextMesh>().text = "S: 0";
    }
}
