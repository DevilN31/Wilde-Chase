using System;
using System.Collections;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using Game_Factory.Scripts.MeliorGames.Projectiles;
using Game_Factory.Scripts.MeliorGames.TimeService;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerShoot : MonoBehaviour
  {
    public PlayerView View;

    public TrajectoryRenderer TrajectoryRenderer;
    private Camera mainCamera;

    public float FireRate = 0.5f;
    public float MaxSpeed = 16f;

    public Transform SpawnTransform;
    public Vector3 TargetPosition;

    public List<Projectile> Projectiles = new List<Projectile>();

    public float AngleInDegrees;
    public bool ReadyToShoot;

    private float speed;
    private readonly float gravity = Physics.gravity.y;
    private float lastShotTime;

    private bool buttonDowned;

    private LevelContainer levelContainer;
    private List<Projectile> thrownProjectiles = new List<Projectile>();

    private void Awake()
    {
      lastShotTime = Time.time + 1.5f;
    }

    public void Init(LevelContainer _levelContainer, Camera camera)
    {
      levelContainer = _levelContainer;
      mainCamera = camera;
      
      foreach (Level level in levelContainer.Levels)
      {
        level.Finished += DestroyProjectiles;
      }
    }

    private void Update()
    {
      if (EventSystem.current.IsPointerOverGameObject() ||
          EventSystem.current.currentSelectedGameObject != null)
        return;

      if (CheckAttackCooldown())
      {
        if (Input.GetMouseButtonDown(0))
        {
          buttonDowned = true;
          //Debug.Log("Mouse down");
          TimeControl.Instance.SlowDown();
          View.SwitchAiming(true);
        }

        if (Input.GetMouseButton(0) && buttonDowned)
        {
          SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
          PickTarget();

          CalculateVelocity();
        }

        if (Input.GetMouseButtonUp(0) && buttonDowned)
        {
          //Debug.Log("Mouse up");
          buttonDowned = false;
          TimeControl.Instance.SpeedUp();
          TrajectoryRenderer.HideTrajectory();
          ShotStart(speed);

          TargetPosition = Vector3.zero;
          lastShotTime = Time.time;

          View.SwitchAiming(false);
        }
      }
    }

    private bool CheckAttackCooldown()
    {
      ReadyToShoot = Time.time - lastShotTime > FireRate;
      return ReadyToShoot;
    }

    private void ShotStart(float v)
    {
      Projectile newBullet = Instantiate(PickProjectile(), SpawnTransform.position, Quaternion.identity);
      newBullet.Thrown = true;
      newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;
      thrownProjectiles.Add(newBullet);//AddForce(SpawnTransform.forward * v, ForceMode.VelocityChange);
      //PlayerController.ThrowProps(SpawnTransform.forward * v, SpawnTransform.position);
    }

    private void CalculateVelocity()
    {
      Vector3 fromTo = TargetPosition - transform.position;
      Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

      transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

      float x = fromToXZ.magnitude;
      float y = fromTo.y;

      float angleInRadians = AngleInDegrees * Mathf.PI / 180;

      float v2 = (gravity * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
      speed = Mathf.Sqrt(Mathf.Abs(v2));
      speed = Mathf.Clamp(speed, 0f, MaxSpeed);
      //Debug.Log(speed / MaxSpeed);
      View.ControlThrow(speed / MaxSpeed);

      TrajectoryRenderer.ShowTrajectory(transform.position, SpawnTransform.forward * speed);
    }

    private void PickTarget()
    {
      var groundPlane = new Plane(Vector3.up, Vector3.zero);
      Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

      if (groundPlane.Raycast(ray, out float position))
      {
        Vector3 touchedWorldPosition = ray.GetPoint(position);

        TargetPosition = touchedWorldPosition;
      }
    }

    private Projectile PickProjectile()
    {
      int projectileIndex = Random.Range(0, Projectiles.Count);

      return Projectiles[projectileIndex];
    }

    public void SetReload(float reloadTime)
    {
      lastShotTime = Time.time + reloadTime;
    }

    private void DestroyProjectiles(Level level)
    {
      for (int i = thrownProjectiles.Count - 1; i >= 0; i--)
      {
        Destroy(thrownProjectiles[i].gameObject);
        thrownProjectiles.Remove(thrownProjectiles[i]);
      }
    }
  }
}