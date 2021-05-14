using System;
using System.Collections;
using MalbersAnimations.Controller;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
    public class EnemyPlayerFollow : MonoBehaviour
    {
      public Transform Player;
      public MAnimalAIControl AnimalAI;
      public MAnimal Animal;

      public MWayPoint RunAwayPoint;

      public float RemainingDistance;
      
      private bool isHorseRunning;
      private float necessaryDistanceToPlayer;
      private bool runningAway;

      private void Start()
      {
        necessaryDistanceToPlayer = Random.Range(12, 15);
      }

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

      public void RunAway()
      {
        runningAway = true;
        AnimalAI.SetTarget(RunAwayPoint.transform);
        SpeedUpHorse(10);
      }
      
      public void SpeedUpHorse(float speed)
      {
        isHorseRunning = true;
        Animal.SpeedSet_Get("Ground").Speeds[2].Vertical.Value += speed;
      }
      public void SlowDownHorse(float speed)
      {
        Animal.SpeedSet_Get("Ground").Speeds[2].Vertical.Value -= speed;
        isHorseRunning = false;
      }

      private void FixedUpdate()
      {
        if(runningAway)
          return;
        
        RemainingDistance = AnimalAI.RemainingDistance;
        MaintainDistanceToPlayer();
      }

      private void MaintainDistanceToPlayer()
      {
        if (AnimalAI.Target != null)
        {
          if (RemainingDistance >= necessaryDistanceToPlayer && !isHorseRunning)
          {
            isHorseRunning = true;
            SpeedUpHorse(2);
          }
          else if (RemainingDistance < necessaryDistanceToPlayer && isHorseRunning)
          {
            isHorseRunning = false;
            SlowDownHorse(2);
          }
        }
      }

      private IEnumerator SetTargetCoroutine()
      {
        yield return new WaitForSeconds(0.0f);
        AnimalAI.SetTarget(Player.transform, false);
        AnimalAI.StoppingDistance = 10f;
      }
    }
}
