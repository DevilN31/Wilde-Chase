using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerView : MonoBehaviour
  {
    [Range(0,1)]
    public float ThrowPower = 0.0f;
    
    private Animator animator;

    private static readonly int Aim = Animator.StringToHash("Aim");
    private static readonly int Throw = Animator.StringToHash("Throw");
    private static readonly int ThrowControl = Animator.StringToHash("ThrowControl");
    private static readonly int Aiming = Animator.StringToHash("Aiming");

    private void Awake()
    {
      animator = GetComponent<Animator>();
    }

    private void Update()
    {
      ControlThrow(ThrowPower);
    }

    public void PlayAiming()
    {
      //animator.SetTrigger(Aim);
    }

    public void PlayThrow()
    {
      //animator.SetTrigger(Throw);
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