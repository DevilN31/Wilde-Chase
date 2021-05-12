using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
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
        var enemy = GameFactory.CreateEnemy(point.gameObject);
        point.enemies.Add(enemy);
      }
    }
  }
}