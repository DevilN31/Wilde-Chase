using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] int bulletCount = 6;
    [SerializeField] bool canShoot = false;
    [SerializeField] bool reload;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float counter;
    [SerializeField] GameObject pistol;
    [SerializeField] Transform hand;
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("BulletCount", bulletCount);
        pistol.transform.parent = hand;
        pistol.transform.position = hand.position;
        pistol.transform.rotation = hand.rotation;
    }


    void Update()
    {

        if(!canShoot && !reload)
        counter += Time.deltaTime;

        if(counter >= attackSpeed)
        {
            if (bulletCount > 0)
            {
                canShoot = true;
                counter = 0;
            }
            else
                reload = true;
        }

        if (reload && !canShoot)
        {
            reload = false;
            StartCoroutine(Reload());
        }
        
        Vector3 directionToTarget = target.position - transform.position;
        Debug.DrawRay(pistol.transform.position, (pistol.transform.up + directionToTarget) * 50, Color.red);

        if (canShoot && !reload)
        {
            anim.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, pistol.transform.position, bulletPrefab.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = directionToTarget;
            Destroy(bullet, 2);
            bulletCount--;
            anim.SetInteger("BulletCount", bulletCount);
            canShoot = false;
        }
       
    }

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
}
