using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerBasket : MonoBehaviour
{
    bool down = true;
    float heightLimit;
    float bottomLimit;

    void Start()
    {
        bottomLimit = transform.position.y;
        heightLimit = bottomLimit + 0.325f;
    }

    public void PressedButton()
    {
        down = !down;
        if (gameObject.GetComponent<FryerLift>() != null)
        {
            Destroy(gameObject.GetComponent<FryerLift>());
        }
        gameObject.AddComponent<FryerLift>().Direction(down);
    }

    public float GetBottomLimit()
    {
        return bottomLimit;
    }

    public float GetHeightLimit()
    {
        return heightLimit;
    }

    public void Restart()
    {
        down = true;
        transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
    }
}
