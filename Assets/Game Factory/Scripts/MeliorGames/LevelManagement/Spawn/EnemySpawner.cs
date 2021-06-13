using System.Collections;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  public class EnemySpawner : MonoBehaviour
  {
    public List<EnemyInitialPoint> EnemyInitialPoints;

    private GameFactory gameFactory;
    private LevelContainer levelContainer;

    public void Init(GameFactory _gameFactory, LevelContainer _levelContainer)
    {
      gameFactory = _gameFactory;
      levelContainer = _levelContainer;
      Subscribe();
      //Spawn();
      //SpawnEnemies();
    }

    private void Spawn()
    {
      foreach (Level level in levelContainer.Levels)
      {
        foreach (EnemyInitialPoint enemyInitialPoint in level.EnemyInitialPoints)
        {
          for (int i = 0; i < enemyInitialPoint.NumberOfEnemies; i++)
          {
            var enemy = gameFactory.CreateEnemy(enemyInitialPoint.CalculateSpawnPosition());
            enemyInitialPoint.enemies.Add(enemy);
          }
        }
      }
    }

    private void Subscribe()
    {
      foreach (Level level in levelContainer.Levels)
      {
        level.Finished += OnLevelFinished_Handler;
        
        foreach (EnemyInitialPoint enemyInitialPoint in level.EnemyInitialPoints)
        {
          enemyInitialPoint.PlayerAppearance += SpawnOnTrigger;
        }
      }
    }

    private void OnLevelFinished_Handler(Level level)
    {
      if (level.IsAllEnemiesDead())
      {
        foreach (EnemyInitialPoint point in level.EnemyInitialPoints)
        {
          DestroyEnemies(point);
        }
      }
    }

    private void SpawnOnTrigger(EnemyInitialPoint point)
    {
      SpawnEnemies(point);
    }

    private void SpawnEnemies(EnemyInitialPoint point)
    {
      //foreach (EnemyInitialPoint point in EnemyInitialPoints)
      {
        for (int i = 0; i < point.NumberOfEnemies; i++)
        {
          var enemy = gameFactory.CreateEnemy(point.CalculateSpawnPosition());
          point.enemies.Add(enemy);
        }
      }
    }

    private void DestroyEnemies(EnemyInitialPoint point)
    {
      for (int i = point.enemies.Count - 1; i >= 0; i--)
      {
        point.enemies[i].DestroyRider();
        Destroy(point.enemies[i].gameObject);
        point.enemies.Remove(point.enemies[i]);
      }
    }
  }
}