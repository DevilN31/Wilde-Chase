using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingProp : MonoBehaviour
{
    
    Rigidbody rig;
    [SerializeField] int propId;
    [SerializeField] bool isCollided = false;

    [SerializeField]Transform _parentTransform;
    Collider col;
    Quaternion originalRotation;
    bool isResetRotation = true;

    public int PropId
    {
        get { return propId; }
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        originalRotation = transform.localRotation;
    }

    void Start()
    {
        Debug.Log($"Prop: Created {name}");
        
    }

    private void Update()
    {


        if (this.gameObject.activeSelf && !isCollided)
        {
            if (_parentTransform != null)
            {
                transform.eulerAngles += Vector3.up * 2;

                transform.position = _parentTransform.position + (Vector3.up * 0.3f);
                transform.localScale = Vector3.one * 0.3f;
                isResetRotation = false;
            }
            else
            {
                transform.localScale = Vector3.one;
                rig.freezeRotation = false;
                transform.eulerAngles += (Vector3.up + Vector3.right) * 2;
            }
        }
    }

    public void ThrowMe(Vector3 throwForce, Vector3 startPosition)
    {       
        transform.position = startPosition;
        _parentTransform = null;
        col.isTrigger = false;
        rig.useGravity = true;
        rig.velocity = throwForce;
    }

    public void Activate(Transform parentTransform)
    {
        gameObject.SetActive(true);
        col.isTrigger = true;
        transform.rotation = originalRotation;
        rig.freezeRotation = true;
        _parentTransform = parentTransform;
        isCollided = false;
        isResetRotation = true;
        rig.useGravity = false;
    }

    IEnumerator Deactivate()
    {       
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        isCollided = true;

        if (collision.gameObject.GetComponent<EnemyAnimationController>())
        {
            Debug.Log($"Prop {name} collided with {collision.gameObject.name}");
            
            //collision.gameObject.GetComponent<EnemyAnimationController>().EnemyDeath();
            collision.gameObject.GetComponent<EnemyAnimationController>().StartCoroutine("EnemyDeath");
        }
        StartCoroutine(Deactivate());
        
    }
}
