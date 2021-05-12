using System;
using Game_Factory.Scripts.MeliorGames.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class GameOverPopUp : GameplayPopUp
  {
    public Button RestartButton;

    private SceneLoader sceneLoader;

    public void Init(SceneLoader _sceneLoader)
    {
      sceneLoader = _sceneLoader;
    }

    private void Start()
    {
      RestartButton.onClick.AddListener(() =>
      {
        Debug.Log("RestartTap!");
        sceneLoader.Load("Test");
      });
    }
  }
}