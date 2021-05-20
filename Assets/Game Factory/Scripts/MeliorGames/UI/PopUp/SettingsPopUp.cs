using System;
using System.Globalization;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class SettingsPopUp : GameplayPopUp
  {
    public Button BackButton;
    public Toggle SlingShotControlToggle;
    public Slider SensitivitySlider;
    public TMP_Text SliderValue;

    private PlayerShoot playerShoot;

    public void Init(PlayerShoot playerShooter)
    {
      playerShoot = playerShooter;
      SlingShotControlToggle.isOn = SaveLoadService.Instance.GameSettings.IsInputInverted;
      SensitivitySlider.value = SaveLoadService.Instance.GameSettings.Sensitivity;
      SliderValue.text = Math.Round(SensitivitySlider.value, 1).ToString(CultureInfo.InvariantCulture);
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

      SensitivitySlider.onValueChanged.AddListener(value =>
      {
        playerShoot.Sensitivity = SensitivitySlider.value;
        SliderValue.text = Math.Round(SensitivitySlider.value, 1).ToString(CultureInfo.InvariantCulture);
        SaveLoadService.Instance.GameSettings.Sensitivity = value;
        SaveLoadService.Instance.SaveSettings();
      });
    }
  }
}