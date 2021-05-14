using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.LevelManagement.Progress
{
  public class LevelContainer : MonoBehaviour
  {
    public List<Level> Levels;

    private void Awake()
    {
      foreach (Level level in Levels)
      {
        level.Finished += OnLevelFinished_Handler;
      }
    }

    private void OnLevelFinished_Handler(Level level)
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
        }
      }
    }
  }
}