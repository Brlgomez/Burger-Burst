using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerLift : MonoBehaviour
{

    float currentY;
    bool down;

    void Start()
    {
        currentY = transform.position.y;
    }

    void Update()
    {
        if (down)
        {
            currentY -= Time.deltaTime / 2;
            if (currentY < GetComponent<FryerBasket>().GetBottomLimit())
            {
                Destroy(GetComponent<FryerLift>());
            }
        }
        else
        {
            currentY += Time.deltaTime / 2;
            if (currentY > GetComponent<FryerBasket>().GetHeightLimit())
            {
                Destroy(GetComponent<FryerLift>());
            }
        }
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }

    public void Direction(bool dir)
    {
        if (dir)
        {
            down = dir;
        }
    }
}
