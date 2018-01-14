using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public List<Being> creditList = new List<Being>();

    public class Being
    {
        public int number;
        public int price;
        public bool unlocked;
        public string name;
        public string role;
        public string description1;
        public string description2;
        public Sprite sprite;

        public Being(int num, string description)
        {
            number = num;
            price = 0;
            unlocked = true;
            name = description.Split('*')[0];
            role = description.Split('*')[1];
            description1 = description.Split('*')[2];
            description2 = description.Split('*')[3];
            sprite = (Sprite)Resources.Load("Sprites/Credits/" + num, typeof(Sprite));
        }
    }

    void Awake()
    {
        SetCreditList();
    }

    public void SetCreditList()
    {
        TextAsset t = new TextAsset();
        t = Resources.Load("Credits") as TextAsset;
        string[] allDescriptions = t.text.Split('\n');
        for (int i = 0; i < allDescriptions.Length; i++)
        {
            string description = allDescriptions[i].Replace("NEWLINE", "\n");
            creditList.Add(new Being(i, description));
        }
    }
}
