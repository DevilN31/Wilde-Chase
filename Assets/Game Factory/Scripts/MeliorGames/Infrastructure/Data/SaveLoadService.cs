using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure.Data
{
  public class SaveLoadService : MonoBehaviour
  {
    public static SaveLoadService Instance;
    
    private const string ProgressKey = "Progress";
    private const string SettingsKey = "Settings";

    [HideInInspector]
    public PlayerProgress PlayerProgress;

    [HideInInspector] 
    public GameSettings GameSettings;

    private void Awake()
    {
      Instance = this;
    }

    public void SaveProgress() => 
      PlayerPrefs.SetString(ProgressKey, PlayerProgress.ToJson());

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    public void SaveSettings() => 
      PlayerPrefs.SetString(SettingsKey, GameSettings.ToJson());

    public GameSettings LoadSettings() => 
      PlayerPrefs.GetString(SettingsKey)?.ToDeserialized<GameSettings>();
  }
}