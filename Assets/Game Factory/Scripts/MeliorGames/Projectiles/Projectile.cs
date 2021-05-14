using System;
using Game_Factory.Scripts.MeliorGames.Units;
using Microsoft.Win32;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Factory.Scripts.MeliorGames.Projectiles
{
  public class Projectile : MonoBehaviour
  {
    public Team Team;
    public bool Thrown;
    public GameObject View;

    public TrailRenderer Trail;
    public Rigidbody Rigidbody;
    
    private Vector3 rotationDirection = Vector3.zero;
    private float rotationPower;

    private void Awake()
    {
      Trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
      //rotationDirection = new Vector3(Random.Range(0, 2) ? , Random.Range(-1f, 1f), 0);
      //rotationPower = Random.Range(1f, 4f);
    }

    private void Update()
    {
      if (Thrown)
      {
        View.transform.eulerAngles += (Vector3.up + Vector3.right) * 1; //rotationDirection * rotationPower; 
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