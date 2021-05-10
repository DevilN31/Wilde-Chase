using System;
using System.Collections;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
    public class EnemyPlayerFollow : MonoBehaviour
    {
      public Transform Player;
      public MAnimalAIControl AnimalAI;

      public void SetTarget(Transform target)
      {
        Player = target;
        //StartCoroutine(SetTargetCoroutine());
      }

      public void StartHorse()
      {
        AnimalAI.SetTarget(Player.transform, false);
        AnimalAI.StoppingDistance = 10f;
      }

      public void StopHorse()
      {
        AnimalAI.Stop();
        AnimalAI.enabled = false;
      }
      
      private IEnumerator SetTargetCoroutine()
      {
        yield return new WaitForSeconds(0.0f);
        AnimalAI.SetTarget(Player.transform, false);
        AnimalAI.StoppingDistance = 10f;
      }
    }
}
