using System;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using Game_Factory.Scripts.MeliorGames.TimeService;
using Game_Factory.Scripts.MeliorGames.UI.PopUp;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI
{
  public class GameplayHUD : MonoBehaviour
  {
    public TMP_Text LevelNameTxt;
    public Slider HealthBar;
    public TMP_Text ReloadingText;

    public Button PauseButton;

    public LoadingCurtain LoadingCurtain;
    public PausePopUp PausePopUp;
    public GameOverPopUp GameOverPopUp;
    public WinPopUp WinPopUp;
    public NextLevelPopUp NextLevelPopUp;

    private PlayerMain player;
    private LevelContainer levelContainer;
    private SceneLoader sceneLoader;

    public void Init(PlayerMain _player, LevelContainer _levelContainer, SceneLoader _sceneLoader)
    {
      player = _player;
      levelContainer = _levelContainer;
      sceneLoader = _sceneLoader;

      player.Receiver.DamageReceived += UpdateHealthBar;
      player.Died += GameOverPopUp.Open;
      UpdateHealthBar();

      SetLevelNumber(SaveLoadService.Instance.PlayerProgress.LevelID);
      SubscribeOnLevelChange();
      
      NextLevelPopUp.Init(LoadingCurtain, player, this);
      GameOverPopUp.Init(sceneLoader, LoadingCurtain);
      WinPopUp.Init(sceneLoader, LoadingCurtain);
      PausePopUp.Init(sceneLoader, LoadingCurtain);
    }

    private void Start()
    {
      PauseButton.onClick.AddListener(() =>
      {
        PausePopUp.Open();
        TimeControl.Instance.PauseGame();
      });
    }

    private void Update()
    {
      CheckPlayerReload();
    }

    private void SubscribeOnLevelChange()
    {
      foreach (Level level in levelContainer.Levels)
      {
        level.Finished += OnLevelFinished_Handler;
      }
    }

    private void OnLevelFinished_Handler(Level level)
    {
      bool enemiesDead = level.IsAllEnemiesDead();

      if (enemiesDead)
      {
        if (level.Index >= levelContainer.Levels.Count)
        {
          WinPopUp.Open();
        }
        else
        {
          TimeControl.Instance.PauseGame();
          NextLevelPopUp.Open();
          UpdateLevelText(level);
        }
      }
      else
      {
        GameOverPopUp.Open();
      }
    }

    public void UpdateHealthBar()
    {
      HealthBar.value = player.Health / player.MaxHealth;
    }

    private void UpdateLevelText(Level level)
    {
      SetLevelNumber(level.Index + 1);
    }

    private void SetLevelNumber(int index)
    {
      LevelNameTxt.text = $"Level {index}";
    }

    private void CheckPlayerReload()
    {
      ReloadingText.enabled = !player.Shooter.ReadyToShoot;
    }
  }
}