using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Path
{
  public class TrackWaypoints : MonoBehaviour
  {
    public Color LineColor;
    
    [Range(0,1)]
    public float SphereRadius;
    
    public List<Waypoint> Nodes = new List<Waypoint>();

    private void OnDrawGizmos()
    {
      Gizmos.color = LineColor;

      Waypoint[] path = GetComponentsInChildren<Waypoint>();

      Nodes = new List<Waypoint>();

      foreach (Waypoint t in path)
      {
        Nodes.Add(t);
      }

      for (int i = 0; i < Nodes.Count; i++)
      {
        Vector3 currentWaypoint = Nodes[i].transform.position;
        Vector3 previousWaypoint;

        if (i != 0)
          previousWaypoint = Nodes[i - 1].transform.position;
        else
          previousWaypoint = Nodes[i].transform.position;
        
        Gizmos.DrawLine(previousWaypoint, currentWaypoint);
        Gizmos.DrawSphere(currentWaypoint, SphereRadius);
      }
    }
  }
}