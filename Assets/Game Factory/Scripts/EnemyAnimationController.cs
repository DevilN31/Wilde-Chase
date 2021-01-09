using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] int bulletCount = 6;
    [SerializeField] bool canShoot = false;
    public bool reload = false;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float counter;
    [SerializeField] GameObject pistol;
    [SerializeField] Transform hand;
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    Animator anim;
    CapsuleCollider col;
    Vector3 directionToTarget;
    float lastShot;


    void Start()
    {
        if(target == null)
        target = GameObject.FindGameObjectWithTag("Player").transform;

        lastShot = Time.time;
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        anim.SetInteger("BulletCount", bulletCount);
        pistol.transform.parent = hand;
        pistol.transform.position = hand.position;
        pistol.transform.rotation = hand.rotation;
    }


    void Update()
    {
        if (!col.enabled)
        {
            col.enabled = true;
            Debug.Log("Enemy: Collider enabled!");
        }

        directionToTarget = target.position - transform.position;
        Debug.DrawRay(pistol.transform.position, (pistol.transform.up + directionToTarget) * 50, Color.red);

        AttemptShoot(attackSpeed);

        /*
        if((Time.time - lastShot) > attackSpeed && !reload)
        {
            if (bulletCount > 0)
            {
                anim.SetTrigger("Shoot");
                lastShot = Time.time;
            }
            else
                reload = true;
        }

        if (reload)
        {
            // reload = false;
            // StartCoroutine(Reload());
            canShoot = false;
            anim.SetTrigger("Reload");
        }
        */  
        
        /*
        if (canShoot)
        {
            anim.SetTrigger("Shoot");
        }
        */
       
    }

    void AttemptShoot(float shootSpeed)
    {
        if (bulletCount <= 0 && !reload)
        {
            reload = true;
            anim.SetTrigger("Reload");
            return;
        }

        if ((Time.time - lastShot) < shootSpeed || reload)
        {
            return;
        }

        lastShot = Time.time;
        anim.SetTrigger("Shoot");
    }

    public void SetReloadFalse()
    {
        lastShot = Time.time;
        bulletCount = 6;
        //canShoot = true;
        reload = false;
    }

    public void Shoot()
    {
        bulletCount--;
        Debug.Log("Enemy is shooting");
        GameObject bullet = Instantiate(bulletPrefab, pistol.transform.position, bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = directionToTarget * 3;
        Destroy(bullet, 2);
        // anim.SetInteger("BulletCount", bulletCount);
        canShoot = false;
    }

    /*
    IEnumerator Reload()
    {
        Debug.Log("Reload");
        canShoot = false;
        yield return new WaitForSeconds(3.2f);
        bulletCount = 6;
        //  anim.SetInteger("BulletCount", bulletCount);
        anim.SetTrigger("Reload");
        counter = 0;
        canShoot = true;
        reload = false;


    }
    */
}
