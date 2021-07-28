using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn;
using Game_Factory.Scripts.MeliorGames.Units.Enemy;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Progress
{
  public class Level : MonoBehaviour
  {
    public int Index;
    public LevelInitialPoint InitialPoint;
    public LevelFinishPoint FinishPoint;

    public List<EnemyInitialPoint> EnemyInitialPoints;

    public float Distance;

    public MWayPoint wayPoint;

    public Action<Level> Started;
    public Action<Level> Finished;

    private void Start()
    {
      FinishPoint.TriggerObserver.TriggerEnter += FinishPointTriggerEnter;
      FinishPoint.TriggerObserver.TriggerExit += FinishPointTriggerExit;

      Distance = (FinishPoint.transform.position - InitialPoint.transform.position).magnitude;
    }
    
    public bool IsAllEnemiesDead()
    {
      foreach (EnemyInitialPoint point in EnemyInitialPoints)
      {
        if (!point.IsEnemiesDead())
          return false;
      }

      return true;
    }

    private void FinishPointTriggerEnter(Collider obj)
    {
      Finished?.Invoke(this);
    }
    
    private void FinishPointTriggerExit(Collider obj)
    {
    }
  }
}