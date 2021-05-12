using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.CameraControl
{
  public class CameraFollow : MonoBehaviour
  {
    public Transform MovementTarget;
    public Transform RotationTarget;
    public float SmoothTime = 0.3f;
    public float RotationSpeed;
    
    public Vector3 offset;
    
    private Vector3 velocity;

    public void SetTarget(Transform target) => 
      MovementTarget = target;

    public void CalculateOffset()
    {
      offset = new Vector3(transform.position.x - MovementTarget.position.x,
        transform.position.y - MovementTarget.position.y,
        transform.position.z - MovementTarget.position.z);
    }

    private void FixedUpdate()
    {
      if (MovementTarget != null)
      {
        float positionX = Mathf.SmoothDamp(transform.position.x, MovementTarget.position.x + offset.x, ref velocity.x, SmoothTime);
        float positionY = Mathf.SmoothDamp(transform.position.y, MovementTarget.position.y + offset.y, ref velocity.y, SmoothTime);
        float positionZ = Mathf.SmoothDamp(transform.position.z, MovementTarget.position.z + offset.z, ref velocity.z, SmoothTime);

        transform.position = new Vector3(positionX, positionY, positionZ);

        //var towardsAngle = Quaternion.LookRotation(RotationTarget.position - transform.position);
        //transform.rotation = towardsAngle;
        //transform.rotation = Quaternion.Slerp(transform.rotation, RotationTarget.rotation, RotationSpeed * Time.fixedDeltaTime);
      }
    }
  }
}