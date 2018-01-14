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
        public string description;
        public Sprite sprite;

        public Being(int num, string n, string r, string descipt)
        {
            number = num;
            price = 0;
            unlocked = true;
            name = n;
            role = r;
            description = descipt;
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
            creditList.Add(new Being(i, description.Split('*')[0], description.Split('*')[1], description.Split('*')[2]));
        }
    }
}
