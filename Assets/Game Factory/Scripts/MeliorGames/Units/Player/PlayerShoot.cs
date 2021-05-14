using System;
using System.Collections.Generic;
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
    public Camera MainCamera;

    public float FireRate = 0.5f;

    public Transform SpawnTransform;
    public Vector3 TargetPosition;
    public float TargetMagnitude;

    public List<Projectile> Projectiles = new List<Projectile>();

    public float AngleInDegrees;

    public bool ReadyToShoot;

    private float speed;

    private readonly float gravity = Physics.gravity.y;

    private float lastShotTime;

    private void Start()
    {
      lastShotTime = Time.time;
    }

    private void Update()
    {
      if (EventSystem.current.IsPointerOverGameObject() ||
          EventSystem.current.currentSelectedGameObject != null)
        return;

      if(!CheckAttackCooldown())
        return;
      
      if (Input.GetMouseButtonDown(0))
      {
        TimeControl.Instance.SlowDown();
        //View.SwitchAiming(true);
      }


      if (Input.GetMouseButton(0))
      {
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
        TargetPosition = PickTarget();

        //View.PlayAiming();

        CalculateVelocity();
      }

      if (Input.GetMouseButtonUp(0))
      {
        TimeControl.Instance.SpeedUp();
        TrajectoryRenderer.HideTrajectory();
        Shot(speed);

        TargetPosition = Vector3.zero;
        TargetMagnitude = 0f;

        lastShotTime = Time.time;
        
        //View.SwitchAiming(false);
        //View.PlayThrow();
      }
    }
    
    private bool CheckAttackCooldown()
    {
      ReadyToShoot = Time.time - lastShotTime > FireRate;
      return ReadyToShoot;
    }

    private void Shot(float v)
    {
      Projectile newBullet = Instantiate(PickProjectile(), SpawnTransform.position, Quaternion.identity);
      newBullet.Thrown = true;
      newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v; //AddForce(SpawnTransform.forward * v, ForceMode.VelocityChange);
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

      TrajectoryRenderer.ShowTrajectory(transform.position, SpawnTransform.forward * speed);
    }

    private Vector3 PickTarget()
    {
      var groundPlane = new Plane(Vector3.up, Vector3.zero);
      Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

      if (groundPlane.Raycast(ray, out float position))
      {
        Vector3 touchedWorldPosition = ray.GetPoint(position);
        
        Vector3 finalVector = Vector3.zero;

        float xClamp = Mathf.Clamp(touchedWorldPosition.x, -15f, 15f);
        float yClamp = Mathf.Clamp(touchedWorldPosition.y, -15f, 15f);
        float zClamp = Mathf.Clamp(touchedWorldPosition.z, -15f, 15f);
        

        //touchedWorldPosition = new Vector3(xClamp, yClamp, zClamp);

        //TargetMagnitude = touchedWorldPosition.magnitude;
        //View.ThrowPower = Mathf.Clamp(TargetMagnitude / 15, 0f, 1f);

        //TargetMagnitude = (TargetPosition - transform.position).magnitude;

        //if (TargetMagnitude <= 16)
        {
          finalVector = touchedWorldPosition;
        }

        return finalVector;
      }

      return Vector3.zero;
    }

    private Projectile PickProjectile()
    {
      int projectileIndex = Random.Range(0, Projectiles.Count);

      return Projectiles[projectileIndex];
    }
  }
}