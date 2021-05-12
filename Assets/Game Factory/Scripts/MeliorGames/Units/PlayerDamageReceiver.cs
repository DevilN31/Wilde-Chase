using System;
using Game_Factory.Scripts.MeliorGames.Projectiles;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units
{
  public class PlayerDamageReceiver : MonoBehaviour
  {
    public Collider Collider;

    public Action DamageReceived;

    private void OnCollisionEnter(Collision other)
    {
      if(!other.collider.TryGetComponent(out EnemyProjectile projectile)) return;
      
      DamageReceived?.Invoke();
    }
  }
}