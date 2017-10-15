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

    public void AddFloatingText(GameObject obj, string text, Color col, float size)
    {
        GameObject newAftermathText = Instantiate(aftermathText);
        newAftermathText.GetComponent<Renderer>().material.color = col;
        newAftermathText.transform.position = new Vector3(
            obj.transform.position.x,
            obj.transform.position.y + 0.1f,
            obj.transform.position.z
        );
        newAftermathText.transform.localScale *= size;
        newAftermathText.AddComponent<AftermathText>().GetComponent<AftermathText>().UpdateText(text, size/2);
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
