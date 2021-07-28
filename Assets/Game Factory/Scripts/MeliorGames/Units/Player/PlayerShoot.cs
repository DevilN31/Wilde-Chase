using System;
using System.Collections;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Audio;
using Game_Factory.Scripts.MeliorGames.Infrastructure.StaticData;
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
    public ThrowPreview ThrowPreview;

    public TrajectoryRenderer TrajectoryRenderer;
    private Camera mainCamera;

    [Range(0.1f, 2f)] public float Sensitivity = 1f;
    public float FireRate = 0.5f;
    public float MaxSpeed = 16f;

    public Transform SpawnTransform;
    public Vector3 TargetPosition;

    public List<Projectile> Projectiles = new List<Projectile>();

    public float AngleInDegrees;
    public bool ReadyToShoot;

    public bool IsInputInverted;

    private float speed;
    private readonly float gravity = Physics.gravity.y;
    private float lastShotTime;

    private bool buttonDowned;

    private Projectile pickedProjectile;

    private LevelContainer levelContainer;
    private List<Projectile> thrownProjectiles = new List<Projectile>();

    private bool isFirstAttack = true;

    public event Action<Projectile> ProjectilePicked;
    public event Action ProjectileThrown;

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

          if (isFirstAttack)
          {
            isFirstAttack = false;
            AudioService.Instance.PlaySound(EAudio.CombativeRoar);
          }

          ProjectilePicked?.Invoke(pickedProjectile = PickProjectile());
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
          buttonDowned = false;
          AudioService.Instance.PlaySound(EAudio.PlayerThrow);
          TimeControl.Instance.SpeedUp();
          TrajectoryRenderer.HideTrajectory();
          ProjectileThrown?.Invoke();
          Shot(speed);

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

    private void Shot(float v)
    {
      Projectile newBullet = Instantiate(pickedProjectile, SpawnTransform.position, Quaternion.identity);
      newBullet.Thrown = true;
      newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;
      thrownProjectiles.Add(newBullet);
    }

    private void CalculateVelocity()
    {
      Vector3 fromTo = TargetPosition - SpawnTransform.position;
      Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

      transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

      float x = fromToXZ.magnitude;
      float y = fromTo.y;

      float angleInRadians = AngleInDegrees * Mathf.PI / 180;

      float v2 = (gravity * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
      speed = Mathf.Sqrt(Mathf.Abs(v2));
      speed = Mathf.Clamp(speed, 0f, MaxSpeed);

      View.ControlThrow(speed / MaxSpeed);
      
      Debug.DrawRay(transform.position, SpawnTransform.forward * speed, Color.red);

      TrajectoryRenderer.ShowTrajectory(SpawnTransform.position, SpawnTransform.forward * speed);
    }

    private void PickTarget()
    {
      var groundPlane = new Plane(Vector3.up, Vector3.zero);
      var input = Input.mousePosition;

      input.y *= Sensitivity;

      if (IsInputInverted)
      {
        input.y = Screen.height / 2 - input.y;
        input.y = Mathf.Clamp(input.y, 0f, Screen.height / 2);
        input.x = Screen.width - input.x;
      }

     // Debug.Log(input);

      Ray ray = mainCamera.ScreenPointToRay(input);

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