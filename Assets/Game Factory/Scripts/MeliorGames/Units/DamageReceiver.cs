using System;
using System.Collections;
using Game_Factory.Scripts.MeliorGames.Projectiles;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units
{
  public class DamageReceiver : MonoBehaviour
  {
    public Team Team;
    public Collider Collider;

    public Action DamageReceived;

    private void Start()
    {
      StartCoroutine(TurnOnCollider());
    }

    private void OnCollisionEnter(Collision other)
    {
      if (!other.collider.TryGetComponent(out Projectile projectile)) return;
      if (projectile.Team == Team) return;

      projectile.Rigidbody.velocity = Vector3.zero;
      projectile.Thrown = false;
      
      Debug.Log("Damage Received");
      DamageReceived?.Invoke();
    }

    private IEnumerator TurnOnCollider()
    {
      yield return new WaitForSeconds(0.2f);
      Collider.enabled = true;
    }
  }
}
