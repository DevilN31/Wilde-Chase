using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI
{
  public class LevelProgressBar : MonoBehaviour
  {
    public Image ImageCurrent;

    public void SetValue(float current, float max) =>
      ImageCurrent.fillAmount = current / max;
  }
}