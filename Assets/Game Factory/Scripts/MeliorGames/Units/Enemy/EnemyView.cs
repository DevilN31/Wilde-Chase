using System;
using UnityEditor;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
    public class EnemyView : MonoBehaviour
    {
      private Animator animator;
      
      private static readonly int Shoot = Animator.StringToHash("Shoot");
      private static readonly int Reload = Animator.StringToHash("Reload");
      private static readonly int Direction = Animator.StringToHash("Blend");

      private void Awake()
      {
        animator = GetComponent<Animator>();
      }

      public void AimDirection(float direction)
      {
        animator.SetFloat(Direction, direction);
      }

      public void PlayShoot()
      {
        animator.SetTrigger(Shoot);
      }

      public void PlayReload()
      {
        animator.SetTrigger(Reload);
      }
    }
}
