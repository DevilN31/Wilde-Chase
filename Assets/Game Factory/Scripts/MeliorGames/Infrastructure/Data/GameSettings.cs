using System;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure.Data
{
  [Serializable]
  public class GameSettings
  {
    public bool IsInputInverted;
    public float Sensitivity = 1f;

    public float MusicVolume = 1f;
    public float SoundsVolume = 1f;
  }
}