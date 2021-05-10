using System;
using MalbersAnimations.HAP;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  public class EnemyController : MonoBehaviour
  {
    public MRider Rider;
    public Animator Animator;
    public DamageReceiver DamageReceiver;
    public EnemyPlayerFollow PlayerFollow;
    public EnemyShoot EnemyShoot;
    public Aggro EnemyAggro;
    public Ragdoll Ragdoll;

    private void Start()
    {
      DamageReceiver.DamageReceived += OnDamageReceived_Handler;
    }

    private void OnDamageReceived_Handler()
    {
      TakeOffHorse();
      EnemyAggro.enabled = false;
      Animator.enabled = false;
      EnemyShoot.enabled = false;
      PlayerFollow.StopHorse();
      Ragdoll.EnableRagdollState();
    }

    private void TakeOffHorse()
    {
      transform.parent = null;
      Rider.DismountAnimal();
    }
    
  }
}