using System;
using Game_Factory.Scripts.MeliorGames.Audio;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn;
using Game_Factory.Scripts.MeliorGames.TimeService;
using Game_Factory.Scripts.MeliorGames.UI;
using UnityEngine;
using GameAnalyticsSDK;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure
{
  public class App : MonoBehaviour
  {
    public GameFactory GameFactory;
    public GameplayHUD GameplayHUD;
    public SceneLoader SceneLoader;

    public PlayerSpawner PlayerSpawner;
    public EnemySpawner EnemySpawner;

    public LevelContainer LevelContainer;

    public SaveLoadService SaveLoadService;


    private Level currentLevel;

    private void Start()
    {
      Init();
    }

        private void Init()
        {
            GameplayHUD.LoadingCurtain.Show();
            GameplayHUD.LoadingCurtain.Hide();
            LoadProgress();
            LoadSettings();
            AudioService.Instance.Init();
            PlayerSpawner.Init(GameFactory, PlayerInitialPoint(), currentLevel.wayPoint, LevelContainer);
            EnemySpawner.Init(GameFactory, LevelContainer);
            LevelContainer.Init(GameFactory.PlayerContainer.PlayerMain);
            GameplayHUD.Init(GameFactory.PlayerContainer.PlayerMain, LevelContainer, SceneLoader);
            GameAnalytics.Initialize();
        }

    private GameObject PlayerInitialPoint()
    {
      Debug.Log(SaveLoadService.PlayerProgress.LevelID);

      currentLevel = LevelContainer.Levels.Find(
        level => level.Index == SaveLoadService.PlayerProgress.LevelID);

      return currentLevel.InitialPoint.gameObject;
    }

    private void LoadProgress()
    {
      SaveLoadService.PlayerProgress = SaveLoadService.LoadProgress() ?? NewGameProgress();
    }

    private void LoadSettings()
    {
      SaveLoadService.GameSettings = SaveLoadService.LoadSettings() ?? NewGameSettings();
    }

    private GameSettings NewGameSettings()
    {
      return new GameSettings();
    }

    private PlayerProgress NewGameProgress()
    {
      return new PlayerProgress();
    }
  }
}