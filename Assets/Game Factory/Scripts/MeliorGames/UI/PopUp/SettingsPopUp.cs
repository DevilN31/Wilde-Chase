using System;
using System.Globalization;
using Game_Factory.Scripts.MeliorGames.Audio;
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
    public TMP_Text SensitivitySliderText;
    
    public Slider MusicVolumeSlider;
    public TMP_Text MusicVolumeSliderText;
    
    public Slider SoundsVolumeSlider;
    public TMP_Text SoundsVolumeSliderText;

    private PlayerShoot playerShoot;
    private AudioService audioService;

    public void Init(PlayerShoot playerShooter)
    {
      playerShoot = playerShooter;
      audioService = AudioService.Instance;

      SlingShotControlToggle.isOn = SaveLoadService.Instance.GameSettings.IsInputInverted;
      
      SensitivitySlider.value = SaveLoadService.Instance.GameSettings.Sensitivity;
      SensitivitySliderText.text = Math.Round(SensitivitySlider.value, 1).ToString(CultureInfo.InvariantCulture);

      MusicVolumeSlider.value = SaveLoadService.Instance.GameSettings.MusicVolume;
      MusicVolumeSliderText.text = Math.Round(MusicVolumeSlider.value, 1).ToString(CultureInfo.InvariantCulture);
      
      SoundsVolumeSlider.value = SaveLoadService.Instance.GameSettings.SoundsVolume;
      SoundsVolumeSliderText.text = Math.Round(SoundsVolumeSlider.value, 1).ToString(CultureInfo.InvariantCulture);
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
        SensitivitySliderText.text = Math.Round(SensitivitySlider.value, 1).ToString(CultureInfo.InvariantCulture);
        SaveLoadService.Instance.GameSettings.Sensitivity = value;
        SaveLoadService.Instance.SaveSettings();
      });
      
      MusicVolumeSlider.onValueChanged.AddListener(value =>
      {
        audioService.MusicAudioSource.volume = MusicVolumeSlider.value;
        MusicVolumeSliderText.text = Math.Round(MusicVolumeSlider.value, 1).ToString(CultureInfo.InvariantCulture);
        SaveLoadService.Instance.GameSettings.MusicVolume = value;
        SaveLoadService.Instance.SaveSettings();
      });
      
      SoundsVolumeSlider.onValueChanged.AddListener(value =>
      {
        audioService.SoundsAudioSource.volume = SoundsVolumeSlider.value;
        SoundsVolumeSliderText.text = Math.Round(SoundsVolumeSlider.value, 1).ToString(CultureInfo.InvariantCulture);
        SaveLoadService.Instance.GameSettings.SoundsVolume = value;
        SaveLoadService.Instance.SaveSettings();
      });
    }
  }
}