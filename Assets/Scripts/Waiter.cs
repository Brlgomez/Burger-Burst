using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour
{
    int neededBurgers, neededFries, neededDrinks;
    int amountOfBurgers, amountOfFries, amountOfDrinks;
    float timeForBonus = 0;
    float maxTimeOfBonus;
    float ratioOfTime;
    float costOfMeal;
    bool orderComplete;
    List<GameObject> onPlatter = new List<GameObject>();
    GameObject head, thinkBubble;

    float speed = 1;
    float originalSpeed;
    int maxDeathTime = 1;
    float alpha = 1;
    int timeForDamage = 3;
    float damageTime = 0;
    Vector3[] availableSpritePositions;
    bool playAttack = true;

    void Start()
    {
        originalSpeed = speed;
        availableSpritePositions = new Vector3[3];
        availableSpritePositions[0] = new Vector3(0, -0.25f, 0.5f);
        availableSpritePositions[1] = new Vector3(-0.5f, 1, 0.4f);
        availableSpritePositions[2] = new Vector3(0.5f, 1, 0.3f);
        thinkBubble = transform.GetChild(0).gameObject;
        thinkBubble.AddComponent<IncreaseSize>();
        for (int i = 3; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Head")
            {
                head = transform.GetChild(i).gameObject;
                break;
            }
        }
        GetComponent<Animator>().Play("Walking");
        GetComponent<Animator>().SetBool("Walking", true);
        GetComponent<Animator>().SetFloat("Speed", speed / 2);
        WakeUp();
        SetOrder();
    }

    void Update()
    {
        thinkBubble.transform.position = new Vector3(head.transform.position.x, thinkBubble.transform.position.y, head.transform.position.z);
		if (!orderComplete && head.transform.position.z > -1)
        {
            Walk();
        }
        else if (orderComplete)
        {
            alpha -= Time.deltaTime;
            transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                transform.GetChild(0).GetChild(i).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
            }
            for (int i = 3; i < transform.childCount; i++)
            {
                MakeAllObjectsInvisible(transform.GetChild(i).gameObject);
            }
            if (alpha < 0.01f)
            {
                Camera.main.GetComponent<WaiterManager>().RemoveWaiter(gameObject);
            }
        }
        else if (!orderComplete && head.transform.position.z < -1)
        {
            GetComponent<Animator>().SetFloat("Speed", 0);
            damageTime += Time.deltaTime;
            if (damageTime > timeForDamage && playAttack)
            {
                GetComponent<Animator>().Play("Attack");
                playAttack = false;
            }
            if (damageTime > (timeForDamage + 0.5f)) 
            {
				Camera.main.GetComponent<Gameplay>().DeductNumberOfErrors();
				damageTime = 0;
                playAttack = true;
			}
        }
    }

    void Walk()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, Time.deltaTime * speed);
        GetComponent<Animator>().SetFloat("Speed", speed / 2);
        speed = originalSpeed;
        if (Mathf.Round(transform.position.z) < -2f) 
        {
            GetComponent<Animator>().SetBool("Attacking", true);
            GetComponent<Animator>().SetBool("Walking", false);
            speed = 0;
		}
    }

    void SetOrder()
    {
        int maxAmountOfProduct = Mathf.CeilToInt(Camera.main.GetComponent<Gameplay>().GetCompletedOrdersCount() / 5) + 1;
        int amountOfProduct;
        orderComplete = false;
        costOfMeal = 0;
        amountOfProduct = Random.Range(1, maxAmountOfProduct);
        if (amountOfProduct > 5)
        {
            amountOfProduct = 5;
        }
        for (int i = 0; i < amountOfProduct; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.33f)
            {
                neededBurgers++;
                continue;
            }
            else if (rand > 0.33f && rand < 0.66f)
            {
                neededFries++;
                continue;
            }
            else
            {
                neededDrinks++;
            }
        }
        SetUpSprites();
        timeForBonus = 5 + (neededBurgers * 5) + (neededFries * 5) + (neededDrinks * 5);
        maxTimeOfBonus = timeForBonus;
    }

    public void AddToPlatter(GameObject obj)
    {
        if (obj.tag == "Thrown" && !orderComplete)
        {
            if (obj.name == "Burger(Clone)")
            {
                amountOfBurgers++;
                if (amountOfBurgers > neededBurgers)
                {
                    obj.transform.parent = null;
                }
                else
                {
                    AddToBody(obj);
                }
            }
            else if (obj.name == "Drink(Clone)")
            {
                amountOfDrinks++;
                if (amountOfDrinks > neededDrinks)
                {
                    obj.transform.parent = null;
                }
                else
                {
                    AddToBody(obj);
                }
            }
            else if (obj.name == "Fries(Clone)")
            {
                amountOfFries++;
                if (amountOfFries > neededFries)
                {
                    obj.transform.parent = null;
                }
                else
                {
                    AddToBody(obj);
                }
            }
        }
        else if (obj.tag == "Thrown" && orderComplete)
        {
            Destroy(obj.GetComponent<Rigidbody>());
            Camera.main.GetComponent<Gameplay>().IncreaseNumberOfLostProduct(obj);
        }
    }

    void AddToBody(GameObject obj) {
		Destroy(obj.GetComponent<Rigidbody>());
		Destroy(obj.GetComponent<RemoveObjects>());
		costOfMeal += Camera.main.GetComponent<Gameplay>().IncreaseNumberOfSentProduct(obj);
		obj.tag = "OnPlatter";
		onPlatter.Add(obj);
		CheckOrder();
    }

    void CheckOrder()
    {
        if (amountOfBurgers >= neededBurgers && amountOfFries >= neededFries && amountOfDrinks >= neededDrinks)
        {
            Camera.main.GetComponent<Gameplay>().IncreaseCompletedOrders();
            orderComplete = true;
            Died();
            //CheckTip();
        }
    }

    void CheckTip()
    {
        ratioOfTime = timeForBonus / maxTimeOfBonus;
        if (ratioOfTime > 0)
        {
            float tipAmount = ratioOfTime * costOfMeal * 0.5f;
            tipAmount = Mathf.Round(tipAmount * 100f) / 100f;
            Camera.main.GetComponent<Gameplay>().AddTip(head, tipAmount);
        }
        if (ratioOfTime > 0.5f)
        {
            Camera.main.GetComponent<Gameplay>().AddLife(1);
        }
    }

    void WakeUp()
    {
        for (int i = 3; i < transform.childCount; i++)
        {
            TurnOnAllForces(transform.GetChild(i).gameObject);
        }
    }

	void TurnOnAllForces(GameObject node)
	{
		if (node.transform.childCount == 0)
		{
			TurnOnForce(node.transform.gameObject);
			return;
		}
		else
		{
			for (int i = 0; i < node.transform.childCount; i++)
			{
				TurnOnForce(node.transform.gameObject);
				TurnOnAllForces(node.transform.GetChild(i).gameObject);
			}
		}
	}

	void TurnOnForce(GameObject obj)
	{
        if (obj.GetComponent<Rigidbody>() != null)
		{
			obj.GetComponent<Rigidbody>().useGravity = true;
			obj.GetComponent<Rigidbody>().isKinematic = false;
		}
		if (obj.GetComponent<ConstantForce>() != null)
		{
			obj.GetComponent<ConstantForce>().enabled = true;
		}
		if (obj.GetComponent<Collider>() != null)
		{
			obj.GetComponent<Collider>().enabled = true;
		}
	}

    void Died()
    {
        GetComponent<Animator>().enabled = false;
        for (int i = 3; i < transform.childCount; i++)
        {
            TurnOffAllForces(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < onPlatter.Count; i++)
        {
            onPlatter[i].gameObject.layer = 11;
            Camera.main.GetComponent<FoodManager>().ChangeToTransparentMaterial(onPlatter[i].gameObject);
        }
    }

    void TurnOffAllForces(GameObject node)
    {
        if (node.transform.childCount == 0) {
            TurnOffForce(node.transform.gameObject);
            return;
        } else {
            for (int i = 0; i < node.transform.childCount; i++) {
                TurnOffForce(node.transform.gameObject);
                TurnOffAllForces(node.transform.GetChild(i).gameObject);
            }
        }
    }

    void TurnOffForce (GameObject obj) 
    {
		obj.gameObject.layer = 11;
        if (obj.GetComponent<Renderer>() != null)
        {
            obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().zombieClear;
        }
		if (obj.GetComponent<ConstantForce>() != null)
		{
			obj.GetComponent<ConstantForce>().enabled = false;
		}
        if (obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().mass *= 0.25f;
        }
    }

	void MakeAllObjectsInvisible(GameObject node)
	{
		if (node.transform.childCount == 0)
		{
            MakeObjectInvisible(node.transform.gameObject);
			return;
		}
		else
		{
			for (int i = 0; i < node.transform.childCount; i++)
			{
                MakeObjectInvisible(node.transform.gameObject);
                MakeAllObjectsInvisible(node.transform.GetChild(i).gameObject);
			}
		}
	}

	void MakeObjectInvisible(GameObject obj)
	{
        if (obj.GetComponent<Renderer>() != null)
        {
            obj.GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
        }
	}

    public void SetSpeed(float s)
    {
        speed = s;
        originalSpeed = s;
    }

	void SetUpSprites()
	{
		int spritePosition = 0;
		if (neededBurgers > 0)
		{
			GameObject sprite = Instantiate(Camera.main.GetComponent<WaiterManager>().burgers[neededBurgers - 1], thinkBubble.transform);
			sprite.transform.localPosition = availableSpritePositions[spritePosition];
			spritePosition++;
		}
		if (neededFries > 0)
		{
			GameObject sprite = Instantiate(Camera.main.GetComponent<WaiterManager>().fries[neededFries - 1], thinkBubble.transform);
			sprite.transform.localPosition = availableSpritePositions[spritePosition];
			spritePosition++;
		}
		if (neededDrinks > 0)
		{
			GameObject sprite = Instantiate(Camera.main.GetComponent<WaiterManager>().drinks[neededDrinks - 1], thinkBubble.transform);
			sprite.transform.localPosition = availableSpritePositions[spritePosition];
			spritePosition++;
		}
	}
}
