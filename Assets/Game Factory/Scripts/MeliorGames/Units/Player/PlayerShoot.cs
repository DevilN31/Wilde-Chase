using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerShoot : MonoBehaviour
  {
    public TrajectoryRenderer TrajectoryRenderer;

    public Transform SpawnTransform;
    public Vector3 TargetPosition;

    public GameObject Projectile;

    public float AngleInDegrees;

    private float speed;

    private float g = Physics.gravity.y;
  
    public float Power = 100;


    private void Update()
    {
      if (Input.GetMouseButton(0))
      {
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
        TargetPosition = PickTarget();
    
        CalculateVelocity();
      }
    
      if (Input.GetMouseButtonUp(0))
      {
        TrajectoryRenderer.HideTrajectory();
        Shot(speed);
      }
    }

    private void Shot(float v)
    {
      GameObject newBullet = Instantiate(Projectile, SpawnTransform.position, Quaternion.identity);
      //newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;
      newBullet.GetComponent<Rigidbody>().AddForce(SpawnTransform.forward * v, ForceMode.VelocityChange);
    }

    private void CalculateVelocity()
    {
      Vector3 fromTo = TargetPosition - transform.position;
      Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

      transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

      float x = fromToXZ.magnitude;
      float y = fromTo.y;

      float angleInRadians = AngleInDegrees * Mathf.PI / 180;

      float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
      speed = Mathf.Sqrt(Mathf.Abs(v2));

      TrajectoryRenderer.ShowTrajectory(transform.position, SpawnTransform.forward * speed);
    }

    private Vector3 PickTarget()
    {
      var groundPlane = new Plane(Vector3.up, Vector3.zero);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (groundPlane.Raycast(ray, out float position))
      {
        Vector3 touchedWorldPosition = ray.GetPoint(position);

        return new Vector3(touchedWorldPosition.x, touchedWorldPosition.y, touchedWorldPosition.z);
      }

      return Vector3.zero;
    }
  }
}