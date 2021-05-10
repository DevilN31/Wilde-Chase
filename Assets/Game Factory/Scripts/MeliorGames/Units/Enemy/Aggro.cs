using System;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  public class Aggro : MonoBehaviour
  {
    //public TriggerObserver TriggerObserver;
    public EnemyShoot EnemyShoot;
    public EnemyPlayerFollow EnemyPlayerFollow;

    private bool hasAggroTarget;

    private void Start()
    {
      //TriggerObserver.TriggerEnter += TriggerEnter;
      //TriggerObserver.TriggerExit += TriggerExit;
      
      SwitchFollowOff();
      SwitchShootOff();
    }

    private void OnDisable()
    {
      //TriggerObserver.TriggerEnter -= TriggerEnter;
      //TriggerObserver.TriggerExit -= TriggerExit;
    }

    public void TriggerEnemy()
    {
      if (!hasAggroTarget)
      {
        hasAggroTarget = true;
        
        SwitchFollowOn();
        SwitchShootOn();
      }
    }

    private void TriggerEnter(Collider obj)
    {
      if (!hasAggroTarget)
      {
        Debug.Log("Aggro");
        hasAggroTarget = true;
        SwitchFollowOn();
        SwitchShootOn();
      }
    }
    
    private void TriggerExit(Collider obj)
    {
      if (hasAggroTarget)
      {
        hasAggroTarget = false;
      }
    }

    private void SwitchFollowOn() =>
      EnemyPlayerFollow.StartHorse();

    private void SwitchShootOn() => 
      EnemyShoot.enabled = true;

    private void SwitchFollowOff() => 
      EnemyPlayerFollow.AnimalAI.Stop();

    private void SwitchShootOff() => 
      EnemyShoot.enabled = false;
  }
}