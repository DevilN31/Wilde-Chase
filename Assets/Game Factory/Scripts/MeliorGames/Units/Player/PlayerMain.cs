using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerMain : MonoBehaviour
  {
    public float MaxHealth = 100;
    public float Health;
    
    public PlayerDamageReceiver Receiver;
    public PlayerShoot Shooter;

    public Action Died;

    private void Start()
    {
      Receiver.DamageReceived += OnDamageReceived_Handler;
      ResetHealth();
    }

    private void OnDamageReceived_Handler()
    {
      Health -= 10;

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