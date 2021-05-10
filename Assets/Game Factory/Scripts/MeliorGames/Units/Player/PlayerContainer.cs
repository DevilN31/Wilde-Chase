using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerContainer : MonoBehaviour
  {
    public PlayerShoot PlayerShoot;
    public MAnimalAIControl PlayerHorse;
    public Transform ShootTarget;
    public Transform PlayerTransform;

    public void Init(MWayPoint wayPoint, Camera camera)
    {
      PlayerHorse.SetTarget(wayPoint.transform, true);
      PlayerShoot.MainCamera = camera;
    }
  }
}