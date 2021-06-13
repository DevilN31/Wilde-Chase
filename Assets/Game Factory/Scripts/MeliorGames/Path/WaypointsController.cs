using System;
using MalbersAnimations.Controller;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Path
{
  public class WaypointsController : MonoBehaviour
  {
    public MAnimalAIControl MAnimalAIControl;

    public Transform[] Waypoints;

    private int currentWaypointIndex = 0;

    private void Start()
    {
      MAnimalAIControl.SetTarget(Waypoints[0]);
    }

    private void Update()
    {
      FindNextWaypoint();
    }

    private void FindNextWaypoint()
    {
      float distanceToWaypoint = 0;

      if (currentWaypointIndex < Waypoints.Length)
      {
        distanceToWaypoint = Vector3.Distance(transform.position, Waypoints[currentWaypointIndex].position);
      }

      //Debug.Log(distanceToWaypoint);
      Debug.Log(currentWaypointIndex);

      if (distanceToWaypoint <= 2f && currentWaypointIndex < Waypoints.Length)
      {
        currentWaypointIndex++;

        if (currentWaypointIndex == Waypoints.Length)
        {
          MAnimalAIControl.Stop();
        }
        else
        {
          MAnimalAIControl.SetTarget(Waypoints[currentWaypointIndex]);
        }
      }
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.green;

      for (int i = 0; i < Waypoints.Length; i++)
      {
        if(i < Waypoints.Length -1)
          Gizmos.DrawLine(Waypoints[i].position + Vector3.up, Waypoints[i + 1].position + Vector3.up);
      }
    }
  }
}