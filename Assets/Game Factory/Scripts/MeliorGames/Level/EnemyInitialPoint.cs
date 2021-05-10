using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Units.Enemy;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Level
{
  public class EnemyInitialPoint : MonoBehaviour
  {
    public InitialPointTriggerObserver TriggerObserver;
    public List<EnemyContainer> enemies = new List<EnemyContainer>();

    private void Start()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerExit += TriggerExit;
    }


    private void TriggerEnter(Collider obj)
    {
      TriggerEnemies();
    }

    private void TriggerExit(Collider obj)
    {
    }

    private void TriggerEnemies()
    {
      foreach (EnemyContainer enemy in enemies)
      {
        enemy.Aggro.TriggerEnemy();
      }
    }
  }
}