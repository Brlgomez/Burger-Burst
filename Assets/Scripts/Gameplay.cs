using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

    int numberOfLostProducts = 0;
    int numberOfSentProducts = 0;
    float costToMakeBurger = 2.48f;
    float costToMakeFries = 1.98f;
    float costToMakeDrink = 0.98f;
    float costOfBurger = 4.95f;
    float costOfFries = 3.95f;
    float costOfDrink = 2.95f;
    GameObject lostText;
    GameObject sentText;

	void Start () {
        lostText = GameObject.Find("LostProducts");
        sentText = GameObject.Find("SentProducts");
    }
	
    public void IncreaseNumberOfLostProduct (int n) {
        numberOfLostProducts += n;
        lostText.GetComponent<TextMesh>().text = numberOfLostProducts.ToString();
    }

    public void IncreaseNumberOfSentProduct (int n) {
        numberOfSentProducts += n;
        sentText.GetComponent<TextMesh>().text = numberOfSentProducts.ToString();
    }
}
