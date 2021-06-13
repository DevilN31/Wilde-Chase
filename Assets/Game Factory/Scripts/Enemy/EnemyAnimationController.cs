using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] int bulletCount = 3;
    public bool reload = false;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float counter;
    [SerializeField] GameObject pistol;
    [SerializeField] Transform hand;
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    [Header("Ragdoll Settings")]
    [SerializeField] Rigidbody HipRig;

    [SerializeField] List<Collider> ragdollParts;

    Animator anim;
    CapsuleCollider col;
    Vector3 directionToTarget;
    float lastShot;
    EnemyAiTesti enemyAi;
    float angleToTarget;

    public float AngleToTarget
    {
        get { return angleToTarget; }
    }

    private void Awake()
    {
        if(GameManager.instance != null) // add self to enemy list on Game Manager
        GameManager.instance.enemiesInLevel.Add(this.gameObject);
        FindAndDisableRagdollParts();
    }

    void Start()
    {
        lastShot = Time.time;
        col = GetComponent<CapsuleCollider>();
        enemyAi = GetComponentInParent<EnemyAiTesti>();
        anim = GetComponent<Animator>();
        anim.SetInteger("BulletCount", bulletCount);
        pistol.transform.parent = hand;
        pistol.transform.position = hand.position;
        pistol.transform.rotation = hand.rotation;
    }


    void Update()
    {
        if (target == null) // updates target
            target = GameManager.instance.player.transform;

        angleToTarget = Vector3.Angle(target.forward,transform.forward) / 360;
        anim.SetFloat("Blend", angleToTarget);

        if (!col.enabled) // sometimes collider gets disabled by Animal script
        {
            col.enabled = true;
        }

        directionToTarget = target.position - transform.position;
        Debug.DrawRay(pistol.transform.position, (pistol.transform.up + directionToTarget) * 50, Color.red);

        if (GameManager.instance != null)
        {
            float distanceFromPlayer = Vector3.Distance(GameManager.instance.player.transform.position, transform.position);

            if (distanceFromPlayer > enemyAi.SphereCastRadius / 2 && !enemyAi.IsHorseRunning) // speed up if too far from player
                enemyAi.SpeedUpHorse(5);
            else if (enemyAi.IsHorseRunning)
                enemyAi.SlowDownHorse(5);
        }

        if (Vector3.Distance(target.position, transform.position) <= enemyAi.SphereCastRadius * 0.6f) // attack player
            AttemptShoot(attackSpeed);


        if (Input.GetKeyDown(KeyCode.E)) // For Testing
        {
            StartCoroutine(EnemyDeath());
        }
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

    public void SetReloadFalse() // called from animation
    {
        lastShot = Time.time;
        bulletCount = 6;
        reload = false;
    }

    public void Shoot() // called crom animation
    {
        bulletCount--;
        GameObject bullet = Instantiate(bulletPrefab, pistol.transform.position, bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = directionToTarget * 3;
        Destroy(bullet, 2);
    }

    void FindAndDisableRagdollParts()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider col in colliders)
        {
            if(col.gameObject != this.gameObject)
            {
                col.isTrigger = true;
                ragdollParts.Add(col);
            }
        }
    }

    void EnableRagdollParts() // Used in death func
    {
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        anim.enabled = false;
        foreach(Collider col in ragdollParts)
        {
            col.isTrigger = false;
        }
    }

   public IEnumerator EnemyDeath()
    {
        enemyAi.UnmountHorse();
        
        transform.parent = null;
        EnableRagdollParts();
        enemyAi.StartHorseRunAway();


        GameManager.instance.enemiesInLevel.Remove(this.gameObject);
        yield return null;
        HipRig.constraints = RigidbodyConstraints.None;
        Destroy(this.gameObject, 5f);
        this.enabled = false;
    }
}
