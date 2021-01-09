using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerWagonPosition;
    [SerializeField] Transform hand;
    [SerializeField] float throwForce = 10f;
    [SerializeField] bool isAiming;
    [SerializeField] List<ThrowingProp> throwableProps;
    [SerializeField] List<ThrowingProp> throwablePool;

    Animator anim;

    float rotX = 0;

    public float ThrowForce
    {
        get { return throwForce; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        isAiming = false;
        InstantiateProps();
    }

    void Update()
    {
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
            Time.timeScale = 0.7f;
            isAiming = true;
            rotX += Input.GetAxis("Mouse X") * 5f;
            rotX = Mathf.Clamp(rotX, -45f, 45f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotX, transform.localEulerAngles.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;
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
}
