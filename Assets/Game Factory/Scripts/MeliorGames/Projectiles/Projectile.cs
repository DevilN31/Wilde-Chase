using System;
using Game_Factory.Scripts.MeliorGames.Units;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Projectiles
{
  public class Projectile : MonoBehaviour
  {
    public Team Team;
    public bool Thrown;
    public GameObject View;

    public TrailRenderer Trail;
    public Rigidbody Rigidbody;

    private void Awake()
    {
      Trail = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
      if (Thrown)
      {
        View.transform.eulerAngles += (Vector3.up + Vector3.right) * 1;
      }
    }

    private void OnCollisionEnter(Collision other)
    {
      //if (other.gameObject.CompareTag("Ground"))
      {
        Thrown = false;
        //Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        Rigidbody.velocity = Vector3.zero;
        Trail.enabled = false;
      }
    }
  }
}