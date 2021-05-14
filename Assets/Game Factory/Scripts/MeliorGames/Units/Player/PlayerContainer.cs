using System;
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

    public void Init(MWayPoint wayPoint, Camera camera)
    {
      PlayerHorseAI.SetTarget(wayPoint.transform, true);
      PlayerShoot.MainCamera = camera;
    }
  }
}