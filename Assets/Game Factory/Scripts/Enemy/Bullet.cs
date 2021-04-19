using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float speed = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if(!collision.gameObject.GetComponent<PlayerController>())
            Destroy(this.gameObject);

        Destroy(this.gameObject,5f);

    }
}
