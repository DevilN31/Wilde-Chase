using System.Collections;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  public class EnemySpawner : MonoBehaviour
  {
    public GameFactory GameFactory;
    
    public List<EnemyInitialPoint> EnemyInitialPoints;

    public void Init()
    {
      SpawnEnemies();
    }

    private void SpawnEnemies()
    {
      foreach (EnemyInitialPoint point in EnemyInitialPoints)
      {
        for (int i = 0; i < point.NumberOfEnemies; i++)
        {
          var enemy = GameFactory.CreateEnemy(point.CalculateSpawnPosition());
          point.enemies.Add(enemy);
        }
      }
    }
  }
}