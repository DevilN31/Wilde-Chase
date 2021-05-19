using System;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerContainer : MonoBehaviour
  {
    public PlayerMain PlayerMain;
    public PlayerShoot PlayerShoot;
    public MAnimalAIControl PlayerHorseAI;
    public MAnimal PlayerHorse;
    public Transform ShootTarget;
    public Transform PlayerTransform;
    public Transform WagonTransform;

    public void Init(MWayPoint wayPoint, Camera camera, LevelContainer _levelContainer)
    {
      PlayerHorseAI.SetTarget(wayPoint.transform, true);
      PlayerShoot.Init(_levelContainer, camera);
    }
  }
}