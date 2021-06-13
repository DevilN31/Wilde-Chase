using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerRagdoll : MonoBehaviour
  {
    public Rigidbody HipRigidBody;
    
    public Collider[] ragdollColliders;
    public Rigidbody[] ragdollRigidbodies;

    private void Awake()
    {
      DisableRagdollState();
    }

    private void DisableRagdollState()
    {
      foreach (Collider ragdollCollider in ragdollColliders)
      {
        ragdollCollider.enabled = false;
      }
      
      for (int i = 0; i < ragdollRigidbodies.Length; i++)
      {
        ragdollRigidbodies[i].constraints = RigidbodyConstraints.FreezeAll;
        ragdollRigidbodies[i].collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        ragdollRigidbodies[i].isKinematic = true;
      }
    }
    
    public void EnableRagdollState()
    {
      HipRigidBody.constraints = RigidbodyConstraints.None;

      foreach (Collider ragdollCollider in ragdollColliders)
      {
        ragdollCollider.enabled = true;
      }
      
      for (int i = 0; i < ragdollRigidbodies.Length; i++)
      {
        ragdollRigidbodies[i].isKinematic = false;
        ragdollRigidbodies[i].constraints = RigidbodyConstraints.None;
      }
    }
  }
}