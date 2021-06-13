using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class ProjectilePositionPlaceHolder : MonoBehaviour
  {
    public Transform Target;

    private void LateUpdate()
    {
      transform.eulerAngles = Target.eulerAngles;
    }
  }
}