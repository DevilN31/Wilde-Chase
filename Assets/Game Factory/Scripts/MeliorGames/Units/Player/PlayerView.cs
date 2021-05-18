using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerView : MonoBehaviour
  {
    public float ThrowPower
    {
      get => _throwPower;
      set => _throwPower = Mathf.Clamp(value, 0f, 0.99f);
    }

    [Range(0,1)]
    [SerializeField]
    public float _throwPower = 0.0f;
    
    private Animator animator;
    
    private static readonly int ThrowControl = Animator.StringToHash("ThrowControl");
    private static readonly int Aiming = Animator.StringToHash("Aiming");

    private void Awake()
    {
      animator = GetComponent<Animator>();
    }

    public void ControlThrow(float value)
    {
      animator.SetFloat(ThrowControl, value);
    }

    public void SwitchAiming(bool state)
    {
      animator.SetBool(Aiming, state);
    }
  }
}