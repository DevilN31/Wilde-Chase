using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerMain : MonoBehaviour
  {
    public float Health = 100;
    
    public DamageReceiver Receiver;

    private void Start()
    {
      Receiver.DamageReceived += OnDamageReceived_Handler;
    }

    private void OnDamageReceived_Handler()
    {
      Health -= 10;
      
      if(Health <= 0)
        Debug.Log("Player is dead");
    }
  }
}