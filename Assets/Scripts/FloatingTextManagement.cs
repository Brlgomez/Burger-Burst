using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManagement : MonoBehaviour
{

    GameObject aftermathText;

    void Start()
    {
        aftermathText = GameObject.Find("Aftermath Text");

    }

    public void AddFloatingText(GameObject obj, float money)
    {
        GameObject newAftermathText = Instantiate(aftermathText);
        if (money >= 0)
        {
            newAftermathText.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            newAftermathText.GetComponent<Renderer>().material.color = Color.red;
        }
        newAftermathText.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + Random.Range(0.0f, 1.0f), obj.transform.position.z);
        newAftermathText.AddComponent<AftermathText>().GetComponent<AftermathText>().UpdateText("$" + money.ToString("F2"));
        newAftermathText.tag = "FloatingText";
    }

    public void DeleteAllText()
    {
        GameObject[] allText = GameObject.FindGameObjectsWithTag("FloatingText");
        foreach (GameObject text in allText)
        {
            Destroy(text);
        }
    }
}
