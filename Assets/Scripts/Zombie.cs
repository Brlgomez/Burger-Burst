using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    int updateInterval = 1;

    int neededBurgers, neededFries, neededDrinks;
    int amountOfBurgers, amountOfFries, amountOfDrinks;
    bool orderComplete;
    List<GameObject> onPlatter = new List<GameObject>();
    GameObject head, thinkBubble, hair, leftForearm, rightForearm, upperBody;
    GameObject leftHand, rightHand, leftFoot, rightFoot, leftLeg, rightLeg;
    GameObject rightUpperArm, leftUpperArm, leftThigh, rightThigh, lowerBody;
    ParticleSystem deathParticles, powerParticles;
    Material originalMaterial;
    Vector3 initialBubbleSize;

    bool particlesPlaying;
    float speed = 1.25f;
    float originalSpeed;
    static int maxDeathTime = 5;
    float alpha = 1;
    float ragDollTime = 1;
    int timeForDamage = 3;
    float damageTime;
    bool playAttack = true;
    static float bubbleMinScale = 0.15f;
    float startingZ;
    static int endingZ = -1;
    float animationSpeed = 0.5f;
    static int maxFrozenTime = 3;
    float frozenTime;
    bool frozen;
    int damageAmount = 10;
    int pointsMultiplier = 1;
    float timerForIdleNoise;
    int timeForNextIdelSound;

    enum ZombieType { regular, coin, healing, instantKill, poison, speed, ice };
    enum ZombieSize { normal, large, small };
    ZombieType thisZombieType = ZombieType.regular;
    ZombieSize thisZombieSize = ZombieSize.normal;

    Renderer myRenderer;
    bool isVisible = true;

    void Awake()
    {
        originalSpeed = speed;
        GetBodyParts(gameObject);
        thinkBubble = head.transform.GetChild(1).gameObject;
        GetComponent<Animator>().Play("Walking");
        GetComponent<Animator>().SetBool("Walking", true);
        GetComponent<Animator>().SetFloat("Speed", speed * animationSpeed);
        startingZ = head.transform.position.z;
        deathParticles = upperBody.transform.GetChild(2).GetComponent<ParticleSystem>();
        WakeUp();
        SetOrder();
        PickNextIdleSoundTime();
        thinkBubble.AddComponent<IncreaseSize>();
        myRenderer = head.GetComponent<Renderer>();
        if (transform.localScale.x > 1.1f)
        {
            thisZombieSize = ZombieSize.large;
            damageAmount *= 2;
            thinkBubble.transform.position = new Vector3(
                thinkBubble.transform.position.x - 0.25f,
                thinkBubble.transform.position.y - 0.5f,
                thinkBubble.transform.position.z
            );
        }
        else if (transform.localScale.x < 0.9f)
        {
            thisZombieSize = ZombieSize.small;
            damageAmount /= 2;
        }
        originalMaterial = head.GetComponent<Renderer>().material;
    }

    void FixedUpdate()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            if (!orderComplete && !frozen && isVisible)
            {
                if (head.transform.position.z > endingZ)
                {
                    leftHand.GetComponent<Rigidbody>().AddRelativeForce(-leftHand.transform.forward * Random.Range(20, 25));
                    rightHand.GetComponent<Rigidbody>().AddRelativeForce(-rightHand.transform.forward * Random.Range(20, 25));
                }
                else if (head.transform.position.z <= endingZ)
                {
                    leftHand.GetComponent<Rigidbody>().AddRelativeForce(-leftHand.transform.forward * Random.Range(5, 10));
                    rightHand.GetComponent<Rigidbody>().AddRelativeForce(-rightHand.transform.forward * Random.Range(5, 10));
                }
            }
        }
    }

    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            if (!orderComplete && !frozen)
            {
                IncreaseIdleSoundTimer();
                if (head.transform.position.z > endingZ)
                {
                    Walk();
                }
                else if (head.transform.position.z <= endingZ)
                {
                    NearFoodTruck();
                }
            }
            if (orderComplete)
            {
                OrderCompleted();
            }
            if (myRenderer.isVisible)
            {
                updateInterval = 2;
                isVisible = true;
                OrderBubbleScale();
                if (powerParticles != null && powerParticles.isPaused)
                {
                    powerParticles.Play();
                }
            }
            else
            {
                isVisible = false;
                updateInterval = 6;
                if (powerParticles != null && powerParticles.isPlaying)
                {
                    powerParticles.Pause();
                }
            }
            if (frozen)
            {
                frozenTime += Time.deltaTime * updateInterval;
                if (frozenTime > maxFrozenTime)
                {
                    UnFreezeZombie();
                }
            }
        }
    }

    void Walk()
    {
        if (Mathf.Round(head.transform.position.z) < endingZ - 0.25f)
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
            transform.position = Vector3.MoveTowards(
                transform.position, transform.position + transform.forward, Time.deltaTime * speed * updateInterval);
            GetComponent<Animator>().SetFloat("Speed", speed * animationSpeed);
            speed = originalSpeed;
        }
    }

    void NearFoodTruck()
    {
        GetComponent<Animator>().SetFloat("Speed", 0);
        damageTime += Time.deltaTime * updateInterval;
        if (rightThigh.GetComponent<Rigidbody>().isKinematic)
        {
            rightThigh.GetComponent<Rigidbody>().isKinematic = false;
            rightThigh.GetComponent<Rigidbody>().useGravity = true;
            leftThigh.GetComponent<Rigidbody>().isKinematic = false;
            leftThigh.GetComponent<Rigidbody>().useGravity = true;
            upperBody.GetComponent<ConstantForce>().force = new Vector3(0, 150, 0);
            rightFoot.GetComponent<ConstantForce>().force = new Vector3(0, -100, 0);
            leftFoot.GetComponent<ConstantForce>().force = new Vector3(0, -100, 0);
        }
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
            ZombieDamageTypes();
            damageTime = 0;
            playAttack = true;
        }
    }

    void OrderCompleted()
    {
        ChangeOrderAlpha();
        ragDollTime -= ((Time.deltaTime * updateInterval) / maxDeathTime);
        if (ragDollTime < 0.5f && !particlesPlaying)
        {
            if (powerParticles != null)
            {
                powerParticles.Play();
                powerParticles.Stop();
                Behaviour halo = (Behaviour)powerParticles.GetComponent("Halo");
                halo.enabled = false;
            }
            deathParticles.Play();
            MakeZombieDisappear();
            particlesPlaying = true;
            Camera.main.GetComponent<SoundAndMusicManager>().PlayPuffSound(gameObject);
            if (thisZombieType == ZombieType.coin)
            {
                Camera.main.GetComponent<Gameplay>().StartCoinLancher(5, thinkBubble);
            }
            else if (thisZombieType == ZombieType.healing)
            {
                Camera.main.GetComponent<Gameplay>().StartCoinLancher(1, thinkBubble);
                Camera.main.GetComponent<Gameplay>().AddLife(15, thinkBubble);
            }
            else
            {
                Camera.main.GetComponent<Gameplay>().StartCoinLancher(1, thinkBubble);
            }
        }
        if (ragDollTime < 0)
        {
            Camera.main.GetComponent<ZombieManager>().RemoveWaiter(gameObject);
        }
    }

    void ZombieDamageTypes()
    {
        switch (thisZombieType)
        {
            case ZombieType.instantKill:
                if (!Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().noInstantKill.powerUpNumber))
                {
                    Camera.main.GetComponent<Gameplay>().ReduceHealth(1000, upperBody);
                }
                else
                {
                    Camera.main.GetComponent<Gameplay>().ReduceHealth(damageAmount, upperBody);
                }
                break;
            case ZombieType.poison:
                thisZombieType = ZombieType.regular;
                powerParticles.Play();
                powerParticles.Stop();
                Behaviour halo = (Behaviour)powerParticles.GetComponent("Halo");
                halo.enabled = false;
                Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -1);
                if (!Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().noPoison.powerUpNumber))
                {
                    Camera.main.GetComponent<Gameplay>().ReduceHealth(damageAmount, upperBody);
                    if (Camera.main.transform.GetComponent<Poisoned>())
                    {
                        Camera.main.transform.GetComponent<Poisoned>().ResetTime();
                    }
                    else
                    {
                        Camera.main.transform.gameObject.AddComponent<Poisoned>();
                    }
                }
                else
                {
                    Camera.main.GetComponent<Gameplay>().ReduceHealth(damageAmount, upperBody);
                }
                break;
            case ZombieType.ice:
                thisZombieType = ZombieType.regular;
                powerParticles.Play();
                powerParticles.Stop();
                Behaviour halo2 = (Behaviour)powerParticles.GetComponent("Halo");
                halo2.enabled = false;
                Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -1);
                if (!Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().noIce.powerUpNumber))
                {
                    Camera.main.GetComponent<Gameplay>().ReduceHealth(damageAmount, upperBody);
                    if (Camera.main.GetComponent<Frozen>())
                    {
                        Camera.main.GetComponent<Frozen>().RestartTime();
                    }
                    else
                    {
                        Camera.main.gameObject.AddComponent<Frozen>();
                    }
                }
                else
                {
                    Camera.main.GetComponent<Gameplay>().ReduceHealth(damageAmount, upperBody);
                }
                break;
            default:
                Camera.main.GetComponent<Gameplay>().ReduceHealth(damageAmount, upperBody);
                break;
        }
    }

    /* Food functions */

    void SetOrder()
    {
        int maxAmountOfProduct = Mathf.CeilToInt(Camera.main.GetComponent<Gameplay>().GetCompletedOrdersCount() / 5) + 1;
        int amountOfProduct;
        int absoluteMax = 5;
        int absoluteMin = 1;
        orderComplete = false;
        if (transform.localScale.x > 1.1f)
        {
            absoluteMin = 3;
        }
        amountOfProduct = Random.Range(absoluteMin, maxAmountOfProduct);
        if (transform.localScale.x > 1.1f)
        {
            absoluteMax = 7;
        }
        else if (transform.localScale.x < 0.9f)
        {
            absoluteMax = 3;
        }
        if (amountOfProduct > absoluteMax)
        {
            amountOfProduct = absoluteMax;
        }
        for (int i = 0; i < amountOfProduct; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.333f)
            {
                neededBurgers++;
                continue;
            }
            if (rand >= 0.333f && rand < 0.666f)
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
            if (obj.name == "Burger(Clone)" || obj.name == "Burger(Clone)Copy")
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
            else if (obj.name == "Drink(Clone)" || obj.name == "Drink(Clone)Copy")
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
            else if (obj.name == "Fries(Clone)" || obj.name == "Fries(Clone)Copy")
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
            if (Camera.main.GetComponent<PlayerPrefsManager>().ContainsUpgrade(Camera.main.GetComponent<PowerUpsManager>().freeze.powerUpNumber))
            {
                FreezeZombie();
                if (obj.GetComponent<MagnetPowerUp>())
                {
                    Destroy(obj.GetComponent<MagnetPowerUp>());
                }
            }
        }
    }

    void AddToBody(GameObject obj)
    {
        Vector3 velocity = obj.GetComponent<Rigidbody>().velocity * obj.GetComponent<Rigidbody>().mass;
        obj.transform.parent.GetComponent<Rigidbody>().velocity = velocity;
        GameObject landParticles = Instantiate(Camera.main.GetComponent<ObjectManager>().LandOnZombieParticles());
        landParticles.transform.position = obj.transform.position;
        landParticles.GetComponent<ParticleSystem>().Play();
        landParticles.AddComponent<DestroySpawnedParticle>();
        Camera.main.GetComponent<Gameplay>().IncreasePoints(obj, pointsMultiplier);
        Camera.main.GetComponent<PlayerPrefsManager>().IncreaseFoodLanded();
        Camera.main.GetComponent<VibrationManager>().MediumTapticFeedback();
        obj.GetComponent<RemoveObjects>().DropProduct();
        obj.GetComponent<ParticleSystem>().Stop();
        obj.GetComponent<Collider>().isTrigger = true;
        Destroy(obj.GetComponent<Rigidbody>());
        Destroy(obj.GetComponent<RemoveObjects>());
        if (obj.GetComponent<MagnetPowerUp>() != null)
        {
            Destroy(obj.GetComponent<MagnetPowerUp>());
        }
        obj.tag = "OnPlatter";
        onPlatter.Add(obj);
        CheckOrder();
    }

    void CheckOrder()
    {
        if (amountOfBurgers >= neededBurgers && amountOfFries >= neededFries && amountOfDrinks >= neededDrinks)
        {
            upperBody.GetComponent<Rigidbody>().velocity += Vector3.up * 75;
            Camera.main.GetComponent<Gameplay>().IncreaseCompletedOrders();
            orderComplete = true;
            Camera.main.GetComponent<PlayerPrefsManager>().IncreaseOrdersCompleted();
            Camera.main.GetComponent<SoundAndMusicManager>().StopLoopFromSourceAndLowerVolume(gameObject, -1);
            PlayGruntNoise();
            Died();
        }
        else
        {
            damageTime -= 0.5f;
            PlayGroanNoise();
            SetUpSprites();
        }
    }

    /* Waking up */

    void WakeUp()
    {
        for (int i = 0; i < transform.childCount; i++)
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
        for (int i = 0; i < node.transform.childCount; i++)
        {
            TurnOnForce(node.transform.gameObject);
            TurnOnAllForces(node.transform.GetChild(i).gameObject);
        }
    }

    void TurnOnForce(GameObject obj)
    {
        if (obj.GetComponent<MeshRenderer>() != null && originalMaterial != null && obj.tag != "OnPlatter")
        {
            obj.GetComponent<Renderer>().material = originalMaterial;
        }
        if (obj.GetComponent<Rigidbody>() != null && obj != rightThigh && obj != leftThigh)
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

    /* Died. Called After completing order */

    void Died()
    {
        thinkBubble.transform.parent = transform;
        GetComponent<Animator>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            TurnOffAllForces(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < onPlatter.Count; i++)
        {
            onPlatter[i].gameObject.layer = 11;
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
        obj.tag = "Dead Waiter";
        if (obj.GetComponent<ConstantForce>() != null)
        {
            obj.GetComponent<ConstantForce>().enabled = false;
        }
        if (obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().drag = 0;
            obj.GetComponent<Rigidbody>().angularDrag = 0;
            if ((obj == rightThigh || obj == leftThigh) && !frozen)
            {
                obj.GetComponent<Rigidbody>().useGravity = true;
                obj.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    /* Disappearing. Called once dead for a certain amount of time */

    void MakeZombieDisappear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            MakeAllObjectsInvisible(transform.GetChild(i).gameObject);
        }
    }

    void MakeAllObjectsInvisible(GameObject node)
    {
        if (node.transform.childCount == 0)
        {
            MakeObjectInvisible(node.transform.gameObject);
            return;
        }
        for (int i = 0; i < node.transform.childCount; i++)
        {
            MakeObjectInvisible(node.transform.gameObject);
            MakeAllObjectsInvisible(node.transform.GetChild(i).gameObject);
        }
    }

    void MakeObjectInvisible(GameObject obj)
    {
        if (obj.GetComponent<Renderer>() != null && obj.GetComponent<MeshFilter>() != null)
        {
            obj.GetComponent<MeshFilter>().mesh = null;
        }
    }

    /* Freezing Rigidbodies */

    void Freeze()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            FreezeRigidBodies(transform.GetChild(i).gameObject);
        }
    }

    void FreezeRigidBodies(GameObject node)
    {
        if (node.transform.childCount == 0)
        {
            FreezeRigidBody(node.transform.gameObject);
            return;
        }
        for (int i = 0; i < node.transform.childCount; i++)
        {
            FreezeRigidBody(node.transform.gameObject);
            FreezeRigidBodies(node.transform.GetChild(i).gameObject);
        }
    }

    void FreezeRigidBody(GameObject obj)
    {
        if (obj.GetComponent<MeshRenderer>() != null && obj.tag != "OnPlatter")
        {
            obj.GetComponent<Renderer>().material = Camera.main.GetComponent<Materials>().ice;
        }
        if (obj.GetComponent<ConstantForce>() != null)
        {
            obj.GetComponent<ConstantForce>().enabled = false;
        }
        if (obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    /* Setting up functions */

    void GetBodyParts(GameObject part)
    {
        switch (part.name)
        {
            case "Head":
                head = part.gameObject;
                break;
            case "Left_Forearm":
                leftForearm = part.gameObject;
                break;
            case "Right_Forearm":
                rightForearm = part.gameObject;
                break;
            case "Hair":
                hair = part.gameObject;
                break;
            case "Left_Hand":
                leftHand = part.gameObject;
                break;
            case "Right_Hand":
                rightHand = part.gameObject;
                break;
            case "Left_Foot":
                leftFoot = part.gameObject;
                break;
            case "Right_Foot":
                rightFoot = part.gameObject;
                break;
            case "Left_Leg":
                leftLeg = part.gameObject;
                break;
            case "Right_Leg":
                rightLeg = part.gameObject;
                break;
            case "Left_Upper_Arm":
                leftUpperArm = part.gameObject;
                break;
            case "Right_Upper_Arm":
                rightUpperArm = part.gameObject;
                break;
            case "Left_Thigh":
                leftThigh = part.gameObject;
                break;
            case "Right_Thigh":
                rightThigh = part.gameObject;
                break;
            case "Lower_Body":
                lowerBody = part.gameObject;
                break;
            case "Upper_Body":
                upperBody = part.gameObject;
                break;
        }
        if (part.transform.childCount == 0)
        {
            return;
        }
        for (int i = 0; i < part.transform.childCount; i++)
        {
            GetBodyParts(part.transform.GetChild(i).gameObject);
        }
    }

    public void SetZombie(float s, Mesh h, Mesh lF, Mesh rF, Mesh rH, Mesh lH, Mesh lFoot, Mesh rFoot, Mesh lL,
                          Mesh rL, Mesh rU, Mesh lU, Mesh lT, Mesh rT, Mesh headMesh, Mesh lB, Mesh uB, Texture outfit)
    {
        speed = s;
        originalSpeed = s;
        SetUpLimb(hair, h, outfit);
        SetUpLimb(leftForearm, lF, outfit);
        SetUpLimb(rightForearm, rF, outfit);
        SetUpLimb(leftHand, lH, outfit);
        SetUpLimb(rightHand, rH, outfit);
        SetUpLimb(leftFoot, lFoot, outfit);
        SetUpLimb(rightFoot, rFoot, outfit);
        SetUpLimb(leftLeg, lL, outfit);
        SetUpLimb(rightLeg, rL, outfit);
        SetUpLimb(leftUpperArm, lU, outfit);
        SetUpLimb(rightUpperArm, rU, outfit);
        SetUpLimb(rightForearm, rF, outfit);
        SetUpLimb(leftThigh, lT, outfit);
        SetUpLimb(rightThigh, rT, outfit);
        SetUpLimb(head, headMesh, outfit);
        SetUpLimb(lowerBody, lB, outfit);
        SetUpLimb(upperBody, uB, outfit);
        switch (outfit.name)
        {
            case "Coin Zombie":
                thisZombieType = ZombieType.coin;
                powerParticles = upperBody.transform.GetChild(3).GetComponent<ParticleSystem>();
                powerParticles.Play();
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieSparckleSound(GetComponent<AudioSource>());
                Behaviour halo = (Behaviour)powerParticles.GetComponent("Halo");
                halo.enabled = true;
                break;
            case "Health Zombie":
                thisZombieType = ZombieType.healing;
                powerParticles = upperBody.transform.GetChild(4).GetComponent<ParticleSystem>();
                powerParticles.Play();
                Behaviour halo2 = (Behaviour)powerParticles.GetComponent("Halo");
                halo2.enabled = true;
                break;
            case "Death Zombie":
                pointsMultiplier = 3;
                thisZombieType = ZombieType.instantKill;
                powerParticles = upperBody.transform.GetChild(5).GetComponent<ParticleSystem>();
                powerParticles.Play();
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieInstantDeathSound(GetComponent<AudioSource>());
                Behaviour halo3 = (Behaviour)powerParticles.GetComponent("Halo");
                halo3.enabled = true;
                speed = 1;
                originalSpeed = speed;
                break;
            case "Poison Zombie":
                pointsMultiplier = 2;
                thisZombieType = ZombieType.poison;
                powerParticles = upperBody.transform.GetChild(6).GetComponent<ParticleSystem>();
                powerParticles.Play();
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieBubblingSound(GetComponent<AudioSource>());
                Behaviour halo4 = (Behaviour)powerParticles.GetComponent("Halo");
                halo4.enabled = true;
                break;
            case "Fast Zombie":
                pointsMultiplier = 2;
                thisZombieType = ZombieType.speed;
                speed = 3;
                originalSpeed = speed;
                break;
            case "Ice Zombie":
                pointsMultiplier = 2;
                thisZombieType = ZombieType.ice;
                powerParticles = upperBody.transform.GetChild(7).GetComponent<ParticleSystem>();
                powerParticles.Play();
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieIceSound(GetComponent<AudioSource>());
                Behaviour halo5 = (Behaviour)powerParticles.GetComponent("Halo");
                halo5.enabled = true;
                break;
        }
    }

    void SetUpLimb(GameObject limb, Mesh newMesh, Texture outfit)
    {
        limb.GetComponent<MeshFilter>().mesh = Instantiate(newMesh);
        limb.GetComponent<Renderer>().material.mainTexture = outfit;
    }

    void SetUpSprites()
    {
        for (int i = 0; i < 4; i++)
        {
            thinkBubble.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
        }
        int spritePosition = 0;
        if ((neededBurgers - amountOfBurgers > 0 && neededFries - amountOfFries == 0 && neededDrinks - amountOfDrinks == 0) ||
            (neededFries - amountOfFries > 0 && neededBurgers - amountOfBurgers == 0 && neededDrinks - amountOfDrinks == 0) ||
            (neededDrinks - amountOfDrinks > 0 && neededFries - amountOfFries == 0 && neededBurgers - amountOfBurgers == 0))
        {
            if (neededBurgers - amountOfBurgers > 0)
            {
                Sprite sprite = Camera.main.GetComponent<ZombieManager>().burgers[neededBurgers - amountOfBurgers - 1];
                thinkBubble.transform.GetChild(spritePosition).GetComponent<SpriteRenderer>().sprite = sprite;
                spritePosition++;
            }
            if (neededFries - amountOfFries > 0)
            {
                Sprite sprite = Camera.main.GetComponent<ZombieManager>().fries[neededFries - amountOfFries - 1];
                thinkBubble.transform.GetChild(spritePosition).GetComponent<SpriteRenderer>().sprite = sprite;
                spritePosition++;
            }
            if (neededDrinks - amountOfDrinks > 0)
            {
                Sprite sprite = Camera.main.GetComponent<ZombieManager>().drinks[neededDrinks - amountOfDrinks - 1];
                thinkBubble.transform.GetChild(spritePosition).GetComponent<SpriteRenderer>().sprite = sprite;
                spritePosition++;
            }
        }
        else
        {
            spritePosition++;
            if (neededBurgers - amountOfBurgers > 0)
            {
                Sprite sprite = Camera.main.GetComponent<ZombieManager>().burgers[neededBurgers - amountOfBurgers - 1];
                thinkBubble.transform.GetChild(spritePosition).GetComponent<SpriteRenderer>().sprite = sprite;
                spritePosition++;
            }
            if (neededFries - amountOfFries > 0)
            {
                Sprite sprite = Camera.main.GetComponent<ZombieManager>().fries[neededFries - amountOfFries - 1];
                thinkBubble.transform.GetChild(spritePosition).GetComponent<SpriteRenderer>().sprite = sprite;
                spritePosition++;
            }
            if (neededDrinks - amountOfDrinks > 0)
            {
                Sprite sprite = Camera.main.GetComponent<ZombieManager>().drinks[neededDrinks - amountOfDrinks - 1];
                thinkBubble.transform.GetChild(spritePosition).GetComponent<SpriteRenderer>().sprite = sprite;
                spritePosition++;
            }
        }
    }

    void ChangeOrderAlpha()
    {
        if (alpha > 0)
        {
            alpha -= Time.deltaTime * 4 * updateInterval;
            thinkBubble.GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
            for (int i = 0; i < thinkBubble.transform.childCount; i++)
            {
                thinkBubble.transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(1, 1, 1, alpha);
            }
        }
    }

    void OrderBubbleScale()
    {
        int order = -Mathf.RoundToInt(transform.position.z * 100);
        thinkBubble.GetComponent<SpriteRenderer>().sortingOrder = order - 200;
        thinkBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order - 199;
        thinkBubble.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order - 199;
        thinkBubble.transform.GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = order - 198;
        thinkBubble.transform.GetChild(3).GetComponent<SpriteRenderer>().sortingOrder = order - 197;
        if (thinkBubble.transform.localScale.x > bubbleMinScale)
        {
            if ((head.transform.position.z - endingZ) / (startingZ - endingZ) > bubbleMinScale)
            {
                SetGlobalScale(thinkBubble, Vector3.one * ((head.transform.position.z - endingZ) / (startingZ - endingZ)) * 16);
            }
            else
            {
                SetGlobalScale(thinkBubble, Vector3.one * bubbleMinScale * 15.99f);
            }
        }
    }

    public void SetGlobalScale(GameObject obj, Vector3 globalScale)
    {
        obj.transform.localScale = Vector3.one;
        obj.transform.localScale = new Vector3(globalScale.x / obj.transform.lossyScale.x,
                                               globalScale.y / obj.transform.lossyScale.y,
                                               globalScale.z / obj.transform.lossyScale.z);
    }

    public void FreezeZombie()
    {
        Camera.main.GetComponent<SoundAndMusicManager>().PlayFreezeSound(gameObject);
        frozen = true;
        speed = 0;
        updateInterval = 15;
        frozenTime = 0;
        GetComponent<Animator>().SetFloat("Speed", 0);
        Freeze();
    }

    public void UnFreezeZombie()
    {
        if (!orderComplete)
        {
            frozen = false;
            speed = originalSpeed;
            updateInterval = 3;
            frozenTime = 0;
            GetComponent<Animator>().SetFloat("Speed", speed * animationSpeed);
            WakeUp();
        }
    }

    public void DestroyScript()
    {
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject.GetComponent<Zombie>());
    }

    void PlayGroanNoise()
    {
        if (thisZombieSize == ZombieSize.small)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieGroanSound(gameObject, 2);
        }
        else if (thisZombieSize == ZombieSize.large)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieGroanSound(gameObject, 0.75f);
        }
        else
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieGroanSound(gameObject, 1);
        }
    }

    void PlayGruntNoise()
    {
        if (thisZombieSize == ZombieSize.small)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieGruntSound(gameObject, 2);
        }
        else if (thisZombieSize == ZombieSize.large)
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieGruntSound(gameObject, 0.75f);
        }
        else
        {
            Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieGruntSound(gameObject, 1);
        }
    }

    void IncreaseIdleSoundTimer()
    {
        timerForIdleNoise += (Time.deltaTime * updateInterval);
        if (timerForIdleNoise > timeForNextIdelSound)
        {
            if (thisZombieSize == ZombieSize.small)
            {
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieIdleSound(gameObject, 2);
            }
            else if (thisZombieSize == ZombieSize.large)
            {
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieIdleSound(gameObject, 0.75f);
            }
            else
            {
                Camera.main.GetComponent<SoundAndMusicManager>().PlayZombieIdleSound(gameObject, 1);
            }
            PickNextIdleSoundTime();
        }
    }

    void PickNextIdleSoundTime()
    {
        timerForIdleNoise = 0;
        timeForNextIdelSound = Random.Range(5, 30);
    }
}
