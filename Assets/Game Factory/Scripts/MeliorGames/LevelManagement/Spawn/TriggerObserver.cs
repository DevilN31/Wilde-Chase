using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  [RequireComponent(typeof(Collider))]
  public class TriggerObserver : MonoBehaviour
  {
    public Color GizmoColor;

    public BoxCollider Collider;

    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;

    private bool triggerEntered;

    private void OnTriggerEnter(Collider other)
    {
      if (!triggerEntered)
      {
        triggerEntered = true;
        TriggerEnter?.Invoke(other);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (triggerEntered)
      {
        triggerEntered = false;
        TriggerExit?.Invoke(other);
      }
    }

    private void OnDrawGizmos()
    {
      if (!Collider)
        return;

      Gizmos.color = GizmoColor;
      Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
    }
  }
}