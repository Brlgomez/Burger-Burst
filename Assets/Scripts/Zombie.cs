using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    int neededBurgers, neededFries, neededDrinks;
    int amountOfBurgers, amountOfFries, amountOfDrinks;
    bool orderComplete;
    List<GameObject> onPlatter = new List<GameObject>();
    GameObject head, thinkBubble, hair;

    float speed = 1;
    float originalSpeed;
    int maxDeathTime = 2;
    float alpha = 1;
    int timeForDamage = 3;
    float damageTime;
    Vector3[] availableSpritePositions;
    bool playAttack = true;
    float bubbleMinScale = 0.25f;
    float startingZ;
    float endingZ = -1;
    float animationSpeed = 0.5f;

    void Start()
    {
        originalSpeed = speed;
        availableSpritePositions = new Vector3[3];
        availableSpritePositions[0] = new Vector3(0, 2.25f, 0.5f);
        availableSpritePositions[1] = new Vector3(-0.5f, 4, 0.4f);
        availableSpritePositions[2] = new Vector3(0.5f, 4, 0.3f);
        thinkBubble = transform.GetChild(0).gameObject;
        thinkBubble.AddComponent<IncreaseSize>();
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).name == "Head")
			{
				head = transform.GetChild(i).gameObject;
			}
		}

		GetComponent<Animator>().Play("Walking");
        GetComponent<Animator>().SetBool("Walking", true);
        GetComponent<Animator>().SetFloat("Speed", speed * animationSpeed);
        startingZ = head.transform.position.z;
        SetBodyParts();
		WakeUp();
        SetOrder();
    }

    void Update()
    {
        if (!orderComplete && head.transform.position.z > endingZ)
        {
            Walk();
        }
        else if (orderComplete)
        {
            alpha -= Time.deltaTime / maxDeathTime;
            transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha / maxDeathTime);
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                transform.GetChild(0).GetChild(i).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
            }
            for (int i = 1; i < transform.childCount; i++)
            {
                MakeAllObjectsInvisible(transform.GetChild(i).gameObject);
            }
            if (alpha < 0.01f)
            {
                Camera.main.GetComponent<ZombieManager>().RemoveWaiter(gameObject);
            }
        }
        else if (!orderComplete && head.transform.position.z < endingZ)
        {
            GetComponent<Animator>().SetFloat("Speed", 0);
            damageTime += Time.deltaTime;
            if (damageTime > timeForDamage && playAttack)
            {
                if (Random.value > 0.5f)
                {
                    GetComponent<Animator>().Play("Attack");
                }
                else
                {
                    GetComponent<Animator>().Play("Attack Left");
                }
                playAttack = false;
            }
            if (damageTime > (timeForDamage + 0.25f))
            {
                Camera.main.GetComponent<Gameplay>().DeductNumberOfErrors();
                if (Camera.main.gameObject.GetComponent<GettingHurt>() == null)
                {
                    Camera.main.gameObject.AddComponent<GettingHurt>();
                }
                damageTime = 0;
                playAttack = true;
                if (Camera.main.GetComponent<Gameplay>().IsGameOver())
                {
                    Camera.main.GetComponent<ZombieManager>().DeleteAllScripts();
                }
            }
        }
        thinkBubble.transform.localPosition = new Vector3(
            head.transform.localPosition.x + thinkBubble.transform.localScale.x,
            head.transform.localPosition.y + thinkBubble.transform.localScale.x + 0.5f,
            head.transform.localPosition.z
        );
    }

    void Walk()
    {
        if ((head.transform.position.z - endingZ) / (startingZ - endingZ) > bubbleMinScale)
        {
            thinkBubble.transform.localScale = Vector3.one * ((head.transform.position.z - endingZ) / (startingZ - endingZ));
        }
        if (Mathf.Round(head.transform.position.z) < -1.25)
        {
            GetComponent<Animator>().SetBool("Attacking", true);
            GetComponent<Animator>().SetBool("Walking", false);
            speed = 0;
            GetComponent<Animator>().SetFloat("Speed", speed);
        }
        else
        {
            GetComponent<Animator>().SetBool("Attacking", false);
            GetComponent<Animator>().SetBool("Walking", true);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, Time.deltaTime * speed);
            GetComponent<Animator>().SetFloat("Speed", speed * animationSpeed);
            speed = originalSpeed;
        }
    }

    void SetOrder()
    {
        int maxAmountOfProduct = Mathf.CeilToInt(Camera.main.GetComponent<Gameplay>().GetCompletedOrdersCount() / 5) + 1;
        int amountOfProduct;
        orderComplete = false;
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
            if (rand > 0.33f && rand < 0.66f)
            {
                neededFries++;
                continue;
            }
            neededDrinks++;
        }
        SetUpSprites();
    }

    public void AddToPlatter(GameObject obj)
    {
        if (!orderComplete)
        {
            switch (obj.name)
            {
                case "Burger(Clone)":
                    amountOfBurgers++;
                    if (amountOfBurgers > neededBurgers)
                    {
                        obj.transform.parent = null;
                    }
                    else
                    {
                        AddToBody(obj);
                    }
                    break;
                case "Drink(Clone)":
                    amountOfDrinks++;
                    if (amountOfDrinks > neededDrinks)
                    {
                        obj.transform.parent = null;
                    }
                    else
                    {
                        AddToBody(obj);
                    }
                    break;
                case "Fries(Clone)":
                    amountOfFries++;
                    if (amountOfFries > neededFries)
                    {
                        obj.transform.parent = null;
                    }
                    else
                    {
                        AddToBody(obj);
                    }
                    break;
            }
        }
    }

    void AddToBody(GameObject obj)
    {
        Camera.main.GetComponent<Gameplay>().IncreasePoints(obj);
        obj.GetComponent<RemoveObjects>().DropProduct();
        Destroy(obj.GetComponent<Rigidbody>());
        Destroy(obj.GetComponent<RemoveObjects>());
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
        }
    }

    void WakeUp()
    {
        for (int i = 1; i < transform.childCount; i++)
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
        if (obj.GetComponent<Rigidbody>() != null && obj.name != "Right_Leg" && obj.name != "Left_Leg")
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
        for (int i = 1; i < transform.childCount; i++)
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
        if (node.transform.childCount == 0)
        {
            TurnOffForce(node.transform.gameObject);
            return;
        }
        for (int i = 0; i < node.transform.childCount; i++)
        {
            TurnOffForce(node.transform.gameObject);
            TurnOffAllForces(node.transform.GetChild(i).gameObject);
        }
    }

    void TurnOffForce(GameObject obj)
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
            if (obj.name == "Right_Leg" || obj.name == "Left_Leg")
            {
                obj.GetComponent<Rigidbody>().useGravity = true;
                obj.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void DestroyScript()
    {
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject.GetComponent<Zombie>());
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
            obj.GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
        }
    }

    public void SetZombie(float s, GameObject h)
    {
        speed = s;
        originalSpeed = s;
        hair = Instantiate(h);
    }

    void SetBodyParts()
    {
        hair.transform.position = new Vector3(head.transform.position.x, head.transform.position.y + 0.5f, head.transform.position.z);
		hair.transform.parent = head.transform;
        hair.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void SetUpSprites()
    {
        int spritePosition = 0;
        if (neededBurgers > 0)
        {
            GameObject sprite = Instantiate(Camera.main.GetComponent<ZombieManager>().burgers[neededBurgers - 1], thinkBubble.transform);
            sprite.transform.localPosition = availableSpritePositions[spritePosition];
            spritePosition++;
        }
        if (neededFries > 0)
        {
            GameObject sprite = Instantiate(Camera.main.GetComponent<ZombieManager>().fries[neededFries - 1], thinkBubble.transform);
            sprite.transform.localPosition = availableSpritePositions[spritePosition];
            spritePosition++;
        }
        if (neededDrinks > 0)
        {
            GameObject sprite = Instantiate(Camera.main.GetComponent<ZombieManager>().drinks[neededDrinks - 1], thinkBubble.transform);
            sprite.transform.localPosition = availableSpritePositions[spritePosition];
            spritePosition++;
        }
    }
}
