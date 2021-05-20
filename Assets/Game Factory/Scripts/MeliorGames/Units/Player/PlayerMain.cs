using System;
using Game_Factory.Scripts.MeliorGames.Units.Animation;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerMain : MonoBehaviour
  {
    public float MaxHealth = 100;
    public float Health;
    
    public PlayerDamageReceiver Receiver;
    public PlayerShoot Shooter;
    public PlayerView View;

    public Action Died;
    public Action HealthChanged;

    [SerializeField]
    private bool isVulnerable;

    private void Start()
    {
      Receiver.DamageReceived += OnDamageReceived_Handler;
      View.StateEntered += ViewOnStateEntered;
      View.StateExited += ViewOnStateExited;
      ResetHealth();
    }

    private void ViewOnStateEntered(AnimatorState state)
    {
      if (state == AnimatorState.Crouch)
        isVulnerable = false;
    }

    private void ViewOnStateExited(AnimatorState state)
    {
      if (state == AnimatorState.Crouch)
        isVulnerable = true;
    }

    private void OnDamageReceived_Handler()
    {
      if(!isVulnerable)
        return;
      
      Health -= 10;
      HealthChanged?.Invoke();
      
      if (Health <= 0)
      {
        Receiver.Collider.enabled = false;
        Died?.Invoke();
      }
    }

    public void ResetHealth()
    {
      Health = MaxHealth;
      
    }
  }
}