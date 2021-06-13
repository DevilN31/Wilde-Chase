using System.Collections.Generic;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.CameraControl
{
  public class CameraSwitch : MonoBehaviour
  {
    public Camera MainCamera;
    public Camera AdditionCamera;

    public void ToMain()
    {
      MainCamera.enabled = true;
      AdditionCamera.enabled = false;
    }

    public void ToAdditional()
    {
      MainCamera.enabled = false;
      AdditionCamera.enabled = true;
    }
  }
}