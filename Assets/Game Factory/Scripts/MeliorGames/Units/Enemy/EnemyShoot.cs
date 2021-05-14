using System;
using System.Collections;
using Game_Factory.Scripts.MeliorGames.Projectiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  public class EnemyShoot : MonoBehaviour
  {
    public EnemyView View;

    public bool Reloading;
    public float FireRate;
    public int MaxBulletCount = 5;

    public EnemyProjectile BulletPrefab;
    public Transform BulletSpawn;
    public Transform Target;

    private const float ReloadDuration = 3f;
    private const float IdleDuration = 3.333f;
    private int bulletCurrentCount;
    private float lastShot;
    private float angleToTarget;

    private bool ableToShoot;

    private void Start()
    {
      bulletCurrentCount = MaxBulletCount;
      
      StartCoroutine(InitialDelay());
    }

    public void SetTarget(Transform target)
    {
      Target = target;
    }

    private void Update()
    {
      if(!ableToShoot)
        return;
      
      PickDirection();
      if (CheckAttackCooldown() && !Reloading)
      {
        Attack(PickDirection());
        CheckForReload();
      }
    }

    private bool CheckAttackCooldown()
    {
      return Time.time - lastShot > FireRate;
    }

    private void CheckForReload()
    {
      if (bulletCurrentCount <= 0)
      {
        View.PlayReload();
        StartCoroutine(ReloadingCoroutine(ReloadDuration));
      }
    }

    private IEnumerator InitialDelay()
    {
      float randomDelay = Random.Range(0f, 5f);
      yield return new WaitForSeconds(randomDelay);
      
      View.SetReadyToShoot();
      lastShot = Time.time + IdleDuration + Random.Range(0f, 5f);
      PickDirection();
      
      ableToShoot = true;
    }

    private IEnumerator ReloadingCoroutine(float duration)
    {
      Reloading = true;
      yield return new WaitForSeconds(duration);
      Reloading = false;
      bulletCurrentCount = MaxBulletCount;
    }

    private Vector3 PickDirection()
    {
      angleToTarget = Vector3.Angle(Target.forward, BulletSpawn.transform.forward) / 360;
      Vector3 targetDirection = Target.position - BulletSpawn.transform.position;
      Debug.DrawRay(BulletSpawn.transform.position, (BulletSpawn.transform.up + targetDirection) * 50f, Color.blue);

      View.AimDirection(angleToTarget);

      return targetDirection;
    }

    private void Attack(Vector3 target)
    {
      View.PlayShoot();
      bulletCurrentCount--;
      EnemyProjectile bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
      bullet.GetComponent<Rigidbody>().velocity = target * 5;
      lastShot = Time.time;
    }

    public void Shoot()
    {
    }

    public void SetReloadFalse()
    {
    }
  }
}