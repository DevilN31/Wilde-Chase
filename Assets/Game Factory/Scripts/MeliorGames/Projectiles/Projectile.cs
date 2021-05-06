using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public bool Thrown;
        public GameObject View;

        private void Update()
        {
            if (Thrown)
            {
                View.transform.eulerAngles += (Vector3.up + Vector3.right) * 2;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyAnimationController>())
            {
                Debug.Log($"Prop {name} collided with {collision.gameObject.name}");
            
                //collision.gameObject.GetComponent<EnemyAnimationController>().EnemyDeath();
                collision.gameObject.GetComponent<EnemyAnimationController>().StartCoroutine("EnemyDeath");
            }
        }
    }
}
