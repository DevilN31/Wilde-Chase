using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingProp : MonoBehaviour
{
    
    Rigidbody rig;
    [SerializeField] int propId;

    public int PropId
    {
        get { return propId; }
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Debug.Log($"Created: {name}");
        
    }

    public void ThrowMe(float throwForce)
    {
        rig.velocity = transform.forward * throwForce + transform.up * 2;
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAnimationController>())
        {
            Destroy(collision.gameObject);
        }
        StartCoroutine(Deactivate());
        
    }
}
