using System;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.TimeService;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class GameOverPopUp : GameplayPopUp
  {
    public Button RestartButton;

    private SceneLoader sceneLoader;
    private LoadingCurtain loadingCurtain;

    public void Init(SceneLoader _sceneLoader, LoadingCurtain _loadingCurtain)
    {
      sceneLoader = _sceneLoader;
      loadingCurtain = _loadingCurtain;
    }

    private void Start()
    {
      RestartButton.onClick.AddListener(() =>
      {
        TimeControl.Instance.SpeedUp();
        TimeControl.Instance.RunGame();
        sceneLoader.Load("Level");
      });
    }
  }
}