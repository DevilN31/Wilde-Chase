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
    public Button RestartButton;
    public Button QuitButton;

    private SceneLoader sceneLoader;
    private LoadingCurtain loadingCurtain;

    public void Init(SceneLoader _sceneLoader, LoadingCurtain _loadingCurtain)
    {
      sceneLoader = _sceneLoader;
      loadingCurtain = _loadingCurtain;
    }

    private void Start()
    {
      ResumeButton.onClick.AddListener(() =>
      {
        Close();
        TimeControl.Instance.RunGame();
      });
      
      RestartButton.onClick.AddListener(() =>
      {
        TimeControl.Instance.RunGame();
        loadingCurtain.Show();
        PlayerPrefs.DeleteAll();
        Close();
        sceneLoader.Load("Test", loadingCurtain.Hide);
      });
      
      QuitButton.onClick.AddListener(Application.Quit);
    }
  }
}