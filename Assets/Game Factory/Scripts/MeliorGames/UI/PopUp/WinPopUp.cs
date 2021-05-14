﻿using Game_Factory.Scripts.MeliorGames.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class WinPopUp : GameplayPopUp
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
        sceneLoader.Load("Test");
      });
    }
  }
}