﻿using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Enemy
{
  public class EnemyContainer : MonoBehaviour
  {
    public EnemyShoot EnemyShoot;
    public EnemyPlayerFollow EnemyPlayerFollow;
    public Aggro Aggro;

    public void Init(Transform followTarget, Transform shootTarget)
    {
      EnemyShoot.SetTarget(shootTarget);
      EnemyPlayerFollow.SetTarget(followTarget);
    }
  }
}