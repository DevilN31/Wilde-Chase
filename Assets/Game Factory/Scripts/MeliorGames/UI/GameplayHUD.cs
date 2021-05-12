using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
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

    public Button PauseButton;

    public PausePopUp PausePopUp;
    public GameOverPopUp GameOverPopUp;

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
      
      GameOverPopUp.Init(sceneLoader);
    }

    private void UpdateHealthBar()
    {
      HealthBar.value = player.Health / player.MaxHealth;
    }
  }
}