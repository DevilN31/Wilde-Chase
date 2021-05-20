using System;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.TimeService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class PausePopUp : GameplayPopUp
  {
    public Button ResumeButton;
    public Button SettingsButton;
    public Button RestartButton;
    public Button QuitButton;

    private SceneLoader sceneLoader;
    private LoadingCurtain loadingCurtain;
    private SettingsPopUp settingPopUp;

    public void Init(SceneLoader _sceneLoader, LoadingCurtain _loadingCurtain, SettingsPopUp _settingsPopUp)
    {
      sceneLoader = _sceneLoader;
      loadingCurtain = _loadingCurtain;
      settingPopUp = _settingsPopUp;
    }

    private void Start()
    {
      ResumeButton.onClick.AddListener(() =>
      {
        Close();
        TimeControl.Instance.RunGame();
      });
      
      SettingsButton.onClick.AddListener(settingPopUp.Open);
      
      RestartButton.onClick.AddListener(() =>
      {
        TimeControl.Instance.RunGame();
        loadingCurtain.Show();
        PlayerPrefs.DeleteAll();
        Close();
        sceneLoader.Load("Level", loadingCurtain.Hide);
      });
      
      QuitButton.onClick.AddListener(Application.Quit);
    }
  }
}