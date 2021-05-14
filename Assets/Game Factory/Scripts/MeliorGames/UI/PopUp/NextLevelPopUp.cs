using Game_Factory.Scripts.MeliorGames.Infrastructure;
using Game_Factory.Scripts.MeliorGames.TimeService;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class NextLevelPopUp : GameplayPopUp
  {
    public Button NextLevelButton;
    
    private LoadingCurtain loadingCurtain;
    
    public void Init(LoadingCurtain _loadingCurtain)
    {
      loadingCurtain = _loadingCurtain;
    }

    private void Start()
    {
      NextLevelButton.onClick.AddListener(() =>
      {
        TimeControl.Instance.RunGame();
        loadingCurtain.Show();
        loadingCurtain.Hide();
        Close();
      });
    }
  }
}