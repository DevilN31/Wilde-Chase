using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  public class EnemyContainer : MonoBehaviour
  {
    public EnemyController EnemyController;
    public EnemyShoot EnemyShoot;
    public EnemyPlayerFollow EnemyPlayerFollow;
    public Aggro Aggro;

    public void Init(Transform followTarget, Transform shootTarget, MWayPoint runAwayWaypoint)
    {
      EnemyShoot.SetTarget(shootTarget);
      EnemyPlayerFollow.SetTarget(followTarget);
      EnemyPlayerFollow.RunAwayPoint = runAwayWaypoint;
    }

    public void DestroyRider()
    {
      Destroy(EnemyShoot.gameObject);
    }
  }
}