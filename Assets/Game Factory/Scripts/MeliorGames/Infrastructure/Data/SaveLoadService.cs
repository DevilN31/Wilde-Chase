using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure.Data
{
  public class SaveLoadService : MonoBehaviour
  {
    public static SaveLoadService Instance;
    
    private const string ProgressKey = "Progress";

    [HideInInspector]
    public PlayerProgress PlayerProgress;

    private void Awake()
    {
      Instance = this;
    }

    public void SaveProgress()
    {
      PlayerPrefs.SetString(ProgressKey, PlayerProgress.ToJson());
    }

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
  }
}