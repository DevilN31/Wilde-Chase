using System;
using Game_Factory.Scripts.MeliorGames.Units;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Projectiles
{
  public class EnemyProjectile : MonoBehaviour
  {
    public Rigidbody Rigidbody;

    private void OnCollisionEnter(Collision other)
    {
      //Debug.Log(other.gameObject.name);
     Destroy(gameObject);
    }
  }
}