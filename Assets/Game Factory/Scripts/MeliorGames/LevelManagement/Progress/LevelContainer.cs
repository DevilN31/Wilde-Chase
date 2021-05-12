using System;
using System.Collections.Generic;
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
        level.LevelFinished += OnLevelFinishedHandler;
      }
    }

    private void OnLevelFinishedHandler(Level level)
    {
      bool enemiesDead = level.IsAllEnemiesDead();
      
      Debug.Log(enemiesDead ? "Everyone dead" : "Someone is alive");
    }
  }
}