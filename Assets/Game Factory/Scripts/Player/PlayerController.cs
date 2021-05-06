using MalbersAnimations;
using MalbersAnimations.Controller;
using MalbersAnimations.HAP;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [Header("Editor Properties")]
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform playerWagonPosition;
    [SerializeField] Transform hand;
    [SerializeField] Slider hpSlider;
    [SerializeField] GameObject wagonFence;
    [SerializeField] float rotationSpeed = 1.5f;
    [SerializeField] float senseivity = 1;
    [SerializeField] int invertPitch = 1;
    public bool slingshotControl = true;
    [SerializeField] List<Collider> ragdollParts;
    [SerializeField] Rigidbody hipsRig;
    [SerializeField] GameObject wagon;
    [Header("Stats")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;
    [SerializeField] int forceApplied;
    [SerializeField] bool isAiming;
    [SerializeField] bool isCrouching;
    [Header("Sphere Cast Properties")]
    [SerializeField] float sphereCastRadius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] MAnimalAIControl MAnimalAI;
    [SerializeField] MAnimal mAnimal;
    [Header("Throwable")]
    [SerializeField] int selectedProp = -1;
    [SerializeField] List<ThrowingProp> throwableProps;
    [SerializeField] List<ThrowingProp> throwablePool;

    Animator anim;
    CapsuleCollider col;
    Vector3 forceApllied;
    Vector2 dragDir;
    float yaw = 0;
    float pitch = 0;
    float originalRatationSpeed;
    TrajectoryPredictor tp;
    Rigidbody rig;

    public float Sensetivity
    {
        get { return senseivity; }
    }

    public Animator WagonFenceAnimator
    {
        get { return wagonFence.GetComponent<Animator>(); }
    }

    private void Awake()
    {
        if(GameManager.instance != null)
        GameManager.instance.player = this.gameObject;

        isCrouching = true;
        isAiming = false;
    }

    void Start()
    {
        if (UiManager.instance != null)
        {
            if (!UiManager.instance.DisabledScripts.Exists(mono => mono == this))
                UiManager.instance.AddSelfToDisabledList(this);
        }
        tp = GetComponent<TrajectoryPredictor>();
        originalRatationSpeed = 2f;
        rotationSpeed = originalRatationSpeed * senseivity;
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        hpSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        UpdateHpSlider();
        FindAndDisableRagdollParts();
        InstantiateProps();
        SetClosestWaypoint();

        if (DataManager.instance.GetPlayerPrefsBool("SlingshotControl") != -1) // Updates movement control type
        {
            if (DataManager.instance.GetPlayerPrefsBool("SlingshotControl") == 1)
                slingshotControl = true;
            else
                slingshotControl = false;
        }      
    }

    void Update()
    {
      
        if (currentHealth <= 0) 
        {
            Debug.Log("Player is dead!");
            if (!UiManager.instance.MenuPanel.activeSelf)
                UiManager.instance.ShowMenu();
        }

        if (isCrouching ) // Checks if player is crouching -> updates player colider height
        {
            col.height = 0.7f;
        }
        else
            col.height = 1.6f;

        //if (playerWagonPosition != null) // ONLY FOR TESTING!!!
        {
            transform.parent = playerWagonPosition;

            transform.position = transform.parent.position;

            //if(isCrouching)
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, transform.parent.eulerAngles, 0.1f);          
        }

    }

    private void FixedUpdate()
    {
        
        // if (UiManager.instance.TouchField != null)
        // {
        //     if (UiManager.instance.TouchField.Pressed) // Checkes if player touched the screen -> starts rotation control
        //     {
        //         isCrouching = false;
        //         dragDir = UiManager.instance.TouchField.TouchDir;
        //         yaw += dragDir.x * rotationSpeed * Time.fixedDeltaTime;
        //         yaw = Mathf.Clamp(yaw, -45, 45);
        //
        //         forceApplied = (int)(UiManager.instance.TouchField.TouchDist * Time.fixedDeltaTime);
        //
        //         Time.timeScale = 0.5f; // Slows time while aiming
        //         anim.speed = 2f;
        //         isAiming = true;
        //         anim.SetBool("isAiming", true);
        //
        //         Vector3 RotateTo = transform.localEulerAngles;
        //         if (slingshotControl)
        //             RotateTo.y = -yaw;
        //         else
        //             RotateTo.y = yaw;
        //
        //         transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles,RotateTo,1);
        //
        //         forceApllied = transform.forward * forceApplied + transform.up * 2;
        //
        //     }
        //     else if (isAiming && !UiManager.instance.TouchField.Pressed) // resets variables if not aiming 
        //     {
        //         Time.timeScale = 1;
        //         anim.speed = 1f;
        //         isAiming = false;
        //         yaw = 0;
        //         anim.SetBool("isAiming", false);
        //         anim.SetTrigger("Throw");
        //     }
        // }
        
    }

    /*void LateUpdate()
    {
        if (isAiming)
        {
            //set line duration to delta time so that it only lasts the length of a frame
            tp.debugLineDuration = Time.unscaledDeltaTime;
            //tell the predictor to predict a 3d line. this will also cause it to draw a prediction line
            //because drawDebugOnPredict is set to true
            tp.Predict3D(hand.position, transform.forward * forceApplied, Physics.gravity);
        }
    }*/

        public void ThrowProps(Vector3 force, Vector3 position) // Player attack (called from animation)
    {
        //int randomProp = SelectPropIndex();
        //throwablePool[randomProp].gameObject.SetActive(true);
        //throwablePool[randomProp].transform.position = hand.position;
        //throwablePool[randomProp].transform.rotation = transform.rotation;
        //throwablePool[randomProp].ThrowMe(forceApllied);
        //throwablePool[selectedProp].ThrowMe(forceApllied);
        SelectPropIndex();
        throwablePool[selectedProp].ThrowMe(force, position);
        selectedProp = -1;
        isCrouching = true;
    }

    int SelectPropIndex() // selects the next prop from the prop list
    {
        selectedProp = Random.Range(0, throwablePool.Count);

        while (throwablePool[selectedProp].gameObject.activeSelf)
        {
            selectedProp = Random.Range(0, throwablePool.Count);
        }

        return selectedProp;
    }

    public void ShowSelectedProp() // shows the selected prop in-game
    {
        if (selectedProp == -1)
        {
            selectedProp = SelectPropIndex();
            throwablePool[selectedProp].Activate(hand.transform);
        }
        else
            Debug.Log("PlayerController: couldn't show selected prop");
    }


    IEnumerator ThrowStuffSequens() // was used to test props, no longer in use.
    {
        for(int i = 0; i < 25; i++)
        {
            anim.SetBool("isAiming", true);
            isCrouching = false;
            anim.SetTrigger("Throw");
            yield return new WaitForSeconds(3);
        }
    }

    void InstantiateProps() // Instantiates all the props from the throwableProps list
    {
        foreach(ThrowingProp t in throwableProps)
        {
            ThrowingProp temp = Instantiate(t, hand.position, transform.rotation);
            throwablePool.Add(temp);
            temp.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHpSlider();
    }

    void UpdateHpSlider()
    {
        if(hpSlider != null)
        hpSlider.value = currentHealth / maxHealth;
    }

    public void ChangeSensetivity(float newSensetivity)
    {
        senseivity = newSensetivity;
        DataManager.instance.SetPlayerPrefsfloat("Sensetivity", senseivity);
        rotationSpeed = originalRatationSpeed * senseivity;
    }

    public void ChangeControlType()
    {
        slingshotControl = !slingshotControl;
        DataManager.instance.SetPlayerPrefsBool("SlingshotControl", slingshotControl);
        Debug.Log($"Player Controller: Slingshot change {slingshotControl}");
    }

    public void SetClosestWaypoint() // Sets target point for AnimalAi script 
    {
        RaycastHit hit;
        Debug.DrawRay(playerWagonPosition.position, -playerWagonPosition.forward * sphereCastRadius, Color.magenta, 10f);
        if (Physics.SphereCast(playerWagonPosition.position, sphereCastRadius, -playerWagonPosition.forward, out hit, 100f, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            if (hit.collider.gameObject.GetComponent<MWayPoint>())
            {
                MAnimalAI.SetTarget(hit.collider.gameObject.transform, true);
                MAnimalAI.enabled = true;
                Debug.Log($"Player Controller: {name} closest Waypoint is: {hit.collider.name}");
            }
            else
                Debug.Log($"Player Controller: {name} can't find closest waypoint, hit: {hit.collider.name}");
        }
        else
        {
            Debug.Log($"Player Controller: Didn't hit anything!");
        }
    }

    public IEnumerator PlayerEndDeath()
    {
        playerCamera.gameObject.GetComponent<CameraControl>().MoveToTransform();

        MAnimalAI.enabled = false;
        mAnimal.SpeedSet_Get("Ground").Speeds[2].Vertical.Value += 10;

        yield return new WaitForSeconds(3f);

        playerWagonPosition = null;
        transform.parent = null;
        EnableRagdollParts();
        anim.enabled = false;

         yield return null;
        hipsRig.constraints = RigidbodyConstraints.None;
        wagon.GetComponent<ConfigurableJoint>().breakForce = 0.001f;
        hipsRig.AddForce((Vector3.forward + Vector3.up) * 250, ForceMode.Impulse);
        yield return new WaitForSeconds(2.5f);
        UiManager.instance.ShowMenu();
    }

    void FindAndDisableRagdollParts() // finds all Ragdoll parts in player
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            if (col.gameObject != this.gameObject)
            {
                col.isTrigger = true;
                ragdollParts.Add(col);
            }
        }
    }

    void EnableRagdollParts()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        anim.enabled = false;
        foreach (Collider col in ragdollParts)
        {
            col.isTrigger = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        if(playerWagonPosition != null)
        Handles.DrawWireDisc(playerWagonPosition.position, Vector3.up, sphereCastRadius);
    }
#endif
}
