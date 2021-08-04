﻿using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using UnityEngine;
using YsoCorp.GameUtils; // Publisher SDK

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Progress
{
  public class LevelContainer : MonoBehaviour
  {
    public float DistanceToFinish;
    
    public List<Level> Levels;

    private PlayerMain player;

    public Level currentLevel;

    private void Awake()
    {
      foreach (Level level in Levels)
      {
        level.Started += OnLevelStarted_Handler;
        level.Finished += OnLevelFinished_Handler;
      }
    }

    public void Init(PlayerMain _player)
    {
      player = _player;
      player.Died += OnPlayerDied_Handler;
      
      currentLevel = Levels.Find(level => level.Index == SaveLoadService.Instance.PlayerProgress.LevelID);
    }

    private void Update()
    {
      CalculateDistanceToFinish(currentLevel);
    }
    
    private void CalculateDistanceToFinish(Level level)
    {
      DistanceToFinish = (level.FinishPoint.transform.position - player.transform.position).magnitude - 3f;
      //Debug.Log(DistanceToFinish);
    }


    private void OnPlayerDied_Handler()
    {
      DistractEnemies();
    }

    private void DistractEnemies()
    {
      foreach (Level level in Levels)
      {
        foreach (EnemyInitialPoint enemyInitialPoint in level.EnemyInitialPoints)
        {
          enemyInitialPoint.DistractEnemies();
        }
      }
    }
    
    private void OnLevelStarted_Handler(Level level)
    {
      Debug.Log($"Started level {level.Index}");
            YCManager.instance.OnGameStarted(level.Index); // Publisher SDK
    }
    
    private void OnLevelFinished_Handler(Level level)
    {
            Debug.Log($"Finished level {level.Index}");
            YCManager.instance.OnGameFinished(true); // Publisher SDK
            SaveProgress(level);
    }

    private void SaveProgress(Level level)
    {
      bool enemiesDead = level.IsAllEnemiesDead();

      if (enemiesDead)
      {
        if (level.Index >= Levels.Count)
        {
          SaveLoadService.Instance.PlayerProgress.LevelID = Levels[0].Index;
          SaveLoadService.Instance.SaveProgress();
        }
        else
        {
          SaveLoadService.Instance.PlayerProgress.LevelID = level.Index + 1;
          SaveLoadService.Instance.SaveProgress();
          currentLevel = Levels.Find(current => current.Index == level.Index + 1);
          //currentLevel.Started?.Invoke(currentLevel);
        }
      }
      else
      {
        player.Die();
        DistractEnemies();
      }
    }
  }
}