using System;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class SettingsPopUp : GameplayPopUp
  {
    public Button BackButton;
    public Toggle SlingShotControlToggle;

    private PlayerShoot playerShoot;

    public void Init(PlayerShoot playerShooter)
    {
      playerShoot = playerShooter;
      SlingShotControlToggle.isOn = SaveLoadService.Instance.GameSettings.IsInputInverted;
    }

    public void Start()
    {
      BackButton.onClick.AddListener(Close);
      
      SlingShotControlToggle.onValueChanged.AddListener(value =>
      {
        Debug.Log(value);
        playerShoot.IsInputInverted = value;
        SaveLoadService.Instance.GameSettings.IsInputInverted = value;
        SaveLoadService.Instance.SaveSettings();
      });
    }
  }
}