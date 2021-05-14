using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Units.Enemy;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn
{
  public class EnemyInitialPoint : MonoBehaviour
  {
    public int NumberOfEnemies = 1;
    public float SpawnRadius;

    public TriggerObserver TriggerObserver;
    public List<EnemyContainer> enemies = new List<EnemyContainer>();

    private void Start()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerExit += TriggerExit;
    }

    public Vector3 CalculateSpawnPosition()
    {
      Vector3 spawnPosition;

      do
      {
        float randX = Random.Range(-SpawnRadius / 2, SpawnRadius / 2);
        float randZ = Random.Range(-SpawnRadius / 2, SpawnRadius / 2);
        spawnPosition =
          new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
      } while (enemies.Find(enemy => enemy.transform.position == spawnPosition));

      return spawnPosition;
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

  #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
      Handles.color = Color.grey;
      Handles.DrawWireDisc(transform.position, Vector3.up, SpawnRadius);
    }
  #endif
  }
}