using System;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Spawn;
using Game_Factory.Scripts.MeliorGames.UI;
using UnityEngine;

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
      LoadProgress();
      PlayerSpawner.Init(PlayerInitialPoint(), currentLevel.wayPoint);
      EnemySpawner.Init();
      GameplayHUD.Init(GameFactory.PlayerContainer.PlayerMain, LevelContainer, SceneLoader);
    }

    private GameObject PlayerInitialPoint()
    {
      Debug.Log(SaveLoadService.PlayerProgress.LevelID);
      
      currentLevel =  LevelContainer.Levels.Find(
        level => level.Index == SaveLoadService.PlayerProgress.LevelID);

      return currentLevel.InitialPoint.gameObject;
    }

    private void LoadProgress()
    {
      SaveLoadService.PlayerProgress = SaveLoadService.LoadProgress() ?? NewGameProgress();
    }

    private PlayerProgress NewGameProgress()
    {
      return new PlayerProgress();
    }
  }
}
