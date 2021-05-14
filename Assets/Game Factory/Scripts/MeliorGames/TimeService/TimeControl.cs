using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.TimeService
{
  public class TimeControl : MonoBehaviour
  {
    public static TimeControl Instance;
    
    private float slowTimeScale = 0.35f;
    private float normalTimeScale = 1f;
    private float fixedDeltaTimeFactor = 0.02f;

    private float gamePauseTimeScale = 0f;
    private float gameRunTimeScale = 1f;

    private void Awake()
    {
      Instance = this;
    }

    public void RunGame()
    {
      Time.timeScale = gameRunTimeScale;
    }

    public void PauseGame()
    {
      Time.timeScale = gamePauseTimeScale;
    }

    public void SpeedUp()
    {
      Time.timeScale = normalTimeScale;
      Time.fixedDeltaTime = normalTimeScale * fixedDeltaTimeFactor;
    }

    public void SlowDown()
    {
      Time.timeScale = slowTimeScale;
      Time.fixedDeltaTime = slowTimeScale * fixedDeltaTimeFactor;
    }

  }
}