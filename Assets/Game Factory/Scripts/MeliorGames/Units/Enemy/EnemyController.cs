using System;
using Game_Factory.Scripts.MeliorGames.Audio;
using Game_Factory.Scripts.MeliorGames.Infrastructure.StaticData;
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

    public bool Dead;

    private void Start()
    {
      DamageReceiver.DamageReceived += OnDamageReceived_Handler;
    }

    private void OnDamageReceived_Handler()
    {
      if (Dead) return;
      
      AudioService.Instance.PlaySound(EAudio.EnemyHurt);
      Dead = true;
      TakeOffHorse();
      EnemyAggro.enabled = false;
      Animator.enabled = false;
      EnemyShoot.enabled = false;
      //PlayerFollow.StopHorse();
      PlayerFollow.RunAway();
      Ragdoll.EnableRagdollState();
    }

    private void TakeOffHorse()
    {
      transform.parent = null;
      Rider.DismountAnimal();
    }
    
  }
}