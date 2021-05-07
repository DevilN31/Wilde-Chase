using System;
using System.Collections;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
    public class EnemyPlayerFollow : MonoBehaviour
    {
      public GameObject Player;
      
      public MAnimalAIControl AnimalAI;

      private void Start()
      {
        StartCoroutine(SetTargetCoroutine());
      }

      private IEnumerator SetTargetCoroutine()
      {
        yield return new WaitForSeconds(1f);
        AnimalAI.SetTarget(Player.transform, true);
        AnimalAI.StoppingDistance = 10f;
      }

      public void StopHorse()
      {
        AnimalAI.Stop();
        AnimalAI.enabled = false;
      }
    }
}
