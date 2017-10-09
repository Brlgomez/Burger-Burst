using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaCup : MonoBehaviour
{
    float maxTimeUnderFountain = 5;
    float top = 0.65f;
    float currentY;
    float maxScale = 1.5f;
    float currentScale;
    float incY, incS;

    void Start()
    {
        currentY = transform.GetChild(0).transform.localPosition.y;
        currentScale = transform.GetChild(0).transform.localScale.x;
        incY = (top - currentY) * (1.0f / maxTimeUnderFountain);
        incS = (maxScale - currentScale) * (1.0f / maxTimeUnderFountain);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "SodaFromMachine")
        {
            FillCup();
        }
        if (other.name == "Lid(Clone)")
        {
            CupReady(other.gameObject);
        }
    }

    void FillCup()
    {
        if (currentY < top)
        {
            currentY += Time.deltaTime * incY;
			currentScale += Time.deltaTime * incS;
			transform.GetChild(0).transform.localPosition = new Vector3(
                transform.GetChild(0).transform.localPosition.x,
                currentY,
                transform.GetChild(0).transform.localPosition.z
            );
			transform.GetChild(0).transform.localScale = new Vector3(
	            currentScale,
	            currentScale,
	            transform.GetChild(0).transform.localScale.z
            );
        }
    }

    void CupReady(GameObject lid)
    {
        int worth = Mathf.RoundToInt(top - Mathf.Abs(currentY - top));
		if (worth == 0)
		{
			Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drinks", Color.gray);
		}
		else if (worth == 1)
		{
			Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drink", Color.green);
		}
		else if (worth > 1)
		{
			Camera.main.GetComponent<FloatingTextManagement>().AddFloatingText(gameObject, "+ " + worth + " Drinks", Color.green);
		}
		Camera.main.GetComponent<Gameplay>().AddDrinks(worth);
		lid.transform.parent = gameObject.transform;
		if (gameObject.GetComponent<FadeObject>() == null)
		{
			gameObject.AddComponent<FadeObject>();
		}
        Destroy(GetComponent<SodaCup>());
    }
}
