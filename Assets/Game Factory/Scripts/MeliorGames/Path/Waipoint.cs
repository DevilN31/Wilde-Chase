using System;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Path
{
  public class Waipoint : MonoBehaviour
  {
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position + Vector3.up, 0.5f);
    }
  }
}