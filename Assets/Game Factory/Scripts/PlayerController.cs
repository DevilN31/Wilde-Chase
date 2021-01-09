using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerWagonPosition;
    [SerializeField] Transform hand;
    [Header("Stats")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;
    [SerializeField] float throwForce = 10f;
    [SerializeField] bool isAiming;
    [SerializeField] bool isCrouching;
    [Header("Throwable")]
    [SerializeField] List<ThrowingProp> throwableProps;
    [SerializeField] List<ThrowingProp> throwablePool;

    Animator anim;
    CapsuleCollider col;

    float rotX = 0;

    public float ThrowForce
    {
        get { return throwForce; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        isCrouching = true;
        isAiming = false;
        currentHealth = maxHealth;
        InstantiateProps();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            Destroy(this.gameObject);
        }

        if (isCrouching)
            col.height = 0.7f;
        else
            col.height = 1.6f;

        if (playerWagonPosition != null) // ONLY FOR TESTING!!!
        {
            transform.parent = playerWagonPosition;

            transform.position = transform.parent.position;
            if(!isAiming)
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.rotation, 0.5f);
            
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //anim.SetTrigger("Throw");
            StartCoroutine(ThrowStuffSequens());
        }

        if (Input.GetMouseButton(0))
        {
            Time.timeScale = 0.5f;
            isAiming = true;
            anim.SetBool("isAiming", isAiming);
            isCrouching = false;
            rotX += Input.GetAxis("Mouse X") * 5f;
            rotX = Mathf.Clamp(rotX, -45f, 45f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotX, transform.localEulerAngles.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;
            anim.SetBool("isAiming", false);
            anim.SetTrigger("Throw");
        }
    }

   public void ThrowProps()
    {
        int randomProp = Random.Range(0, throwablePool.Count);
        /*
        if (throwablePool.)
        {
            // Call Throw func!
        }
        else
        {
            ThrowingProp temp = Instantiate(throwableProps[randomProp], hand.position, transform.rotation);
            // temp.gameObject.GetComponent<Rigidbody>().velocity = temp.transform.forward * throwForce;
            throwablePool.Add(temp);
            temp.ThrowMe(ThrowForce);
        }
        */
        if (!throwablePool[randomProp].gameObject.activeSelf)
        {
            throwablePool[randomProp].gameObject.SetActive(true);
            throwablePool[randomProp].transform.position = hand.position;
            throwablePool[randomProp].transform.rotation = transform.rotation;
            throwablePool[randomProp].ThrowMe(throwForce);
        }

        isAiming = false;
        isCrouching = true;
    }

    IEnumerator ThrowStuffSequens()
    {
        for(int i = 0; i < 25; i++)
        {
            anim.SetTrigger("Throw");
            yield return new WaitForSeconds(3);
        }
    }

    void InstantiateProps()
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
    }
}
