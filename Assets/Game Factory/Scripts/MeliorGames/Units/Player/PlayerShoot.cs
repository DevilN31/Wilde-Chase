using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Projectiles;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerShoot : MonoBehaviour
  {
    public TrajectoryRenderer TrajectoryRenderer;
    public Camera MainCamera;

    public Transform SpawnTransform;
    public Vector3 TargetPosition;
    
    public List<Projectile> Projectiles = new List<Projectile>();

    public float AngleInDegrees;

    private float speed;

    private readonly float gravity = Physics.gravity.y;
    
    private void Update()
    {
      if (Input.GetMouseButton(0))
      {
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
        TargetPosition = PickTarget();
        Time.timeScale = 0.35f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    
        CalculateVelocity();
      }
    
      if (Input.GetMouseButtonUp(0))
      {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        TrajectoryRenderer.HideTrajectory();
        Shot(speed);
      }
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

        return new Vector3(touchedWorldPosition.x, touchedWorldPosition.y, touchedWorldPosition.z);
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