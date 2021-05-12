using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Factory.Scripts.MeliorGames.UI.PopUp
{
  public class GameplayPopUp : MonoBehaviour
  {
    public GameObject Blocker;
    public GameObject Frame;

    private MaskableGraphic blockerMaskableGraphic;

    private void Awake()
    {
      blockerMaskableGraphic = Blocker.GetComponent<MaskableGraphic>();
    }

    public virtual void Open()
    {
      gameObject.SetActive(true);
      Fade(blockerMaskableGraphic, 0, 0.000001f);
      Fade(blockerMaskableGraphic, 1, 1f);
    }

    public virtual void Close()
    {
      gameObject.SetActive(false);
      Fade(blockerMaskableGraphic, 0, 1f);
    }

    private void Fade(MaskableGraphic maskableGraphic, float targetAlpha, float duration)
    {
      maskableGraphic.CrossFadeAlpha(targetAlpha, duration, true);
    }
  }
}