using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
ScreenTextManagment : MonoBehaviour
{
    int initialLife;
    int initialBurgers;
    int initialFries;
    int initialDrinks;
    GameObject line1, line2, line3, line4, line5;
    Color originalScreenColor;
    Color red = Color.red;
    bool pressDown = false;

    void Start()
    {
        initialLife = gameObject.GetComponent<Gameplay>().GetLife();
        initialBurgers = gameObject.GetComponent<Gameplay>().GetBurgerCount();
        initialFries = gameObject.GetComponent<Gameplay>().GetFriesCount();
        initialDrinks = gameObject.GetComponent<Gameplay>().GetDrinkCount();
        originalScreenColor = new Color(0, 0.5f, 1);
        line1 = GameObject.Find("Line1");
        line2 = GameObject.Find("Line2");
        line3 = GameObject.Find("Line3");
        line4 = GameObject.Find("Line4");
        line5 = GameObject.Find("Line5");
        ChangeToMenuText();
    }

    public void ChangeToMenuText()
    {
        RevertButtons();
        ChangeTextColorToOriginal();
        line1.transform.GetChild(0).tag = "Untagged";
        line5.transform.GetChild(0).tag = "Untagged";
        line1.GetComponent<TextMesh>().text = "GAME";
        line2.GetComponent<TextMesh>().text = "Play";
        line3.GetComponent<TextMesh>().text = "Scores";
        line4.GetComponent<TextMesh>().text = "Settings";
        line5.GetComponent<TextMesh>().text = "";
    }

    public void ChangeToGamePlayText()
    {
        RevertButtons();
        line1.transform.GetChild(0).tag = "Untagged";
        ChangeBurgerCount();
        ChangeFriesCount();
        ChangeDrinkCount();
		ChangeMistakeText();
	}

    public void ChangeToPauseText()
    {
        RevertButtons();
        ChangeTextColorToOriginal();
        line1.transform.GetChild(0).tag = "Untagged";
        line5.transform.GetChild(0).tag = "Untagged";
        line1.GetComponent<TextMesh>().text = "PAUSED";
        line2.GetComponent<TextMesh>().text = "Resume";
        line3.GetComponent<TextMesh>().text = "Restart";
        line4.GetComponent<TextMesh>().text = "Quit";
        line5.GetComponent<TextMesh>().text = "";
    }

    public void ChangeMistakeText()
    {
        int n = Camera.main.GetComponent<Gameplay>().GetLife();
        if (n >= 1)
        {
            line1.GetComponent<TextMesh>().text = "H : " + n.ToString();
        }
        else
        {
            line1.GetComponent<TextMesh>().text = "DEAD";
            line2.GetComponent<TextMesh>().text = "";
			line3.GetComponent<TextMesh>().text = "Restart";
			line4.GetComponent<TextMesh>().text = "Quit";
            line5.GetComponent<TextMesh>().text = "";
            line2.tag = "Untagged";
			line5.tag = "Untagged";
            line3.GetComponent<TextMesh>().color = red;
            line4.GetComponent<TextMesh>().color = red;
        }
        Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialLife);
        line1.GetComponent<TextMesh>().color = newColor;
    }

    public void ChangeBurgerCount()
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            int n = Camera.main.GetComponent<Gameplay>().GetBurgerCount();
            Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialBurgers);
            line2.GetComponent<TextMesh>().color = newColor;
            line2.GetComponent<TextMesh>().text = "B : " + n;
        }
    }

    public void ChangeFriesCount()
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            int n = Camera.main.GetComponent<Gameplay>().GetFriesCount();
            Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialFries);
            line3.GetComponent<TextMesh>().color = newColor;
            line3.GetComponent<TextMesh>().text = "F : " + n;
        }
    }

    public void ChangeDrinkCount()
    {
        if (!Camera.main.GetComponent<Gameplay>().IsGameOver())
        {
            int n = Camera.main.GetComponent<Gameplay>().GetDrinkCount();
            Color newColor = Color.Lerp(red, originalScreenColor, ((float)n) / initialDrinks);
            line4.GetComponent<TextMesh>().color = newColor;
            line4.GetComponent<TextMesh>().text = "D : " + n;
        }
    }

    public void RestartScreens()
    {
        ChangeMistakeText();
        ChangeTextColorToOriginal();
        line1.GetComponent<TextMesh>().text = "H : " + initialLife;
        line2.GetComponent<TextMesh>().text = "B : " + initialBurgers;
        line3.GetComponent<TextMesh>().text = "F : " + initialFries;
        line4.GetComponent<TextMesh>().text = "D : " + initialDrinks;
    }

    public void ChangeToGrillArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).tag = "Untagged";
        line2.transform.GetChild(0).tag = "Untagged";
        line5.GetComponent<TextMesh>().text = "Counter";
    }

    public void ChangeToFryerArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).tag = "Untagged";
        line3.transform.GetChild(0).tag = "Untagged";
        line5.GetComponent<TextMesh>().text = "Counter";
    }

    public void ChangeToSodaMachineArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).tag = "Untagged";
        line4.transform.GetChild(0).tag = "Untagged";
        line5.GetComponent<TextMesh>().text = "Counter";
    }

    public void ChangeToFrontArea()
    {
        RevertButtons();
        line1.transform.GetChild(0).tag = "Untagged";
        line5.transform.GetChild(0).tag = "Untagged";
        line5.GetComponent<TextMesh>().text = "";
    }

	public void RevertButtons()
	{
		line1.transform.GetChild(0).tag = "UI";
		line2.transform.GetChild(0).tag = "UI";
		line3.transform.GetChild(0).tag = "UI";
		line4.transform.GetChild(0).tag = "UI";
		line5.transform.GetChild(0).tag = "UI";
	}

    void ChangeTextColorToOriginal()
    {
        line1.GetComponent<TextMesh>().color = originalScreenColor;
        line2.GetComponent<TextMesh>().color = originalScreenColor;
        line3.GetComponent<TextMesh>().color = originalScreenColor;
        line4.GetComponent<TextMesh>().color = originalScreenColor;
        line5.GetComponent<TextMesh>().color = originalScreenColor;
    }

	public void PressTextDown(GameObject target)
	{
        if (!pressDown)
        {
            float r = target.GetComponent<TextMesh>().color.r;
            float g = target.GetComponent<TextMesh>().color.g;
            float b = target.GetComponent<TextMesh>().color.b;
            target.GetComponent<TextMesh>().color = new Color(r / 2, g / 2, b / 2);
            pressDown = true;
        }
	}

	public void PressTextUp(GameObject target)
	{
        if (pressDown)
        {
            float r = target.GetComponent<TextMesh>().color.r;
            float g = target.GetComponent<TextMesh>().color.g;
            float b = target.GetComponent<TextMesh>().color.b;
            target.GetComponent<TextMesh>().color = new Color(r * 2, g * 2, b * 2);
            pressDown = false;
        }
	}
}
