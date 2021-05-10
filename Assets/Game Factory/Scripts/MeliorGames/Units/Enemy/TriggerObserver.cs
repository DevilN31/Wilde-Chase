using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  [RequireComponent(typeof(Collider))]
  public class TriggerObserver : MonoBehaviour
  {
    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;

    private void OnTriggerEnter(Collider other) =>
      TriggerEnter?.Invoke(other);

    private void OnTriggerExit(Collider other) =>
      TriggerExit?.Invoke(other);
  }
}