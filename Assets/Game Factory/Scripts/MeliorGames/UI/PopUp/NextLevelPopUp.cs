using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using Game_Factory.Scripts.MeliorGames.TimeService;
using Game_Factory.Scripts.MeliorGames.Units.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class NextLevelPopUp : GameplayPopUp
  {
    public Button NextLevelButton;
    
    private LoadingCurtain loadingCurtain;
    private PlayerMain player;
    private GameplayHUD HUD;
    private LevelContainer levelContainer;

    public void Init(LoadingCurtain _loadingCurtain, PlayerMain _player, GameplayHUD _HUD, LevelContainer _levelContainer)
    {
      loadingCurtain = _loadingCurtain;
      player = _player;
      HUD = _HUD;
      levelContainer = _levelContainer;
    }

    private void Start()
    {
      NextLevelButton.onClick.AddListener(() =>
      {
        TimeControl.Instance.SpeedUp();
        TimeControl.Instance.RunGame();
        loadingCurtain.Show();
        loadingCurtain.Hide();
        player.ResetHealth();
        player.Shooter.SetReload(1.5f);
        HUD.UpdateHealthBar();
        levelContainer.currentLevel.Started?.Invoke(levelContainer.currentLevel);
        Close();
      });
    }
  }
}