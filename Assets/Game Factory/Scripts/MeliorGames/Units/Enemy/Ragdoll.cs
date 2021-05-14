using System.Collections;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  public class Ragdoll : MonoBehaviour
  {
    public Rigidbody HipRigidBody;
    
    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;

    private void Awake()
    {
      FindRagdollPColliders();
      FindRagdollRigidbodies();
      DisableRagdollState();
    }

    private void FindRagdollPColliders()
    {
      ragdollColliders = GetComponentsInChildren<Collider>();
    }

    private void FindRagdollRigidbodies()
    {
      ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }
    
    private void DisableRagdollState()
    {
      foreach (Collider ragdollCollider in ragdollColliders)
      {
        ragdollCollider.enabled = false;
      }
      
      for (int i = 1; i < ragdollRigidbodies.Length; i++)
      {
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
      
      for (int i = 1; i < ragdollRigidbodies.Length; i++)
      {
        ragdollRigidbodies[i].isKinematic = false;
      }
    }
  }
}