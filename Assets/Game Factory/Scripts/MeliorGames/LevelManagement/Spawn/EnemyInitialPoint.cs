using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Units.Enemy;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  public class EnemyInitialPoint : MonoBehaviour
  {
    public TriggerObserver TriggerObserver;
    public List<EnemyContainer> enemies = new List<EnemyContainer>();

    private void Start()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerExit += TriggerExit;
    }

    public bool IsEnemiesDead()
    {
      foreach (EnemyContainer enemy in enemies)
      {
        if (!enemy.EnemyController.Dead)
          return false;
      }

      return true;
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