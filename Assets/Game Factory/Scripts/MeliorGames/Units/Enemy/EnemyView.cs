using System;
using UnityEditor;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
    public class EnemyView : MonoBehaviour
    {
      public GameObject Pistol;
      public GameObject Hand;
      
      private Animator animator;
      
      private static readonly int Shoot = Animator.StringToHash("Shoot");
      private static readonly int Reload = Animator.StringToHash("Reload");
      private static readonly int Direction = Animator.StringToHash("Blend");
      private static readonly int ReadyToShoot = Animator.StringToHash("ReadyToShoot");

      private void Awake()
      {
        animator = GetComponent<Animator>();
        
        PinPistol();
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

      public void SetReadyToShoot()
      {
        animator.SetTrigger(ReadyToShoot);
      }

      private void PinPistol()
      {
        Pistol.transform.parent = Hand.transform;
        Pistol.transform.position = Hand.transform.position;
        Pistol.transform.rotation = Hand.transform.rotation;
      }
    }
}
